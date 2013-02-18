﻿﻿namespace WinterEngine.Toolset.GUI.Views
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
            this.itemViewControl = new WinterEngine.Toolset.Controls.ItemPropertiesControl();
            this.treeCategoryControlItem = new WinterEngine.Toolset.Controls.TreeCategoryControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemViewControl
            // 
            this.itemViewControl.BackupItem = null;
            this.itemViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemViewControl.Location = new System.Drawing.Point(0, 0);
            this.itemViewControl.Name = "itemViewControl";
            this.itemViewControl.Size = new System.Drawing.Size(657, 455);
            this.itemViewControl.TabIndex = 4;
            // 
            // treeCategoryControlItem
            // 
            this.treeCategoryControlItem.ActiveGameObject = null;
            this.treeCategoryControlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlItem.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Item;
            this.treeCategoryControlItem.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlItem.Name = "treeCategoryControlItem";
            this.treeCategoryControlItem.Size = new System.Drawing.Size(100, 455);
            this.treeCategoryControlItem.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlItem);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.itemViewControl);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 5;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

         }

         #endregion

         private Controls.TreeCategoryControl treeCategoryControlItem;
         private Controls.ItemPropertiesControl itemViewControl;
         private System.Windows.Forms.SplitContainer splitContainer1;

     }
 }