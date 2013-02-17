namespace WinterEngine.Editor.Graphics
{
    partial class ContentPackageEditor
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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildContentPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageTilesets = new System.Windows.Forms.TabPage();
            this.splitContainerTileset = new System.Windows.Forms.SplitContainer();
            this.tilesetDetailsControl = new WinterEngine.Editor.Graphics.TilesetDetailsControl();
            this.panelTilesetProperties = new System.Windows.Forms.Panel();
            this.tilesetSpriteSheetControl1 = new WinterEngine.Editor.Graphics.TilesetSpriteSheetControl();
            this.tabPageCharacters = new System.Windows.Forms.TabPage();
            this.splitContainerCharacters = new System.Windows.Forms.SplitContainer();
            this.panelCharacterSpriteSheet = new System.Windows.Forms.Panel();
            this.panelCharacterProperties = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageTilesets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTileset)).BeginInit();
            this.splitContainerTileset.Panel1.SuspendLayout();
            this.splitContainerTileset.Panel2.SuspendLayout();
            this.splitContainerTileset.SuspendLayout();
            this.panelTilesetProperties.SuspendLayout();
            this.tabPageCharacters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCharacters)).BeginInit();
            this.splitContainerCharacters.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator2,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(100, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildContentPackageToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // buildContentPackageToolStripMenuItem
            // 
            this.buildContentPackageToolStripMenuItem.Name = "buildContentPackageToolStripMenuItem";
            this.buildContentPackageToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.buildContentPackageToolStripMenuItem.Text = "Build Content Package";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageTilesets);
            this.tabControlMain.Controls.Add(this.tabPageCharacters);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(764, 488);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageTilesets
            // 
            this.tabPageTilesets.Controls.Add(this.splitContainerTileset);
            this.tabPageTilesets.Location = new System.Drawing.Point(4, 22);
            this.tabPageTilesets.Name = "tabPageTilesets";
            this.tabPageTilesets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTilesets.Size = new System.Drawing.Size(756, 462);
            this.tabPageTilesets.TabIndex = 1;
            this.tabPageTilesets.Text = "Tilesets";
            this.tabPageTilesets.UseVisualStyleBackColor = true;
            // 
            // splitContainerTileset
            // 
            this.splitContainerTileset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTileset.Location = new System.Drawing.Point(3, 3);
            this.splitContainerTileset.Name = "splitContainerTileset";
            // 
            // splitContainerTileset.Panel1
            // 
            this.splitContainerTileset.Panel1.Controls.Add(this.tilesetDetailsControl);
            // 
            // splitContainerTileset.Panel2
            // 
            this.splitContainerTileset.Panel2.Controls.Add(this.panelTilesetProperties);
            this.splitContainerTileset.Size = new System.Drawing.Size(750, 456);
            this.splitContainerTileset.SplitterDistance = 372;
            this.splitContainerTileset.TabIndex = 0;
            // 
            // tilesetDetailsControl
            // 
            this.tilesetDetailsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tilesetDetailsControl.Location = new System.Drawing.Point(0, 0);
            this.tilesetDetailsControl.Name = "tilesetDetailsControl";
            this.tilesetDetailsControl.Size = new System.Drawing.Size(372, 456);
            this.tilesetDetailsControl.TabIndex = 0;
            // 
            // panelTilesetProperties
            // 
            this.panelTilesetProperties.Controls.Add(this.tilesetSpriteSheetControl1);
            this.panelTilesetProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTilesetProperties.Location = new System.Drawing.Point(0, 0);
            this.panelTilesetProperties.Name = "panelTilesetProperties";
            this.panelTilesetProperties.Size = new System.Drawing.Size(374, 456);
            this.panelTilesetProperties.TabIndex = 0;
            // 
            // tilesetSpriteSheetControl1
            // 
            this.tilesetSpriteSheetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetSpriteSheetControl1.Location = new System.Drawing.Point(0, 0);
            this.tilesetSpriteSheetControl1.Name = "tilesetSpriteSheetControl1";
            this.tilesetSpriteSheetControl1.Size = new System.Drawing.Size(374, 456);
            this.tilesetSpriteSheetControl1.TabIndex = 0;
            // 
            // tabPageCharacters
            // 
            this.tabPageCharacters.Controls.Add(this.splitContainerCharacters);
            this.tabPageCharacters.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharacters.Name = "tabPageCharacters";
            this.tabPageCharacters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCharacters.Size = new System.Drawing.Size(756, 462);
            this.tabPageCharacters.TabIndex = 0;
            this.tabPageCharacters.Text = "Characters";
            this.tabPageCharacters.UseVisualStyleBackColor = true;
            // 
            // splitContainerCharacters
            // 
            this.splitContainerCharacters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCharacters.Location = new System.Drawing.Point(3, 3);
            this.splitContainerCharacters.Name = "splitContainerCharacters";
            this.splitContainerCharacters.Size = new System.Drawing.Size(750, 456);
            this.splitContainerCharacters.SplitterDistance = 335;
            this.splitContainerCharacters.TabIndex = 1;
            // 
            // panelCharacterSpriteSheet
            // 
            this.panelCharacterSpriteSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharacterSpriteSheet.Location = new System.Drawing.Point(0, 0);
            this.panelCharacterSpriteSheet.Name = "panelCharacterSpriteSheet";
            this.panelCharacterSpriteSheet.Size = new System.Drawing.Size(335, 456);
            this.panelCharacterSpriteSheet.TabIndex = 0;
            // 
            // panelCharacterProperties
            // 
            this.panelCharacterProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharacterProperties.Location = new System.Drawing.Point(0, 0);
            this.panelCharacterProperties.Name = "panelCharacterProperties";
            this.panelCharacterProperties.Size = new System.Drawing.Size(411, 456);
            this.panelCharacterProperties.TabIndex = 0;
            // 
            // ContentPackageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 512);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(780, 550);
            this.Name = "ContentPackageEditor";
            this.Text = "Winter Engine - Graphic Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageTilesets.ResumeLayout(false);
            this.splitContainerTileset.Panel1.ResumeLayout(false);
            this.splitContainerTileset.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTileset)).EndInit();
            this.splitContainerTileset.ResumeLayout(false);
            this.panelTilesetProperties.ResumeLayout(false);
            this.tabPageCharacters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCharacters)).EndInit();
            this.splitContainerCharacters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCharacters;
        private System.Windows.Forms.TabPage tabPageTilesets;
        private System.Windows.Forms.SplitContainer splitContainerTileset;
        private System.Windows.Forms.Panel panelTilesetProperties;
        private System.Windows.Forms.SplitContainer splitContainerCharacters;
        private System.Windows.Forms.Panel panelCharacterSpriteSheet;
        private System.Windows.Forms.Panel panelCharacterProperties;
        private TilesetDetailsControl tilesetDetailsControl;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildContentPackageToolStripMenuItem;
        private TilesetSpriteSheetControl tilesetSpriteSheetControl1;
    }
}

