namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class AreaViewControl
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
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageViewer = new System.Windows.Forms.TabPage();
            this.panelAreaObjectViewer = new System.Windows.Forms.Panel();
            this.tabPageAreaDetails = new System.Windows.Forms.TabPage();
            this.buttonDiscardChangesItemDetails = new System.Windows.Forms.Button();
            this.buttonSaveChangesItemDetails = new System.Windows.Forms.Button();
            this.labelItemDetailsHeader = new System.Windows.Forms.Label();
            this.textBoxItemResref = new System.Windows.Forms.TextBox();
            this.textBoxItemTag = new System.Windows.Forms.TextBox();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tabPageComments = new System.Windows.Forms.TabPage();
            this.tabPageScripts = new System.Windows.Forms.TabPage();
            this.tabControlProperties.SuspendLayout();
            this.tabPageViewer.SuspendLayout();
            this.tabPageAreaDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageViewer);
            this.tabControlProperties.Controls.Add(this.tabPageAreaDetails);
            this.tabControlProperties.Controls.Add(this.tabPageScripts);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageViewer
            // 
            this.tabPageViewer.Controls.Add(this.panelAreaObjectViewer);
            this.tabPageViewer.Location = new System.Drawing.Point(4, 22);
            this.tabPageViewer.Name = "tabPageViewer";
            this.tabPageViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageViewer.Size = new System.Drawing.Size(367, 426);
            this.tabPageViewer.TabIndex = 0;
            this.tabPageViewer.Text = "Viewer";
            this.tabPageViewer.UseVisualStyleBackColor = true;
            // 
            // panelAreaObjectViewer
            // 
            this.panelAreaObjectViewer.Location = new System.Drawing.Point(0, 0);
            this.panelAreaObjectViewer.Name = "panelAreaObjectViewer";
            this.panelAreaObjectViewer.Size = new System.Drawing.Size(367, 426);
            this.panelAreaObjectViewer.TabIndex = 0;
            // 
            // tabPageAreaDetails
            // 
            this.tabPageAreaDetails.Controls.Add(this.buttonDiscardChangesItemDetails);
            this.tabPageAreaDetails.Controls.Add(this.buttonSaveChangesItemDetails);
            this.tabPageAreaDetails.Controls.Add(this.labelItemDetailsHeader);
            this.tabPageAreaDetails.Controls.Add(this.textBoxItemResref);
            this.tabPageAreaDetails.Controls.Add(this.textBoxItemTag);
            this.tabPageAreaDetails.Controls.Add(this.textBoxItemName);
            this.tabPageAreaDetails.Controls.Add(this.labelItemResref);
            this.tabPageAreaDetails.Controls.Add(this.labelItemTag);
            this.tabPageAreaDetails.Controls.Add(this.labelItemName);
            this.tabPageAreaDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageAreaDetails.Name = "tabPageAreaDetails";
            this.tabPageAreaDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAreaDetails.Size = new System.Drawing.Size(367, 426);
            this.tabPageAreaDetails.TabIndex = 1;
            this.tabPageAreaDetails.Text = "Details";
            this.tabPageAreaDetails.UseVisualStyleBackColor = true;
            // 
            // buttonDiscardChangesItemDetails
            // 
            this.buttonDiscardChangesItemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChangesItemDetails.Location = new System.Drawing.Point(215, 390);
            this.buttonDiscardChangesItemDetails.Name = "buttonDiscardChangesItemDetails";
            this.buttonDiscardChangesItemDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesItemDetails.TabIndex = 9;
            this.buttonDiscardChangesItemDetails.Text = "Discard Changes";
            this.buttonDiscardChangesItemDetails.UseVisualStyleBackColor = true;
            // 
            // buttonSaveChangesItemDetails
            // 
            this.buttonSaveChangesItemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChangesItemDetails.Location = new System.Drawing.Point(81, 390);
            this.buttonSaveChangesItemDetails.Name = "buttonSaveChangesItemDetails";
            this.buttonSaveChangesItemDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonSaveChangesItemDetails.TabIndex = 8;
            this.buttonSaveChangesItemDetails.Text = "Save Changes";
            this.buttonSaveChangesItemDetails.UseVisualStyleBackColor = true;
            // 
            // labelItemDetailsHeader
            // 
            this.labelItemDetailsHeader.AutoSize = true;
            this.labelItemDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDetailsHeader.Location = new System.Drawing.Point(101, 16);
            this.labelItemDetailsHeader.Name = "labelItemDetailsHeader";
            this.labelItemDetailsHeader.Size = new System.Drawing.Size(158, 31);
            this.labelItemDetailsHeader.TabIndex = 7;
            this.labelItemDetailsHeader.Text = "Item Details";
            // 
            // textBoxItemResref
            // 
            this.textBoxItemResref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemResref.Location = new System.Drawing.Point(81, 120);
            this.textBoxItemResref.Name = "textBoxItemResref";
            this.textBoxItemResref.Size = new System.Drawing.Size(265, 20);
            this.textBoxItemResref.TabIndex = 6;
            // 
            // textBoxItemTag
            // 
            this.textBoxItemTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemTag.Location = new System.Drawing.Point(81, 92);
            this.textBoxItemTag.Name = "textBoxItemTag";
            this.textBoxItemTag.Size = new System.Drawing.Size(265, 20);
            this.textBoxItemTag.TabIndex = 5;
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemName.Location = new System.Drawing.Point(81, 66);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.Size = new System.Drawing.Size(265, 20);
            this.textBoxItemName.TabIndex = 4;
            // 
            // labelItemResref
            // 
            this.labelItemResref.AutoSize = true;
            this.labelItemResref.Location = new System.Drawing.Point(6, 123);
            this.labelItemResref.Name = "labelItemResref";
            this.labelItemResref.Size = new System.Drawing.Size(41, 13);
            this.labelItemResref.TabIndex = 3;
            this.labelItemResref.Text = "Resref:";
            // 
            // labelItemTag
            // 
            this.labelItemTag.AutoSize = true;
            this.labelItemTag.Location = new System.Drawing.Point(6, 95);
            this.labelItemTag.Name = "labelItemTag";
            this.labelItemTag.Size = new System.Drawing.Size(29, 13);
            this.labelItemTag.TabIndex = 2;
            this.labelItemTag.Text = "Tag:";
            // 
            // labelItemName
            // 
            this.labelItemName.AutoSize = true;
            this.labelItemName.Location = new System.Drawing.Point(6, 68);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(38, 13);
            this.labelItemName.TabIndex = 1;
            this.labelItemName.Text = "Name:";
            // 
            // tabPageComments
            // 
            this.tabPageComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(367, 426);
            this.tabPageComments.TabIndex = 2;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // tabPageScripts
            // 
            this.tabPageScripts.Location = new System.Drawing.Point(4, 22);
            this.tabPageScripts.Name = "tabPageScripts";
            this.tabPageScripts.Size = new System.Drawing.Size(367, 426);
            this.tabPageScripts.TabIndex = 3;
            this.tabPageScripts.Text = "Scripts";
            this.tabPageScripts.UseVisualStyleBackColor = true;
            // 
            // AreaViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlProperties);
            this.Name = "AreaViewControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageViewer.ResumeLayout(false);
            this.tabPageAreaDetails.ResumeLayout(false);
            this.tabPageAreaDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageViewer;
        private System.Windows.Forms.TabPage tabPageAreaDetails;
        private System.Windows.Forms.Button buttonDiscardChangesItemDetails;
        private System.Windows.Forms.Button buttonSaveChangesItemDetails;
        private System.Windows.Forms.Label labelItemDetailsHeader;
        private System.Windows.Forms.TextBox textBoxItemResref;
        private System.Windows.Forms.TextBox textBoxItemTag;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.Panel panelAreaObjectViewer;
        private System.Windows.Forms.TabPage tabPageScripts;
        private System.Windows.Forms.TabPage tabPageComments;

    }
}
