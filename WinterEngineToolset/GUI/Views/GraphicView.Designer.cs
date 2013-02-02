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
            this.graphicPropertiesControl1 = new WinterEngine.Toolset.Controls.ViewControls.GraphicPropertiesControl();
            this.treeCategoryControlGraphic = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.SuspendLayout();
            // 
            // graphicPropertiesControl1
            // 
            this.graphicPropertiesControl1.BackupTileset = null;
            this.graphicPropertiesControl1.Location = new System.Drawing.Point(192, 4);
            this.graphicPropertiesControl1.Name = "graphicPropertiesControl1";
            this.graphicPropertiesControl1.Size = new System.Drawing.Size(375, 452);
            this.graphicPropertiesControl1.TabIndex = 6;
            // 
            // treeCategoryControlGraphic
            // 
            this.treeCategoryControlGraphic.ActiveGameObject = null;
            this.treeCategoryControlGraphic.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Tileset;
            this.treeCategoryControlGraphic.Location = new System.Drawing.Point(0, 4);
            this.treeCategoryControlGraphic.Name = "treeCategoryControlGraphic";
            this.treeCategoryControlGraphic.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlGraphic.TabIndex = 5;
            // 
            // GraphicView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.graphicPropertiesControl1);
            this.Controls.Add(this.treeCategoryControlGraphic);
            this.Name = "GraphicView";
            this.Size = new System.Drawing.Size(570, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl treeCategoryControlGraphic;
        private Controls.ViewControls.GraphicPropertiesControl graphicPropertiesControl1;

    }
}
