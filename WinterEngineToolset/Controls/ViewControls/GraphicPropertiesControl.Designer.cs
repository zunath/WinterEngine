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
            this.textBoxTilesetName = new WinterEngine.Toolset.Controls.GenericControls.NameTextBox();
            this.labelTilesetHeader = new System.Windows.Forms.Label();
            this.labelSpriteSheet = new System.Windows.Forms.Label();
            this.comboBoxSpriteSheet = new System.Windows.Forms.ComboBox();
            this.labelTilesetName = new System.Windows.Forms.Label();
            this.tabPageCharacters = new System.Windows.Forms.TabPage();
            this.tabPagePlaceables = new System.Windows.Forms.TabPage();
            this.tabControlProperties.SuspendLayout();
            this.tabPageTilesets.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDiscardChangesTilesetDetails
            // 
            this.buttonDiscardChangesTilesetDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDiscardChangesTilesetDetails.Enabled = false;
            this.buttonDiscardChangesTilesetDetails.Location = new System.Drawing.Point(151, 426);
            this.buttonDiscardChangesTilesetDetails.Name = "buttonDiscardChangesTilesetDetails";
            this.buttonDiscardChangesTilesetDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesTilesetDetails.TabIndex = 9;
            this.buttonDiscardChangesTilesetDetails.Text = "Discard Changes";
            this.buttonDiscardChangesTilesetDetails.UseVisualStyleBackColor = true;
            this.buttonDiscardChangesTilesetDetails.Click += new System.EventHandler(this.buttonDiscardChangesTilesetDetails_Click);
            // 
            // buttonApplyChangesTilesetDetails
            // 
            this.buttonApplyChangesTilesetDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonApplyChangesTilesetDetails.Enabled = false;
            this.buttonApplyChangesTilesetDetails.Location = new System.Drawing.Point(35, 426);
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
            this.tabControlProperties.Controls.Add(this.tabPageTilesets);
            this.tabControlProperties.Controls.Add(this.tabPageCharacters);
            this.tabControlProperties.Controls.Add(this.tabPagePlaceables);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(302, 417);
            this.tabControlProperties.TabIndex = 2;
            // 
            // tabPageTilesets
            // 
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
            // textBoxTilesetName
            // 
            this.textBoxTilesetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.comboBoxSpriteSheet.FormattingEnabled = true;
            this.comboBoxSpriteSheet.Location = new System.Drawing.Point(80, 92);
            this.comboBoxSpriteSheet.Name = "comboBoxSpriteSheet";
            this.comboBoxSpriteSheet.Size = new System.Drawing.Size(183, 21);
            this.comboBoxSpriteSheet.TabIndex = 9;
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
            this.tabPageCharacters.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharacters.Name = "tabPageCharacters";
            this.tabPageCharacters.Size = new System.Drawing.Size(294, 391);
            this.tabPageCharacters.TabIndex = 1;
            this.tabPageCharacters.Text = "Characters";
            this.tabPageCharacters.UseVisualStyleBackColor = true;
            // 
            // tabPagePlaceables
            // 
            this.tabPagePlaceables.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceables.Name = "tabPagePlaceables";
            this.tabPagePlaceables.Size = new System.Drawing.Size(294, 391);
            this.tabPagePlaceables.TabIndex = 2;
            this.tabPagePlaceables.Text = "Placeables";
            this.tabPagePlaceables.UseVisualStyleBackColor = true;
            // 
            // GraphicPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChangesTilesetDetails);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.buttonApplyChangesTilesetDetails);
            this.Name = "GraphicPropertiesControl";
            this.Size = new System.Drawing.Size(308, 452);
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
        private System.Windows.Forms.Label labelSpriteSheet;
        private System.Windows.Forms.ComboBox comboBoxSpriteSheet;
        private System.Windows.Forms.Label labelTilesetHeader;
        private GenericControls.NameTextBox textBoxTilesetName;
    }
}
