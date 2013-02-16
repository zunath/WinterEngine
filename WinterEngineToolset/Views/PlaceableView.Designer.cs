namespace WinterEngine.Toolset.GUI.Views
{
    partial class PlaceableView
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
            this.treeCategoryControlPlaceable = new WinterEngine.Toolset.Controls.TreeCategoryControl();
            this.placeableViewControl = new WinterEngine.Toolset.Controls.PlaceablePropertiesControl();
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
            // treeCategoryControlPlaceable
            // 
            this.treeCategoryControlPlaceable.ActiveGameObject = null;
            this.treeCategoryControlPlaceable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlPlaceable.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Placeable;
            this.treeCategoryControlPlaceable.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlPlaceable.Name = "treeCategoryControlPlaceable";
            this.treeCategoryControlPlaceable.Size = new System.Drawing.Size(181, 455);
            this.treeCategoryControlPlaceable.TabIndex = 3;
            // 
            // placeableViewControl
            // 
            this.placeableViewControl.BackupPlaceable = null;
            this.placeableViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeableViewControl.Location = new System.Drawing.Point(0, 0);
            this.placeableViewControl.Name = "placeableViewControl";
            this.placeableViewControl.Size = new System.Drawing.Size(313, 455);
            this.placeableViewControl.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlPlaceable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.placeableViewControl);
            this.splitContainer2.Size = new System.Drawing.Size(576, 455);
            this.splitContainer2.SplitterDistance = 313;
            this.splitContainer2.TabIndex = 0;
            // 
            // PlaceableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PlaceableView";
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

        private Controls.TreeCategoryControl treeCategoryControlPlaceable;
        private Controls.PlaceablePropertiesControl placeableViewControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

    }
}
