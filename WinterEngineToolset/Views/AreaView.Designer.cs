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
            this.areaViewControl = new WinterEngine.Toolset.Controls.AreaPropertiesControl();
            this.treeCategoryControlArea = new WinterEngine.Toolset.Controls.TreeCategoryControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // areaViewControl
            // 
            this.areaViewControl.BackupArea = null;
            this.areaViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaViewControl.Location = new System.Drawing.Point(0, 0);
            this.areaViewControl.Name = "areaViewControl";
            this.areaViewControl.Size = new System.Drawing.Size(313, 455);
            this.areaViewControl.TabIndex = 6;
            // 
            // treeCategoryControlArea
            // 
            this.treeCategoryControlArea.ActiveGameObject = null;
            this.treeCategoryControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlArea.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.treeCategoryControlArea.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlArea.Name = "treeCategoryControlArea";
            this.treeCategoryControlArea.Size = new System.Drawing.Size(187, 455);
            this.treeCategoryControlArea.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 7;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.areaViewControl);
            this.splitContainer2.Size = new System.Drawing.Size(570, 455);
            this.splitContainer2.SplitterDistance = 313;
            this.splitContainer2.TabIndex = 0;
            // 
            // AreaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AreaView";
            this.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TreeCategoryControl treeCategoryControlArea;
        private Controls.AreaPropertiesControl areaViewControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

    }
}
