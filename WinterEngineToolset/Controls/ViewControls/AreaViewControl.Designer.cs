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
            this.tabPageItemViewer = new System.Windows.Forms.TabPage();
            this.tabPageItemDetails = new System.Windows.Forms.TabPage();
            this.buttonDiscardChangesItemDetails = new System.Windows.Forms.Button();
            this.buttonSaveChangesItemDetails = new System.Windows.Forms.Button();
            this.labelItemDetailsHeader = new System.Windows.Forms.Label();
            this.textBoxItemResref = new System.Windows.Forms.TextBox();
            this.textBoxItemTag = new System.Windows.Forms.TextBox();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.objectViewer3D1 = new WinterEngine.Toolset.Controls.XnaControls.ObjectViewer3D();
            this.tabControlProperties.SuspendLayout();
            this.tabPageItemViewer.SuspendLayout();
            this.tabPageItemDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageItemViewer);
            this.tabControlProperties.Controls.Add(this.tabPageItemDetails);
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageItemViewer
            // 
            this.tabPageItemViewer.Controls.Add(this.objectViewer3D1);
            this.tabPageItemViewer.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemViewer.Name = "tabPageItemViewer";
            this.tabPageItemViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItemViewer.Size = new System.Drawing.Size(367, 426);
            this.tabPageItemViewer.TabIndex = 0;
            this.tabPageItemViewer.Text = "Viewer";
            this.tabPageItemViewer.UseVisualStyleBackColor = true;
            // 
            // tabPageItemDetails
            // 
            this.tabPageItemDetails.Controls.Add(this.buttonDiscardChangesItemDetails);
            this.tabPageItemDetails.Controls.Add(this.buttonSaveChangesItemDetails);
            this.tabPageItemDetails.Controls.Add(this.labelItemDetailsHeader);
            this.tabPageItemDetails.Controls.Add(this.textBoxItemResref);
            this.tabPageItemDetails.Controls.Add(this.textBoxItemTag);
            this.tabPageItemDetails.Controls.Add(this.textBoxItemName);
            this.tabPageItemDetails.Controls.Add(this.labelItemResref);
            this.tabPageItemDetails.Controls.Add(this.labelItemTag);
            this.tabPageItemDetails.Controls.Add(this.labelItemName);
            this.tabPageItemDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemDetails.Name = "tabPageItemDetails";
            this.tabPageItemDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItemDetails.Size = new System.Drawing.Size(367, 426);
            this.tabPageItemDetails.TabIndex = 1;
            this.tabPageItemDetails.Text = "Details";
            this.tabPageItemDetails.UseVisualStyleBackColor = true;
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
            // objectViewer3D1
            // 
            this.objectViewer3D1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.objectViewer3D1.Location = new System.Drawing.Point(0, 1);
            this.objectViewer3D1.Name = "objectViewer3D1";
            this.objectViewer3D1.Size = new System.Drawing.Size(367, 426);
            this.objectViewer3D1.TabIndex = 0;
            // 
            // AreaViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlProperties);
            this.Name = "AreaViewControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageItemViewer.ResumeLayout(false);
            this.tabPageItemDetails.ResumeLayout(false);
            this.tabPageItemDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageItemViewer;
        private System.Windows.Forms.TabPage tabPageItemDetails;
        private System.Windows.Forms.Button buttonDiscardChangesItemDetails;
        private System.Windows.Forms.Button buttonSaveChangesItemDetails;
        private System.Windows.Forms.Label labelItemDetailsHeader;
        private System.Windows.Forms.TextBox textBoxItemResref;
        private System.Windows.Forms.TextBox textBoxItemTag;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private XnaControls.ObjectViewer3D objectViewer3D1;

    }
}
