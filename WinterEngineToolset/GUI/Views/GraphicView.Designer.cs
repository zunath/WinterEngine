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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeCategoryControlGraphic = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.graphicPropertiesControl = new WinterEngine.Toolset.Controls.ViewControls.GraphicPropertiesControl();
            this.tilesetViewerControl = new WinterEngine.Toolset.Controls.ViewControls.TilesetViewerControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlGraphic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.graphicPropertiesControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tilesetViewerControl);
            this.splitContainer2.Size = new System.Drawing.Size(570, 455);
            this.splitContainer2.SplitterDistance = 313;
            this.splitContainer2.TabIndex = 7;
            // 
            // treeCategoryControlGraphic
            // 
            this.treeCategoryControlGraphic.ActiveGameObject = null;
            this.treeCategoryControlGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlGraphic.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Tileset;
            this.treeCategoryControlGraphic.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlGraphic.Name = "treeCategoryControlGraphic";
            this.treeCategoryControlGraphic.Size = new System.Drawing.Size(187, 455);
            this.treeCategoryControlGraphic.TabIndex = 5;
            // 
            // graphicPropertiesControl
            // 
            this.graphicPropertiesControl.BackupTileset = null;
            this.graphicPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicPropertiesControl.Location = new System.Drawing.Point(0, 0);
            this.graphicPropertiesControl.Name = "graphicPropertiesControl";
            this.graphicPropertiesControl.Size = new System.Drawing.Size(313, 455);
            this.graphicPropertiesControl.TabIndex = 6;
            // 
            // tilesetViewerControl
            // 
            this.tilesetViewerControl.ActiveSpriteSheet = null;
            this.tilesetViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetViewerControl.Location = new System.Drawing.Point(0, 0);
            this.tilesetViewerControl.Name = "tilesetViewerControl";
            this.tilesetViewerControl.Size = new System.Drawing.Size(253, 455);
            this.tilesetViewerControl.TabIndex = 7;
            // 
            // GraphicView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "GraphicView";
            this.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl treeCategoryControlGraphic;
        private Controls.ViewControls.GraphicPropertiesControl graphicPropertiesControl;
        private Controls.ViewControls.TilesetViewerControl tilesetViewerControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

    }
}
