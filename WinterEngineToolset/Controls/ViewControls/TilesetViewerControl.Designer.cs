namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class TilesetViewerControl
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
            this.pictureBoxTileset = new System.Windows.Forms.PictureBox();
            this.hScrollBarLeftRight = new System.Windows.Forms.HScrollBar();
            this.vScrollBarUpDown = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTileset)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxTileset
            // 
            this.pictureBoxTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxTileset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxTileset.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTileset.Name = "pictureBoxTileset";
            this.pictureBoxTileset.Size = new System.Drawing.Size(178, 452);
            this.pictureBoxTileset.TabIndex = 2;
            this.pictureBoxTileset.TabStop = false;
            this.pictureBoxTileset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseClick);
            // 
            // hScrollBarLeftRight
            // 
            this.hScrollBarLeftRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBarLeftRight.LargeChange = 1;
            this.hScrollBarLeftRight.Location = new System.Drawing.Point(0, 435);
            this.hScrollBarLeftRight.Maximum = 0;
            this.hScrollBarLeftRight.Name = "hScrollBarLeftRight";
            this.hScrollBarLeftRight.Size = new System.Drawing.Size(178, 17);
            this.hScrollBarLeftRight.TabIndex = 3;
            // 
            // vScrollBarUpDown
            // 
            this.vScrollBarUpDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBarUpDown.LargeChange = 1;
            this.vScrollBarUpDown.Location = new System.Drawing.Point(161, 0);
            this.vScrollBarUpDown.Maximum = 0;
            this.vScrollBarUpDown.Name = "vScrollBarUpDown";
            this.vScrollBarUpDown.Size = new System.Drawing.Size(17, 435);
            this.vScrollBarUpDown.TabIndex = 4;
            // 
            // TilesetViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBarUpDown);
            this.Controls.Add(this.hScrollBarLeftRight);
            this.Controls.Add(this.pictureBoxTileset);
            this.Name = "TilesetViewerControl";
            this.Size = new System.Drawing.Size(178, 452);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTileset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTileset;
        private System.Windows.Forms.HScrollBar hScrollBarLeftRight;
        private System.Windows.Forms.VScrollBar vScrollBarUpDown;

    }
}
