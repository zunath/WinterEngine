namespace WinterEngine.Toolset
{
    partial class MainForm
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
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemManageHakPaks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHakpakBuilder = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageCreatures = new System.Windows.Forms.TabPage();
            this.creatureView = new WinterEngine.Toolset.GUI.Views.CreatureView();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageAreas = new System.Windows.Forms.TabPage();
            this.areaView = new WinterEngine.Toolset.GUI.Views.AreaView();
            this.tabPageItems = new System.Windows.Forms.TabPage();
            this.itemView = new WinterEngine.Toolset.GUI.Views.ItemView();
            this.tabPagePlaceables = new System.Windows.Forms.TabPage();
            this.placeableView = new WinterEngine.Toolset.GUI.Views.PlaceableView();
            this.tabPageConversations = new System.Windows.Forms.TabPage();
            this.panelConversationControl = new System.Windows.Forms.Panel();
            this.buttonAddConversationCategory = new System.Windows.Forms.Button();
            this.treeViewConversations = new System.Windows.Forms.TreeView();
            this.tabPageScripts = new System.Windows.Forms.TabPage();
            this.panelScriptControl = new System.Windows.Forms.Panel();
            this.buttonAddScriptCategory = new System.Windows.Forms.Button();
            this.treeViewScripts = new System.Windows.Forms.TreeView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStripMain.SuspendLayout();
            this.tabPageCreatures.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageAreas.SuspendLayout();
            this.tabPageItems.SuspendLayout();
            this.tabPagePlaceables.SuspendLayout();
            this.tabPageConversations.SuspendLayout();
            this.tabPageScripts.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.contentToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(764, 24);
            this.menuStripMain.TabIndex = 0;
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
            // 
            // toolStripMenuItemExportERF
            // 
            this.toolStripMenuItemExportERF.Enabled = false;
            this.toolStripMenuItemExportERF.Name = "toolStripMenuItemExportERF";
            this.toolStripMenuItemExportERF.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItemExportERF.Text = "Export";
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
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
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
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemManageHakPaks,
            this.toolStripMenuItemHakpakBuilder});
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.contentToolStripMenuItem.Text = "Content";
            // 
            // toolStripMenuItemManageHakPaks
            // 
            this.toolStripMenuItemManageHakPaks.Name = "toolStripMenuItemManageHakPaks";
            this.toolStripMenuItemManageHakPaks.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemManageHakPaks.Text = "Manage Hakpaks";
            // 
            // toolStripMenuItemHakpakBuilder
            // 
            this.toolStripMenuItemHakpakBuilder.Name = "toolStripMenuItemHakpakBuilder";
            this.toolStripMenuItemHakpakBuilder.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemHakpakBuilder.Text = "Hakpak Builder";
            this.toolStripMenuItemHakpakBuilder.Click += new System.EventHandler(this.toolStripMenuItemHakpakBuilder_Click);
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
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItemAbout.Text = "About";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // tabPageCreatures
            // 
            this.tabPageCreatures.Controls.Add(this.creatureView);
            this.tabPageCreatures.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatures.Name = "tabPageCreatures";
            this.tabPageCreatures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreatures.Size = new System.Drawing.Size(756, 462);
            this.tabPageCreatures.TabIndex = 0;
            this.tabPageCreatures.Text = "Creatures";
            this.tabPageCreatures.UseVisualStyleBackColor = true;
            // 
            // creatureView
            // 
            this.creatureView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.creatureView.Location = new System.Drawing.Point(0, 7);
            this.creatureView.Name = "creatureView";
            this.creatureView.Size = new System.Drawing.Size(570, 455);
            this.creatureView.TabIndex = 0;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageAreas);
            this.tabControlMain.Controls.Add(this.tabPageCreatures);
            this.tabControlMain.Controls.Add(this.tabPageItems);
            this.tabControlMain.Controls.Add(this.tabPagePlaceables);
            this.tabControlMain.Controls.Add(this.tabPageConversations);
            this.tabControlMain.Controls.Add(this.tabPageScripts);
            this.tabControlMain.Enabled = false;
            this.tabControlMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(764, 488);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageAreas
            // 
            this.tabPageAreas.Controls.Add(this.areaView);
            this.tabPageAreas.Location = new System.Drawing.Point(4, 22);
            this.tabPageAreas.Name = "tabPageAreas";
            this.tabPageAreas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAreas.Size = new System.Drawing.Size(756, 462);
            this.tabPageAreas.TabIndex = 0;
            this.tabPageAreas.Text = "Areas";
            this.tabPageAreas.UseVisualStyleBackColor = true;
            // 
            // areaView
            // 
            this.areaView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.areaView.Location = new System.Drawing.Point(0, 7);
            this.areaView.Name = "areaView";
            this.areaView.Size = new System.Drawing.Size(570, 455);
            this.areaView.TabIndex = 0;
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.itemView);
            this.tabPageItems.Location = new System.Drawing.Point(4, 22);
            this.tabPageItems.Name = "tabPageItems";
            this.tabPageItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItems.Size = new System.Drawing.Size(756, 462);
            this.tabPageItems.TabIndex = 1;
            this.tabPageItems.Text = "Items";
            this.tabPageItems.UseVisualStyleBackColor = true;
            // 
            // itemView
            // 
            this.itemView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemView.Location = new System.Drawing.Point(0, 7);
            this.itemView.Name = "itemView";
            this.itemView.Size = new System.Drawing.Size(570, 455);
            this.itemView.TabIndex = 0;
            // 
            // tabPagePlaceables
            // 
            this.tabPagePlaceables.Controls.Add(this.placeableView);
            this.tabPagePlaceables.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceables.Name = "tabPagePlaceables";
            this.tabPagePlaceables.Size = new System.Drawing.Size(756, 462);
            this.tabPagePlaceables.TabIndex = 2;
            this.tabPagePlaceables.Text = "Placeables";
            this.tabPagePlaceables.UseVisualStyleBackColor = true;
            // 
            // placeableView
            // 
            this.placeableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.placeableView.Location = new System.Drawing.Point(0, 7);
            this.placeableView.Name = "placeableView";
            this.placeableView.Size = new System.Drawing.Size(570, 455);
            this.placeableView.TabIndex = 0;
            // 
            // tabPageConversations
            // 
            this.tabPageConversations.Controls.Add(this.panelConversationControl);
            this.tabPageConversations.Controls.Add(this.buttonAddConversationCategory);
            this.tabPageConversations.Controls.Add(this.treeViewConversations);
            this.tabPageConversations.Location = new System.Drawing.Point(4, 22);
            this.tabPageConversations.Name = "tabPageConversations";
            this.tabPageConversations.Size = new System.Drawing.Size(756, 462);
            this.tabPageConversations.TabIndex = 3;
            this.tabPageConversations.Text = "Conversations";
            this.tabPageConversations.UseVisualStyleBackColor = true;
            // 
            // panelConversationControl
            // 
            this.panelConversationControl.Location = new System.Drawing.Point(198, 6);
            this.panelConversationControl.Name = "panelConversationControl";
            this.panelConversationControl.Size = new System.Drawing.Size(378, 445);
            this.panelConversationControl.TabIndex = 8;
            // 
            // buttonAddConversationCategory
            // 
            this.buttonAddConversationCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddConversationCategory.Location = new System.Drawing.Point(8, 428);
            this.buttonAddConversationCategory.Name = "buttonAddConversationCategory";
            this.buttonAddConversationCategory.Size = new System.Drawing.Size(184, 23);
            this.buttonAddConversationCategory.TabIndex = 5;
            this.buttonAddConversationCategory.Text = "Add Category";
            this.buttonAddConversationCategory.UseVisualStyleBackColor = true;
            // 
            // treeViewConversations
            // 
            this.treeViewConversations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewConversations.Location = new System.Drawing.Point(6, 6);
            this.treeViewConversations.Name = "treeViewConversations";
            this.treeViewConversations.Size = new System.Drawing.Size(186, 416);
            this.treeViewConversations.TabIndex = 4;
            // 
            // tabPageScripts
            // 
            this.tabPageScripts.Controls.Add(this.panelScriptControl);
            this.tabPageScripts.Controls.Add(this.buttonAddScriptCategory);
            this.tabPageScripts.Controls.Add(this.treeViewScripts);
            this.tabPageScripts.Location = new System.Drawing.Point(4, 22);
            this.tabPageScripts.Name = "tabPageScripts";
            this.tabPageScripts.Size = new System.Drawing.Size(756, 462);
            this.tabPageScripts.TabIndex = 4;
            this.tabPageScripts.Text = "Scripts";
            this.tabPageScripts.UseVisualStyleBackColor = true;
            // 
            // panelScriptControl
            // 
            this.panelScriptControl.Location = new System.Drawing.Point(198, 6);
            this.panelScriptControl.Name = "panelScriptControl";
            this.panelScriptControl.Size = new System.Drawing.Size(378, 445);
            this.panelScriptControl.TabIndex = 8;
            // 
            // buttonAddScriptCategory
            // 
            this.buttonAddScriptCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddScriptCategory.Location = new System.Drawing.Point(8, 428);
            this.buttonAddScriptCategory.Name = "buttonAddScriptCategory";
            this.buttonAddScriptCategory.Size = new System.Drawing.Size(184, 23);
            this.buttonAddScriptCategory.TabIndex = 5;
            this.buttonAddScriptCategory.Text = "Add Category";
            this.buttonAddScriptCategory.UseVisualStyleBackColor = true;
            // 
            // treeViewScripts
            // 
            this.treeViewScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewScripts.Location = new System.Drawing.Point(6, 6);
            this.treeViewScripts.Name = "treeViewScripts";
            this.treeViewScripts.Size = new System.Drawing.Size(186, 416);
            this.treeViewScripts.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 512);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(780, 550);
            this.Name = "MainForm";
            this.Text = "Winter Engine Toolset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tabPageCreatures.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageAreas.ResumeLayout(false);
            this.tabPageItems.ResumeLayout(false);
            this.tabPagePlaceables.ResumeLayout(false);
            this.tabPageConversations.ResumeLayout(false);
            this.tabPageScripts.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWebsite;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageCreatures;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageAreas;
        private System.Windows.Forms.TabPage tabPageItems;
        private System.Windows.Forms.TabPage tabPagePlaceables;
        private System.Windows.Forms.TabPage tabPageConversations;
        private System.Windows.Forms.TabPage tabPageScripts;
        private System.Windows.Forms.Button buttonAddConversationCategory;
        private System.Windows.Forms.TreeView treeViewConversations;
        private System.Windows.Forms.Button buttonAddScriptCategory;
        private System.Windows.Forms.TreeView treeViewScripts;
        private System.Windows.Forms.Panel panelScriptControl;
        private System.Windows.Forms.Panel panelConversationControl;
        private GUI.Views.AreaView areaView;
        private GUI.Views.CreatureView creatureView;
        private GUI.Views.ItemView itemView;
        private GUI.Views.PlaceableView placeableView;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManageHakPaks;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHakpakBuilder;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

