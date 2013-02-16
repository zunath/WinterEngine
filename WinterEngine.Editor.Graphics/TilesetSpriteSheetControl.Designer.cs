namespace WinterEngine.Editor.Graphics
{
    partial class TilesetSpriteSheetControl
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
            this.panelTileProperties = new System.Windows.Forms.Panel();
            this.radioButtonDirectionalMode = new System.Windows.Forms.RadioButton();
            this.radioButtonPassableMode = new System.Windows.Forms.RadioButton();
            this.hScrollBarSpriteSheet = new System.Windows.Forms.HScrollBar();
            this.vScrollBarSpriteSheet = new System.Windows.Forms.VScrollBar();
            this.panelSpriteSheet = new System.Windows.Forms.Panel();
            this.panelTileProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTileProperties
            // 
            this.panelTileProperties.Controls.Add(this.radioButtonDirectionalMode);
            this.panelTileProperties.Controls.Add(this.radioButtonPassableMode);
            this.panelTileProperties.Controls.Add(this.hScrollBarSpriteSheet);
            this.panelTileProperties.Controls.Add(this.vScrollBarSpriteSheet);
            this.panelTileProperties.Controls.Add(this.panelSpriteSheet);
            this.panelTileProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTileProperties.Location = new System.Drawing.Point(0, 0);
            this.panelTileProperties.Name = "panelTileProperties";
            this.panelTileProperties.Size = new System.Drawing.Size(374, 456);
            this.panelTileProperties.TabIndex = 1;
            // 
            // radioButtonDirectionalMode
            // 
            this.radioButtonDirectionalMode.AutoSize = true;
            this.radioButtonDirectionalMode.Location = new System.Drawing.Point(107, 6);
            this.radioButtonDirectionalMode.Name = "radioButtonDirectionalMode";
            this.radioButtonDirectionalMode.Size = new System.Drawing.Size(105, 17);
            this.radioButtonDirectionalMode.TabIndex = 5;
            this.radioButtonDirectionalMode.Text = "Directional Mode";
            this.radioButtonDirectionalMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonPassableMode
            // 
            this.radioButtonPassableMode.AutoSize = true;
            this.radioButtonPassableMode.Checked = true;
            this.radioButtonPassableMode.Location = new System.Drawing.Point(3, 6);
            this.radioButtonPassableMode.Name = "radioButtonPassableMode";
            this.radioButtonPassableMode.Size = new System.Drawing.Size(98, 17);
            this.radioButtonPassableMode.TabIndex = 4;
            this.radioButtonPassableMode.TabStop = true;
            this.radioButtonPassableMode.Text = "Passable Mode";
            this.radioButtonPassableMode.UseVisualStyleBackColor = true;
            // 
            // hScrollBarSpriteSheet
            // 
            this.hScrollBarSpriteSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBarSpriteSheet.Location = new System.Drawing.Point(2, 437);
            this.hScrollBarSpriteSheet.Name = "hScrollBarSpriteSheet";
            this.hScrollBarSpriteSheet.Size = new System.Drawing.Size(350, 17);
            this.hScrollBarSpriteSheet.TabIndex = 3;
            // 
            // vScrollBarSpriteSheet
            // 
            this.vScrollBarSpriteSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarSpriteSheet.Location = new System.Drawing.Point(355, 29);
            this.vScrollBarSpriteSheet.Name = "vScrollBarSpriteSheet";
            this.vScrollBarSpriteSheet.Size = new System.Drawing.Size(17, 405);
            this.vScrollBarSpriteSheet.TabIndex = 2;
            // 
            // panelSpriteSheet
            // 
            this.panelSpriteSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpriteSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpriteSheet.Location = new System.Drawing.Point(2, 29);
            this.panelSpriteSheet.Name = "panelSpriteSheet";
            this.panelSpriteSheet.Size = new System.Drawing.Size(350, 405);
            this.panelSpriteSheet.TabIndex = 1;
            // 
            // TilesetSpriteSheetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTileProperties);
            this.Name = "TilesetSpriteSheetControl";
            this.Size = new System.Drawing.Size(374, 456);
            this.panelTileProperties.ResumeLayout(false);
            this.panelTileProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTileProperties;
        private System.Windows.Forms.RadioButton radioButtonDirectionalMode;
        private System.Windows.Forms.RadioButton radioButtonPassableMode;
        private System.Windows.Forms.HScrollBar hScrollBarSpriteSheet;
        private System.Windows.Forms.VScrollBar vScrollBarSpriteSheet;
        private System.Windows.Forms.Panel panelSpriteSheet;
    }
}
