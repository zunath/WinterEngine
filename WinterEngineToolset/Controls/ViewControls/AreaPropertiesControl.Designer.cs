namespace WinterEngine.Toolset.Controls.ViewControls
{
    partial class AreaPropertiesControl
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
            this.resrefTextBoxArea = new WinterEngine.Toolset.Controls.GenericControls.ResrefTextBox();
            this.tagTextBoxArea = new WinterEngine.Toolset.Controls.GenericControls.TagTextBox();
            this.nameTextBoxArea = new WinterEngine.Toolset.Controls.GenericControls.NameTextBox();
            this.labelAreaDetailsHeader = new System.Windows.Forms.Label();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tabPageAudio = new System.Windows.Forms.TabPage();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.tabPageComments = new System.Windows.Forms.TabPage();
            this.labelAreaComments = new System.Windows.Forms.Label();
            this.textBoxAreaComments = new System.Windows.Forms.TextBox();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.tabControlProperties.SuspendLayout();
            this.tabPageViewer.SuspendLayout();
            this.tabPageAreaDetails.SuspendLayout();
            this.tabPageComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageViewer);
            this.tabControlProperties.Controls.Add(this.tabPageAreaDetails);
            this.tabControlProperties.Controls.Add(this.tabPageAudio);
            this.tabControlProperties.Controls.Add(this.tabPageEvents);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(375, 417);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageViewer
            // 
            this.tabPageViewer.Controls.Add(this.panelAreaObjectViewer);
            this.tabPageViewer.Location = new System.Drawing.Point(4, 22);
            this.tabPageViewer.Name = "tabPageViewer";
            this.tabPageViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageViewer.Size = new System.Drawing.Size(367, 391);
            this.tabPageViewer.TabIndex = 0;
            this.tabPageViewer.Text = "Viewer";
            this.tabPageViewer.UseVisualStyleBackColor = true;
            // 
            // panelAreaObjectViewer
            // 
            this.panelAreaObjectViewer.Location = new System.Drawing.Point(0, 0);
            this.panelAreaObjectViewer.Name = "panelAreaObjectViewer";
            this.panelAreaObjectViewer.Size = new System.Drawing.Size(367, 391);
            this.panelAreaObjectViewer.TabIndex = 0;
            // 
            // tabPageAreaDetails
            // 
            this.tabPageAreaDetails.Controls.Add(this.resrefTextBoxArea);
            this.tabPageAreaDetails.Controls.Add(this.tagTextBoxArea);
            this.tabPageAreaDetails.Controls.Add(this.nameTextBoxArea);
            this.tabPageAreaDetails.Controls.Add(this.labelAreaDetailsHeader);
            this.tabPageAreaDetails.Controls.Add(this.labelItemResref);
            this.tabPageAreaDetails.Controls.Add(this.labelItemTag);
            this.tabPageAreaDetails.Controls.Add(this.labelItemName);
            this.tabPageAreaDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageAreaDetails.Name = "tabPageAreaDetails";
            this.tabPageAreaDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAreaDetails.Size = new System.Drawing.Size(367, 391);
            this.tabPageAreaDetails.TabIndex = 1;
            this.tabPageAreaDetails.Text = "Details";
            this.tabPageAreaDetails.UseVisualStyleBackColor = true;
            // 
            // resrefTextBoxArea
            // 
            this.resrefTextBoxArea.Enabled = false;
            this.resrefTextBoxArea.IsValid = false;
            this.resrefTextBoxArea.Location = new System.Drawing.Point(81, 112);
            this.resrefTextBoxArea.Name = "resrefTextBoxArea";
            this.resrefTextBoxArea.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.resrefTextBoxArea.ResrefText = "";
            this.resrefTextBoxArea.Size = new System.Drawing.Size(265, 28);
            this.resrefTextBoxArea.TabIndex = 10;
            // 
            // tagTextBoxArea
            // 
            this.tagTextBoxArea.IsValid = true;
            this.tagTextBoxArea.Location = new System.Drawing.Point(81, 86);
            this.tagTextBoxArea.Name = "tagTextBoxArea";
            this.tagTextBoxArea.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.tagTextBoxArea.Size = new System.Drawing.Size(265, 28);
            this.tagTextBoxArea.TabIndex = 9;
            this.tagTextBoxArea.TagText = "";
            // 
            // nameTextBoxArea
            // 
            this.nameTextBoxArea.IsValid = true;
            this.nameTextBoxArea.Location = new System.Drawing.Point(81, 58);
            this.nameTextBoxArea.Name = "nameTextBoxArea";
            this.nameTextBoxArea.NameText = "";
            this.nameTextBoxArea.Size = new System.Drawing.Size(265, 28);
            this.nameTextBoxArea.TabIndex = 8;
            // 
            // labelAreaDetailsHeader
            // 
            this.labelAreaDetailsHeader.AutoSize = true;
            this.labelAreaDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAreaDetailsHeader.Location = new System.Drawing.Point(101, 16);
            this.labelAreaDetailsHeader.Name = "labelAreaDetailsHeader";
            this.labelAreaDetailsHeader.Size = new System.Drawing.Size(162, 31);
            this.labelAreaDetailsHeader.TabIndex = 7;
            this.labelAreaDetailsHeader.Text = "Area Details";
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
            // tabPageAudio
            // 
            this.tabPageAudio.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudio.Name = "tabPageAudio";
            this.tabPageAudio.Size = new System.Drawing.Size(367, 391);
            this.tabPageAudio.TabIndex = 4;
            this.tabPageAudio.Text = "Audio";
            this.tabPageAudio.UseVisualStyleBackColor = true;
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Size = new System.Drawing.Size(367, 391);
            this.tabPageEvents.TabIndex = 3;
            this.tabPageEvents.Text = "Events";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // tabPageComments
            // 
            this.tabPageComments.Controls.Add(this.labelAreaComments);
            this.tabPageComments.Controls.Add(this.textBoxAreaComments);
            this.tabPageComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(367, 391);
            this.tabPageComments.TabIndex = 2;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // labelAreaComments
            // 
            this.labelAreaComments.AutoSize = true;
            this.labelAreaComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAreaComments.Location = new System.Drawing.Point(80, 16);
            this.labelAreaComments.Name = "labelAreaComments";
            this.labelAreaComments.Size = new System.Drawing.Size(209, 31);
            this.labelAreaComments.TabIndex = 14;
            this.labelAreaComments.Text = "Area Comments";
            // 
            // textBoxAreaComments
            // 
            this.textBoxAreaComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxAreaComments.MaxLength = 4000;
            this.textBoxAreaComments.Multiline = true;
            this.textBoxAreaComments.Name = "textBoxAreaComments";
            this.textBoxAreaComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAreaComments.Size = new System.Drawing.Size(361, 317);
            this.textBoxAreaComments.TabIndex = 13;
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChanges.Enabled = false;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(207, 426);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChanges.TabIndex = 13;
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
            this.buttonApplyChanges.TabIndex = 12;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // AreaPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "AreaPropertiesControl";
            this.Size = new System.Drawing.Size(375, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageViewer.ResumeLayout(false);
            this.tabPageAreaDetails.ResumeLayout(false);
            this.tabPageAreaDetails.PerformLayout();
            this.tabPageComments.ResumeLayout(false);
            this.tabPageComments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageViewer;
        private System.Windows.Forms.TabPage tabPageAreaDetails;
        private System.Windows.Forms.Label labelAreaDetailsHeader;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.Panel panelAreaObjectViewer;
        private System.Windows.Forms.TabPage tabPageEvents;
        private System.Windows.Forms.TabPage tabPageComments;
        private System.Windows.Forms.Label labelAreaComments;
        private System.Windows.Forms.TextBox textBoxAreaComments;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonApplyChanges;
        private System.Windows.Forms.TabPage tabPageAudio;
        private GenericControls.ResrefTextBox resrefTextBoxArea;
        private GenericControls.TagTextBox tagTextBoxArea;
        private GenericControls.NameTextBox nameTextBoxArea;

    }
}
