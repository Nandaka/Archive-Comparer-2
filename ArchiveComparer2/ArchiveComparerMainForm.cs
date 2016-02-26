using ArchiveComparer2.Library;
using ArchiveComparer2.Properties;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArchiveComparer2
{
    public partial class ArchiveComparer2Form : Form
    {
        private ArchiveDuplicateDetector detector;
        private static ILog Logger;

        public ArchiveComparer2Form()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.Text += fvi.ProductVersion;

            log4net.GlobalContext.Properties["Date"] = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            log4net.Config.XmlConfigurator.Configure();
            if (Logger == null)
            {
                Logger = LogManager.GetLogger(typeof(ArchiveComparer2Form));
            }
            Logger.Info("Starting Up Archive Comparer " + fvi.ProductVersion);

            detector = new ArchiveDuplicateDetector();
            detector.Notify += new ArchiveDuplicateDetector.NotifyEventHandler(detector_Notify);

            if (Properties.Settings.Default.UpdateRequired)
            {
                Logger.Info("Upgrading application settings");
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateRequired = false;
                Properties.Settings.Default.Save();
            }

            cbxPriority.SelectedIndex = Properties.Settings.Default.threadPriority;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Logger != null)
            {
                var ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    Logger.Error("Unhandled Exception: " + ex.Message + " at " + sender.ToString(), ex);
                }
                else
                {
                    Logger.Error("Unhandled Exception at " + sender.ToString());
                }
            }
        }

        private delegate void NotifyCallback(NotifyEventArgs e);

        private void Notify(NotifyEventArgs e)
        {
            if (this.InvokeRequired || txtLog.InvokeRequired || dgvResult.InvokeRequired || statusStrip1.InvokeRequired)
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

                if (e.Status == OperationStatus.STOPPED)
                {
                    btnSearch.Enabled = true;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                }

                if (e.Status == OperationStatus.COMPLETE)
                {
                    btnSearch.Enabled = true;
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
                DataGridViewRow rowSep = AddRowSep(list[i].Original);
                DataGridViewRow row = AddRow(list[i].Original);

                for (int j = 0; j < list[i].Duplicates.Count; ++j)
                {
                    DataGridViewRow rowDup = AddRow(list[i].Duplicates[j]);
                }
            }

            dgvResult.ResumeLayout();
        }

        private DataGridViewRow AddRowSep(DuplicateArchiveInfo data)
        {
            int index = dgvResult.Rows.Add();
            DataGridViewRow row = dgvResult.Rows[index];
            row.DefaultCellStyle.BackColor = Color.Black;

            row.ReadOnly = true;
            var cellCheck = new HMergedCell();
            cellCheck.LeftColumn = 0;
            cellCheck.RightColumn = 1;
            cellCheck.Value = data.DupGroup.ToString("D4");
            row.Cells["colCheck"] = cellCheck;

            var cellGroup = new HMergedCell();
            cellGroup.LeftColumn = 0;
            cellGroup.RightColumn = 1;
            cellGroup.Value = data.DupGroup.ToString("D4");
            row.Cells["colDupGroup"] = cellGroup;

            string filenameOnly = data.Filename.Substring(data.Filename.LastIndexOf("\\") + 1);
            row.Cells["colFilename"] = new HMergedCell();
            var mergedCells = (HMergedCell)row.Cells["colFilename"];
            mergedCells.LeftColumn = 2;
            mergedCells.RightColumn = 13;
            mergedCells.Value = filenameOnly;

            int nOffset = 2;
            for (int j = nOffset; j < 14; j++)
            {
                row.Cells[j] = new HMergedCell();
                HMergedCell pCell = (HMergedCell)row.Cells[j];
                pCell.LeftColumn = 2;
                pCell.RightColumn = 13;
                pCell.Value = filenameOnly;
            }
            row.Cells["colMatchType"].Value = "SEPARATOR";

            // Fill numeric data for sorting
            row.Cells["colItemsCount"].Value = -1;
            row.Cells["colFileSize"].Value = -1L;
            row.Cells["colCreationTime"].Value = DateTime.MinValue;
            return row;
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
            }
            else if (data.MatchType == MatchType.EQUALCOUNT)
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }

            row.Cells["colPercentage"].Value = data.MatchType == MatchType.ORIGINAL ? "" : data.Percentage.ToString();
            row.Cells["colMatchType"].Value = data.MatchType.ToString();
            row.Cells["colItemsCount"].Value = data.Items.Count;
            row.Cells["colItemsCount"].ToolTipText = String.Format("File Count: {0}\r\nDir Count: {1}", data.Items.Count, data.DirectoryCount);

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
                result += info.Crc.ToUpper() + " " + Util.ConvertBytesToCompactString(info.Size).PadLeft(10, ' ') + " " + info.Filename + Environment.NewLine;
            }
            return result;
        }

        private bool CheckDeleteGroup()
        {
            string currGroup = null;
            string prevGroup = null;
            bool allSelected = true;
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                if (row.Cells["colMatchType"].Value.ToString() == "SEPARATOR") continue;
                currGroup = row.Cells["colDupGroup"].Value.ToString().Trim();
                bool isLastRow = row.Index == row.DataGridView.Rows.GetLastRow(DataGridViewElementStates.None);

                if (false == Convert.ToBoolean(row.Cells["colCheck"].Value) &&
                    String.IsNullOrWhiteSpace(row.Cells["colStatus"].Value.ToString()))
                {
                    allSelected = false;
                }

                // next group
                if (currGroup != prevGroup && prevGroup != null && !isLastRow)
                {
                    // prev group is all selected
                    if (allSelected)
                    {
                        DialogResult result = MessageBox.Show("Delete all for group: " + prevGroup, "Delete All Confirmation", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.Cancel) return false;
                    }
                    allSelected = true;
                    prevGroup = null;
                }
                // last row
                if (isLastRow)
                {
                    if (allSelected)
                    {
                        DialogResult result = MessageBox.Show("Delete all for group: " + currGroup, "Delete All Confirmation", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.Cancel) return false;
                    }
                }
                prevGroup = currGroup;
            }
            return true;
        }

        private void Delete(DeleteMode mode)
        {
            if (CheckDeleteGroup())
            {
                foreach (DataGridViewRow row in dgvResult.Rows)
                {
                    if (row.Cells["colMatchType"].Value.ToString() == "SEPARATOR") continue;
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
        }

        public List<string> GetPathList()
        {
            List<string> paths = new List<string>();
            string[] tmp = txtPath.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            paths.AddRange(tmp);
            paths.Sort();
            int i = 1;
            for (int j = 0; j < paths.Count; ++j)
            {
                if (!paths[j].EndsWith("\\")) paths[j] += "\\";
            }
            while (paths.Count > i)
            {
                string temp = paths[i - 1];

                if (paths[i].StartsWith(temp))
                {
                    paths.RemoveAt(i);
                }
                else ++i;
            }
            return paths;
        }

        #region event handler

        private void detector_Notify(object sender, NotifyEventArgs e)
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
                Priority = (ThreadPriority)cbxPriority.SelectedIndex,
                PreventStanby = chkPreventStanby.Checked,
                IgnoreSmallFile = chkIgnoreSmallFileSize.Checked,
                SmallFileSizeLimit = ulong.Parse(txtSmallFileSizeLimit.Text)
            };
            detector.SearchThreading(option);
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            btnSearch.Enabled = false;
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
            ClearDeleted();
        }

        private void ClearDeleted()
        {
            int i = 0;
            while (i < dgvResult.Rows.Count && dgvResult.Rows.Count > 0)
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
            // clear deleted rows
            ClearDeleted();

            // clear group with only 1 row

            int i = 0;
            int groupCount = 0;
            string prevGroup = "";

            while (i < dgvResult.Rows.Count && dgvResult.Rows.Count > 0)
            {
                DataGridViewRow row = dgvResult.Rows[i];

                string currGroup = row.Cells["colDupGroup"].Value.ToString().Trim();
                if (prevGroup != currGroup)
                {
                    if (groupCount == 1)
                    {
                        // previous group only have 1 item + 1 separator
                        --i;
                        dgvResult.Rows.RemoveAt(i);
                        --i;
                        dgvResult.Rows.RemoveAt(i);
                    }

                    prevGroup = currGroup;
                    groupCount = 0;
                }
                else
                {
                    ++groupCount;
                }
                ++i;
            }

            //// clean up last entry
            if (prevGroup == dgvResult.Rows[i - 1].Cells["colDupGroup"].Value.ToString().Trim()) ++groupCount;
            if (groupCount == 2)
            {
                --i;
                dgvResult.Rows.RemoveAt(i);
                --i;
                dgvResult.Rows.RemoveAt(i);
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

        #endregion event handler

        private void ArchiveComparer2Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgvResult.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("The duplicate list is not empty! Do you really want to exit?", "Program Closing", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void cbxPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (detector != null) detector.ChangeThreadPriority((ThreadPriority)cbxPriority.SelectedIndex);
        }

        private void SelectDuplicates(MatchType mode)
        {
            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                var matchType = row.Cells["colMatchType"].Value.ToString();
                if (matchType == "SEPARATOR") continue;

                if (matchType == mode.ToString())
                {
                    row.Cells["colCheck"].Value = true;
                }
            }
        }

        private void btnSelectEqual_Click(object sender, EventArgs e)
        {
            SelectDuplicates(MatchType.EQUALCOUNT);
        }

        private void btnSelectSubset_Click(object sender, EventArgs e)
        {
            SelectDuplicates(MatchType.SUBSET);
        }

        private void btnSelectOriginal_Click(object sender, EventArgs e)
        {
            SelectDuplicates(MatchType.ORIGINAL);
        }

        private void txtSmallFileSizeLimit_TextChanged(object sender, EventArgs e)
        {
            ulong parsed;
            bool result = ulong.TryParse(txtSmallFileSizeLimit.Text, out parsed);
            if (!result)
            {
                MessageBox.Show(String.Format("Invalid value for Small File Size Limit: {0}", txtSmallFileSizeLimit.Text, "Invalid Value"));
                txtSmallFileSizeLimit.Text = "0";
            }
        }
    }

    public enum DeleteMode
    {
        RECYCLED, PERMANENT
    }
}