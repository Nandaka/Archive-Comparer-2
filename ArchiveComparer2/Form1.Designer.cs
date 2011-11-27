namespace ArchiveComparer2
{
    partial class Form1
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDelPermanent = new System.Windows.Forms.Button();
            this.btnDelRecycled = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.txtBlackList = new System.Windows.Forms.TextBox();
            this.txtFilePattern = new System.Windows.Forms.TextBox();
            this.txtLimitPercentage = new System.Windows.Forms.TextBox();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDupGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatchType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArchivedSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoMatchesCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSkipped = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 476);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(795, 17);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: Ready";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(771, 461);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDelPermanent);
            this.tabPage1.Controls.Add(this.btnDelRecycled);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.dgvResult);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(763, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDelPermanent
            // 
            this.btnDelPermanent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelPermanent.Location = new System.Drawing.Point(636, 409);
            this.btnDelPermanent.Name = "btnDelPermanent";
            this.btnDelPermanent.Size = new System.Drawing.Size(121, 23);
            this.btnDelPermanent.TabIndex = 13;
            this.btnDelPermanent.Text = "Delete Permanently";
            this.btnDelPermanent.UseVisualStyleBackColor = true;
            this.btnDelPermanent.Click += new System.EventHandler(this.btnDelPermanent_Click);
            // 
            // btnDelRecycled
            // 
            this.btnDelRecycled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelRecycled.Location = new System.Drawing.Point(509, 409);
            this.btnDelRecycled.Name = "btnDelRecycled";
            this.btnDelRecycled.Size = new System.Drawing.Size(121, 23);
            this.btnDelRecycled.TabIndex = 12;
            this.btnDelRecycled.Text = "Delete to Recycle Bin";
            this.btnDelRecycled.UseVisualStyleBackColor = true;
            this.btnDelRecycled.Click += new System.EventHandler(this.btnDelRecycled_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(3, 409);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
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
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colDupGroup,
            this.colFilename,
            this.colPercentage,
            this.colMatchType,
            this.colItemsCount,
            this.colSize,
            this.colArchivedSize,
            this.colNoMatchesCount,
            this.colFileSize,
            this.colCreationTime,
            this.colSkipped});
            this.dgvResult.Location = new System.Drawing.Point(6, 33);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(751, 370);
            this.dgvResult.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(371, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(41, 6);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(324, 20);
            this.txtPath.TabIndex = 7;
            this.txtPath.Text = "D:\\New Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Path";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(763, 435);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Enabled = false;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(757, 429);
            this.txtLog.TabIndex = 10;
            this.txtLog.WordWrap = false;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btnReset);
            this.tabPage3.Controls.Add(this.btnSave);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(763, 435);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Config";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Limit Percentage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "File Pattern";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(682, 406);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(601, 406);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBlackList);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFilePattern);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLimitPercentage);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 108);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Matching Setting";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLog);
            this.groupBox2.Location = new System.Drawing.Point(6, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Blacklist Pattern";
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = global::ArchiveComparer2.Properties.Settings.Default.EnableTextBoxLogging;
            this.chkLog.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ArchiveComparer2.Properties.Settings.Default, "EnableTextBoxLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkLog.Location = new System.Drawing.Point(9, 19);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(141, 17);
            this.chkLog.TabIndex = 6;
            this.chkLog.Text = "Enable Textbox Logging";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // txtBlackList
            // 
            this.txtBlackList.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "BlackListPattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBlackList.Location = new System.Drawing.Point(98, 71);
            this.txtBlackList.Name = "txtBlackList";
            this.txtBlackList.Size = new System.Drawing.Size(248, 20);
            this.txtBlackList.TabIndex = 5;
            this.txtBlackList.Text = global::ArchiveComparer2.Properties.Settings.Default.BlackListPattern;
            // 
            // txtFilePattern
            // 
            this.txtFilePattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "FilePattern", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFilePattern.Location = new System.Drawing.Point(72, 19);
            this.txtFilePattern.Name = "txtFilePattern";
            this.txtFilePattern.Size = new System.Drawing.Size(274, 20);
            this.txtFilePattern.TabIndex = 1;
            this.txtFilePattern.Text = global::ArchiveComparer2.Properties.Settings.Default.FilePattern;
            // 
            // txtLimitPercentage
            // 
            this.txtLimitPercentage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ArchiveComparer2.Properties.Settings.Default, "LimitPercentage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLimitPercentage.Location = new System.Drawing.Point(98, 45);
            this.txtLimitPercentage.Name = "txtLimitPercentage";
            this.txtLimitPercentage.Size = new System.Drawing.Size(248, 20);
            this.txtLimitPercentage.TabIndex = 3;
            this.txtLimitPercentage.Text = global::ArchiveComparer2.Properties.Settings.Default.LimitPercentage;
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
            this.colDupGroup.FillWeight = 40F;
            this.colDupGroup.Frozen = true;
            this.colDupGroup.HeaderText = "Dup. Group";
            this.colDupGroup.Name = "colDupGroup";
            this.colDupGroup.ReadOnly = true;
            this.colDupGroup.Width = 40;
            // 
            // colFilename
            // 
            this.colFilename.FillWeight = 300F;
            this.colFilename.Frozen = true;
            this.colFilename.HeaderText = "Filename";
            this.colFilename.Name = "colFilename";
            this.colFilename.ReadOnly = true;
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
            // colSize
            // 
            this.colSize.HeaderText = "Real Size";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            // 
            // colArchivedSize
            // 
            this.colArchivedSize.HeaderText = "Archived Size";
            this.colArchivedSize.Name = "colArchivedSize";
            this.colArchivedSize.ReadOnly = true;
            // 
            // colNoMatchesCount
            // 
            this.colNoMatchesCount.FillWeight = 150F;
            this.colNoMatchesCount.HeaderText = "No Match Count";
            this.colNoMatchesCount.Name = "colNoMatchesCount";
            this.colNoMatchesCount.ReadOnly = true;
            this.colNoMatchesCount.Width = 150;
            // 
            // colFileSize
            // 
            this.colFileSize.HeaderText = "File Size";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.ReadOnly = true;
            // 
            // colCreationTime
            // 
            this.colCreationTime.FillWeight = 130F;
            this.colCreationTime.HeaderText = "Creation Time";
            this.colCreationTime.Name = "colCreationTime";
            this.colCreationTime.ReadOnly = true;
            this.colCreationTime.Width = 130;
            // 
            // colSkipped
            // 
            this.colSkipped.FillWeight = 50F;
            this.colSkipped.HeaderText = "Skipped";
            this.colSkipped.Name = "colSkipped";
            this.colSkipped.ReadOnly = true;
            this.colSkipped.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 493);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblStatus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDupGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArchivedSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoMatchesCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSkipped;
    }
}

