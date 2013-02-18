﻿namespace WinterEngine.Toolset.GUI.Views
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeCategoryControlArea = new WinterEngine.Toolset.Controls.TreeCategoryControl();
            this.areaViewControl = new WinterEngine.Toolset.Controls.AreaPropertiesControl();
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
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.areaViewControl);
            this.splitContainer1.Size = new System.Drawing.Size(322, 455);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 7;
            // 
            // treeCategoryControlArea
            // 
            this.treeCategoryControlArea.ActiveGameObject = null;
            this.treeCategoryControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlArea.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.treeCategoryControlArea.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlArea.Name = "treeCategoryControlArea";
            this.treeCategoryControlArea.Size = new System.Drawing.Size(100, 455);
            this.treeCategoryControlArea.TabIndex = 5;
            // 
            // areaViewControl
            // 
            this.areaViewControl.BackupArea = null;
            this.areaViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaViewControl.Location = new System.Drawing.Point(0, 0);
            this.areaViewControl.Name = "areaViewControl";
            this.areaViewControl.Size = new System.Drawing.Size(218, 455);
            this.areaViewControl.TabIndex = 6;
            // 
            // AreaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AreaView";
            this.Size = new System.Drawing.Size(322, 455);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TreeCategoryControl treeCategoryControlArea;
        private Controls.AreaPropertiesControl areaViewControl;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}