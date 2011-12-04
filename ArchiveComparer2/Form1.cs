using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using ArchiveComparer2.Library;
using log4net;
using ArchiveComparer2.Properties;
using System.Diagnostics;

namespace ArchiveComparer2
{
    public partial class ArchiveComparer2Form : Form
    {
        ArchiveDuplicateDetector detector;
        static ILog Logger = LogManager.GetLogger(typeof(ArchiveComparer2Form).ToString());

        public ArchiveComparer2Form()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            detector = new ArchiveDuplicateDetector();
            detector.Notify += new ArchiveDuplicateDetector.NotifyEventHandler(detector_Notify);
        }
        
        private delegate void NotifyCallback(NotifyEventArgs e);

        private void Notify(NotifyEventArgs e)
        {
            if (this.InvokeRequired || txtLog.InvokeRequired || dgvResult.InvokeRequired || statusStrip1.InvokeRequired )
            {
                NotifyCallback d = new NotifyCallback(Notify);
                this.Invoke(d, e);
            }
            else
            {
                string text = e.Status.ToString() + ": " + e.Message;
                if (e.Status == OperationStatus.PAUSED ||
                    e.Status == OperationStatus.RESUMED ||
                    e.Status == OperationStatus.STOPPED)
                {
                    this.Enabled = true;
                    MessageBox.Show(e.Message);
                }

                if (e.Status == OperationStatus.BUILDING_FILE_LIST ||
                    e.Status == OperationStatus.CALCULATING_CRC ||
                    e.Status == OperationStatus.BUILDING_DUPLICATE_LIST ||
                    e.Status == OperationStatus.READY ||
                    e.Status == OperationStatus.COMPLETE)
                {
                    lblStatus.Text = text;
                    pgCount.Visible = true;
                    pgCount.Maximum = e.TotalCount;
                    pgCount.Value = e.CurrentCount;
                }

                if (e.Status == OperationStatus.COMPLETE)
                {
                    Fill(e.DupList);
                }

                if (chkLog.Checked)
                {
                    if (e.Status == OperationStatus.CALCULATING_CRC || 
                        e.Status == OperationStatus.ERROR || 
                        e.Status == OperationStatus.BUILDING_DUPLICATE_LIST)
                    {
                        txtLog.Text += text + Environment.NewLine;
                    }
                    else if (chkLogAll.Checked)
                    {
                        txtLog.Text += text + Environment.NewLine;
                    }
                }
            }
        }
        
        private void Fill(List<DuplicateArchiveInfoList> list)
        {
            dgvResult.SuspendLayout();
            for (int i = 0; i < list.Count; ++i)
            {
                DataGridViewRow row = AddRow(list[i].Original);

                for (int j = 0; j < list[i].Duplicates.Count; ++j)
                {
                    DataGridViewRow rowDup = AddRow(list[i].Duplicates[j]);                    
                }
            }

            dgvResult.ResumeLayout();

        }

        private DataGridViewRow AddRow(DuplicateArchiveInfo data)
        {
            int index = dgvResult.Rows.Add();
            DataGridViewRow row = dgvResult.Rows[index];
            row.Cells["colCheck"].Value = false;
            row.Cells["colFilename"].Value = data.Filename;
            if (data.MatchType == MatchType.ORIGINAL)
            {
                row.DefaultCellStyle.BackColor = Color.DarkGray;
                row.Cells["colFilename"].Style.Font = new Font(row.InheritedStyle.Font, row.InheritedStyle.Font.Style | FontStyle.Bold);
            }
            else if (data.MatchType == MatchType.EQUALCOUNT)
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }

            row.Cells["colPercentage"].Value = data.MatchType == MatchType.ORIGINAL ? "" : data.Percentage.ToString();
            row.Cells["colMatchType"].Value = data.MatchType.ToString();
            row.Cells["colItemsCount"].Value = data.Items.Count;

            if (data.NoMatches != null)
            {
                row.Cells["colNoMatchesCount"].Value = data.NoMatches.Count > 0 ? data.NoMatches.Count.ToString() : "";
                row.Cells["colNoMatchesCount"].ToolTipText = ConcatItem(data.NoMatches);
            }
            
            row.Cells["colSize"].Value = data.RealSize.ToString();
            row.Cells["colArchivedSize"].Value = data.ArchivedSize.ToString();
            row.Cells["colDupGroup"].Value = data.MatchType == MatchType.ORIGINAL ? data.DupGroup.ToString() : data.DupGroup + " ";
            row.Cells["colFileSize"].Value = data.FileSize;
            row.Cells["colCreationTime"].Value = data.CreationTime;

            if (data.Skipped != null)
            {
                row.Cells["colSkipped"].Value = data.Skipped.Count;
                row.Cells["colSkipped"].ToolTipText = ConcatItem(data.Skipped);
                row.Cells["colItemsCount"].Value = data.Items.Count + " (" + (data.Items.Count - data.Skipped.Count) + ")";
            }
            else
            {
                row.Cells["colSkipped"].ToolTipText = "";
            }

            if (data.Skipped != null && data.Skipped.Count > 0)
            {
                row.DefaultCellStyle.Font = new Font(row.InheritedStyle.Font, FontStyle.Italic);
            }

            row.Cells["colCrc"].Value = data.ToCRCString();

            return row;
        }

        private string ConcatItem(List<ArchiveFileInfoSmall> list)
        {
            string result = "";
            foreach (var info in list)
            {
                result += info.Crc + " " + info.Filename + Environment.NewLine;
            }
            return result;
        }

        private void Delete(DeleteMode mode)
        {
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (true == Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    string filename = row.Cells["colFilename"].Value.ToString();
                    try
                    {
                        Font fnt = new System.Drawing.Font(row.InheritedStyle.Font, FontStyle.Strikeout);
                        row.DefaultCellStyle.Font = fnt;
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.Cells["colCheck"].Value = false;
                        if (mode == DeleteMode.RECYCLED)
                        {
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(filename, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                        }
                        else if (mode == DeleteMode.PERMANENT)
                        {
                            System.IO.File.Delete(filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message + "(" + filename + ").");
                    }
                }
            }
            MessageBox.Show("Done.");
        }

        #region event handler
        void detector_Notify(object sender, NotifyEventArgs e)
        {
            Notify(e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DuplicateSearchOption option = new DuplicateSearchOption( path: txtPath.Text, 
                                                                      limit: Convert.ToInt32(txtLimitPercentage.Text),
                                                                      ignoreLimit: Convert.ToInt32(txtIgnoreLimit.Text),
                                                                      filePattern: txtFilePattern.Text,
                                                                      blacklistPattern:txtBlackList.Text);
            detector.SearchThreading(option);
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvResult.SuspendLayout();
            dgvResult.Rows.Clear();
            dgvResult.ResumeLayout();
        }

        private void btnDelRecycled_Click(object sender, EventArgs e)
        {
            Delete(DeleteMode.RECYCLED);
        }
                
        private void btnDelPermanent_Click(object sender, EventArgs e)
        {
            Delete(DeleteMode.PERMANENT);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
        }

        private void txtLog_DoubleClick(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (detector != null)
            {
                this.Enabled = false;
                if (btnPause.Text == "Pause")
                {
                    lblStatus.Text = "Pausing...";
                    detector.Pause();                    
                    btnPause.Text = "Resume";
                }
                else
                {
                    lblStatus.Text = "Resuming...";
                    detector.Resume();
                    btnPause.Text = "Pause";
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (detector != null)
            {
                lblStatus.Text = "Stopping...";
                detector.Stop();
                this.Enabled = false;
            }
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResult.Rows.Count > 0)
            {
                if (e.ColumnIndex == dgvResult.Columns["colFilename"].Index)
                {
                    Process.Start(dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
            }
        }
        #endregion
    }

    public enum DeleteMode
    {
        RECYCLED, PERMANENT
    }
}
