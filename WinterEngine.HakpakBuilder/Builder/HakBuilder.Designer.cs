namespace WinterEngine.Hakpak.Builder
{
    partial class HakBuilder
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
            this.listBoxResources = new System.Windows.Forms.ListBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonRemoveFiles = new System.Windows.Forms.Button();
            this.labelResources = new System.Windows.Forms.Label();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.openFileDialogBuilder = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerProcess = new System.ComponentModel.BackgroundWorker();
            this.progressBarBuild = new System.Windows.Forms.ProgressBar();
            this.saveFileDialogSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.labelResourceName = new System.Windows.Forms.Label();
            this.labelItemPartType = new System.Windows.Forms.Label();
            this.comboBoxItemPartType = new System.Windows.Forms.ComboBox();
            this.textBoxResourceName = new System.Windows.Forms.TextBox();
            this.radioButton2D = new System.Windows.Forms.RadioButton();
            this.radioButton3D = new System.Windows.Forms.RadioButton();
            this.labelLinksTo = new System.Windows.Forms.Label();
            this.listBoxLinkTo = new System.Windows.Forms.ListBox();
            this.checkBoxIsItem = new System.Windows.Forms.CheckBox();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxResources
            // 
            this.listBoxResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxResources.FormattingEnabled = true;
            this.listBoxResources.HorizontalScrollbar = true;
            this.listBoxResources.Location = new System.Drawing.Point(12, 184);
            this.listBoxResources.Name = "listBoxResources";
            this.listBoxResources.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxResources.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResources.Size = new System.Drawing.Size(255, 251);
            this.listBoxResources.TabIndex = 0;
            this.listBoxResources.SelectedIndexChanged += new System.EventHandler(this.listBoxResources_SelectedIndexChanged);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(592, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripMenuItemBuild,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Visible = false;
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Visible = false;
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemBuild
            // 
            this.toolStripMenuItemBuild.Name = "toolStripMenuItemBuild";
            this.toolStripMenuItemBuild.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemBuild.Text = "Build";
            this.toolStripMenuItemBuild.Click += new System.EventHandler(this.toolStripMenuItemBuild_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuild.Location = new System.Drawing.Point(505, 422);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(75, 23);
            this.buttonBuild.TabIndex = 2;
            this.buttonBuild.Text = "Build";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 37);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(79, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Hakpak Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(12, 53);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(255, 20);
            this.textBoxName.TabIndex = 4;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(9, 87);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(104, 13);
            this.labelDescription.TabIndex = 5;
            this.labelDescription.Text = "Hakpak Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(12, 103);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(255, 49);
            this.textBoxDescription.TabIndex = 6;
            // 
            // buttonRemoveFiles
            // 
            this.buttonRemoveFiles.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRemoveFiles.Location = new System.Drawing.Point(411, 422);
            this.buttonRemoveFiles.Name = "buttonRemoveFiles";
            this.buttonRemoveFiles.Size = new System.Drawing.Size(88, 23);
            this.buttonRemoveFiles.TabIndex = 7;
            this.buttonRemoveFiles.Text = "Remove File(s)";
            this.buttonRemoveFiles.UseVisualStyleBackColor = true;
            this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
            // 
            // labelResources
            // 
            this.labelResources.AutoSize = true;
            this.labelResources.Location = new System.Drawing.Point(12, 163);
            this.labelResources.Name = "labelResources";
            this.labelResources.Size = new System.Drawing.Size(56, 13);
            this.labelResources.TabIndex = 8;
            this.labelResources.Text = "Resource:";
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddFiles.Location = new System.Drawing.Point(330, 422);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFiles.TabIndex = 9;
            this.buttonAddFiles.Text = "Add Files...";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // openFileDialogBuilder
            // 
            this.openFileDialogBuilder.Multiselect = true;
            // 
            // backgroundWorkerProcess
            // 
            this.backgroundWorkerProcess.WorkerReportsProgress = true;
            this.backgroundWorkerProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerProcess_DoWork);
            this.backgroundWorkerProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerProcess_ProgressChanged);
            this.backgroundWorkerProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerProcess_RunWorkerCompleted);
            // 
            // progressBarBuild
            // 
            this.progressBarBuild.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarBuild.Location = new System.Drawing.Point(0, 451);
            this.progressBarBuild.Name = "progressBarBuild";
            this.progressBarBuild.Size = new System.Drawing.Size(592, 22);
            this.progressBarBuild.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarBuild.TabIndex = 10;
            // 
            // labelResourceName
            // 
            this.labelResourceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelResourceName.AutoSize = true;
            this.labelResourceName.Location = new System.Drawing.Point(325, 37);
            this.labelResourceName.Name = "labelResourceName";
            this.labelResourceName.Size = new System.Drawing.Size(87, 13);
            this.labelResourceName.TabIndex = 11;
            this.labelResourceName.Text = "Resource Name:";
            // 
            // labelItemPartType
            // 
            this.labelItemPartType.AutoSize = true;
            this.labelItemPartType.Location = new System.Drawing.Point(329, 116);
            this.labelItemPartType.Name = "labelItemPartType";
            this.labelItemPartType.Size = new System.Drawing.Size(56, 13);
            this.labelItemPartType.TabIndex = 12;
            this.labelItemPartType.Text = "Part Type:";
            // 
            // comboBoxItemPartType
            // 
            this.comboBoxItemPartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemPartType.Enabled = false;
            this.comboBoxItemPartType.FormattingEnabled = true;
            this.comboBoxItemPartType.Location = new System.Drawing.Point(330, 132);
            this.comboBoxItemPartType.Name = "comboBoxItemPartType";
            this.comboBoxItemPartType.Size = new System.Drawing.Size(250, 21);
            this.comboBoxItemPartType.TabIndex = 13;
            this.comboBoxItemPartType.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemPartType_SelectedIndexChanged);
            // 
            // textBoxResourceName
            // 
            this.textBoxResourceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResourceName.Enabled = false;
            this.textBoxResourceName.Location = new System.Drawing.Point(328, 53);
            this.textBoxResourceName.MaxLength = 64;
            this.textBoxResourceName.Name = "textBoxResourceName";
            this.textBoxResourceName.Size = new System.Drawing.Size(255, 20);
            this.textBoxResourceName.TabIndex = 14;
            this.textBoxResourceName.TextChanged += new System.EventHandler(this.textBoxResourceName_TextChanged);
            // 
            // radioButton2D
            // 
            this.radioButton2D.AutoSize = true;
            this.radioButton2D.Checked = true;
            this.radioButton2D.Enabled = false;
            this.radioButton2D.Location = new System.Drawing.Point(487, 87);
            this.radioButton2D.Name = "radioButton2D";
            this.radioButton2D.Size = new System.Drawing.Size(39, 17);
            this.radioButton2D.TabIndex = 16;
            this.radioButton2D.TabStop = true;
            this.radioButton2D.Text = "2D";
            this.radioButton2D.UseVisualStyleBackColor = true;
            this.radioButton2D.CheckedChanged += new System.EventHandler(this.radioButton2D_CheckedChanged);
            // 
            // radioButton3D
            // 
            this.radioButton3D.AutoSize = true;
            this.radioButton3D.Enabled = false;
            this.radioButton3D.Location = new System.Drawing.Point(541, 87);
            this.radioButton3D.Name = "radioButton3D";
            this.radioButton3D.Size = new System.Drawing.Size(39, 17);
            this.radioButton3D.TabIndex = 17;
            this.radioButton3D.Text = "3D";
            this.radioButton3D.UseVisualStyleBackColor = true;
            this.radioButton3D.CheckedChanged += new System.EventHandler(this.radioButton3D_CheckedChanged);
            // 
            // labelLinksTo
            // 
            this.labelLinksTo.AutoSize = true;
            this.labelLinksTo.Location = new System.Drawing.Point(325, 163);
            this.labelLinksTo.Name = "labelLinksTo";
            this.labelLinksTo.Size = new System.Drawing.Size(51, 13);
            this.labelLinksTo.TabIndex = 18;
            this.labelLinksTo.Text = "Links To:";
            // 
            // listBoxLinkTo
            // 
            this.listBoxLinkTo.Enabled = false;
            this.listBoxLinkTo.FormattingEnabled = true;
            this.listBoxLinkTo.HorizontalScrollbar = true;
            this.listBoxLinkTo.Location = new System.Drawing.Point(328, 184);
            this.listBoxLinkTo.Name = "listBoxLinkTo";
            this.listBoxLinkTo.Size = new System.Drawing.Size(252, 225);
            this.listBoxLinkTo.TabIndex = 21;
            // 
            // checkBoxIsItem
            // 
            this.checkBoxIsItem.AutoSize = true;
            this.checkBoxIsItem.Enabled = false;
            this.checkBoxIsItem.Location = new System.Drawing.Point(332, 87);
            this.checkBoxIsItem.Name = "checkBoxIsItem";
            this.checkBoxIsItem.Size = new System.Drawing.Size(46, 17);
            this.checkBoxIsItem.TabIndex = 22;
            this.checkBoxIsItem.Text = "Item";
            this.checkBoxIsItem.UseVisualStyleBackColor = true;
            this.checkBoxIsItem.CheckedChanged += new System.EventHandler(this.checkBoxIsItem_CheckedChanged);
            // 
            // HakBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 473);
            this.Controls.Add(this.checkBoxIsItem);
            this.Controls.Add(this.listBoxLinkTo);
            this.Controls.Add(this.labelLinksTo);
            this.Controls.Add(this.radioButton3D);
            this.Controls.Add(this.radioButton2D);
            this.Controls.Add(this.textBoxResourceName);
            this.Controls.Add(this.comboBoxItemPartType);
            this.Controls.Add(this.labelItemPartType);
            this.Controls.Add(this.labelResourceName);
            this.Controls.Add(this.progressBarBuild);
            this.Controls.Add(this.buttonAddFiles);
            this.Controls.Add(this.labelResources);
            this.Controls.Add(this.buttonRemoveFiles);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.listBoxResources);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(295, 482);
            this.Name = "HakBuilder";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hakpak Builder";
            this.Load += new System.EventHandler(this.HakBuilder_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxResources;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBuild;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonRemoveFiles;
        private System.Windows.Forms.Label labelResources;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.OpenFileDialog openFileDialogBuilder;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorkerProcess;
        private System.Windows.Forms.ProgressBar progressBarBuild;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSaveAs;
        private System.Windows.Forms.Label labelResourceName;
        private System.Windows.Forms.Label labelItemPartType;
        private System.Windows.Forms.ComboBox comboBoxItemPartType;
        private System.Windows.Forms.TextBox textBoxResourceName;
        private System.Windows.Forms.RadioButton radioButton2D;
        private System.Windows.Forms.RadioButton radioButton3D;
        private System.Windows.Forms.Label labelLinksTo;
        private System.Windows.Forms.ListBox listBoxLinkTo;
        private System.Windows.Forms.CheckBox checkBoxIsItem;
    }
}