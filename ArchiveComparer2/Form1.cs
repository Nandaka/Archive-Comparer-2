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

namespace ArchiveComparer2
{
    public partial class Form1 : Form
    {
        ArchiveDuplicateDetector detector;
        static ILog Logger = LogManager.GetLogger(typeof(Form1).ToString());

        public Form1()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            detector = new ArchiveDuplicateDetector();
            detector.Notify += new ArchiveDuplicateDetector.NotifyEventHandler(detector_Notify);
        }

        void detector_Notify(object sender, NotifyEventArgs e)
        {
            Notify(e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            detector.SearchThreading(txtPath.Text, Convert.ToInt32(txtLimitPercentage.Text), txtFilePattern.Text, txtBlackList.Text);
        }

        private delegate void NotifyCallback(NotifyEventArgs e);

        private void Notify(NotifyEventArgs e)
        {

            if (this.txtLog.InvokeRequired || this.lblStatus.InvokeRequired || dgvResult.InvokeRequired)
            {
                NotifyCallback d = new NotifyCallback(Notify);
                this.Invoke(d, e);
            }
            else
            {
                string text = e.Status.ToString() + ": " + e.Message;
                if (e.Status == OperationStatus.COMPLETE)
                {
                    lblStatus.Text = text;
                    Fill(e.DupList);
                }
                else if (e.Status == OperationStatus.FILTERING || e.Status == OperationStatus.BUILDING_DUPLICATE_LIST)
                {
                    lblStatus.Text = text;
                }
                else
                {
                    if (chkLog.Checked)
                    {
                        txtLog.Text += text + Environment.NewLine;
                    }
                }
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        private void Fill(List<DuplicateArchiveInfoList> list)
        {
            dgvResult.SuspendLayout();
            for (int i = 0; i < list.Count; ++i)
            {
                DataGridViewRow row = AddRow(list[i].Original);
                row.DefaultCellStyle.BackColor = Color.DarkGray;

                for (int j = 0; j < list[i].Duplicates.Count; ++j)
                {
                    DataGridViewRow rowDup = AddRow(list[i].Duplicates[j]);

                    if (i % 2 == 0)
                    {
                        rowDup.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        rowDup.DefaultCellStyle.BackColor = Color.White;
                    }
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
            row.Cells["colPercentage"].Value = data.MatchType == MatchType.ORIGINAL ? "" : data.Percentage.ToString();
            row.Cells["colMatchType"].Value = data.MatchType.ToString();
            row.Cells["colItemsCount"].Value = data.Items.Count;

            if (data.NoMatches != null)
            {
                row.Cells["colNoMatchesCount"].Value = data.NoMatches.Count;
                row.Cells["colNoMatchesCount"].ToolTipText = ConcatItem(data.NoMatches);
            }
            else
            {
                row.Cells["colNoMatchesCount"].Value = 0;
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
            }
            else
            {
                row.Cells["colSkipped"].ToolTipText = "";
            }

            if (data.Skipped != null && data.Skipped.Count > 0)
            {
                row.DefaultCellStyle.Font = new Font(row.InheritedStyle.Font, FontStyle.Italic);
            }
            return row;
        }

        private string ConcatItem(List<ArchiveFileInfoSmall> list)
        {
            string result = "";
            foreach (var info in list)
            {
                result += info.Filename + Environment.NewLine;
            }
            return result;
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
    }

    public enum DeleteMode
    {
        RECYCLED, PERMANENT
    }
}
