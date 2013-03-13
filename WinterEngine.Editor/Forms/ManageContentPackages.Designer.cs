namespace WinterEngine.Editor.Forms
{
    partial class ManageContentPackages
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
            this.listBoxContentPackages = new System.Windows.Forms.ListBox();
            this.buttonAddContentPackage = new System.Windows.Forms.Button();
            this.buttonRemoveContentPackage = new System.Windows.Forms.Button();
            this.textBoxContentPackageName = new System.Windows.Forms.TextBox();
            this.labelContentPackageName = new System.Windows.Forms.Label();
            this.labelContentPackageDescription = new System.Windows.Forms.Label();
            this.textBoxContentPackageDescription = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // listBoxContentPackages
            // 
            this.listBoxContentPackages.FormattingEnabled = true;
            this.listBoxContentPackages.HorizontalScrollbar = true;
            this.listBoxContentPackages.Location = new System.Drawing.Point(12, 77);
            this.listBoxContentPackages.Name = "listBoxContentPackages";
            this.listBoxContentPackages.Size = new System.Drawing.Size(491, 160);
            this.listBoxContentPackages.TabIndex = 0;
            // 
            // buttonAddContentPackage
            // 
            this.buttonAddContentPackage.Location = new System.Drawing.Point(15, 243);
            this.buttonAddContentPackage.Name = "buttonAddContentPackage";
            this.buttonAddContentPackage.Size = new System.Drawing.Size(91, 23);
            this.buttonAddContentPackage.TabIndex = 2;
            this.buttonAddContentPackage.Text = "Add";
            this.buttonAddContentPackage.UseVisualStyleBackColor = true;
            this.buttonAddContentPackage.Click += new System.EventHandler(this.buttonAddContentPackage_Click);
            // 
            // buttonRemoveContentPackage
            // 
            this.buttonRemoveContentPackage.Location = new System.Drawing.Point(112, 243);
            this.buttonRemoveContentPackage.Name = "buttonRemoveContentPackage";
            this.buttonRemoveContentPackage.Size = new System.Drawing.Size(91, 23);
            this.buttonRemoveContentPackage.TabIndex = 3;
            this.buttonRemoveContentPackage.Text = "Remove";
            this.buttonRemoveContentPackage.UseVisualStyleBackColor = true;
            this.buttonRemoveContentPackage.Click += new System.EventHandler(this.buttonRemoveContentPackage_Click);
            // 
            // textBoxContentPackageName
            // 
            this.textBoxContentPackageName.Location = new System.Drawing.Point(175, 16);
            this.textBoxContentPackageName.Name = "textBoxContentPackageName";
            this.textBoxContentPackageName.ReadOnly = true;
            this.textBoxContentPackageName.Size = new System.Drawing.Size(328, 20);
            this.textBoxContentPackageName.TabIndex = 4;
            // 
            // labelContentPackageName
            // 
            this.labelContentPackageName.AutoSize = true;
            this.labelContentPackageName.Location = new System.Drawing.Point(12, 19);
            this.labelContentPackageName.Name = "labelContentPackageName";
            this.labelContentPackageName.Size = new System.Drawing.Size(124, 13);
            this.labelContentPackageName.TabIndex = 5;
            this.labelContentPackageName.Text = "Content Package Name:";
            // 
            // labelContentPackageDescription
            // 
            this.labelContentPackageDescription.AutoSize = true;
            this.labelContentPackageDescription.Location = new System.Drawing.Point(12, 49);
            this.labelContentPackageDescription.Name = "labelContentPackageDescription";
            this.labelContentPackageDescription.Size = new System.Drawing.Size(149, 13);
            this.labelContentPackageDescription.TabIndex = 6;
            this.labelContentPackageDescription.Text = "Content Package Description:";
            // 
            // textBoxContentPackageDescription
            // 
            this.textBoxContentPackageDescription.Location = new System.Drawing.Point(175, 46);
            this.textBoxContentPackageDescription.Name = "textBoxContentPackageDescription";
            this.textBoxContentPackageDescription.ReadOnly = true;
            this.textBoxContentPackageDescription.Size = new System.Drawing.Size(328, 20);
            this.textBoxContentPackageDescription.TabIndex = 7;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(306, 243);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(100, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Save and Close";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(412, 243);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(91, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(209, 243);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(91, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ManageContentPackages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 277);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.textBoxContentPackageDescription);
            this.Controls.Add(this.labelContentPackageDescription);
            this.Controls.Add(this.labelContentPackageName);
            this.Controls.Add(this.textBoxContentPackageName);
            this.Controls.Add(this.buttonRemoveContentPackage);
            this.Controls.Add(this.buttonAddContentPackage);
            this.Controls.Add(this.listBoxContentPackages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageContentPackages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Content Packages";
            this.Load += new System.EventHandler(this.ManageContentPackages_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxContentPackages;
        private System.Windows.Forms.Button buttonAddContentPackage;
        private System.Windows.Forms.Button buttonRemoveContentPackage;
        private System.Windows.Forms.TextBox textBoxContentPackageName;
        private System.Windows.Forms.Label labelContentPackageName;
        private System.Windows.Forms.Label labelContentPackageDescription;
        private System.Windows.Forms.TextBox textBoxContentPackageDescription;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}