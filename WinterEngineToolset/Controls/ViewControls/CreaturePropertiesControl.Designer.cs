namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class CreaturePropertiesControl
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
            this.tabPageCreatureAppearance = new System.Windows.Forms.TabPage();
            this.panelObjectViewer = new System.Windows.Forms.Panel();
            this.panelCreatureObjectViewer = new System.Windows.Forms.Panel();
            this.tabPageCreatureDetails = new System.Windows.Forms.TabPage();
            this.labelCreatureRace = new System.Windows.Forms.Label();
            this.listBoxCreatureRace = new System.Windows.Forms.ListBox();
            this.resrefTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.ResrefTextBox();
            this.tagTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.TagTextBox();
            this.nameTextBoxItem = new WinterEngine.Toolset.Controls.GenericControls.NameTextBox();
            this.labelItemDetailsHeader = new System.Windows.Forms.Label();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.tabPageCreatureDescription = new System.Windows.Forms.TabPage();
            this.labelCreatureDescription = new System.Windows.Forms.Label();
            this.textBoxCreatureDescription = new System.Windows.Forms.TextBox();
            this.tabPageCreatureComments = new System.Windows.Forms.TabPage();
            this.labelCreatureComments = new System.Windows.Forms.Label();
            this.textBoxCreatureComments = new System.Windows.Forms.TextBox();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.tabControlProperties.SuspendLayout();
            this.tabPageCreatureAppearance.SuspendLayout();
            this.panelObjectViewer.SuspendLayout();
            this.tabPageCreatureDetails.SuspendLayout();
            this.tabPageCreatureDescription.SuspendLayout();
            this.tabPageCreatureComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageCreatureAppearance);
            this.tabControlProperties.Controls.Add(this.tabPageCreatureDetails);
            this.tabControlProperties.Controls.Add(this.tabPageEvents);
            this.tabControlProperties.Controls.Add(this.tabPageCreatureDescription);
            this.tabControlProperties.Controls.Add(this.tabPageCreatureComments);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 417);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageCreatureAppearance
            // 
            this.tabPageCreatureAppearance.Controls.Add(this.panelObjectViewer);
            this.tabPageCreatureAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureAppearance.Name = "tabPageCreatureAppearance";
            this.tabPageCreatureAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreatureAppearance.Size = new System.Drawing.Size(367, 391);
            this.tabPageCreatureAppearance.TabIndex = 0;
            this.tabPageCreatureAppearance.Text = "Appearance";
            this.tabPageCreatureAppearance.UseVisualStyleBackColor = true;
            // 
            // panelObjectViewer
            // 
            this.panelObjectViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelObjectViewer.Controls.Add(this.panelCreatureObjectViewer);
            this.panelObjectViewer.Location = new System.Drawing.Point(0, 0);
            this.panelObjectViewer.Name = "panelObjectViewer";
            this.panelObjectViewer.Size = new System.Drawing.Size(367, 395);
            this.panelObjectViewer.TabIndex = 8;
            // 
            // panelCreatureObjectViewer
            // 
            this.panelCreatureObjectViewer.Location = new System.Drawing.Point(0, 2);
            this.panelCreatureObjectViewer.Name = "panelCreatureObjectViewer";
            this.panelCreatureObjectViewer.Size = new System.Drawing.Size(367, 391);
            this.panelCreatureObjectViewer.TabIndex = 1;
            // 
            // tabPageCreatureDetails
            // 
            this.tabPageCreatureDetails.Controls.Add(this.labelCreatureRace);
            this.tabPageCreatureDetails.Controls.Add(this.listBoxCreatureRace);
            this.tabPageCreatureDetails.Controls.Add(this.resrefTextBoxItem);
            this.tabPageCreatureDetails.Controls.Add(this.tagTextBoxItem);
            this.tabPageCreatureDetails.Controls.Add(this.nameTextBoxItem);
            this.tabPageCreatureDetails.Controls.Add(this.labelItemDetailsHeader);
            this.tabPageCreatureDetails.Controls.Add(this.labelItemResref);
            this.tabPageCreatureDetails.Controls.Add(this.labelItemTag);
            this.tabPageCreatureDetails.Controls.Add(this.labelItemName);
            this.tabPageCreatureDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureDetails.Name = "tabPageCreatureDetails";
            this.tabPageCreatureDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreatureDetails.Size = new System.Drawing.Size(367, 391);
            this.tabPageCreatureDetails.TabIndex = 1;
            this.tabPageCreatureDetails.Text = "Details";
            this.tabPageCreatureDetails.UseVisualStyleBackColor = true;
            // 
            // labelCreatureRace
            // 
            this.labelCreatureRace.AutoSize = true;
            this.labelCreatureRace.Location = new System.Drawing.Point(9, 167);
            this.labelCreatureRace.Name = "labelCreatureRace";
            this.labelCreatureRace.Size = new System.Drawing.Size(36, 13);
            this.labelCreatureRace.TabIndex = 20;
            this.labelCreatureRace.Text = "Race:";
            // 
            // listBoxCreatureRace
            // 
            this.listBoxCreatureRace.FormattingEnabled = true;
            this.listBoxCreatureRace.HorizontalScrollbar = true;
            this.listBoxCreatureRace.Location = new System.Drawing.Point(81, 146);
            this.listBoxCreatureRace.Name = "listBoxCreatureRace";
            this.listBoxCreatureRace.Size = new System.Drawing.Size(265, 69);
            this.listBoxCreatureRace.Sorted = true;
            this.listBoxCreatureRace.TabIndex = 19;
            // 
            // resrefTextBoxItem
            // 
            this.resrefTextBoxItem.Enabled = false;
            this.resrefTextBoxItem.Location = new System.Drawing.Point(81, 112);
            this.resrefTextBoxItem.Name = "resrefTextBoxItem";
            this.resrefTextBoxItem.ResrefText = "";
            this.resrefTextBoxItem.Size = new System.Drawing.Size(265, 28);
            this.resrefTextBoxItem.TabIndex = 18;
            // 
            // tagTextBoxItem
            // 
            this.tagTextBoxItem.Location = new System.Drawing.Point(81, 86);
            this.tagTextBoxItem.Name = "tagTextBoxItem";
            this.tagTextBoxItem.Size = new System.Drawing.Size(265, 28);
            this.tagTextBoxItem.TabIndex = 17;
            this.tagTextBoxItem.TagText = "";
            // 
            // nameTextBoxItem
            // 
            this.nameTextBoxItem.Location = new System.Drawing.Point(81, 58);
            this.nameTextBoxItem.Name = "nameTextBoxItem";
            this.nameTextBoxItem.NameText = "";
            this.nameTextBoxItem.Size = new System.Drawing.Size(265, 28);
            this.nameTextBoxItem.TabIndex = 16;
            // 
            // labelItemDetailsHeader
            // 
            this.labelItemDetailsHeader.AutoSize = true;
            this.labelItemDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDetailsHeader.Location = new System.Drawing.Point(75, 16);
            this.labelItemDetailsHeader.Name = "labelItemDetailsHeader";
            this.labelItemDetailsHeader.Size = new System.Drawing.Size(211, 31);
            this.labelItemDetailsHeader.TabIndex = 7;
            this.labelItemDetailsHeader.Text = "Creature Details";
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
            // tabPageEvents
            // 
            this.tabPageEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Size = new System.Drawing.Size(367, 391);
            this.tabPageEvents.TabIndex = 4;
            this.tabPageEvents.Text = "Events";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // tabPageCreatureDescription
            // 
            this.tabPageCreatureDescription.Controls.Add(this.labelCreatureDescription);
            this.tabPageCreatureDescription.Controls.Add(this.textBoxCreatureDescription);
            this.tabPageCreatureDescription.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureDescription.Name = "tabPageCreatureDescription";
            this.tabPageCreatureDescription.Size = new System.Drawing.Size(367, 391);
            this.tabPageCreatureDescription.TabIndex = 2;
            this.tabPageCreatureDescription.Text = "Description";
            this.tabPageCreatureDescription.UseVisualStyleBackColor = true;
            // 
            // labelCreatureDescription
            // 
            this.labelCreatureDescription.AutoSize = true;
            this.labelCreatureDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatureDescription.Location = new System.Drawing.Point(55, 16);
            this.labelCreatureDescription.Name = "labelCreatureDescription";
            this.labelCreatureDescription.Size = new System.Drawing.Size(264, 31);
            this.labelCreatureDescription.TabIndex = 10;
            this.labelCreatureDescription.Text = "Creature Description";
            // 
            // textBoxCreatureDescription
            // 
            this.textBoxCreatureDescription.Location = new System.Drawing.Point(3, 74);
            this.textBoxCreatureDescription.MaxLength = 4000;
            this.textBoxCreatureDescription.Multiline = true;
            this.textBoxCreatureDescription.Name = "textBoxCreatureDescription";
            this.textBoxCreatureDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCreatureDescription.Size = new System.Drawing.Size(361, 317);
            this.textBoxCreatureDescription.TabIndex = 9;
            // 
            // tabPageCreatureComments
            // 
            this.tabPageCreatureComments.Controls.Add(this.labelCreatureComments);
            this.tabPageCreatureComments.Controls.Add(this.textBoxCreatureComments);
            this.tabPageCreatureComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureComments.Name = "tabPageCreatureComments";
            this.tabPageCreatureComments.Size = new System.Drawing.Size(367, 391);
            this.tabPageCreatureComments.TabIndex = 3;
            this.tabPageCreatureComments.Text = "Comments";
            this.tabPageCreatureComments.UseVisualStyleBackColor = true;
            // 
            // labelCreatureComments
            // 
            this.labelCreatureComments.AutoSize = true;
            this.labelCreatureComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatureComments.Location = new System.Drawing.Point(55, 16);
            this.labelCreatureComments.Name = "labelCreatureComments";
            this.labelCreatureComments.Size = new System.Drawing.Size(258, 31);
            this.labelCreatureComments.TabIndex = 12;
            this.labelCreatureComments.Text = "Creature Comments";
            // 
            // textBoxCreatureComments
            // 
            this.textBoxCreatureComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxCreatureComments.MaxLength = 4000;
            this.textBoxCreatureComments.Multiline = true;
            this.textBoxCreatureComments.Name = "textBoxCreatureComments";
            this.textBoxCreatureComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCreatureComments.Size = new System.Drawing.Size(361, 317);
            this.textBoxCreatureComments.TabIndex = 11;
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChanges.Enabled = false;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(207, 426);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChanges.TabIndex = 11;
            this.buttonDiscardChanges.Text = "Discard Changes";
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            this.buttonDiscardChanges.Click += new System.EventHandler(this.buttonDiscardChanges_Click);
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonApplyChanges.Enabled = false;
            this.buttonApplyChanges.Location = new System.Drawing.Point(73, 426);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChanges.TabIndex = 10;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // CreaturePropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "CreaturePropertiesControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageCreatureAppearance.ResumeLayout(false);
            this.panelObjectViewer.ResumeLayout(false);
            this.tabPageCreatureDetails.ResumeLayout(false);
            this.tabPageCreatureDetails.PerformLayout();
            this.tabPageCreatureDescription.ResumeLayout(false);
            this.tabPageCreatureDescription.PerformLayout();
            this.tabPageCreatureComments.ResumeLayout(false);
            this.tabPageCreatureComments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageCreatureAppearance;
        private System.Windows.Forms.Panel panelObjectViewer;
        private System.Windows.Forms.TabPage tabPageCreatureDetails;
        private System.Windows.Forms.Label labelItemDetailsHeader;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.Panel panelCreatureObjectViewer;
        private System.Windows.Forms.TabPage tabPageCreatureDescription;
        private System.Windows.Forms.TabPage tabPageCreatureComments;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonApplyChanges;
        private GenericControls.ResrefTextBox resrefTextBoxItem;
        private GenericControls.TagTextBox tagTextBoxItem;
        private GenericControls.NameTextBox nameTextBoxItem;
        private System.Windows.Forms.Label labelCreatureRace;
        private System.Windows.Forms.ListBox listBoxCreatureRace;
        private System.Windows.Forms.Label labelCreatureDescription;
        private System.Windows.Forms.TextBox textBoxCreatureDescription;
        private System.Windows.Forms.Label labelCreatureComments;
        private System.Windows.Forms.TextBox textBoxCreatureComments;
        private System.Windows.Forms.TabPage tabPageEvents;
    }
}
