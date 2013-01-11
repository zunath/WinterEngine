using WinterEngine.Library;

namespace WinterEngine.Toolset.GUI.Views
{
    partial class AreaView
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
            this.areaViewControl = new WinterEngine.Toolset.Controls.ViewControls.AreaViewControl();
            this.treeCategoryControlArea = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.SuspendLayout();
            // 
            // areaViewControl
            // 
            this.areaViewControl.Location = new System.Drawing.Point(192, 0);
            this.areaViewControl.Name = "areaViewControl";
            this.areaViewControl.Size = new System.Drawing.Size(375, 452);
            this.areaViewControl.TabIndex = 6;
            // 
            // treeCategoryControlArea
            // 
            this.treeCategoryControlArea.Location = new System.Drawing.Point(0, 4);
            this.treeCategoryControlArea.Name = "treeCategoryControlArea";
            this.treeCategoryControlArea.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlArea.TabIndex = 5;
            this.treeCategoryControlArea.GameObjectResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Area;
            // 
            // AreaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.areaViewControl);
            this.Controls.Add(this.treeCategoryControlArea);
            this.Name = "AreaView";
            this.Size = new System.Drawing.Size(570, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl treeCategoryControlArea;
        private Controls.ViewControls.AreaViewControl areaViewControl;

    }
}
