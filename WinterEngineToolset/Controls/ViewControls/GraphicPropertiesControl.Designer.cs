namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class GraphicPropertiesControl
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
            this.buttonDiscardChangesTilesetDetails = new System.Windows.Forms.Button();
            this.buttonApplyChangesTilesetDetails = new System.Windows.Forms.Button();
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageTilesets = new System.Windows.Forms.TabPage();
            this.labelSpriteSheet = new System.Windows.Forms.Label();
            this.comboBoxSpriteSheet = new System.Windows.Forms.ComboBox();
            this.labelTilesetName = new System.Windows.Forms.Label();
            this.textBoxTilesetName = new System.Windows.Forms.TextBox();
            this.buttonDeleteTileset = new System.Windows.Forms.Button();
            this.buttonAddTileset = new System.Windows.Forms.Button();
            this.listBoxTilesets = new System.Windows.Forms.ListBox();
            this.tabPageCharacters = new System.Windows.Forms.TabPage();
            this.tabPagePlaceables = new System.Windows.Forms.TabPage();
            this.tabPageSpriteSheets = new System.Windows.Forms.TabPage();
            this.tabControlProperties.SuspendLayout();
            this.tabPageTilesets.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDiscardChangesTilesetDetails
            // 
            this.buttonDiscardChangesTilesetDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChangesTilesetDetails.Enabled = false;
            this.buttonDiscardChangesTilesetDetails.Location = new System.Drawing.Point(207, 426);
            this.buttonDiscardChangesTilesetDetails.Name = "buttonDiscardChangesTilesetDetails";
            this.buttonDiscardChangesTilesetDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesTilesetDetails.TabIndex = 9;
            this.buttonDiscardChangesTilesetDetails.Text = "Discard Changes";
            this.buttonDiscardChangesTilesetDetails.UseVisualStyleBackColor = true;
            this.buttonDiscardChangesTilesetDetails.Click += new System.EventHandler(this.buttonDiscardChangesTilesetDetails_Click);
            // 
            // buttonApplyChangesTilesetDetails
            // 
            this.buttonApplyChangesTilesetDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonApplyChangesTilesetDetails.Enabled = false;
            this.buttonApplyChangesTilesetDetails.Location = new System.Drawing.Point(73, 426);
            this.buttonApplyChangesTilesetDetails.Name = "buttonApplyChangesTilesetDetails";
            this.buttonApplyChangesTilesetDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChangesTilesetDetails.TabIndex = 8;
            this.buttonApplyChangesTilesetDetails.Text = "Apply Changes";
            this.buttonApplyChangesTilesetDetails.UseVisualStyleBackColor = true;
            this.buttonApplyChangesTilesetDetails.Click += new System.EventHandler(this.buttonApplyChangesTilesetDetails_Click);
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageSpriteSheets);
            this.tabControlProperties.Controls.Add(this.tabPageTilesets);
            this.tabControlProperties.Controls.Add(this.tabPageCharacters);
            this.tabControlProperties.Controls.Add(this.tabPagePlaceables);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 417);
            this.tabControlProperties.TabIndex = 2;
            // 
            // tabPageTilesets
            // 
            this.tabPageTilesets.Controls.Add(this.labelSpriteSheet);
            this.tabPageTilesets.Controls.Add(this.comboBoxSpriteSheet);
            this.tabPageTilesets.Controls.Add(this.labelTilesetName);
            this.tabPageTilesets.Controls.Add(this.textBoxTilesetName);
            this.tabPageTilesets.Controls.Add(this.buttonDeleteTileset);
            this.tabPageTilesets.Controls.Add(this.buttonAddTileset);
            this.tabPageTilesets.Controls.Add(this.listBoxTilesets);
            this.tabPageTilesets.Location = new System.Drawing.Point(4, 22);
            this.tabPageTilesets.Name = "tabPageTilesets";
            this.tabPageTilesets.Size = new System.Drawing.Size(367, 391);
            this.tabPageTilesets.TabIndex = 0;
            this.tabPageTilesets.Text = "Tilesets";
            this.tabPageTilesets.UseVisualStyleBackColor = true;
            // 
            // labelSpriteSheet
            // 
            this.labelSpriteSheet.AutoSize = true;
            this.labelSpriteSheet.Location = new System.Drawing.Point(7, 48);
            this.labelSpriteSheet.Name = "labelSpriteSheet";
            this.labelSpriteSheet.Size = new System.Drawing.Size(68, 13);
            this.labelSpriteSheet.TabIndex = 6;
            this.labelSpriteSheet.Text = "Sprite Sheet:";
            // 
            // comboBoxSpriteSheet
            // 
            this.comboBoxSpriteSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpriteSheet.FormattingEnabled = true;
            this.comboBoxSpriteSheet.Location = new System.Drawing.Point(96, 45);
            this.comboBoxSpriteSheet.Name = "comboBoxSpriteSheet";
            this.comboBoxSpriteSheet.Size = new System.Drawing.Size(239, 21);
            this.comboBoxSpriteSheet.TabIndex = 5;
            // 
            // labelTilesetName
            // 
            this.labelTilesetName.AutoSize = true;
            this.labelTilesetName.Location = new System.Drawing.Point(3, 22);
            this.labelTilesetName.Name = "labelTilesetName";
            this.labelTilesetName.Size = new System.Drawing.Size(72, 13);
            this.labelTilesetName.TabIndex = 4;
            this.labelTilesetName.Text = "Tileset Name:";
            // 
            // textBoxTilesetName
            // 
            this.textBoxTilesetName.Location = new System.Drawing.Point(96, 19);
            this.textBoxTilesetName.Name = "textBoxTilesetName";
            this.textBoxTilesetName.Size = new System.Drawing.Size(239, 20);
            this.textBoxTilesetName.TabIndex = 3;
            // 
            // buttonDeleteTileset
            // 
            this.buttonDeleteTileset.Location = new System.Drawing.Point(186, 241);
            this.buttonDeleteTileset.Name = "buttonDeleteTileset";
            this.buttonDeleteTileset.Size = new System.Drawing.Size(84, 23);
            this.buttonDeleteTileset.TabIndex = 2;
            this.buttonDeleteTileset.Text = "Delete Tileset";
            this.buttonDeleteTileset.UseVisualStyleBackColor = true;
            this.buttonDeleteTileset.Click += new System.EventHandler(this.buttonDeleteTileset_Click);
            // 
            // buttonAddTileset
            // 
            this.buttonAddTileset.Location = new System.Drawing.Point(96, 241);
            this.buttonAddTileset.Name = "buttonAddTileset";
            this.buttonAddTileset.Size = new System.Drawing.Size(75, 23);
            this.buttonAddTileset.TabIndex = 1;
            this.buttonAddTileset.Text = "Add Tileset";
            this.buttonAddTileset.UseVisualStyleBackColor = true;
            this.buttonAddTileset.Click += new System.EventHandler(this.buttonAddTileset_Click);
            // 
            // listBoxTilesets
            // 
            this.listBoxTilesets.FormattingEnabled = true;
            this.listBoxTilesets.Location = new System.Drawing.Point(0, 270);
            this.listBoxTilesets.Name = "listBoxTilesets";
            this.listBoxTilesets.Size = new System.Drawing.Size(364, 121);
            this.listBoxTilesets.TabIndex = 0;
            this.listBoxTilesets.SelectedIndexChanged += new System.EventHandler(this.listBoxTilesets_SelectedIndexChanged);
            // 
            // tabPageCharacters
            // 
            this.tabPageCharacters.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharacters.Name = "tabPageCharacters";
            this.tabPageCharacters.Size = new System.Drawing.Size(367, 391);
            this.tabPageCharacters.TabIndex = 1;
            this.tabPageCharacters.Text = "Characters";
            this.tabPageCharacters.UseVisualStyleBackColor = true;
            // 
            // tabPagePlaceables
            // 
            this.tabPagePlaceables.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceables.Name = "tabPagePlaceables";
            this.tabPagePlaceables.Size = new System.Drawing.Size(367, 391);
            this.tabPagePlaceables.TabIndex = 2;
            this.tabPagePlaceables.Text = "Placeables";
            this.tabPagePlaceables.UseVisualStyleBackColor = true;
            // 
            // tabPageSpriteSheets
            // 
            this.tabPageSpriteSheets.Location = new System.Drawing.Point(4, 22);
            this.tabPageSpriteSheets.Name = "tabPageSpriteSheets";
            this.tabPageSpriteSheets.Size = new System.Drawing.Size(367, 391);
            this.tabPageSpriteSheets.TabIndex = 3;
            this.tabPageSpriteSheets.Text = "Sprite Sheets";
            this.tabPageSpriteSheets.UseVisualStyleBackColor = true;
            // 
            // GraphicPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChangesTilesetDetails);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.buttonApplyChangesTilesetDetails);
            this.Name = "GraphicPropertiesControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageTilesets.ResumeLayout(false);
            this.tabPageTilesets.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDiscardChangesTilesetDetails;
        private System.Windows.Forms.Button buttonApplyChangesTilesetDetails;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageTilesets;
        private System.Windows.Forms.TabPage tabPageCharacters;
        private System.Windows.Forms.TabPage tabPagePlaceables;
        private System.Windows.Forms.Label labelTilesetName;
        private System.Windows.Forms.TextBox textBoxTilesetName;
        private System.Windows.Forms.Button buttonDeleteTileset;
        private System.Windows.Forms.Button buttonAddTileset;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.Label labelSpriteSheet;
        private System.Windows.Forms.ComboBox comboBoxSpriteSheet;
        private System.Windows.Forms.TabPage tabPageSpriteSheets;
    }
}
