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
            this.textBoxContentPackageName = new System.Windows.Forms.TextBox();
            this.labelContentPackageName = new System.Windows.Forms.Label();
            this.labelContentPackageDescription = new System.Windows.Forms.Label();
            this.textBoxContentPackageDescription = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.checkedListBoxPackages = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // textBoxContentPackageName
            // 
            this.textBoxContentPackageName.Location = new System.Drawing.Point(175, 16);
            this.textBoxContentPackageName.Name = "textBoxContentPackageName";
            this.textBoxContentPackageName.ReadOnly = true;
            this.textBoxContentPackageName.Size = new System.Drawing.Size(206, 20);
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
            this.textBoxContentPackageDescription.Size = new System.Drawing.Size(206, 20);
            this.textBoxContentPackageDescription.TabIndex = 7;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Location = new System.Drawing.Point(142, 242);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(122, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Save and Close";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(270, 242);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(111, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkedListBoxPackages
            // 
            this.checkedListBoxPackages.FormattingEnabled = true;
            this.checkedListBoxPackages.Location = new System.Drawing.Point(15, 82);
            this.checkedListBoxPackages.Name = "checkedListBoxPackages";
            this.checkedListBoxPackages.Size = new System.Drawing.Size(366, 154);
            this.checkedListBoxPackages.TabIndex = 11;
            this.checkedListBoxPackages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxPackages_ItemCheck);
            // 
            // ManageContentPackages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 277);
            this.Controls.Add(this.checkedListBoxPackages);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.textBoxContentPackageDescription);
            this.Controls.Add(this.labelContentPackageDescription);
            this.Controls.Add(this.labelContentPackageName);
            this.Controls.Add(this.textBoxContentPackageName);
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

        private System.Windows.Forms.TextBox textBoxContentPackageName;
        private System.Windows.Forms.Label labelContentPackageName;
        private System.Windows.Forms.Label labelContentPackageDescription;
        private System.Windows.Forms.TextBox textBoxContentPackageDescription;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckedListBox checkedListBoxPackages;
    }
}