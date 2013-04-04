namespace WinterEngine.Editor.Controls
{
    partial class MenuBarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAsModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemImportERF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExportERF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemModuleProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemContentPackageCreator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemManageContentPackages = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.contentToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(327, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewModule,
            this.toolStripMenuItemOpenModule,
            this.toolStripMenuItemCloseModule,
            this.toolStripSeparator1,
            this.toolStripMenuItemSaveModule,
            this.toolStripMenuItemSaveAsModule,
            this.toolStripSeparator2,
            this.toolStripMenuItemImportERF,
            this.toolStripMenuItemExportERF,
            this.toolStripSeparator3,
            this.toolStripMenuItemRecentFiles,
            this.toolStripSeparator4,
            this.toolStripMenuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItemNewModule
            // 
            this.toolStripMenuItemNewModule.Name = "toolStripMenuItemNewModule";
            this.toolStripMenuItemNewModule.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemNewModule.Text = "New Module";
            this.toolStripMenuItemNewModule.Click += new System.EventHandler(this.toolStripMenuItemNewModule_Click);
            // 
            // toolStripMenuItemOpenModule
            // 
            this.toolStripMenuItemOpenModule.Name = "toolStripMenuItemOpenModule";
            this.toolStripMenuItemOpenModule.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemOpenModule.Text = "Open Module";
            this.toolStripMenuItemOpenModule.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItemCloseModule
            // 
            this.toolStripMenuItemCloseModule.Enabled = false;
            this.toolStripMenuItemCloseModule.Name = "toolStripMenuItemCloseModule";
            this.toolStripMenuItemCloseModule.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemCloseModule.Text = "Close Module";
            this.toolStripMenuItemCloseModule.Click += new System.EventHandler(this.toolStripMenuItemCloseModule_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // toolStripMenuItemSaveModule
            // 
            this.toolStripMenuItemSaveModule.Enabled = false;
            this.toolStripMenuItemSaveModule.Name = "toolStripMenuItemSaveModule";
            this.toolStripMenuItemSaveModule.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemSaveModule.Text = "Save";
            this.toolStripMenuItemSaveModule.Click += new System.EventHandler(this.toolStripMenuItemSaveModule_Click);
            // 
            // toolStripMenuItemSaveAsModule
            // 
            this.toolStripMenuItemSaveAsModule.Enabled = false;
            this.toolStripMenuItemSaveAsModule.Name = "toolStripMenuItemSaveAsModule";
            this.toolStripMenuItemSaveAsModule.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemSaveAsModule.Text = "Save As...";
            this.toolStripMenuItemSaveAsModule.Click += new System.EventHandler(this.toolStripMenuItemSaveAsModule_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
            // 
            // toolStripMenuItemImportERF
            // 
            this.toolStripMenuItemImportERF.Enabled = false;
            this.toolStripMenuItemImportERF.Name = "toolStripMenuItemImportERF";
            this.toolStripMenuItemImportERF.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemImportERF.Text = "Import";
            this.toolStripMenuItemImportERF.Click += new System.EventHandler(this.toolStripMenuItemImportERF_Click);
            // 
            // toolStripMenuItemExportERF
            // 
            this.toolStripMenuItemExportERF.Enabled = false;
            this.toolStripMenuItemExportERF.Name = "toolStripMenuItemExportERF";
            this.toolStripMenuItemExportERF.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemExportERF.Text = "Export";
            this.toolStripMenuItemExportERF.Click += new System.EventHandler(this.toolStripMenuItemExportERF_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(144, 6);
            // 
            // toolStripMenuItemRecentFiles
            // 
            this.toolStripMenuItemRecentFiles.Name = "toolStripMenuItemRecentFiles";
            this.toolStripMenuItemRecentFiles.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemRecentFiles.Text = "Recent Files";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(144, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemUndo,
            this.toolStripMenuItemRedo,
            this.toolStripSeparator5,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemCut,
            this.toolStripMenuItemPaste,
            this.toolStripSeparator6,
            this.toolStripMenuItemModuleProperties});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStripMenuItemUndo
            // 
            this.toolStripMenuItemUndo.Enabled = false;
            this.toolStripMenuItemUndo.Name = "toolStripMenuItemUndo";
            this.toolStripMenuItemUndo.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemUndo.Text = "Undo";
            // 
            // toolStripMenuItemRedo
            // 
            this.toolStripMenuItemRedo.Enabled = false;
            this.toolStripMenuItemRedo.Name = "toolStripMenuItemRedo";
            this.toolStripMenuItemRedo.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemRedo.Text = "Redo";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(168, 6);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Enabled = false;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
            // 
            // toolStripMenuItemCut
            // 
            this.toolStripMenuItemCut.Enabled = false;
            this.toolStripMenuItemCut.Name = "toolStripMenuItemCut";
            this.toolStripMenuItemCut.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemCut.Text = "Cut";
            // 
            // toolStripMenuItemPaste
            // 
            this.toolStripMenuItemPaste.Enabled = false;
            this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
            this.toolStripMenuItemPaste.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemPaste.Text = "Paste";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(168, 6);
            // 
            // toolStripMenuItemModuleProperties
            // 
            this.toolStripMenuItemModuleProperties.Enabled = false;
            this.toolStripMenuItemModuleProperties.Name = "toolStripMenuItemModuleProperties";
            this.toolStripMenuItemModuleProperties.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemModuleProperties.Text = "Module Properties";
            this.toolStripMenuItemModuleProperties.Click += new System.EventHandler(this.toolStripMenuItemModuleProperties_Click);
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemContentPackageCreator,
            this.toolStripMenuItemManageContentPackages});
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.contentToolStripMenuItem.Text = "Content";
            // 
            // toolStripMenuItemContentPackageCreator
            // 
            this.toolStripMenuItemContentPackageCreator.Name = "toolStripMenuItemContentPackageCreator";
            this.toolStripMenuItemContentPackageCreator.Size = new System.Drawing.Size(215, 22);
            this.toolStripMenuItemContentPackageCreator.Text = "Content Package Creator";
            this.toolStripMenuItemContentPackageCreator.Click += new System.EventHandler(this.toolStripMenuItemContentBuilder_Click);
            // 
            // toolStripMenuItemManageContentPackages
            // 
            this.toolStripMenuItemManageContentPackages.Name = "toolStripMenuItemManageContentPackages";
            this.toolStripMenuItemManageContentPackages.Size = new System.Drawing.Size(215, 22);
            this.toolStripMenuItemManageContentPackages.Text = "Manage Content Packages";
            this.toolStripMenuItemManageContentPackages.Click += new System.EventHandler(this.toolStripMenuItemManageContentPackages_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWebsite,
            this.toolStripMenuItemAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMenuItemWebsite
            // 
            this.toolStripMenuItemWebsite.Name = "toolStripMenuItemWebsite";
            this.toolStripMenuItemWebsite.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItemWebsite.Text = "Winter Engine Website";
            this.toolStripMenuItemWebsite.Click += new System.EventHandler(this.toolStripMenuItemWebsite_Click);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItemAbout.Text = "About";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // MenuBarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStripMain);
            this.Name = "MenuBarControl";
            this.Size = new System.Drawing.Size(327, 26);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewModule;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenModule;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseModule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveModule;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveAsModule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImportERF;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExportERF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUndo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemModuleProperties;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContentPackageCreator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManageContentPackages;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWebsite;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
