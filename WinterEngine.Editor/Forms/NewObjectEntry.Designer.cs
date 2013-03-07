﻿namespace WinterEngine.Editor.Forms
{
    partial class NewObjectEntry
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelResref = new System.Windows.Forms.Label();
            this.labelTag = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.resrefTextBoxEntry = new WinterEngine.Forms.Controls.Standard.ResrefTextBox();
            this.tagTextBoxEntry = new WinterEngine.Forms.Controls.Standard.TagTextBox();
            this.nameTextBoxEntry = new WinterEngine.Forms.Controls.Standard.NameTextBox();
            this.SuspendLayout();
            // 
            // labelResref
            // 
            this.labelResref.AutoSize = true;
            this.labelResref.Location = new System.Drawing.Point(12, 125);
            this.labelResref.Name = "labelResref";
            this.labelResref.Size = new System.Drawing.Size(41, 13);
            this.labelResref.TabIndex = 5;
            this.labelResref.Text = "Resref:";
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Location = new System.Drawing.Point(12, 96);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(29, 13);
            this.labelTag.TabIndex = 3;
            this.labelTag.Text = "Tag:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Object";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 66);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(132, 181);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(220, 181);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // resrefTextBoxEntry
            // 
            this.resrefTextBoxEntry.IsValid = false;
            this.resrefTextBoxEntry.Location = new System.Drawing.Point(75, 110);
            this.resrefTextBoxEntry.Name = "resrefTextBoxEntry";
            this.resrefTextBoxEntry.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.resrefTextBoxEntry.ResrefText = "";
            this.resrefTextBoxEntry.Size = new System.Drawing.Size(247, 28);
            this.resrefTextBoxEntry.TabIndex = 6;
            // 
            // tagTextBoxEntry
            // 
            this.tagTextBoxEntry.IsValid = false;
            this.tagTextBoxEntry.Location = new System.Drawing.Point(75, 82);
            this.tagTextBoxEntry.Name = "tagTextBoxEntry";
            this.tagTextBoxEntry.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.tagTextBoxEntry.Size = new System.Drawing.Size(247, 28);
            this.tagTextBoxEntry.TabIndex = 4;
            this.tagTextBoxEntry.TagText = "";
            // 
            // nameTextBoxEntry
            // 
            this.nameTextBoxEntry.IsValid = false;
            this.nameTextBoxEntry.Location = new System.Drawing.Point(75, 54);
            this.nameTextBoxEntry.Name = "nameTextBoxEntry";
            this.nameTextBoxEntry.NameText = "";
            this.nameTextBoxEntry.Size = new System.Drawing.Size(247, 28);
            this.nameTextBoxEntry.TabIndex = 2;
            // 
            // NewObjectEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(322, 216);
            this.Controls.Add(this.nameTextBoxEntry);
            this.Controls.Add(this.tagTextBoxEntry);
            this.Controls.Add(this.resrefTextBoxEntry);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTag);
            this.Controls.Add(this.labelResref);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewObjectEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewObjectEntry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelResref;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private WinterEngine.Forms.Controls.Standard.ResrefTextBox resrefTextBoxEntry;
        private WinterEngine.Forms.Controls.Standard.TagTextBox tagTextBoxEntry;
        private WinterEngine.Forms.Controls.Standard.NameTextBox nameTextBoxEntry;

    }
}