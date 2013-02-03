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
            this.hScrollBarLeftRight = new System.Windows.Forms.HScrollBar();
            this.vScrollBarUpDown = new System.Windows.Forms.VScrollBar();
            this.panelTilesetViewer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
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
            // panelTilesetViewer
            // 
            this.panelTilesetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTilesetViewer.Location = new System.Drawing.Point(0, 0);
            this.panelTilesetViewer.Name = "panelTilesetViewer";
            this.panelTilesetViewer.Size = new System.Drawing.Size(161, 435);
            this.panelTilesetViewer.TabIndex = 5;
            // 
            // TilesetViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTilesetViewer);
            this.Controls.Add(this.vScrollBarUpDown);
            this.Controls.Add(this.hScrollBarLeftRight);
            this.Name = "TilesetViewerControl";
            this.Size = new System.Drawing.Size(178, 452);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBarLeftRight;
        private System.Windows.Forms.VScrollBar vScrollBarUpDown;
        private System.Windows.Forms.Panel panelTilesetViewer;

    }
}
