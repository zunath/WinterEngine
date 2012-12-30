namespace WinterEngine.Toolset.GUI.Views
{
    partial class AreaView
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
            this.tabControlAreaSubProperties = new System.Windows.Forms.TabControl();
            this.tabPagePlaceableViewer = new System.Windows.Forms.TabPage();
            this.panelAreaEditorControl = new System.Windows.Forms.Panel();
            this.tabPagePlaceableDetails = new System.Windows.Forms.TabPage();
            this.buttonDiscardChangesAreaDetails = new System.Windows.Forms.Button();
            this.buttonSaveChangesAreaDetails = new System.Windows.Forms.Button();
            this.labelAreaDetailsHeader = new System.Windows.Forms.Label();
            this.textBoxAreaResref = new System.Windows.Forms.TextBox();
            this.textBoxAreaTag = new System.Windows.Forms.TextBox();
            this.textBoxAreaName = new System.Windows.Forms.TextBox();
            this.labelAreaResref = new System.Windows.Forms.Label();
            this.labelAreaTag = new System.Windows.Forms.Label();
            this.labelAreaName = new System.Windows.Forms.Label();
            this.treeCategoryControlArea = new WinterEngine.Toolset.Controls.WinterEngineControls.TreeCategoryControl();
            this.tabControlAreaSubProperties.SuspendLayout();
            this.tabPagePlaceableViewer.SuspendLayout();
            this.tabPagePlaceableDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAreaSubProperties
            // 
            this.tabControlAreaSubProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAreaSubProperties.Controls.Add(this.tabPagePlaceableViewer);
            this.tabControlAreaSubProperties.Controls.Add(this.tabPagePlaceableDetails);
            this.tabControlAreaSubProperties.Location = new System.Drawing.Point(194, 1);
            this.tabControlAreaSubProperties.Name = "tabControlAreaSubProperties";
            this.tabControlAreaSubProperties.SelectedIndex = 0;
            this.tabControlAreaSubProperties.Size = new System.Drawing.Size(375, 452);
            this.tabControlAreaSubProperties.TabIndex = 4;
            // 
            // tabPagePlaceableViewer
            // 
            this.tabPagePlaceableViewer.Controls.Add(this.panelAreaEditorControl);
            this.tabPagePlaceableViewer.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceableViewer.Name = "tabPagePlaceableViewer";
            this.tabPagePlaceableViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlaceableViewer.Size = new System.Drawing.Size(367, 426);
            this.tabPagePlaceableViewer.TabIndex = 0;
            this.tabPagePlaceableViewer.Text = "Viewer";
            this.tabPagePlaceableViewer.UseVisualStyleBackColor = true;
            // 
            // panelAreaEditorControl
            // 
            this.panelAreaEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAreaEditorControl.Location = new System.Drawing.Point(0, 0);
            this.panelAreaEditorControl.Name = "panelAreaEditorControl";
            this.panelAreaEditorControl.Size = new System.Drawing.Size(367, 430);
            this.panelAreaEditorControl.TabIndex = 8;
            // 
            // tabPagePlaceableDetails
            // 
            this.tabPagePlaceableDetails.Controls.Add(this.buttonDiscardChangesAreaDetails);
            this.tabPagePlaceableDetails.Controls.Add(this.buttonSaveChangesAreaDetails);
            this.tabPagePlaceableDetails.Controls.Add(this.labelAreaDetailsHeader);
            this.tabPagePlaceableDetails.Controls.Add(this.textBoxAreaResref);
            this.tabPagePlaceableDetails.Controls.Add(this.textBoxAreaTag);
            this.tabPagePlaceableDetails.Controls.Add(this.textBoxAreaName);
            this.tabPagePlaceableDetails.Controls.Add(this.labelAreaResref);
            this.tabPagePlaceableDetails.Controls.Add(this.labelAreaTag);
            this.tabPagePlaceableDetails.Controls.Add(this.labelAreaName);
            this.tabPagePlaceableDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlaceableDetails.Name = "tabPagePlaceableDetails";
            this.tabPagePlaceableDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlaceableDetails.Size = new System.Drawing.Size(367, 426);
            this.tabPagePlaceableDetails.TabIndex = 1;
            this.tabPagePlaceableDetails.Text = "Details";
            this.tabPagePlaceableDetails.UseVisualStyleBackColor = true;
            // 
            // buttonDiscardChangesAreaDetails
            // 
            this.buttonDiscardChangesAreaDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChangesAreaDetails.Location = new System.Drawing.Point(215, 390);
            this.buttonDiscardChangesAreaDetails.Name = "buttonDiscardChangesAreaDetails";
            this.buttonDiscardChangesAreaDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesAreaDetails.TabIndex = 9;
            this.buttonDiscardChangesAreaDetails.Text = "Discard Changes";
            this.buttonDiscardChangesAreaDetails.UseVisualStyleBackColor = true;
            // 
            // buttonSaveChangesAreaDetails
            // 
            this.buttonSaveChangesAreaDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChangesAreaDetails.Location = new System.Drawing.Point(81, 390);
            this.buttonSaveChangesAreaDetails.Name = "buttonSaveChangesAreaDetails";
            this.buttonSaveChangesAreaDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonSaveChangesAreaDetails.TabIndex = 8;
            this.buttonSaveChangesAreaDetails.Text = "Save Changes";
            this.buttonSaveChangesAreaDetails.UseVisualStyleBackColor = true;
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
            // textBoxAreaResref
            // 
            this.textBoxAreaResref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAreaResref.Location = new System.Drawing.Point(81, 120);
            this.textBoxAreaResref.Name = "textBoxAreaResref";
            this.textBoxAreaResref.Size = new System.Drawing.Size(265, 20);
            this.textBoxAreaResref.TabIndex = 6;
            // 
            // textBoxAreaTag
            // 
            this.textBoxAreaTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAreaTag.Location = new System.Drawing.Point(81, 92);
            this.textBoxAreaTag.Name = "textBoxAreaTag";
            this.textBoxAreaTag.Size = new System.Drawing.Size(265, 20);
            this.textBoxAreaTag.TabIndex = 5;
            // 
            // textBoxAreaName
            // 
            this.textBoxAreaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAreaName.Location = new System.Drawing.Point(81, 66);
            this.textBoxAreaName.Name = "textBoxAreaName";
            this.textBoxAreaName.Size = new System.Drawing.Size(265, 20);
            this.textBoxAreaName.TabIndex = 4;
            // 
            // labelAreaResref
            // 
            this.labelAreaResref.AutoSize = true;
            this.labelAreaResref.Location = new System.Drawing.Point(6, 123);
            this.labelAreaResref.Name = "labelAreaResref";
            this.labelAreaResref.Size = new System.Drawing.Size(41, 13);
            this.labelAreaResref.TabIndex = 3;
            this.labelAreaResref.Text = "Resref:";
            // 
            // labelAreaTag
            // 
            this.labelAreaTag.AutoSize = true;
            this.labelAreaTag.Location = new System.Drawing.Point(6, 95);
            this.labelAreaTag.Name = "labelAreaTag";
            this.labelAreaTag.Size = new System.Drawing.Size(29, 13);
            this.labelAreaTag.TabIndex = 2;
            this.labelAreaTag.Text = "Tag:";
            // 
            // labelAreaName
            // 
            this.labelAreaName.AutoSize = true;
            this.labelAreaName.Location = new System.Drawing.Point(6, 68);
            this.labelAreaName.Name = "labelAreaName";
            this.labelAreaName.Size = new System.Drawing.Size(38, 13);
            this.labelAreaName.TabIndex = 1;
            this.labelAreaName.Text = "Name:";
            // 
            // treeCategoryControlArea
            // 
            this.treeCategoryControlArea.Location = new System.Drawing.Point(2, 4);
            this.treeCategoryControlArea.Name = "treeCategoryControlArea";
            this.treeCategoryControlArea.Size = new System.Drawing.Size(194, 449);
            this.treeCategoryControlArea.TabIndex = 5;
            this.treeCategoryControlArea.WinterObjectResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Area;
            // 
            // AreaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeCategoryControlArea);
            this.Controls.Add(this.tabControlAreaSubProperties);
            this.Name = "AreaView";
            this.Size = new System.Drawing.Size(570, 455);
            this.tabControlAreaSubProperties.ResumeLayout(false);
            this.tabPagePlaceableViewer.ResumeLayout(false);
            this.tabPagePlaceableDetails.ResumeLayout(false);
            this.tabPagePlaceableDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinterEngineControls.TreeCategoryControl treeCategoryControlArea;
        private System.Windows.Forms.TabControl tabControlAreaSubProperties;
        private System.Windows.Forms.TabPage tabPagePlaceableViewer;
        private System.Windows.Forms.Panel panelAreaEditorControl;
        private System.Windows.Forms.TabPage tabPagePlaceableDetails;
        private System.Windows.Forms.Button buttonDiscardChangesAreaDetails;
        private System.Windows.Forms.Button buttonSaveChangesAreaDetails;
        private System.Windows.Forms.Label labelAreaDetailsHeader;
        private System.Windows.Forms.TextBox textBoxAreaResref;
        private System.Windows.Forms.TextBox textBoxAreaTag;
        private System.Windows.Forms.TextBox textBoxAreaName;
        private System.Windows.Forms.Label labelAreaResref;
        private System.Windows.Forms.Label labelAreaTag;
        private System.Windows.Forms.Label labelAreaName;

    }
}
