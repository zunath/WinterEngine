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
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageTilesets = new System.Windows.Forms.TabPage();
            this.buttonDeleteTileset = new System.Windows.Forms.Button();
            this.buttonNewTileset = new System.Windows.Forms.Button();
            this.listBoxTilesets = new System.Windows.Forms.ListBox();
            this.textBoxTilesetName = new WinterEngine.Toolset.Controls.GenericControls.NameTextBox();
            this.labelTilesetHeader = new System.Windows.Forms.Label();
            this.labelSpriteSheet = new System.Windows.Forms.Label();
            this.comboBoxSpriteSheet = new System.Windows.Forms.ComboBox();
            this.labelTilesetName = new System.Windows.Forms.Label();
            this.tabPageCharacters = new System.Windows.Forms.TabPage();
            this.listBoxCharacters = new System.Windows.Forms.ListBox();
            this.tabPagePlaceables = new System.Windows.Forms.TabPage();
            this.listBoxPlaceables = new System.Windows.Forms.ListBox();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.tabControlProperties.SuspendLayout();
            this.tabPageTilesets.SuspendLayout();
            this.tabPageCharacters.SuspendLayout();
            this.tabPagePlaceables.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageTilesets);
            this.tabControlProperties.Controls.Add(this.tabPageCharacters);
            this.tabControlProperties.Controls.Add(this.tabPagePlaceables);
            this.tabControlProperties.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(302, 417);
            this.tabControlProperties.TabIndex = 2;
            // 
            // tabPageTilesets
            // 
            this.tabPageTilesets.Controls.Add(this.buttonDeleteTileset);
            this.tabPageTilesets.Controls.Add(this.buttonNewTileset);
            this.tabPageTilesets.Controls.Add(this.listBoxTilesets);
            this.tabPageTilesets.Controls.Add(this.textBoxTilesetName);
            this.tabPageTilesets.Controls.Add(this.labelTilesetHeader);
            this.tabPageTilesets.Controls.Add(this.labelSpriteSheet);
            this.tabPageTilesets.Controls.Add(this.comboBoxSpriteSheet);
            this.tabPageTilesets.Controls.Add(this.labelTilesetName);
            this.tabPageTilesets.Location = new System.Drawing.Point(4, 22);
            this.tabPageTilesets.Name = "tabPageTilesets";
            this.tabPageTilesets.Size = new System.Drawing.Size(294, 391);
            this.tabPageTilesets.TabIndex = 0;
            this.tabPageTilesets.Text = "Tilesets";
            this.tabPageTilesets.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteTileset
            // 
            this.buttonDeleteTileset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDeleteTileset.Location = new System.Drawing.Point(148, 193);
            this.buttonDeleteTileset.Name = "buttonDeleteTileset";
            this.buttonDeleteTileset.Size = new System.Drawing.Size(81, 23);
            this.buttonDeleteTileset.TabIndex = 15;
            this.buttonDeleteTileset.Text = "Delete Tileset";
            this.buttonDeleteTileset.UseVisualStyleBackColor = true;
            this.buttonDeleteTileset.Click += new System.EventHandler(this.buttonDeleteTileset_Click);
            // 
            // buttonNewTileset
            // 
            this.buttonNewTileset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonNewTileset.Location = new System.Drawing.Point(67, 193);
            this.buttonNewTileset.Name = "buttonNewTileset";
            this.buttonNewTileset.Size = new System.Drawing.Size(75, 23);
            this.buttonNewTileset.TabIndex = 14;
            this.buttonNewTileset.Text = "New Tileset";
            this.buttonNewTileset.UseVisualStyleBackColor = true;
            this.buttonNewTileset.Click += new System.EventHandler(this.buttonNewTileset_Click);
            // 
            // listBoxTilesets
            // 
            this.listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTilesets.FormattingEnabled = true;
            this.listBoxTilesets.Location = new System.Drawing.Point(0, 222);
            this.listBoxTilesets.Name = "listBoxTilesets";
            this.listBoxTilesets.Size = new System.Drawing.Size(294, 173);
            this.listBoxTilesets.TabIndex = 13;
            this.listBoxTilesets.SelectedValueChanged += new System.EventHandler(this.listBoxTilesets_SelectedValueChanged);
            // 
            // textBoxTilesetName
            // 
            this.textBoxTilesetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTilesetName.Enabled = false;
            this.textBoxTilesetName.IsValid = false;
            this.textBoxTilesetName.Location = new System.Drawing.Point(81, 58);
            this.textBoxTilesetName.Name = "textBoxTilesetName";
            this.textBoxTilesetName.NameText = "";
            this.textBoxTilesetName.Size = new System.Drawing.Size(216, 28);
            this.textBoxTilesetName.TabIndex = 12;
            // 
            // labelTilesetHeader
            // 
            this.labelTilesetHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTilesetHeader.AutoSize = true;
            this.labelTilesetHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTilesetHeader.Location = new System.Drawing.Point(60, 19);
            this.labelTilesetHeader.Name = "labelTilesetHeader";
            this.labelTilesetHeader.Size = new System.Drawing.Size(186, 31);
            this.labelTilesetHeader.TabIndex = 11;
            this.labelTilesetHeader.Text = "Tileset Details";
            // 
            // labelSpriteSheet
            // 
            this.labelSpriteSheet.AutoSize = true;
            this.labelSpriteSheet.Location = new System.Drawing.Point(6, 95);
            this.labelSpriteSheet.Name = "labelSpriteSheet";
            this.labelSpriteSheet.Size = new System.Drawing.Size(68, 13);
            this.labelSpriteSheet.TabIndex = 10;
            this.labelSpriteSheet.Text = "Sprite Sheet:";
            // 
            // comboBoxSpriteSheet
            // 
            this.comboBoxSpriteSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSpriteSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpriteSheet.Enabled = false;
            this.comboBoxSpriteSheet.FormattingEnabled = true;
            this.comboBoxSpriteSheet.Location = new System.Drawing.Point(80, 92);
            this.comboBoxSpriteSheet.Name = "comboBoxSpriteSheet";
            this.comboBoxSpriteSheet.Size = new System.Drawing.Size(183, 21);
            this.comboBoxSpriteSheet.TabIndex = 9;
            this.comboBoxSpriteSheet.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpriteSheet_SelectedIndexChanged);
            // 
            // labelTilesetName
            // 
            this.labelTilesetName.AutoSize = true;
            this.labelTilesetName.Location = new System.Drawing.Point(6, 68);
            this.labelTilesetName.Name = "labelTilesetName";
            this.labelTilesetName.Size = new System.Drawing.Size(72, 13);
            this.labelTilesetName.TabIndex = 4;
            this.labelTilesetName.Text = "Tileset Name:";
            // 
            // tabPageCharacters
            // 
            this.tabPageCharacters.Controls.Add(this.listBoxCharacters);
            this.tabPageCharacters.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharacters.Name = "tabPageCharacters";
            this.tabPageCharacters.Size = new System.Drawing.Size(294, 391);
            this.tabPageCharacters.TabIndex = 1;
            this.tabPageCharacters.Text = "Characters";
            this.tabPageCharacters.UseVisualStyleBackColor = true;
            // 
            // listBoxCharacters
            // 
            this.listBoxCharacters.FormattingEnabled = true;
            this.listBoxCharacters.Location = new System.Drawing.Point(0, 222);
            this.listBoxCharacters.Name = "listBoxCharacters";
            this.listBoxCharacters.Size = new System.Drawing.Size(294, 173);
            this.listBoxCharacters.TabIndex = 14;
            // 
            // tabPagePlaceables
            // 
            this.tabPagePlaceables.Controls.Add(this.listBoxPlaceables);
            this.tabPagePlaceables.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceables.Name = "tabPagePlaceables";
            this.tabPagePlaceables.Size = new System.Drawing.Size(294, 391);
            this.tabPagePlaceables.TabIndex = 2;
            this.tabPagePlaceables.Text = "Placeables";
            this.tabPagePlaceables.UseVisualStyleBackColor = true;
            // 
            // listBoxPlaceables
            // 
            this.listBoxPlaceables.FormattingEnabled = true;
            this.listBoxPlaceables.Location = new System.Drawing.Point(-3, 222);
            this.listBoxPlaceables.Name = "listBoxPlaceables";
            this.listBoxPlaceables.Size = new System.Drawing.Size(294, 173);
            this.listBoxPlaceables.TabIndex = 14;
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(151, 426);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChanges.TabIndex = 15;
            this.buttonDiscardChanges.Text = "Discard Changes";
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            this.buttonDiscardChanges.Click += new System.EventHandler(this.buttonDiscardChanges_Click);
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonApplyChanges.Location = new System.Drawing.Point(35, 426);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChanges.TabIndex = 14;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonApplyChanges_Click);
            // 
            // GraphicPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "GraphicPropertiesControl";
            this.Size = new System.Drawing.Size(308, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageTilesets.ResumeLayout(false);
            this.tabPageTilesets.PerformLayout();
            this.tabPageCharacters.ResumeLayout(false);
            this.tabPagePlaceables.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageTilesets;
        private System.Windows.Forms.TabPage tabPageCharacters;
        private System.Windows.Forms.TabPage tabPagePlaceables;
        private System.Windows.Forms.Label labelTilesetName;
        private System.Windows.Forms.Label labelSpriteSheet;
        private System.Windows.Forms.ComboBox comboBoxSpriteSheet;
        private System.Windows.Forms.Label labelTilesetHeader;
        private GenericControls.NameTextBox textBoxTilesetName;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.ListBox listBoxCharacters;
        private System.Windows.Forms.ListBox listBoxPlaceables;
        private System.Windows.Forms.Button buttonDeleteTileset;
        private System.Windows.Forms.Button buttonNewTileset;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonApplyChanges;
    }
}
