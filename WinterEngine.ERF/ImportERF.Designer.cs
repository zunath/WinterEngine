namespace WinterEngine.ERF
{
    partial class ImportERF
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
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInstructions1 = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.listBoxResources = new System.Windows.Forms.ListBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonSelectNone = new System.Windows.Forms.Button();
            this.labelInstructions2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(92, 392);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 0;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(173, 392);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelInstructions1
            // 
            this.labelInstructions1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions1.Location = new System.Drawing.Point(18, 51);
            this.labelInstructions1.Name = "labelInstructions1";
            this.labelInstructions1.Size = new System.Drawing.Size(289, 38);
            this.labelInstructions1.TabIndex = 2;
            this.labelInstructions1.Text = "Importing this package would overwrite some resources that already exist in the m" +
    "odule.";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(16, 9);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(322, 25);
            this.labelHeader.TabIndex = 3;
            this.labelHeader.Text = "Overwrite Existing Resources";
            // 
            // listBoxResources
            // 
            this.listBoxResources.FormattingEnabled = true;
            this.listBoxResources.HorizontalScrollbar = true;
            this.listBoxResources.Location = new System.Drawing.Point(20, 145);
            this.listBoxResources.Name = "listBoxResources";
            this.listBoxResources.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResources.Size = new System.Drawing.Size(306, 212);
            this.listBoxResources.TabIndex = 4;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(20, 363);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(147, 23);
            this.buttonSelectAll.TabIndex = 5;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // buttonSelectNone
            // 
            this.buttonSelectNone.Location = new System.Drawing.Point(179, 363);
            this.buttonSelectNone.Name = "buttonSelectNone";
            this.buttonSelectNone.Size = new System.Drawing.Size(147, 23);
            this.buttonSelectNone.TabIndex = 6;
            this.buttonSelectNone.Text = "Select None";
            this.buttonSelectNone.UseVisualStyleBackColor = true;
            this.buttonSelectNone.Click += new System.EventHandler(this.buttonSelectNone_Click);
            // 
            // labelInstructions2
            // 
            this.labelInstructions2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions2.Location = new System.Drawing.Point(17, 89);
            this.labelInstructions2.Name = "labelInstructions2";
            this.labelInstructions2.Size = new System.Drawing.Size(309, 53);
            this.labelInstructions2.TabIndex = 7;
            this.labelInstructions2.Text = "Please select the resources you want to overwrite. Deselect the resources that yo" +
    "u do not want to overwrite.";
            // 
            // ImportERF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 427);
            this.Controls.Add(this.labelInstructions2);
            this.Controls.Add(this.buttonSelectNone);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.listBoxResources);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelInstructions1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportERF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encapsulated Resource File Importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelInstructions1;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.ListBox listBoxResources;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonSelectNone;
        private System.Windows.Forms.Label labelInstructions2;
    }
}