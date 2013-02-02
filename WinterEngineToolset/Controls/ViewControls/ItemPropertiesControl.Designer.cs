namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class ItemPropertiesControl
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
            this.buttonDiscardChangesItemDetails = new System.Windows.Forms.Button();
            this.buttonApplyChangesItemDetails = new System.Windows.Forms.Button();
            this.tabPageComments = new System.Windows.Forms.TabPage();
            this.textBoxItemComments = new System.Windows.Forms.TextBox();
            this.labelItemComments = new System.Windows.Forms.Label();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.textBoxItemDescription = new System.Windows.Forms.TextBox();
            this.tabPageItemProperties = new System.Windows.Forms.TabPage();
            this.labelAssignedItemProperties = new System.Windows.Forms.Label();
            this.labelAvailableItemProperties = new System.Windows.Forms.Label();
            this.listBoxAssignedItemProperties = new System.Windows.Forms.ListBox();
            this.listBoxAvailableItemProperties = new System.Windows.Forms.ListBox();
            this.labelItemProperties = new System.Windows.Forms.Label();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.tabPageItemDetails = new System.Windows.Forms.TabPage();
            this.resrefTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.ResrefTextBox();
            this.tagTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.TagTextBox();
            this.nameTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.NameTextBox();
            this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
            this.labelWeight = new System.Windows.Forms.Label();
            this.numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            this.labelItemPrice = new System.Windows.Forms.Label();
            this.labelItemType = new System.Windows.Forms.Label();
            this.listBoxItemType = new System.Windows.Forms.ListBox();
            this.labelItemDetailsHeader = new System.Windows.Forms.Label();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageComments.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageItemProperties.SuspendLayout();
            this.tabPageItemDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).BeginInit();
            this.tabControlProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDiscardChangesItemDetails
            // 
            this.buttonDiscardChangesItemDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDiscardChangesItemDetails.Enabled = false;
            this.buttonDiscardChangesItemDetails.Location = new System.Drawing.Point(151, 426);
            this.buttonDiscardChangesItemDetails.Name = "buttonDiscardChangesItemDetails";
            this.buttonDiscardChangesItemDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesItemDetails.TabIndex = 9;
            this.buttonDiscardChangesItemDetails.Text = "Discard Changes";
            this.buttonDiscardChangesItemDetails.UseVisualStyleBackColor = true;
            this.buttonDiscardChangesItemDetails.Click += new System.EventHandler(this.buttonDiscardChangesItemDetails_Click);
            // 
            // buttonApplyChangesItemDetails
            // 
            this.buttonApplyChangesItemDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonApplyChangesItemDetails.Enabled = false;
            this.buttonApplyChangesItemDetails.Location = new System.Drawing.Point(35, 426);
            this.buttonApplyChangesItemDetails.Name = "buttonApplyChangesItemDetails";
            this.buttonApplyChangesItemDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChangesItemDetails.TabIndex = 8;
            this.buttonApplyChangesItemDetails.Text = "Apply Changes";
            this.buttonApplyChangesItemDetails.UseVisualStyleBackColor = true;
            this.buttonApplyChangesItemDetails.Click += new System.EventHandler(this.buttonSaveChangesItemDetails_Click);
            // 
            // tabPageComments
            // 
            this.tabPageComments.Controls.Add(this.textBoxItemComments);
            this.tabPageComments.Controls.Add(this.labelItemComments);
            this.tabPageComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(300, 391);
            this.tabPageComments.TabIndex = 4;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // textBoxItemComments
            // 
            this.textBoxItemComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxItemComments.MaxLength = 4000;
            this.textBoxItemComments.Multiline = true;
            this.textBoxItemComments.Name = "textBoxItemComments";
            this.textBoxItemComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxItemComments.Size = new System.Drawing.Size(291, 317);
            this.textBoxItemComments.TabIndex = 10;
            // 
            // labelItemComments
            // 
            this.labelItemComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemComments.AutoSize = true;
            this.labelItemComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemComments.Location = new System.Drawing.Point(51, 16);
            this.labelItemComments.Name = "labelItemComments";
            this.labelItemComments.Size = new System.Drawing.Size(205, 31);
            this.labelItemComments.TabIndex = 9;
            this.labelItemComments.Text = "Item Comments";
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.Controls.Add(this.labelItemDescription);
            this.tabPageDescription.Controls.Add(this.textBoxItemDescription);
            this.tabPageDescription.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(300, 391);
            this.tabPageDescription.TabIndex = 3;
            this.tabPageDescription.Text = "Description";
            this.tabPageDescription.UseVisualStyleBackColor = true;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDescription.AutoSize = true;
            this.labelItemDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDescription.Location = new System.Drawing.Point(43, 16);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(211, 31);
            this.labelItemDescription.TabIndex = 8;
            this.labelItemDescription.Text = "Item Description";
            // 
            // textBoxItemDescription
            // 
            this.textBoxItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemDescription.Location = new System.Drawing.Point(3, 74);
            this.textBoxItemDescription.MaxLength = 4000;
            this.textBoxItemDescription.Multiline = true;
            this.textBoxItemDescription.Name = "textBoxItemDescription";
            this.textBoxItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxItemDescription.Size = new System.Drawing.Size(291, 317);
            this.textBoxItemDescription.TabIndex = 0;
            // 
            // tabPageItemProperties
            // 
            this.tabPageItemProperties.Controls.Add(this.labelAssignedItemProperties);
            this.tabPageItemProperties.Controls.Add(this.labelAvailableItemProperties);
            this.tabPageItemProperties.Controls.Add(this.listBoxAssignedItemProperties);
            this.tabPageItemProperties.Controls.Add(this.listBoxAvailableItemProperties);
            this.tabPageItemProperties.Controls.Add(this.labelItemProperties);
            this.tabPageItemProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemProperties.Name = "tabPageItemProperties";
            this.tabPageItemProperties.Size = new System.Drawing.Size(300, 391);
            this.tabPageItemProperties.TabIndex = 2;
            this.tabPageItemProperties.Text = "Properties";
            this.tabPageItemProperties.UseVisualStyleBackColor = true;
            // 
            // labelAssignedItemProperties
            // 
            this.labelAssignedItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssignedItemProperties.AutoSize = true;
            this.labelAssignedItemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssignedItemProperties.Location = new System.Drawing.Point(158, 62);
            this.labelAssignedItemProperties.Name = "labelAssignedItemProperties";
            this.labelAssignedItemProperties.Size = new System.Drawing.Size(79, 20);
            this.labelAssignedItemProperties.TabIndex = 12;
            this.labelAssignedItemProperties.Text = "Assigned:";
            // 
            // labelAvailableItemProperties
            // 
            this.labelAvailableItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAvailableItemProperties.AutoSize = true;
            this.labelAvailableItemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAvailableItemProperties.Location = new System.Drawing.Point(14, 62);
            this.labelAvailableItemProperties.Name = "labelAvailableItemProperties";
            this.labelAvailableItemProperties.Size = new System.Drawing.Size(76, 20);
            this.labelAvailableItemProperties.TabIndex = 11;
            this.labelAvailableItemProperties.Text = "Available:";
            // 
            // listBoxAssignedItemProperties
            // 
            this.listBoxAssignedItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAssignedItemProperties.FormattingEnabled = true;
            this.listBoxAssignedItemProperties.HorizontalScrollbar = true;
            this.listBoxAssignedItemProperties.Location = new System.Drawing.Point(153, 85);
            this.listBoxAssignedItemProperties.Name = "listBoxAssignedItemProperties";
            this.listBoxAssignedItemProperties.Size = new System.Drawing.Size(144, 303);
            this.listBoxAssignedItemProperties.TabIndex = 10;
            // 
            // listBoxAvailableItemProperties
            // 
            this.listBoxAvailableItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAvailableItemProperties.FormattingEnabled = true;
            this.listBoxAvailableItemProperties.HorizontalScrollbar = true;
            this.listBoxAvailableItemProperties.Location = new System.Drawing.Point(3, 87);
            this.listBoxAvailableItemProperties.Name = "listBoxAvailableItemProperties";
            this.listBoxAvailableItemProperties.Size = new System.Drawing.Size(144, 303);
            this.listBoxAvailableItemProperties.TabIndex = 9;
            // 
            // labelItemProperties
            // 
            this.labelItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemProperties.AutoSize = true;
            this.labelItemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemProperties.Location = new System.Drawing.Point(55, 16);
            this.labelItemProperties.Name = "labelItemProperties";
            this.labelItemProperties.Size = new System.Drawing.Size(198, 31);
            this.labelItemProperties.TabIndex = 8;
            this.labelItemProperties.Text = "Item Properties";
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Size = new System.Drawing.Size(300, 391);
            this.tabPageEvents.TabIndex = 5;
            this.tabPageEvents.Text = "Events";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // tabPageItemDetails
            // 
            this.tabPageItemDetails.Controls.Add(this.resrefTextBoxItem);
            this.tabPageItemDetails.Controls.Add(this.tagTextBoxItem);
            this.tabPageItemDetails.Controls.Add(this.nameTextBoxItem);
            this.tabPageItemDetails.Controls.Add(this.numericUpDownWeight);
            this.tabPageItemDetails.Controls.Add(this.labelWeight);
            this.tabPageItemDetails.Controls.Add(this.numericUpDownPrice);
            this.tabPageItemDetails.Controls.Add(this.labelItemPrice);
            this.tabPageItemDetails.Controls.Add(this.labelItemType);
            this.tabPageItemDetails.Controls.Add(this.listBoxItemType);
            this.tabPageItemDetails.Controls.Add(this.labelItemDetailsHeader);
            this.tabPageItemDetails.Controls.Add(this.labelItemResref);
            this.tabPageItemDetails.Controls.Add(this.labelItemTag);
            this.tabPageItemDetails.Controls.Add(this.labelItemName);
            this.tabPageItemDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemDetails.Name = "tabPageItemDetails";
            this.tabPageItemDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItemDetails.Size = new System.Drawing.Size(300, 391);
            this.tabPageItemDetails.TabIndex = 1;
            this.tabPageItemDetails.Text = "Details";
            this.tabPageItemDetails.UseVisualStyleBackColor = true;
            // 
            // resrefTextBoxItem
            // 
            this.resrefTextBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resrefTextBoxItem.Enabled = false;
            this.resrefTextBoxItem.IsValid = false;
            this.resrefTextBoxItem.Location = new System.Drawing.Point(81, 112);
            this.resrefTextBoxItem.Name = "resrefTextBoxItem";
            this.resrefTextBoxItem.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.resrefTextBoxItem.ResrefText = "";
            this.resrefTextBoxItem.Size = new System.Drawing.Size(216, 28);
            this.resrefTextBoxItem.TabIndex = 15;
            // 
            // tagTextBoxItem
            // 
            this.tagTextBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTextBoxItem.IsValid = true;
            this.tagTextBoxItem.Location = new System.Drawing.Point(81, 86);
            this.tagTextBoxItem.Name = "tagTextBoxItem";
            this.tagTextBoxItem.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.tagTextBoxItem.Size = new System.Drawing.Size(216, 28);
            this.tagTextBoxItem.TabIndex = 14;
            this.tagTextBoxItem.TagText = "";
            // 
            // nameTextBoxItem
            // 
            this.nameTextBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBoxItem.IsValid = false;
            this.nameTextBoxItem.Location = new System.Drawing.Point(81, 58);
            this.nameTextBoxItem.Name = "nameTextBoxItem";
            this.nameTextBoxItem.NameText = "";
            this.nameTextBoxItem.Size = new System.Drawing.Size(216, 28);
            this.nameTextBoxItem.TabIndex = 0;
            // 
            // numericUpDownWeight
            // 
            this.numericUpDownWeight.Location = new System.Drawing.Point(219, 228);
            this.numericUpDownWeight.Name = "numericUpDownWeight";
            this.numericUpDownWeight.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownWeight.TabIndex = 13;
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(172, 231);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(41, 13);
            this.labelWeight.TabIndex = 12;
            this.labelWeight.Text = "Weight";
            // 
            // numericUpDownPrice
            // 
            this.numericUpDownPrice.Location = new System.Drawing.Point(57, 228);
            this.numericUpDownPrice.Name = "numericUpDownPrice";
            this.numericUpDownPrice.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownPrice.TabIndex = 11;
            // 
            // labelItemPrice
            // 
            this.labelItemPrice.AutoSize = true;
            this.labelItemPrice.Location = new System.Drawing.Point(17, 231);
            this.labelItemPrice.Name = "labelItemPrice";
            this.labelItemPrice.Size = new System.Drawing.Size(34, 13);
            this.labelItemPrice.TabIndex = 10;
            this.labelItemPrice.Text = "Price:";
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
            // listBoxItemType
            // 
            this.listBoxItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxItemType.FormattingEnabled = true;
            this.listBoxItemType.HorizontalScrollbar = true;
            this.listBoxItemType.Location = new System.Drawing.Point(81, 146);
            this.listBoxItemType.Name = "listBoxItemType";
            this.listBoxItemType.Size = new System.Drawing.Size(216, 69);
            this.listBoxItemType.Sorted = true;
            this.listBoxItemType.TabIndex = 8;
            // 
            // labelItemDetailsHeader
            // 
            this.labelItemDetailsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDetailsHeader.AutoSize = true;
            this.labelItemDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDetailsHeader.Location = new System.Drawing.Point(69, 16);
            this.labelItemDetailsHeader.Name = "labelItemDetailsHeader";
            this.labelItemDetailsHeader.Size = new System.Drawing.Size(158, 31);
            this.labelItemDetailsHeader.TabIndex = 7;
            this.labelItemDetailsHeader.Text = "Item Details";
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
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageItemDetails);
            this.tabControlProperties.Controls.Add(this.tabPageEvents);
            this.tabControlProperties.Controls.Add(this.tabPageItemProperties);
            this.tabControlProperties.Controls.Add(this.tabPageDescription);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(308, 417);
            this.tabControlProperties.TabIndex = 2;
            // 
            // ItemPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChangesItemDetails);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.buttonApplyChangesItemDetails);
            this.Name = "ItemPropertiesControl";
            this.Size = new System.Drawing.Size(308, 452);
            this.Load += new System.EventHandler(this.ItemPropertiesControl_Load);
            this.tabPageComments.ResumeLayout(false);
            this.tabPageComments.PerformLayout();
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageDescription.PerformLayout();
            this.tabPageItemProperties.ResumeLayout(false);
            this.tabPageItemProperties.PerformLayout();
            this.tabPageItemDetails.ResumeLayout(false);
            this.tabPageItemDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).EndInit();
            this.tabControlProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDiscardChangesItemDetails;
        private System.Windows.Forms.Button buttonApplyChangesItemDetails;
        private System.Windows.Forms.TabPage tabPageComments;
        private System.Windows.Forms.TextBox textBoxItemComments;
        private System.Windows.Forms.Label labelItemComments;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.Label labelItemDescription;
        private System.Windows.Forms.TextBox textBoxItemDescription;
        private System.Windows.Forms.TabPage tabPageItemProperties;
        private System.Windows.Forms.Label labelAssignedItemProperties;
        private System.Windows.Forms.Label labelAvailableItemProperties;
        private System.Windows.Forms.ListBox listBoxAssignedItemProperties;
        private System.Windows.Forms.ListBox listBoxAvailableItemProperties;
        private System.Windows.Forms.Label labelItemProperties;
        private System.Windows.Forms.TabPage tabPageEvents;
        private System.Windows.Forms.TabPage tabPageItemDetails;
        private GenericControls.ResrefTextBox resrefTextBoxItem;
        private GenericControls.TagTextBox tagTextBoxItem;
        private GenericControls.NameTextBox nameTextBoxItem;
        private System.Windows.Forms.NumericUpDown numericUpDownWeight;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.NumericUpDown numericUpDownPrice;
        private System.Windows.Forms.Label labelItemPrice;
        private System.Windows.Forms.Label labelItemType;
        private System.Windows.Forms.ListBox listBoxItemType;
        private System.Windows.Forms.Label labelItemDetailsHeader;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.TabControl tabControlProperties;
    }
}
