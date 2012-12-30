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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.modulePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.winterEngineWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageCreatures = new System.Windows.Forms.TabPage();
            this.creatureView = new WinterEngine.Toolset.GUI.Views.CreatureView();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageAreas = new System.Windows.Forms.TabPage();
            this.areaView = new WinterEngine.Toolset.GUI.Views.AreaView();
            this.tabPageItems = new System.Windows.Forms.TabPage();
            this.itemView1 = new WinterEngine.Toolset.GUI.Views.ItemView();
            this.tabPagePlaceables = new System.Windows.Forms.TabPage();
            this.tabPageConversations = new System.Windows.Forms.TabPage();
            this.panelConversationControl = new System.Windows.Forms.Panel();
            this.buttonAddConversationCategory = new System.Windows.Forms.Button();
            this.treeViewConversations = new System.Windows.Forms.TreeView();
            this.tabPageScripts = new System.Windows.Forms.TabPage();
            this.panelScriptControl = new System.Windows.Forms.Panel();
            this.buttonAddScriptCategory = new System.Windows.Forms.Button();
            this.treeViewScripts = new System.Windows.Forms.TreeView();
            this.placeableView = new WinterEngine.Toolset.GUI.Views.PlaceableView();
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
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator3,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.newToolStripMenuItem.Text = "New Module";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openToolStripMenuItem.Text = "Open Module";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.closeToolStripMenuItem.Text = "Close Module";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(144, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Files";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(144, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator5,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.modulePropertiesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(168, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(168, 6);
            // 
            // modulePropertiesToolStripMenuItem
            // 
            this.modulePropertiesToolStripMenuItem.Name = "modulePropertiesToolStripMenuItem";
            this.modulePropertiesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.modulePropertiesToolStripMenuItem.Text = "Module Properties";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winterEngineWebsiteToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // winterEngineWebsiteToolStripMenuItem
            // 
            this.winterEngineWebsiteToolStripMenuItem.Name = "winterEngineWebsiteToolStripMenuItem";
            this.winterEngineWebsiteToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.winterEngineWebsiteToolStripMenuItem.Text = "Winter Engine Website";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.aboutToolStripMenuItem.Text = "About";
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
            this.creatureView.Location = new System.Drawing.Point(0, 7);
            this.creatureView.Name = "creatureView";
            this.creatureView.Size = new System.Drawing.Size(570, 455);
            this.creatureView.TabIndex = 0;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageAreas);
            this.tabControlMain.Controls.Add(this.tabPageCreatures);
            this.tabControlMain.Controls.Add(this.tabPageItems);
            this.tabControlMain.Controls.Add(this.tabPagePlaceables);
            this.tabControlMain.Controls.Add(this.tabPageConversations);
            this.tabControlMain.Controls.Add(this.tabPageScripts);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.areaView.Location = new System.Drawing.Point(0, 7);
            this.areaView.Name = "areaView";
            this.areaView.Size = new System.Drawing.Size(570, 455);
            this.areaView.TabIndex = 0;
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.itemView1);
            this.tabPageItems.Location = new System.Drawing.Point(4, 22);
            this.tabPageItems.Name = "tabPageItems";
            this.tabPageItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItems.Size = new System.Drawing.Size(756, 462);
            this.tabPageItems.TabIndex = 1;
            this.tabPageItems.Text = "Items";
            this.tabPageItems.UseVisualStyleBackColor = true;
            // 
            // itemView1
            // 
            this.itemView1.Location = new System.Drawing.Point(0, 7);
            this.itemView1.Name = "itemView1";
            this.itemView1.Size = new System.Drawing.Size(570, 455);
            this.itemView1.TabIndex = 0;
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
            // placeableView
            // 
            this.placeableView.Location = new System.Drawing.Point(0, 7);
            this.placeableView.Name = "placeableView";
            this.placeableView.Size = new System.Drawing.Size(570, 455);
            this.placeableView.TabIndex = 0;
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
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem modulePropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem winterEngineWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
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
        private GUI.Views.ItemView itemView1;
        private GUI.Views.PlaceableView placeableView;
    }
}

