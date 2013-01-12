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
            this.treeCategoryControlPlaceable = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.placeableViewControl = new WinterEngine.Toolset.Controls.ViewControls.PlaceablePropertiesControl();
            this.SuspendLayout();
            // 
            // treeCategoryControlPlaceable
            // 
            this.treeCategoryControlPlaceable.ActiveGameObject = null;
            this.treeCategoryControlPlaceable.GameObjectResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Placeable;
            this.treeCategoryControlPlaceable.Location = new System.Drawing.Point(0, 3);
            this.treeCategoryControlPlaceable.Name = "treeCategoryControlPlaceable";
            this.treeCategoryControlPlaceable.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlPlaceable.TabIndex = 3;
            // 
            // placeableViewControl
            // 
            this.placeableViewControl.BackupPlaceable = null;
            this.placeableViewControl.Location = new System.Drawing.Point(192, 0);
            this.placeableViewControl.Name = "placeableViewControl";
            this.placeableViewControl.Size = new System.Drawing.Size(375, 452);
            this.placeableViewControl.TabIndex = 4;
            // 
            // PlaceableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.placeableViewControl);
            this.Controls.Add(this.treeCategoryControlPlaceable);
            this.Name = "PlaceableView";
            this.Size = new System.Drawing.Size(570, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl treeCategoryControlPlaceable;
        private Controls.ViewControls.PlaceablePropertiesControl placeableViewControl;

    }
}
