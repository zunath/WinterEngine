﻿namespace WinterEngine.Network.MasterServer
{
    partial class MasterServerForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonStartMasterServer = new System.Windows.Forms.Button();
            this.panelGlobalControls = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageServers = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxServers = new System.Windows.Forms.ListBox();
            this.panelServerDetails = new System.Windows.Forms.Panel();
            this.listBoxPlayers = new System.Windows.Forms.ListBox();
            this.textBoxMinLevel = new System.Windows.Forms.TextBox();
            this.labelLevelRange = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.labelServerName = new System.Windows.Forms.Label();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.backgroundWorkerNetwork = new System.ComponentModel.BackgroundWorker();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPing = new System.Windows.Forms.Label();
            this.textBoxPing = new System.Windows.Forms.TextBox();
            this.labelLastUpdateTime = new System.Windows.Forms.Label();
            this.textBoxLastUpdateTime = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.panelGlobalControls.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelServerDetails.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // buttonStartMasterServer
            // 
            this.buttonStartMasterServer.Location = new System.Drawing.Point(15, 3);
            this.buttonStartMasterServer.Name = "buttonStartMasterServer";
            this.buttonStartMasterServer.Size = new System.Drawing.Size(146, 23);
            this.buttonStartMasterServer.TabIndex = 3;
            this.buttonStartMasterServer.Text = "Start Master Server";
            this.buttonStartMasterServer.UseVisualStyleBackColor = true;
            this.buttonStartMasterServer.Click += new System.EventHandler(this.buttonStartMasterServer_Click);
            // 
            // panelGlobalControls
            // 
            this.panelGlobalControls.Controls.Add(this.buttonStartMasterServer);
            this.panelGlobalControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGlobalControls.Location = new System.Drawing.Point(0, 481);
            this.panelGlobalControls.Name = "panelGlobalControls";
            this.panelGlobalControls.Size = new System.Drawing.Size(764, 31);
            this.panelGlobalControls.TabIndex = 6;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageServers);
            this.tabControlMain.Controls.Add(this.tabPageUsers);
            this.tabControlMain.Controls.Add(this.tabPageLog);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(764, 457);
            this.tabControlMain.TabIndex = 7;
            // 
            // tabPageServers
            // 
            this.tabPageServers.Controls.Add(this.splitContainer1);
            this.tabPageServers.Location = new System.Drawing.Point(4, 22);
            this.tabPageServers.Name = "tabPageServers";
            this.tabPageServers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServers.Size = new System.Drawing.Size(756, 431);
            this.tabPageServers.TabIndex = 0;
            this.tabPageServers.Text = "Servers";
            this.tabPageServers.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxServers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelServerDetails);
            this.splitContainer1.Size = new System.Drawing.Size(750, 425);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxServers
            // 
            this.listBoxServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxServers.FormattingEnabled = true;
            this.listBoxServers.Location = new System.Drawing.Point(0, 0);
            this.listBoxServers.Name = "listBoxServers";
            this.listBoxServers.Size = new System.Drawing.Size(286, 425);
            this.listBoxServers.TabIndex = 0;
            // 
            // panelServerDetails
            // 
            this.panelServerDetails.Controls.Add(this.textBoxLastUpdateTime);
            this.panelServerDetails.Controls.Add(this.labelLastUpdateTime);
            this.panelServerDetails.Controls.Add(this.textBoxPing);
            this.panelServerDetails.Controls.Add(this.labelPing);
            this.panelServerDetails.Controls.Add(this.textBoxPort);
            this.panelServerDetails.Controls.Add(this.labelPort);
            this.panelServerDetails.Controls.Add(this.listBoxPlayers);
            this.panelServerDetails.Controls.Add(this.textBoxMinLevel);
            this.panelServerDetails.Controls.Add(this.labelLevelRange);
            this.panelServerDetails.Controls.Add(this.textBoxDescription);
            this.panelServerDetails.Controls.Add(this.labelDescription);
            this.panelServerDetails.Controls.Add(this.textBoxServerIPAddress);
            this.panelServerDetails.Controls.Add(this.labelServerIP);
            this.panelServerDetails.Controls.Add(this.textBoxServerName);
            this.panelServerDetails.Controls.Add(this.labelServerName);
            this.panelServerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelServerDetails.Location = new System.Drawing.Point(0, 0);
            this.panelServerDetails.Name = "panelServerDetails";
            this.panelServerDetails.Size = new System.Drawing.Size(460, 425);
            this.panelServerDetails.TabIndex = 0;
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.Location = new System.Drawing.Point(3, 220);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.Size = new System.Drawing.Size(457, 199);
            this.listBoxPlayers.TabIndex = 10;
            // 
            // textBoxMinLevel
            // 
            this.textBoxMinLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMinLevel.Location = new System.Drawing.Point(81, 168);
            this.textBoxMinLevel.Name = "textBoxMinLevel";
            this.textBoxMinLevel.ReadOnly = true;
            this.textBoxMinLevel.Size = new System.Drawing.Size(374, 20);
            this.textBoxMinLevel.TabIndex = 7;
            // 
            // labelLevelRange
            // 
            this.labelLevelRange.AutoSize = true;
            this.labelLevelRange.Location = new System.Drawing.Point(4, 171);
            this.labelLevelRange.Name = "labelLevelRange";
            this.labelLevelRange.Size = new System.Drawing.Size(71, 13);
            this.labelLevelRange.TabIndex = 6;
            this.labelLevelRange.Text = "Level Range:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(81, 70);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(374, 92);
            this.textBoxDescription.TabIndex = 5;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 112);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxServerIPAddress
            // 
            this.textBoxServerIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerIPAddress.Location = new System.Drawing.Point(81, 43);
            this.textBoxServerIPAddress.Name = "textBoxServerIPAddress";
            this.textBoxServerIPAddress.ReadOnly = true;
            this.textBoxServerIPAddress.Size = new System.Drawing.Size(262, 20);
            this.textBoxServerIPAddress.TabIndex = 3;
            // 
            // labelServerIP
            // 
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Location = new System.Drawing.Point(14, 46);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(61, 13);
            this.labelServerIP.TabIndex = 2;
            this.labelServerIP.Text = "IP Address:";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerName.Location = new System.Drawing.Point(81, 16);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.ReadOnly = true;
            this.textBoxServerName.Size = new System.Drawing.Size(374, 20);
            this.textBoxServerName.TabIndex = 1;
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(3, 19);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(72, 13);
            this.labelServerName.TabIndex = 0;
            this.labelServerName.Text = "Server Name:";
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Location = new System.Drawing.Point(4, 22);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsers.Size = new System.Drawing.Size(756, 431);
            this.tabPageUsers.TabIndex = 1;
            this.tabPageUsers.Text = "Users";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(756, 431);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.AcceptsReturn = true;
            this.textBoxLog.AcceptsTab = true;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(756, 431);
            this.textBoxLog.TabIndex = 0;
            // 
            // backgroundWorkerNetwork
            // 
            this.backgroundWorkerNetwork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerNetwork_DoWork);
            this.backgroundWorkerNetwork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerNetwork_RunWorkerCompleted);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(349, 46);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 11;
            this.labelPort.Text = "Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPort.Location = new System.Drawing.Point(384, 44);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.ReadOnly = true;
            this.textBoxPort.Size = new System.Drawing.Size(71, 20);
            this.textBoxPort.TabIndex = 12;
            // 
            // labelPing
            // 
            this.labelPing.AutoSize = true;
            this.labelPing.Location = new System.Drawing.Point(44, 197);
            this.labelPing.Name = "labelPing";
            this.labelPing.Size = new System.Drawing.Size(31, 13);
            this.labelPing.TabIndex = 13;
            this.labelPing.Text = "Ping:";
            // 
            // textBoxPing
            // 
            this.textBoxPing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPing.Location = new System.Drawing.Point(81, 194);
            this.textBoxPing.Name = "textBoxPing";
            this.textBoxPing.ReadOnly = true;
            this.textBoxPing.Size = new System.Drawing.Size(98, 20);
            this.textBoxPing.TabIndex = 14;
            // 
            // labelLastUpdateTime
            // 
            this.labelLastUpdateTime.AutoSize = true;
            this.labelLastUpdateTime.Location = new System.Drawing.Point(196, 197);
            this.labelLastUpdateTime.Name = "labelLastUpdateTime";
            this.labelLastUpdateTime.Size = new System.Drawing.Size(68, 13);
            this.labelLastUpdateTime.TabIndex = 15;
            this.labelLastUpdateTime.Text = "Last Update:";
            // 
            // textBoxLastUpdateTime
            // 
            this.textBoxLastUpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLastUpdateTime.Location = new System.Drawing.Point(270, 194);
            this.textBoxLastUpdateTime.Name = "textBoxLastUpdateTime";
            this.textBoxLastUpdateTime.ReadOnly = true;
            this.textBoxLastUpdateTime.Size = new System.Drawing.Size(185, 20);
            this.textBoxLastUpdateTime.TabIndex = 16;
            // 
            // MasterServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 512);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelGlobalControls);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(780, 550);
            this.Name = "MasterServerForm";
            this.Text = "Winter Engine - Master Server";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelGlobalControls.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageServers.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelServerDetails.ResumeLayout(false);
            this.panelServerDetails.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button buttonStartMasterServer;
        private System.Windows.Forms.Panel panelGlobalControls;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageServers;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxServers;
        private System.Windows.Forms.Panel panelServerDetails;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.ComponentModel.BackgroundWorker backgroundWorkerNetwork;
        private System.Windows.Forms.TextBox textBoxMinLevel;
        private System.Windows.Forms.Label labelLevelRange;
        private System.Windows.Forms.ListBox listBoxPlayers;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPing;
        private System.Windows.Forms.Label labelPing;
        private System.Windows.Forms.TextBox textBoxLastUpdateTime;
        private System.Windows.Forms.Label labelLastUpdateTime;
    }
}

