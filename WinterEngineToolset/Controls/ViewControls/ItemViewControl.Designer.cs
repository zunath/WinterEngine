﻿namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class ItemViewControl
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
            this.tabPageItemGraphics = new System.Windows.Forms.TabPage();
            this.panelItemEditorControl = new System.Windows.Forms.Panel();
            this.panelItemModelViewer = new System.Windows.Forms.Panel();
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
            this.tabPageItemProperties = new System.Windows.Forms.TabPage();
            this.panelItemIconViewer = new System.Windows.Forms.Panel();
            this.labelModelSelection = new System.Windows.Forms.Label();
            this.labelItemIcon = new System.Windows.Forms.Label();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.tabPageComments = new System.Windows.Forms.TabPage();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelItemProperties = new System.Windows.Forms.Label();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.labelItemComments = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.listBoxModels = new System.Windows.Forms.ListBox();
            this.listBoxIcons = new System.Windows.Forms.ListBox();
            this.listBoxItemType = new System.Windows.Forms.ListBox();
            this.labelItemType = new System.Windows.Forms.Label();
            this.labelItemPrice = new System.Windows.Forms.Label();
            this.numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelWeight = new System.Windows.Forms.Label();
            this.tabControlProperties.SuspendLayout();
            this.tabPageItemGraphics.SuspendLayout();
            this.panelItemEditorControl.SuspendLayout();
            this.tabPageItemDetails.SuspendLayout();
            this.tabPageItemProperties.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageItemGraphics);
            this.tabControlProperties.Controls.Add(this.tabPageItemDetails);
            this.tabControlProperties.Controls.Add(this.tabPageItemProperties);
            this.tabControlProperties.Controls.Add(this.tabPageDescription);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 417);
            this.tabControlProperties.TabIndex = 2;
            // 
            // tabPageItemGraphics
            // 
            this.tabPageItemGraphics.Controls.Add(this.panelItemEditorControl);
            this.tabPageItemGraphics.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemGraphics.Name = "tabPageItemGraphics";
            this.tabPageItemGraphics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItemGraphics.Size = new System.Drawing.Size(367, 391);
            this.tabPageItemGraphics.TabIndex = 0;
            this.tabPageItemGraphics.Text = "Graphics";
            this.tabPageItemGraphics.UseVisualStyleBackColor = true;
            // 
            // panelItemEditorControl
            // 
            this.panelItemEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelItemEditorControl.Controls.Add(this.listBoxIcons);
            this.panelItemEditorControl.Controls.Add(this.listBoxModels);
            this.panelItemEditorControl.Controls.Add(this.labelItemIcon);
            this.panelItemEditorControl.Controls.Add(this.labelModelSelection);
            this.panelItemEditorControl.Controls.Add(this.panelItemIconViewer);
            this.panelItemEditorControl.Controls.Add(this.panelItemModelViewer);
            this.panelItemEditorControl.Location = new System.Drawing.Point(0, 0);
            this.panelItemEditorControl.Name = "panelItemEditorControl";
            this.panelItemEditorControl.Size = new System.Drawing.Size(367, 395);
            this.panelItemEditorControl.TabIndex = 8;
            // 
            // panelItemModelViewer
            // 
            this.panelItemModelViewer.Location = new System.Drawing.Point(145, 41);
            this.panelItemModelViewer.Name = "panelItemModelViewer";
            this.panelItemModelViewer.Size = new System.Drawing.Size(220, 173);
            this.panelItemModelViewer.TabIndex = 1;
            // 
            // tabPageItemDetails
            // 
            this.tabPageItemDetails.Controls.Add(this.numericUpDown1);
            this.tabPageItemDetails.Controls.Add(this.labelWeight);
            this.tabPageItemDetails.Controls.Add(this.numericUpDownPrice);
            this.tabPageItemDetails.Controls.Add(this.labelItemPrice);
            this.tabPageItemDetails.Controls.Add(this.labelItemType);
            this.tabPageItemDetails.Controls.Add(this.listBoxItemType);
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
            this.tabPageItemDetails.Size = new System.Drawing.Size(367, 391);
            this.tabPageItemDetails.TabIndex = 1;
            this.tabPageItemDetails.Text = "Details";
            this.tabPageItemDetails.UseVisualStyleBackColor = true;
            // 
            // buttonDiscardChangesItemDetails
            // 
            this.buttonDiscardChangesItemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChangesItemDetails.Location = new System.Drawing.Point(207, 426);
            this.buttonDiscardChangesItemDetails.Name = "buttonDiscardChangesItemDetails";
            this.buttonDiscardChangesItemDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesItemDetails.TabIndex = 9;
            this.buttonDiscardChangesItemDetails.Text = "Discard Changes";
            this.buttonDiscardChangesItemDetails.UseVisualStyleBackColor = true;
            this.buttonDiscardChangesItemDetails.Click += new System.EventHandler(this.buttonDiscardChangesItemDetails_Click);
            // 
            // buttonSaveChangesItemDetails
            // 
            this.buttonSaveChangesItemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChangesItemDetails.Location = new System.Drawing.Point(73, 426);
            this.buttonSaveChangesItemDetails.Name = "buttonSaveChangesItemDetails";
            this.buttonSaveChangesItemDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonSaveChangesItemDetails.TabIndex = 8;
            this.buttonSaveChangesItemDetails.Text = "Save Changes";
            this.buttonSaveChangesItemDetails.UseVisualStyleBackColor = true;
            this.buttonSaveChangesItemDetails.Click += new System.EventHandler(this.buttonSaveChangesItemDetails_Click);
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
            // tabPageItemProperties
            // 
            this.tabPageItemProperties.Controls.Add(this.labelItemProperties);
            this.tabPageItemProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemProperties.Name = "tabPageItemProperties";
            this.tabPageItemProperties.Size = new System.Drawing.Size(367, 391);
            this.tabPageItemProperties.TabIndex = 2;
            this.tabPageItemProperties.Text = "Properties";
            this.tabPageItemProperties.UseVisualStyleBackColor = true;
            // 
            // panelItemIconViewer
            // 
            this.panelItemIconViewer.Location = new System.Drawing.Point(147, 254);
            this.panelItemIconViewer.Name = "panelItemIconViewer";
            this.panelItemIconViewer.Size = new System.Drawing.Size(220, 134);
            this.panelItemIconViewer.TabIndex = 2;
            // 
            // labelModelSelection
            // 
            this.labelModelSelection.AutoSize = true;
            this.labelModelSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModelSelection.Location = new System.Drawing.Point(104, 14);
            this.labelModelSelection.Name = "labelModelSelection";
            this.labelModelSelection.Size = new System.Drawing.Size(99, 24);
            this.labelModelSelection.TabIndex = 5;
            this.labelModelSelection.Text = "3D Model";
            // 
            // labelItemIcon
            // 
            this.labelItemIcon.AutoSize = true;
            this.labelItemIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemIcon.Location = new System.Drawing.Point(122, 227);
            this.labelItemIcon.Name = "labelItemIcon";
            this.labelItemIcon.Size = new System.Drawing.Size(81, 24);
            this.labelItemIcon.TabIndex = 6;
            this.labelItemIcon.Text = "2D Icon";
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.Controls.Add(this.labelItemDescription);
            this.tabPageDescription.Controls.Add(this.textBoxDescription);
            this.tabPageDescription.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(367, 391);
            this.tabPageDescription.TabIndex = 3;
            this.tabPageDescription.Text = "Description";
            this.tabPageDescription.UseVisualStyleBackColor = true;
            // 
            // tabPageComments
            // 
            this.tabPageComments.Controls.Add(this.textBoxComments);
            this.tabPageComments.Controls.Add(this.labelItemComments);
            this.tabPageComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(367, 391);
            this.tabPageComments.TabIndex = 4;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(3, 74);
            this.textBoxDescription.MaxLength = 4000;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(361, 317);
            this.textBoxDescription.TabIndex = 0;
            // 
            // labelItemProperties
            // 
            this.labelItemProperties.AutoSize = true;
            this.labelItemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemProperties.Location = new System.Drawing.Point(78, 16);
            this.labelItemProperties.Name = "labelItemProperties";
            this.labelItemProperties.Size = new System.Drawing.Size(198, 31);
            this.labelItemProperties.TabIndex = 8;
            this.labelItemProperties.Text = "Item Properties";
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.AutoSize = true;
            this.labelItemDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDescription.Location = new System.Drawing.Point(76, 16);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(211, 31);
            this.labelItemDescription.TabIndex = 8;
            this.labelItemDescription.Text = "Item Description";
            // 
            // labelItemComments
            // 
            this.labelItemComments.AutoSize = true;
            this.labelItemComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemComments.Location = new System.Drawing.Point(74, 16);
            this.labelItemComments.Name = "labelItemComments";
            this.labelItemComments.Size = new System.Drawing.Size(205, 31);
            this.labelItemComments.TabIndex = 9;
            this.labelItemComments.Text = "Item Comments";
            // 
            // textBoxComments
            // 
            this.textBoxComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxComments.MaxLength = 4000;
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComments.Size = new System.Drawing.Size(361, 317);
            this.textBoxComments.TabIndex = 10;
            // 
            // listBoxModels
            // 
            this.listBoxModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxModels.FormattingEnabled = true;
            this.listBoxModels.HorizontalScrollbar = true;
            this.listBoxModels.Location = new System.Drawing.Point(3, 41);
            this.listBoxModels.Name = "listBoxModels";
            this.listBoxModels.Size = new System.Drawing.Size(136, 173);
            this.listBoxModels.Sorted = true;
            this.listBoxModels.TabIndex = 7;
            // 
            // listBoxIcons
            // 
            this.listBoxIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxIcons.FormattingEnabled = true;
            this.listBoxIcons.HorizontalScrollbar = true;
            this.listBoxIcons.Location = new System.Drawing.Point(3, 254);
            this.listBoxIcons.Name = "listBoxIcons";
            this.listBoxIcons.Size = new System.Drawing.Size(136, 134);
            this.listBoxIcons.Sorted = true;
            this.listBoxIcons.TabIndex = 8;
            // 
            // listBoxItemType
            // 
            this.listBoxItemType.FormattingEnabled = true;
            this.listBoxItemType.HorizontalScrollbar = true;
            this.listBoxItemType.Location = new System.Drawing.Point(81, 146);
            this.listBoxItemType.Name = "listBoxItemType";
            this.listBoxItemType.Size = new System.Drawing.Size(265, 69);
            this.listBoxItemType.Sorted = true;
            this.listBoxItemType.TabIndex = 8;
            // 
            // labelItemType
            // 
            this.labelItemType.AutoSize = true;
            this.labelItemType.Location = new System.Drawing.Point(9, 167);
            this.labelItemType.Name = "labelItemType";
            this.labelItemType.Size = new System.Drawing.Size(34, 13);
            this.labelItemType.TabIndex = 9;
            this.labelItemType.Text = "Type:";
            // 
            // labelItemPrice
            // 
            this.labelItemPrice.AutoSize = true;
            this.labelItemPrice.Location = new System.Drawing.Point(78, 231);
            this.labelItemPrice.Name = "labelItemPrice";
            this.labelItemPrice.Size = new System.Drawing.Size(34, 13);
            this.labelItemPrice.TabIndex = 10;
            this.labelItemPrice.Text = "Price:";
            // 
            // numericUpDownPrice
            // 
            this.numericUpDownPrice.Location = new System.Drawing.Point(118, 228);
            this.numericUpDownPrice.Name = "numericUpDownPrice";
            this.numericUpDownPrice.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownPrice.TabIndex = 11;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(280, 228);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(62, 20);
            this.numericUpDown1.TabIndex = 13;
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(233, 231);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(41, 13);
            this.labelWeight.TabIndex = 12;
            this.labelWeight.Text = "Weight";
            // 
            // ItemViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChangesItemDetails);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.buttonSaveChangesItemDetails);
            this.Name = "ItemViewControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageItemGraphics.ResumeLayout(false);
            this.panelItemEditorControl.ResumeLayout(false);
            this.panelItemEditorControl.PerformLayout();
            this.tabPageItemDetails.ResumeLayout(false);
            this.tabPageItemDetails.PerformLayout();
            this.tabPageItemProperties.ResumeLayout(false);
            this.tabPageItemProperties.PerformLayout();
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageDescription.PerformLayout();
            this.tabPageComments.ResumeLayout(false);
            this.tabPageComments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageItemGraphics;
        private System.Windows.Forms.Panel panelItemEditorControl;
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
        private System.Windows.Forms.TabPage tabPageItemProperties;
        private System.Windows.Forms.Panel panelItemModelViewer;
        private System.Windows.Forms.Label labelItemIcon;
        private System.Windows.Forms.Label labelModelSelection;
        private System.Windows.Forms.Panel panelItemIconViewer;
        private System.Windows.Forms.Label labelItemProperties;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.Label labelItemDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TabPage tabPageComments;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label labelItemComments;
        private System.Windows.Forms.ListBox listBoxIcons;
        private System.Windows.Forms.ListBox listBoxModels;
        private System.Windows.Forms.Label labelItemType;
        private System.Windows.Forms.ListBox listBoxItemType;
        private System.Windows.Forms.NumericUpDown numericUpDownPrice;
        private System.Windows.Forms.Label labelItemPrice;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label labelWeight;
    }
}
