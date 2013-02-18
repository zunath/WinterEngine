﻿namespace WinterEngine.Toolset.GUI.Views
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
            this.treeCategoryControlCreature = new WinterEngine.Toolset.Controls.TreeCategoryControl();
            this.creatureViewControl = new WinterEngine.Toolset.Controls.CreaturePropertiesControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeCategoryControlCreature
            // 
            this.treeCategoryControlCreature.ActiveGameObject = null;
            this.treeCategoryControlCreature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategoryControlCreature.GameObjectResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Creature;
            this.treeCategoryControlCreature.Location = new System.Drawing.Point(0, 0);
            this.treeCategoryControlCreature.Name = "treeCategoryControlCreature";
            this.treeCategoryControlCreature.Size = new System.Drawing.Size(100, 455);
            this.treeCategoryControlCreature.TabIndex = 2;
            // 
            // creatureViewControl
            // 
            this.creatureViewControl.BackupCreature = null;
            this.creatureViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creatureViewControl.Location = new System.Drawing.Point(0, 0);
            this.creatureViewControl.Name = "creatureViewControl";
            this.creatureViewControl.Size = new System.Drawing.Size(657, 455);
            this.creatureViewControl.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCategoryControlCreature);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.creatureViewControl);
            this.splitContainer1.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 4;
            // 
            // CreatureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CreatureView";
            this.Size = new System.Drawing.Size(761, 455);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

         }

         #endregion

         private Controls.TreeCategoryControl treeCategoryControlCreature;
         private Controls.CreaturePropertiesControl creatureViewControl;
         private System.Windows.Forms.SplitContainer splitContainer1;

     }
 }