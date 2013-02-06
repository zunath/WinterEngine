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
            this.radioButtonTileset = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacter = new System.Windows.Forms.RadioButton();
            this.radioButtonAudio = new System.Windows.Forms.RadioButton();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxResources
            // 
            this.listBoxResources.FormattingEnabled = true;
            this.listBoxResources.HorizontalScrollbar = true;
            this.listBoxResources.Location = new System.Drawing.Point(12, 184);
            this.listBoxResources.Name = "listBoxResources";
            this.listBoxResources.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxResources.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResources.Size = new System.Drawing.Size(375, 199);
            this.listBoxResources.TabIndex = 0;
            this.listBoxResources.SelectedIndexChanged += new System.EventHandler(this.listBoxResources_SelectedIndexChanged);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(411, 24);
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Visible = false;
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(120, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Visible = false;
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(120, 6);
            // 
            // toolStripMenuItemBuild
            // 
            this.toolStripMenuItemBuild.Name = "toolStripMenuItemBuild";
            this.toolStripMenuItemBuild.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItemBuild.Text = "Build";
            this.toolStripMenuItemBuild.Click += new System.EventHandler(this.toolStripMenuItemBuild_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(120, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(247, 422);
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
            this.textBoxName.Location = new System.Drawing.Point(12, 53);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(375, 20);
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
            this.textBoxDescription.Location = new System.Drawing.Point(12, 103);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(375, 49);
            this.textBoxDescription.TabIndex = 6;
            // 
            // buttonRemoveFiles
            // 
            this.buttonRemoveFiles.Location = new System.Drawing.Point(153, 422);
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
            this.buttonAddFiles.Location = new System.Drawing.Point(72, 422);
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
            this.progressBarBuild.Size = new System.Drawing.Size(411, 22);
            this.progressBarBuild.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarBuild.TabIndex = 10;
            // 
            // radioButtonTileset
            // 
            this.radioButtonTileset.AutoSize = true;
            this.radioButtonTileset.Location = new System.Drawing.Point(15, 389);
            this.radioButtonTileset.Name = "radioButtonTileset";
            this.radioButtonTileset.Size = new System.Drawing.Size(56, 17);
            this.radioButtonTileset.TabIndex = 11;
            this.radioButtonTileset.TabStop = true;
            this.radioButtonTileset.Text = "Tileset";
            this.radioButtonTileset.UseVisualStyleBackColor = true;
            // 
            // radioButtonCharacter
            // 
            this.radioButtonCharacter.AutoSize = true;
            this.radioButtonCharacter.Location = new System.Drawing.Point(77, 389);
            this.radioButtonCharacter.Name = "radioButtonCharacter";
            this.radioButtonCharacter.Size = new System.Drawing.Size(71, 17);
            this.radioButtonCharacter.TabIndex = 12;
            this.radioButtonCharacter.TabStop = true;
            this.radioButtonCharacter.Text = "Character";
            this.radioButtonCharacter.UseVisualStyleBackColor = true;
            // 
            // radioButtonAudio
            // 
            this.radioButtonAudio.AutoSize = true;
            this.radioButtonAudio.Location = new System.Drawing.Point(153, 389);
            this.radioButtonAudio.Name = "radioButtonAudio";
            this.radioButtonAudio.Size = new System.Drawing.Size(52, 17);
            this.radioButtonAudio.TabIndex = 13;
            this.radioButtonAudio.TabStop = true;
            this.radioButtonAudio.Text = "Audio";
            this.radioButtonAudio.UseVisualStyleBackColor = true;
            // 
            // HakBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 473);
            this.Controls.Add(this.radioButtonAudio);
            this.Controls.Add(this.radioButtonCharacter);
            this.Controls.Add(this.radioButtonTileset);
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
        private System.Windows.Forms.RadioButton radioButtonTileset;
        private System.Windows.Forms.RadioButton radioButtonCharacter;
        private System.Windows.Forms.RadioButton radioButtonAudio;
    }
}