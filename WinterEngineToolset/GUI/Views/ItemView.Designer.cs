namespace WinterEngine.Toolset.GUI.Views
{
    partial class ItemView
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
            this.itemViewControl = new WinterEngine.Toolset.Controls.ViewControls.ItemViewControl();
            this.treeCategoryControlItem = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.SuspendLayout();
            // 
            // itemViewControl
            // 
            this.itemViewControl.BackupItem = null;
            this.itemViewControl.Enabled = false;
            this.itemViewControl.Location = new System.Drawing.Point(192, 0);
            this.itemViewControl.Name = "itemViewControl";
            this.itemViewControl.Size = new System.Drawing.Size(375, 452);
            this.itemViewControl.TabIndex = 4;
            // 
            // treeCategoryControlItem
            // 
            this.treeCategoryControlItem.Location = new System.Drawing.Point(0, 3);
            this.treeCategoryControlItem.Name = "treeCategoryControlItem";
            this.treeCategoryControlItem.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlItem.TabIndex = 3;
            this.treeCategoryControlItem.GameObjectResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Item;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.itemViewControl);
            this.Controls.Add(this.treeCategoryControlItem);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(570, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl treeCategoryControlItem;
        private Controls.ViewControls.ItemViewControl itemViewControl;

    }
}
