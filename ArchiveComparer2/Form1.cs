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
        static ILog Logger;

        public ArchiveComparer2Form()
        {
            InitializeComponent();

            log4net.GlobalContext.Properties["Date"] = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            log4net.Config.XmlConfigurator.Configure();
            if (Logger == null)
            {
                Logger = LogManager.GetLogger(typeof(ArchiveComparer2Form));
            }
            Logger.Info("Starting Up Archive Comparer 2");
            
            detector = new ArchiveDuplicateDetector();
            detector.Notify += new ArchiveDuplicateDetector.NotifyEventHandler(detector_Notify);

            cbxPriority.SelectedIndex = Properties.Settings.Default.threadPriority;
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
                    lblStatus.Text = e.Message;
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
                    pgCount.ToolTipText = e.CurrentCount + "/" + e.TotalCount;
                }

                if (e.Status == OperationStatus.COMPLETE)
                {
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
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

                if (chkBFL.Checked && e.Status == OperationStatus.BUILDING_FILE_LIST) Logger.Info(text);
                else if (chkCCRC.Checked && e.Status == OperationStatus.CALCULATING_CRC) Logger.Debug(text);
                else if (chkBDL.Checked && e.Status == OperationStatus.BUILDING_DUPLICATE_LIST) Logger.Debug(text);
                else if (chkCOMP.Checked && e.Status == OperationStatus.COMPARING) Logger.Debug(text);
                else if (chkFIL.Checked && e.Status == OperationStatus.FILTERING) Logger.Debug(text);
                else if (e.Status != OperationStatus.BUILDING_FILE_LIST &&
                        e.Status != OperationStatus.CALCULATING_CRC &&
                        e.Status != OperationStatus.BUILDING_DUPLICATE_LIST &&
                        e.Status != OperationStatus.COMPARING &&
                        e.Status != OperationStatus.FILTERING) Logger.Debug(text);
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
            row.Cells["colDupGroup"].Value = data.MatchType == MatchType.ORIGINAL ? data.DupGroup.ToString("D4") : data.DupGroup.ToString("D4") + " ";
            row.Cells["colFileSize"].Value = data.FileSize;
            row.Cells["colCreationTime"].Value = data.CreationTime;

            if (data.Skipped != null)
            {
                row.Cells["colSkipped"].Value = data.Skipped.Count;
                row.Cells["colSkipped"].ToolTipText = ConcatItem(data.Skipped);
                row.Cells["colItemsCount"].Value = data.Items.Count + " (" + (data.Items.Count + data.Skipped.Count) + ")";
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
            row.Cells["colStatus"].Value = "";

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
                            Logger.Info("Delete to Recyle Bin: " + filename);
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(filename, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                        }
                        else if (mode == DeleteMode.PERMANENT)
                        {
                            Logger.Info("Delete Permanently: " + filename);
                            System.IO.File.Delete(filename);
                        }
                        row.Cells["colStatus"].Value = mode.ToString();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message + "(" + filename + ").");
                        row.Cells["colStatus"].Value = ex.Message;
                    }
                }
            }
            MessageBox.Show("Done.");
        }

        public List<string> GetPathList()
        {
            List<string> paths = new List<string>();
            string[] tmp = txtPath.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            paths.AddRange(tmp);
            paths.Sort();
            int i = 1;
            while (paths.Count > i)
            {
                string temp = paths[i - 1];
                if (!temp.EndsWith("\\")) temp += "\\";

                if (paths[i].StartsWith(temp))
                {
                    paths.RemoveAt(i);
                }
                else ++i;
            }
            return paths;
        }

        #region event handler
        void detector_Notify(object sender, NotifyEventArgs e)
        {
            Notify(e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DuplicateSearchOption option = new DuplicateSearchOption()
            {
                Paths = GetPathList(),
                Limit = Convert.ToInt32(txtLimitPercentage.Text),
                IgnoreLimit = Convert.ToInt32(txtIgnoreLimit.Text),
                FilePattern = txtFilePattern.Text,
                BlacklistPattern = txtBlackList.Text,
                FileCaseInsensitive = chkFileCI.Checked,
                BlacklistCaseInsensitive = chkBlacklistCI.Checked,
                SevenZipPath = txt7zDllPath.Text,
                OnlyPerfectMatch = chkOnlyPerfectMatch.Checked,
                Priority = (ThreadPriority ) cbxPriority.SelectedIndex
            };
            detector.SearchThreading(option);
            btnPause.Enabled = true;
            btnStop.Enabled = true;
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
            Properties.Settings.Default.threadPriority = cbxPriority.SelectedIndex;
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
                    try
                    {
                        Process.Start(dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "File Open Error");
                    }
                }
            }
        }
        
        private void btnClearDeleted_Click(object sender, EventArgs e)
        {
            int i = 0;
            while( i<dgvResult.Rows.Count ) 
            {
                DataGridViewRow row = dgvResult.Rows[i];
                if (row.Cells["colStatus"].Value.ToString() == DeleteMode.PERMANENT.ToString() ||
                    row.Cells["colStatus"].Value.ToString() == DeleteMode.RECYCLED.ToString())
                {
                    dgvResult.Rows.RemoveAt(i);
                }
                else ++i;
            }
        }

        private void btnClearResolved_Click(object sender, EventArgs e)
        {
            int i = 0;
            int groupCount = 0;
            string prevGroup = "";

            while (i < dgvResult.Rows.Count)
            {
                DataGridViewRow row = dgvResult.Rows[i];

                string currGroup = row.Cells["colDupGroup"].Value.ToString().Trim();
                if (prevGroup != currGroup)
                {
                    if (groupCount == 1)
                    {
                        // previous group only have 1 item
                        --i;
                        dgvResult.Rows.RemoveAt(i);
                    }
                    
                    prevGroup = currGroup;
                    groupCount = 1;
                }
                else
                {
                    ++groupCount;
                }

                if (row.Cells["colStatus"].Value.ToString() == DeleteMode.PERMANENT.ToString() ||
                    row.Cells["colStatus"].Value.ToString() == DeleteMode.RECYCLED.ToString())
                {
                    dgvResult.Rows.RemoveAt(i);
                    --groupCount;
                }
                else ++i;
            }

            if (groupCount == 1)
            {
                dgvResult.Rows.RemoveAt(i-1);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.AppendText(Environment.NewLine + folderBrowserDialog1.SelectedPath);

                List<string> paths = GetPathList();
                txtPath.Text = "";
                foreach (string path in paths)
                {
                    txtPath.AppendText(path + Environment.NewLine);
                }
            }
        }

        private void btnResetColWidth_Click(object sender, EventArgs e)
        {
            dgvResult.Columns["colFilename"].Width = dgvResult.Columns["colFilename"].MinimumWidth;
        }
        #endregion
    }

    public enum DeleteMode
    {
        RECYCLED, PERMANENT
    }
}
