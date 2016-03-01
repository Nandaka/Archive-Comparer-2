﻿namespace ArchiveComparer2
{
    partial class ArchiveComparer2Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveComparer2Form));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSelectSubset = new System.Windows.Forms.Button();
            this.btnSelectEqual = new System.Windows.Forms.Button();
            this.btnSelectOriginal = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClearResolved = new System.Windows.Forms.Button();
            this.btnClearDeleted = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDelPermanent = new System.Windows.Forms.Button();
            this.btnDelRecycled = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDupGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilename = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatchType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoMatchesCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSkipped = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArchivedSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCCRC = new System.Windows.Forms.CheckBox();
            this.chkFIL = new System.Windows.Forms.CheckBox();
            this.chkBDL = new System.Windows.Forms.CheckBox();
            this.chkCOMP = new System.Windows.Forms.CheckBox();
            this.chkBFL = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPreventStanby = new System.Windows.Forms.CheckBox();
            this.cbxPriority = new System.Windows.Forms.ComboBox();
            this.btnResetColWidth = new System.Windows.Forms.Button();
            this.txt7zDllPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkLogAll = new System.Windows.Forms.CheckBox();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkIgnoreSmallFileSize = new System.Windows.Forms.CheckBox();
            this.txtSmallFileSizeLimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkOnlyPerfectMatch = new System.Windows.Forms.CheckBox();
            this.chkBlacklistCI = new System.Windows.Forms.CheckBox();
            this.chkFileCI = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIgnoreLimit = new System.Windows.Forms.TextBox();
            this.txtBlackList = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilePattern = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLimitPercentage = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgCount = new System.Windows.Forms.ToolStripProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(16, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1181, 523);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSelectSubset);
            this.tabPage1.Controls.Add(this.btnSelectEqual);
            this.tabPage1.Controls.Add(this.btnSelectOriginal);
            this.tabPage1.Controls.Add(this.btnBrowse);
            this.tabPage1.Controls.Add(this.btnClearResolved);
            this.tabPage1.Controls.Add(this.btnClearDeleted);
            this.tabPage1.Controls.Add(this.btnPause);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnDelPermanent);
            this.tabPage1.Controls.Add(this.btnDelRecycled);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.dgvResult);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1173, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSelectSubset
            // 
            this.btnSelectSubset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectSubset.Location = new System.Drawing.Point(691, 459);
            this.btnSelectSubset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectSubset.Name = "btnSelectSubset";
            this.btnSelectSubset.Size = new System.Drawing.Size(125, 28);
            this.btnSelectSubset.TabIndex = 21;
            this.btnSelectSubset.Text = "Select Subset";
            this.btnSelectSubset.UseVisualStyleBackColor = true;
            this.btnSelectSubset.Click += new System.EventHandler(this.btnSelectSubset_Click);
            // 
            // btnSelectEqual
            // 
            this.btnSelectEqual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectEqual.Location = new System.Drawing.Point(557, 459);
            this.btnSelectEqual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectEqual.Name = "btnSelectEqual";
            this.btnSelectEqual.Size = new System.Drawing.Size(125, 28);
            this.btnSelectEqual.TabIndex = 20;
            this.btnSelectEqual.Text = "Select Equal Count";
            this.btnSelectEqual.UseVisualStyleBackColor = true;
            this.btnSelectEqual.Click += new System.EventHandler(this.btnSelectEqual_Click);
            // 
            // btnSelectOriginal
            // 
            this.btnSelectOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectOriginal.Location = new System.Drawing.Point(424, 459);
            this.btnSelectOriginal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectOriginal.Name = "btnSelectOriginal";
            this.btnSelectOriginal.Size = new System.Drawing.Size(125, 28);
            this.btnSelectOriginal.TabIndex = 19;
            this.btnSelectOriginal.Text = "Select Original";
            this.btnSelectOriginal.UseVisualStyleBackColor = true;
            this.btnSelectOriginal.Click += new System.EventHandler(this.btnSelectOriginal_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(495, 4);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.TabIndex = 18;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClearResolved
            // 
            this.btnClearResolved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearResolved.Location = new System.Drawing.Point(276, 459);
            this.btnClearResolved.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearResolved.Name = "btnClearResolved";
            this.btnClearResolved.Size = new System.Drawing.Size(128, 28);
            this.btnClearResolved.TabIndex = 17;
            this.btnClearResolved.Text = "Clear Resolved";
            this.btnClearResolved.UseVisualStyleBackColor = true;
            this.btnClearResolved.Click += new System.EventHandler(this.btnClearResolved_Click);
            // 
            // btnClearDeleted
            // 
            this.btnClearDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearDeleted.Location = new System.Drawing.Point(140, 459);
            this.btnClearDeleted.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearDeleted.Name = "btnClearDeleted";
            this.btnClearDeleted.Size = new System.Drawing.Size(128, 28);
            this.btnClearDeleted.TabIndex = 16;
            this.btnClearDeleted.Text = "Clear Deleted";
            this.btnClearDeleted.UseVisualStyleBackColor = true;
            this.btnClearDeleted.Click += new System.EventHandler(this.btnClearDeleted_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(955, 5);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 28);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(1063, 5);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 28);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDelPermanent
            // 
            this.btnDelPermanent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelPermanent.Location = new System.Drawing.Point(1001, 459);
            this.btnDelPermanent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelPermanent.Name = "btnDelPermanent";
            this.btnDelPermanent.Size = new System.Drawing.Size(161, 28);
            this.btnDelPermanent.TabIndex = 13;
            this.btnDelPermanent.Text = "Delete Permanently";
            this.btnDelPermanent.UseVisualStyleBackColor = true;
            this.btnDelPermanent.Click += new System.EventHandler(this.btnDelPermanent_Click);
            // 
            // btnDelRecycled
            // 
            this.btnDelRecycled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelRecycled.Location = new System.Drawing.Point(832, 459);
            this.btnDelRecycled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelRecycled.Name = "btnDelRecycled";
            this.btnDelRecycled.Size = new System.Drawing.Size(161, 28);
            this.btnDelRecycled.TabIndex = 12;
            this.btnDelRecycled.Text = "Delete to Recycle Bin";
            this.btnDelRecycled.UseVisualStyleBackColor = true;
            this.btnDelRecycled.Click += new System.EventHandler(this.btnDelRecycled_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(4, 459);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(128, 28);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear List";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colDupGroup,
            this.colFilename,
            this.colPercentage,
            this.colMatchType,
            this.colItemsCount,
            this.colNoMatchesCount,
            this.colSkipped,
            this.colSize,
            this.colArchivedSize,
            this.colFileSize,
            this.colCreationTime,
            this.colCrc,
            this.colStatus});
            this.dgvResult.Location = new System.Drawing.Point(8, 85);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(1155, 367);
            this.dgvResult.TabIndex = 10;
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            // 
            // colCheck
            // 
            this.colCheck.FillWeight = 20F;
            this.colCheck.Frozen = true;
            this.colCheck.HeaderText = "*";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheck.Width = 20;
            // 
            // colDupGroup
            // 
            this.colDupGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.colDupGroup.FillWeight = 40F;
            this.colDupGroup.Frozen = true;
            this.colDupGroup.HeaderText = "Dup. Group";
            this.colDupGroup.MinimumWidth = 40;
            this.colDupGroup.Name = "colDupGroup";
            this.colDupGroup.ReadOnly = true;
            this.colDupGroup.Width = 40;
            // 
            // colFilename
            // 
            this.colFilename.FillWeight = 300F;
            this.colFilename.Frozen = true;
            this.colFilename.HeaderText = "Filename";
            this.colFilename.LinkColor = System.Drawing.Color.Black;
            this.colFilename.MinimumWidth = 300;
            this.colFilename.Name = "colFilename";
            this.colFilename.ReadOnly = true;
            this.colFilename.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFilename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colFilename.Width = 300;
            // 
            // colPercentage
            // 
            this.colPercentage.FillWeight = 55F;
            this.colPercentage.Frozen = true;
            this.colPercentage.HeaderText = "Match %";
            this.colPercentage.Name = "colPercentage";
            this.colPercentage.ReadOnly = true;
            this.colPercentage.Width = 55;
            // 
            // colMatchType
            // 
            this.colMatchType.Frozen = true;
            this.colMatchType.HeaderText = "Match Type";
            this.colMatchType.Name = "colMatchType";
            this.colMatchType.ReadOnly = true;
            // 
            // colItemsCount
            // 
            this.colItemsCount.FillWeight = 50F;
            this.colItemsCount.HeaderText = "Items Count";
            this.colItemsCount.Name = "colItemsCount";
            this.colItemsCount.ReadOnly = true;
            this.colItemsCount.Width = 50;
            // 
            // colNoMatchesCount
            // 
            this.colNoMatchesCount.FillWeight = 50F;
            this.colNoMatchesCount.HeaderText = "No Match";
            this.colNoMatchesCount.Name = "colNoMatchesCount";
            this.colNoMatchesCount.ReadOnly = true;
            this.colNoMatchesCount.Width = 50;
            // 
            // colSkipped
            // 
            this.colSkipped.FillWeight = 50F;
            this.colSkipped.HeaderText = "Skipped";
            this.colSkipped.Name = "colSkipped";
            this.colSkipped.ReadOnly = true;
            this.colSkipped.Width = 50;
            // 
            // colSize
            // 
            this.colSize.FillWeight = 75F;
            this.colSize.HeaderText = "Real Size";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Width = 75;
            // 
            // colArchivedSize
            // 
            this.colArchivedSize.FillWeight = 75F;
            this.colArchivedSize.HeaderText = "Archived Size";
            this.colArchivedSize.Name = "colArchivedSize";
            this.colArchivedSize.ReadOnly = true;
            this.colArchivedSize.Width = 75;
            // 
            // colFileSize
            // 
            this.colFileSize.FillWeight = 75F;
            this.colFileSize.HeaderText = "File Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.ReadOnly = true;
            this.colFileSize.Width = 75;
            // 
            // colCreationTime
            // 
            this.colCreationTime.FillWeight = 130F;
            this.colCreationTime.HeaderText = "Creation Time";
            this.colCreationTime.Name = "colCreationTime";
            this.colCreationTime.ReadOnly = true;
            this.colCreationTime.Width = 130;
            // 
            // colCrc
            // 
            this.colCrc.FillWeight = 150F;
            this.colCrc.HeaderText = "CRC";
            this.colCrc.Name = "colCrc";
            this.colCrc.ReadOnly = true;
            this.colCrc.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(495, 39);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(55, 7);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPath.Size = new System.Drawing.Size(431, 69);
            this.txtPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Path";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1173, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(4, 4);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1165, 486);
            this.txtLog.TabIndex = 10;
            this.txtLog.WordWrap = false;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            this.txtLog.DoubleClick += new System.EventHandler(this.txtLog_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btnReset);
            this.tabPage3.Controls.Add(this.btnSave);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(1173, 494);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Config";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkCCRC);
            this.groupBox3.Controls.Add(this.chkFIL);
            this.groupBox3.Controls.Add(this.chkBDL);
            this.groupBox3.Controls.Add(this.chkCOMP);
            this.groupBox3.Controls.Add(this.chkBFL);
            this.groupBox3.Location = new System.Drawing.Point(8, 273);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(612, 116);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logging";
            // 
            // chkCCRC
            // 
            this.chkCCRC.AutoSize = true;
            this.chkCCRC.Checked = global::ArchiveComparer2.Properties.Settings.Default.logCCRC;
            this.chkCCRC.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "logCCRC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCCRC.Location = new System.Drawing.Point(12, 52);
            this.chkCCRC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCCRC.Name = "chkCCRC";
            this.chkCCRC.Size = new System.Drawing.Size(161, 21);
            this.chkCCRC.TabIndex = 14;
            this.chkCCRC.Text = "CALCULATING_CRC";
            this.chkCCRC.UseVisualStyleBackColor = true;
            // 
            // chkFIL
            // 
            this.chkFIL.AutoSize = true;
            this.chkFIL.Checked = global::ArchiveComparer2.Properties.Settings.Default.logFIL;
            this.chkFIL.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "logFIL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkFIL.Location = new System.Drawing.Point(199, 80);
            this.chkFIL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFIL.Name = "chkFIL";
            this.chkFIL.Size = new System.Drawing.Size(101, 21);
            this.chkFIL.TabIndex = 12;
            this.chkFIL.Text = "FILTERING";
            this.chkFIL.UseVisualStyleBackColor = true;
            // 
            // chkBDL
            // 
            this.chkBDL.AutoSize = true;
            this.chkBDL.Checked = global::ArchiveComparer2.Properties.Settings.Default.logBDL;
            this.chkBDL.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "logBDL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBDL.Location = new System.Drawing.Point(199, 23);
            this.chkBDL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBDL.Name = "chkBDL";
            this.chkBDL.Size = new System.Drawing.Size(215, 21);
            this.chkBDL.TabIndex = 11;
            this.chkBDL.Text = "BUILDING_DUPLICATE_LIST";
            this.chkBDL.UseVisualStyleBackColor = true;
            // 
            // chkCOMP
            // 
            this.chkCOMP.AutoSize = true;
            this.chkCOMP.Checked = global::ArchiveComparer2.Properties.Settings.Default.logCOMP;
            this.chkCOMP.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "logCOMP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCOMP.Location = new System.Drawing.Point(199, 52);
            this.chkCOMP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCOMP.Name = "chkCOMP";
            this.chkCOMP.Size = new System.Drawing.Size(113, 21);
            this.chkCOMP.TabIndex = 13;
            this.chkCOMP.Text = "COMPARING";
            this.chkCOMP.UseVisualStyleBackColor = true;
            // 
            // chkBFL
            // 
            this.chkBFL.AutoSize = true;
            this.chkBFL.Checked = global::ArchiveComparer2.Properties.Settings.Default.logBFL;
            this.chkBFL.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "logBFL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBFL.Location = new System.Drawing.Point(12, 23);
            this.chkBFL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBFL.Name = "chkBFL";
            this.chkBFL.Size = new System.Drawing.Size(167, 21);
            this.chkBFL.TabIndex = 10;
            this.chkBFL.Text = "BUILDING_FILE_LIST";
            this.chkBFL.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPreventStanby);
            this.groupBox2.Controls.Add(this.cbxPriority);
            this.groupBox2.Controls.Add(this.btnResetColWidth);
            this.groupBox2.Controls.Add(this.txt7zDllPath);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkLogAll);
            this.groupBox2.Controls.Add(this.chkLog);
            this.groupBox2.Location = new System.Drawing.Point(8, 176);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(612, 90);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application";
            // 
            // chkPreventStanby
            // 
            this.chkPreventStanby.AutoSize = true;
            this.chkPreventStanby.Checked = global::ArchiveComparer2.Properties.Settings.Default.PreventStanby;
            this.chkPreventStanby.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "PreventStanby", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkPreventStanby.Location = new System.Drawing.Point(293, 23);
            this.chkPreventStanby.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPreventStanby.Name = "chkPreventStanby";
            this.chkPreventStanby.Size = new System.Drawing.Size(119, 21);
            this.chkPreventStanby.TabIndex = 10;
            this.chkPreventStanby.Text = "Prevent Sleep";
            this.chkPreventStanby.UseVisualStyleBackColor = true;
            // 
            // cbxPriority
            // 
            this.cbxPriority.FormattingEnabled = true;
            this.cbxPriority.Items.AddRange(new object[] {
            "Highest",
            "AboveNormal",
            "Normal",
            "BelowNormal",
            "Lowest"});
            this.cbxPriority.Location = new System.Drawing.Point(443, 52);
            this.cbxPriority.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPriority.Name = "cbxPriority";
            this.cbxPriority.Size = new System.Drawing.Size(160, 24);
            this.cbxPriority.TabIndex = 11;
            this.cbxPriority.SelectedIndexChanged += new System.EventHandler(this.cbxPriority_SelectedIndexChanged);
            // 
            // btnResetColWidth
            // 
            this.btnResetColWidth.Location = new System.Drawing.Point(424, 18);
            this.btnResetColWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetColWidth.Name = "btnResetColWidth";
            this.btnResetColWidth.Size = new System.Drawing.Size(180, 28);
            this.btnResetColWidth.TabIndex = 10;
            this.btnResetColWidth.Text = "Reset Column Width";
            this.btnResetColWidth.UseVisualStyleBackColor = true;
            this.btnResetColWidth.Click += new System.EventHandler(this.btnResetColWidth_Click);
            // 
            // txt7zDllPath
            // 
            this.txt7zDllPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "SevenZipPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txt7zDllPath.Location = new System.Drawing.Point(96, 52);
            this.txt7zDllPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt7zDllPath.Name = "txt7zDllPath";
            this.txt7zDllPath.Size = new System.Drawing.Size(325, 22);
            this.txt7zDllPath.TabIndex = 9;
            this.txt7zDllPath.Text = global::ArchiveComparer2.Properties.Settings.Default.SevenZipPath;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "7z.dll path";
            // 
            // chkLogAll
            // 
            this.chkLogAll.AutoSize = true;
            this.chkLogAll.Checked = global::ArchiveComparer2.Properties.Settings.Default.LogAll;
            this.chkLogAll.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "LogAll", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLogAll.Location = new System.Drawing.Point(208, 23);
            this.chkLogAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkLogAll.Name = "chkLogAll";
            this.chkLogAll.Size = new System.Drawing.Size(73, 21);
            this.chkLogAll.TabIndex = 7;
            this.chkLogAll.Text = "Log All";
            this.chkLogAll.UseVisualStyleBackColor = true;
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = global::ArchiveComparer2.Properties.Settings.Default.EnableTextBoxLogging;
            this.chkLog.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "EnableTextBoxLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLog.Location = new System.Drawing.Point(12, 23);
            this.chkLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(182, 21);
            this.chkLog.TabIndex = 6;
            this.chkLog.Text = "Enable Textbox Logging";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.chkIgnoreSmallFileSize);
            this.groupBox1.Controls.Add(this.txtSmallFileSizeLimit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.chkOnlyPerfectMatch);
            this.groupBox1.Controls.Add(this.chkBlacklistCI);
            this.groupBox1.Controls.Add(this.chkFileCI);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIgnoreLimit);
            this.groupBox1.Controls.Add(this.txtBlackList);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFilePattern);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLimitPercentage);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(612, 161);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Matching Setting";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(419, 123);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "bytes";
            // 
            // chkIgnoreSmallFileSize
            // 
            this.chkIgnoreSmallFileSize.AutoSize = true;
            this.chkIgnoreSmallFileSize.Checked = global::ArchiveComparer2.Properties.Settings.Default.SmallFilesCI;
            this.chkIgnoreSmallFileSize.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "SmallFilesCI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIgnoreSmallFileSize.Location = new System.Drawing.Point(12, 122);
            this.chkIgnoreSmallFileSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkIgnoreSmallFileSize.Name = "chkIgnoreSmallFileSize";
            this.chkIgnoreSmallFileSize.Size = new System.Drawing.Size(141, 21);
            this.chkIgnoreSmallFileSize.TabIndex = 13;
            this.chkIgnoreSmallFileSize.Text = "Ignore Small Files";
            this.chkIgnoreSmallFileSize.UseVisualStyleBackColor = true;
            // 
            // txtSmallFileSizeLimit
            // 
            this.txtSmallFileSizeLimit.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "SmallFileSizeLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSmallFileSizeLimit.Location = new System.Drawing.Point(303, 119);
            this.txtSmallFileSizeLimit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSmallFileSizeLimit.Name = "txtSmallFileSizeLimit";
            this.txtSmallFileSizeLimit.Size = new System.Drawing.Size(107, 22);
            this.txtSmallFileSizeLimit.TabIndex = 12;
            this.txtSmallFileSizeLimit.Text = global::ArchiveComparer2.Properties.Settings.Default.SmallFileSizeLimit;
            this.txtSmallFileSizeLimit.TextChanged += new System.EventHandler(this.txtSmallFileSizeLimit_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(164, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "Small File Size Limit";
            // 
            // chkOnlyPerfectMatch
            // 
            this.chkOnlyPerfectMatch.AutoSize = true;
            this.chkOnlyPerfectMatch.Checked = global::ArchiveComparer2.Properties.Settings.Default.OnlyPerfectMatch;
            this.chkOnlyPerfectMatch.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "OnlyPerfectMatch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkOnlyPerfectMatch.Location = new System.Drawing.Point(448, 58);
            this.chkOnlyPerfectMatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkOnlyPerfectMatch.Name = "chkOnlyPerfectMatch";
            this.chkOnlyPerfectMatch.Size = new System.Drawing.Size(150, 21);
            this.chkOnlyPerfectMatch.TabIndex = 10;
            this.chkOnlyPerfectMatch.Text = "Only Perfect Match";
            this.chkOnlyPerfectMatch.UseVisualStyleBackColor = true;
            // 
            // chkBlacklistCI
            // 
            this.chkBlacklistCI.AutoSize = true;
            this.chkBlacklistCI.Checked = global::ArchiveComparer2.Properties.Settings.Default.BlacklistCI;
            this.chkBlacklistCI.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "BlacklistCI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBlacklistCI.Location = new System.Drawing.Point(467, 90);
            this.chkBlacklistCI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBlacklistCI.Name = "chkBlacklistCI";
            this.chkBlacklistCI.Size = new System.Drawing.Size(132, 21);
            this.chkBlacklistCI.TabIndex = 10;
            this.chkBlacklistCI.Text = "Case Insensitive";
            this.chkBlacklistCI.UseVisualStyleBackColor = true;
            // 
            // chkFileCI
            // 
            this.chkFileCI.AutoSize = true;
            this.chkFileCI.Checked = global::ArchiveComparer2.Properties.Settings.Default.FileCI;
            this.chkFileCI.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "FileCI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkFileCI.Location = new System.Drawing.Point(469, 26);
            this.chkFileCI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFileCI.Name = "chkFileCI";
            this.chkFileCI.Size = new System.Drawing.Size(132, 21);
            this.chkFileCI.TabIndex = 9;
            this.chkFileCI.Text = "Case Insensitive";
            this.chkFileCI.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(389, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "files";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ignore If Less Than";
            // 
            // txtIgnoreLimit
            // 
            this.txtIgnoreLimit.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "IgnoreLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIgnoreLimit.Location = new System.Drawing.Point(341, 55);
            this.txtIgnoreLimit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIgnoreLimit.Name = "txtIgnoreLimit";
            this.txtIgnoreLimit.Size = new System.Drawing.Size(39, 22);
            this.txtIgnoreLimit.TabIndex = 7;
            this.txtIgnoreLimit.Text = global::ArchiveComparer2.Properties.Settings.Default.IgnoreLimit;
            // 
            // txtBlackList
            // 
            this.txtBlackList.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "BlackListPattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBlackList.Location = new System.Drawing.Point(131, 87);
            this.txtBlackList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBlackList.Name = "txtBlackList";
            this.txtBlackList.Size = new System.Drawing.Size(329, 22);
            this.txtBlackList.TabIndex = 5;
            this.txtBlackList.Text = global::ArchiveComparer2.Properties.Settings.Default.BlackListPattern;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Blacklist Pattern";
            // 
            // txtFilePattern
            // 
            this.txtFilePattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "FilePattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFilePattern.Location = new System.Drawing.Point(96, 23);
            this.txtFilePattern.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilePattern.Name = "txtFilePattern";
            this.txtFilePattern.Size = new System.Drawing.Size(364, 22);
            this.txtFilePattern.TabIndex = 1;
            this.txtFilePattern.Text = global::ArchiveComparer2.Properties.Settings.Default.FilePattern;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "File Pattern";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Limit Percentage";
            // 
            // txtLimitPercentage
            // 
            this.txtLimitPercentage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "LimitPercentage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLimitPercentage.Location = new System.Drawing.Point(131, 55);
            this.txtLimitPercentage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLimitPercentage.Name = "txtLimitPercentage";
            this.txtLimitPercentage.Size = new System.Drawing.Size(33, 22);
            this.txtLimitPercentage.TabIndex = 3;
            this.txtLimitPercentage.Text = global::ArchiveComparer2.Properties.Settings.Default.LimitPercentage;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(403, 396);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 28);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(520, 396);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pgCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1213, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1193, 20);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Status: Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgCount
            // 
            this.pgCount.AutoToolTip = true;
            this.pgCount.Name = "pgCount";
            this.pgCount.Size = new System.Drawing.Size(133, 20);
            this.pgCount.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgCount.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Dup. Group";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 55F;
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Match %";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 55;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Match Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Items Count";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 50F;
            this.dataGridViewTextBoxColumn5.HeaderText = "No Match";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 50F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Skipped";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 75F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Real Size";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 75;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 75F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Archived Size";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 75;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 75F;
            this.dataGridViewTextBoxColumn9.HeaderText = "File Size";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 75;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 130F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Creation Time";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 130;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.FillWeight = 150F;
            this.dataGridViewTextBoxColumn11.HeaderText = "CRC";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 150;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Status";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // ArchiveComparer2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 569);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1061, 605);
            this.Name = "ArchiveComparer2Form";
            this.Text = "Archive Comparer ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArchiveComparer2Form_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelPermanent;
        private System.Windows.Forms.Button btnDelRecycled;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtFilePattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLimitPercentage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBlackList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkLogAll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pgCount;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIgnoreLimit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearDeleted;
        private System.Windows.Forms.Button btnClearResolved;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txt7zDllPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkBlacklistCI;
        private System.Windows.Forms.CheckBox chkFileCI;
        private System.Windows.Forms.CheckBox chkOnlyPerfectMatch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkCCRC;
        private System.Windows.Forms.CheckBox chkFIL;
        private System.Windows.Forms.CheckBox chkBDL;
        private System.Windows.Forms.CheckBox chkCOMP;
        private System.Windows.Forms.CheckBox chkBFL;
        private System.Windows.Forms.Button btnResetColWidth;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDupGroup;
        private System.Windows.Forms.DataGridViewLinkColumn colFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoMatchesCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSkipped;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArchivedSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ComboBox cbxPriority;
        private System.Windows.Forms.CheckBox chkPreventStanby;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button btnSelectOriginal;
        private System.Windows.Forms.Button btnSelectSubset;
        private System.Windows.Forms.Button btnSelectEqual;
        private System.Windows.Forms.TextBox txtSmallFileSizeLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkIgnoreSmallFileSize;
        private System.Windows.Forms.Label label10;
    }
}

