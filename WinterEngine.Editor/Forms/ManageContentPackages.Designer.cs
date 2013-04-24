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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelContentPackageName = new System.Windows.Forms.Label();
            this.labelContentPackageDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBoxAttachedContentPackages = new System.Windows.Forms.ListBox();
            this.listBoxAvailableContentPackages = new System.Windows.Forms.ListBox();
            this.buttonAddPackage = new System.Windows.Forms.Button();
            this.buttonRemovePackage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(94, 16);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(277, 20);
            this.textBoxName.TabIndex = 4;
            // 
            // labelContentPackageName
            // 
            this.labelContentPackageName.AutoSize = true;
            this.labelContentPackageName.Location = new System.Drawing.Point(12, 19);
            this.labelContentPackageName.Name = "labelContentPackageName";
            this.labelContentPackageName.Size = new System.Drawing.Size(38, 13);
            this.labelContentPackageName.TabIndex = 5;
            this.labelContentPackageName.Text = "Name:";
            // 
            // labelContentPackageDescription
            // 
            this.labelContentPackageDescription.AutoSize = true;
            this.labelContentPackageDescription.Location = new System.Drawing.Point(12, 49);
            this.labelContentPackageDescription.Name = "labelContentPackageDescription";
            this.labelContentPackageDescription.Size = new System.Drawing.Size(63, 13);
            this.labelContentPackageDescription.TabIndex = 6;
            this.labelContentPackageDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(94, 46);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(277, 65);
            this.textBoxDescription.TabIndex = 7;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(47, 452);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(122, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Save and Close";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(195, 452);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxAttachedContentPackages
            // 
            this.listBoxAttachedContentPackages.FormattingEnabled = true;
            this.listBoxAttachedContentPackages.Location = new System.Drawing.Point(15, 299);
            this.listBoxAttachedContentPackages.Name = "listBoxAttachedContentPackages";
            this.listBoxAttachedContentPackages.Size = new System.Drawing.Size(356, 147);
            this.listBoxAttachedContentPackages.TabIndex = 14;
            this.listBoxAttachedContentPackages.SelectedIndexChanged += new System.EventHandler(this.listBoxAttachedContentPackages_SelectedIndexChanged);
            this.listBoxAttachedContentPackages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAttachedContentPackages_MouseDoubleClick);
            // 
            // listBoxAvailableContentPackages
            // 
            this.listBoxAvailableContentPackages.FormattingEnabled = true;
            this.listBoxAvailableContentPackages.Location = new System.Drawing.Point(15, 117);
            this.listBoxAvailableContentPackages.Name = "listBoxAvailableContentPackages";
            this.listBoxAvailableContentPackages.Size = new System.Drawing.Size(356, 147);
            this.listBoxAvailableContentPackages.TabIndex = 15;
            this.listBoxAvailableContentPackages.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableContentPackages_SelectedIndexChanged);
            this.listBoxAvailableContentPackages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAvailableContentPackages_MouseDoubleClick);
            // 
            // buttonAddPackage
            // 
            this.buttonAddPackage.Location = new System.Drawing.Point(94, 270);
            this.buttonAddPackage.Name = "buttonAddPackage";
            this.buttonAddPackage.Size = new System.Drawing.Size(75, 23);
            this.buttonAddPackage.TabIndex = 16;
            this.buttonAddPackage.Text = "Add";
            this.buttonAddPackage.UseVisualStyleBackColor = true;
            this.buttonAddPackage.Click += new System.EventHandler(this.buttonAddPackage_Click);
            // 
            // buttonRemovePackage
            // 
            this.buttonRemovePackage.Location = new System.Drawing.Point(195, 270);
            this.buttonRemovePackage.Name = "buttonRemovePackage";
            this.buttonRemovePackage.Size = new System.Drawing.Size(75, 23);
            this.buttonRemovePackage.TabIndex = 17;
            this.buttonRemovePackage.Text = "Remove";
            this.buttonRemovePackage.UseVisualStyleBackColor = true;
            this.buttonRemovePackage.Click += new System.EventHandler(this.buttonRemovePackage_Click);
            // 
            // ManageContentPackages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 487);
            this.Controls.Add(this.buttonRemovePackage);
            this.Controls.Add(this.buttonAddPackage);
            this.Controls.Add(this.listBoxAvailableContentPackages);
            this.Controls.Add(this.listBoxAttachedContentPackages);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelContentPackageDescription);
            this.Controls.Add(this.labelContentPackageName);
            this.Controls.Add(this.textBoxName);
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

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelContentPackageName;
        private System.Windows.Forms.Label labelContentPackageDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxAttachedContentPackages;
        private System.Windows.Forms.ListBox listBoxAvailableContentPackages;
        private System.Windows.Forms.Button buttonAddPackage;
        private System.Windows.Forms.Button buttonRemovePackage;
    }
}