namespace WinterEngine.Toolset.GUI.Views
{
    partial class CreatureView
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
            this.treeCategoryControlCreature = new WinterEngine.Toolset.Controls.ViewControls.TreeCategoryControl();
            this.creatureViewControl = new WinterEngine.Toolset.Controls.ViewControls.CreatureViewControl();
            this.SuspendLayout();
            // 
            // treeCategoryControlCreature
            // 
            this.treeCategoryControlCreature.Location = new System.Drawing.Point(0, 3);
            this.treeCategoryControlCreature.Name = "treeCategoryControlCreature";
            this.treeCategoryControlCreature.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlCreature.TabIndex = 2;
            this.treeCategoryControlCreature.GameObjectResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Creature;
            // 
            // creatureViewControl
            // 
            this.creatureViewControl.BackupCreature = null;
            this.creatureViewControl.Enabled = false;
            this.creatureViewControl.Location = new System.Drawing.Point(192, 0);
            this.creatureViewControl.Name = "creatureViewControl";
            this.creatureViewControl.Size = new System.Drawing.Size(375, 452);
            this.creatureViewControl.TabIndex = 3;
            // 
            // CreatureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.creatureViewControl);
            this.Controls.Add(this.treeCategoryControlCreature);
            this.Name = "CreatureView";
            this.Size = new System.Drawing.Size(570, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ViewControls.TreeCategoryControl buttonAddCategory;
        private Controls.ViewControls.TreeCategoryControl treeCategoryControlCreature;
        private Controls.ViewControls.CreatureViewControl creatureViewControl;

    }
}
