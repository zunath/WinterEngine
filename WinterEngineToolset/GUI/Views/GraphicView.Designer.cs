using WinterEngine.Library;

namespace WinterEngine.Toolset.GUI.Views
{
    partial class GraphicView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spriteSheetViewerControl = new WinterEngine.Toolset.Controls.ViewControls.TilesetViewerControl();
            this.graphicPropertiesControl = new WinterEngine.Toolset.Controls.ViewControls.GraphicPropertiesControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.spriteSheetViewerControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphicPropertiesControl);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.TabIndex = 8;
            // 
            // spriteSheetViewerControl
            // 
            this.spriteSheetViewerControl.ActiveSpriteSheet = null;
            this.spriteSheetViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSheetViewerControl.Location = new System.Drawing.Point(0, 0);
            this.spriteSheetViewerControl.Name = "spriteSheetViewerControl";
            this.spriteSheetViewerControl.Size = new System.Drawing.Size(245, 455);
            this.spriteSheetViewerControl.TabIndex = 7;
            // 
            // graphicPropertiesControl
            // 
            this.graphicPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicPropertiesControl.Location = new System.Drawing.Point(0, 0);
            this.graphicPropertiesControl.Name = "graphicPropertiesControl";
            this.graphicPropertiesControl.Size = new System.Drawing.Size(512, 455);
            this.graphicPropertiesControl.TabIndex = 6;
            // 
            // GraphicView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "GraphicView";
            this.Size = new System.Drawing.Size(761, 455);
            this.Load += new System.EventHandler(this.GraphicView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.GraphicPropertiesControl graphicPropertiesControl;
        private Controls.ViewControls.TilesetViewerControl spriteSheetViewerControl;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}
