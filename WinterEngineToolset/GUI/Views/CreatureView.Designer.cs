namespace WinterEngine.Toolset.GUI.Views
{
    partial class CreatureView
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Creatures");
            this.tabControlCreatureSubProperties = new System.Windows.Forms.TabControl();
            this.tabPageCreatureViewer = new System.Windows.Forms.TabPage();
            this.panelCreatureEditorControl = new System.Windows.Forms.Panel();
            this.tabPageCreatureDetails = new System.Windows.Forms.TabPage();
            this.buttonDiscardChangesCreatureDetails = new System.Windows.Forms.Button();
            this.buttonSaveChangesCreatureDetails = new System.Windows.Forms.Button();
            this.labelCreatureDetailsHeader = new System.Windows.Forms.Label();
            this.textBoxCreatureResref = new System.Windows.Forms.TextBox();
            this.textBoxCreatureTag = new System.Windows.Forms.TextBox();
            this.textBoxCreatureName = new System.Windows.Forms.TextBox();
            this.labelCreatureResref = new System.Windows.Forms.Label();
            this.labelCreatureTag = new System.Windows.Forms.Label();
            this.labelCreatureName = new System.Windows.Forms.Label();
            this.treeViewCreatures = new System.Windows.Forms.TreeView();
            this.buttonAddCreatureCategory = new WinterEngine.Toolset.Controls.WinterEngineControls.TreeCategoryControl();
            this.tabControlCreatureSubProperties.SuspendLayout();
            this.tabPageCreatureViewer.SuspendLayout();
            this.tabPageCreatureDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlCreatureSubProperties
            // 
            this.tabControlCreatureSubProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCreatureSubProperties.Controls.Add(this.tabPageCreatureViewer);
            this.tabControlCreatureSubProperties.Controls.Add(this.tabPageCreatureDetails);
            this.tabControlCreatureSubProperties.Location = new System.Drawing.Point(192, 0);
            this.tabControlCreatureSubProperties.Name = "tabControlCreatureSubProperties";
            this.tabControlCreatureSubProperties.SelectedIndex = 0;
            this.tabControlCreatureSubProperties.Size = new System.Drawing.Size(375, 452);
            this.tabControlCreatureSubProperties.TabIndex = 1;
            // 
            // tabPageCreatureViewer
            // 
            this.tabPageCreatureViewer.Controls.Add(this.panelCreatureEditorControl);
            this.tabPageCreatureViewer.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureViewer.Name = "tabPageCreatureViewer";
            this.tabPageCreatureViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreatureViewer.Size = new System.Drawing.Size(367, 426);
            this.tabPageCreatureViewer.TabIndex = 0;
            this.tabPageCreatureViewer.Text = "Viewer";
            this.tabPageCreatureViewer.UseVisualStyleBackColor = true;
            // 
            // panelCreatureEditorControl
            // 
            this.panelCreatureEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCreatureEditorControl.Location = new System.Drawing.Point(0, 0);
            this.panelCreatureEditorControl.Name = "panelCreatureEditorControl";
            this.panelCreatureEditorControl.Size = new System.Drawing.Size(367, 430);
            this.panelCreatureEditorControl.TabIndex = 8;
            // 
            // tabPageCreatureDetails
            // 
            this.tabPageCreatureDetails.Controls.Add(this.buttonDiscardChangesCreatureDetails);
            this.tabPageCreatureDetails.Controls.Add(this.buttonSaveChangesCreatureDetails);
            this.tabPageCreatureDetails.Controls.Add(this.labelCreatureDetailsHeader);
            this.tabPageCreatureDetails.Controls.Add(this.textBoxCreatureResref);
            this.tabPageCreatureDetails.Controls.Add(this.textBoxCreatureTag);
            this.tabPageCreatureDetails.Controls.Add(this.textBoxCreatureName);
            this.tabPageCreatureDetails.Controls.Add(this.labelCreatureResref);
            this.tabPageCreatureDetails.Controls.Add(this.labelCreatureTag);
            this.tabPageCreatureDetails.Controls.Add(this.labelCreatureName);
            this.tabPageCreatureDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreatureDetails.Name = "tabPageCreatureDetails";
            this.tabPageCreatureDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreatureDetails.Size = new System.Drawing.Size(367, 426);
            this.tabPageCreatureDetails.TabIndex = 1;
            this.tabPageCreatureDetails.Text = "Details";
            this.tabPageCreatureDetails.UseVisualStyleBackColor = true;
            // 
            // buttonDiscardChangesCreatureDetails
            // 
            this.buttonDiscardChangesCreatureDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDiscardChangesCreatureDetails.Location = new System.Drawing.Point(215, 390);
            this.buttonDiscardChangesCreatureDetails.Name = "buttonDiscardChangesCreatureDetails";
            this.buttonDiscardChangesCreatureDetails.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChangesCreatureDetails.TabIndex = 9;
            this.buttonDiscardChangesCreatureDetails.Text = "Discard Changes";
            this.buttonDiscardChangesCreatureDetails.UseVisualStyleBackColor = true;
            // 
            // buttonSaveChangesCreatureDetails
            // 
            this.buttonSaveChangesCreatureDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveChangesCreatureDetails.Location = new System.Drawing.Point(81, 390);
            this.buttonSaveChangesCreatureDetails.Name = "buttonSaveChangesCreatureDetails";
            this.buttonSaveChangesCreatureDetails.Size = new System.Drawing.Size(91, 23);
            this.buttonSaveChangesCreatureDetails.TabIndex = 8;
            this.buttonSaveChangesCreatureDetails.Text = "Save Changes";
            this.buttonSaveChangesCreatureDetails.UseVisualStyleBackColor = true;
            // 
            // labelCreatureDetailsHeader
            // 
            this.labelCreatureDetailsHeader.AutoSize = true;
            this.labelCreatureDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatureDetailsHeader.Location = new System.Drawing.Point(101, 16);
            this.labelCreatureDetailsHeader.Name = "labelCreatureDetailsHeader";
            this.labelCreatureDetailsHeader.Size = new System.Drawing.Size(211, 31);
            this.labelCreatureDetailsHeader.TabIndex = 7;
            this.labelCreatureDetailsHeader.Text = "Creature Details";
            // 
            // textBoxCreatureResref
            // 
            this.textBoxCreatureResref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreatureResref.Location = new System.Drawing.Point(81, 120);
            this.textBoxCreatureResref.Name = "textBoxCreatureResref";
            this.textBoxCreatureResref.Size = new System.Drawing.Size(265, 20);
            this.textBoxCreatureResref.TabIndex = 6;
            // 
            // textBoxCreatureTag
            // 
            this.textBoxCreatureTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreatureTag.Location = new System.Drawing.Point(81, 92);
            this.textBoxCreatureTag.Name = "textBoxCreatureTag";
            this.textBoxCreatureTag.Size = new System.Drawing.Size(265, 20);
            this.textBoxCreatureTag.TabIndex = 5;
            // 
            // textBoxCreatureName
            // 
            this.textBoxCreatureName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreatureName.Location = new System.Drawing.Point(81, 66);
            this.textBoxCreatureName.Name = "textBoxCreatureName";
            this.textBoxCreatureName.Size = new System.Drawing.Size(265, 20);
            this.textBoxCreatureName.TabIndex = 4;
            // 
            // labelCreatureResref
            // 
            this.labelCreatureResref.AutoSize = true;
            this.labelCreatureResref.Location = new System.Drawing.Point(6, 123);
            this.labelCreatureResref.Name = "labelCreatureResref";
            this.labelCreatureResref.Size = new System.Drawing.Size(41, 13);
            this.labelCreatureResref.TabIndex = 3;
            this.labelCreatureResref.Text = "Resref:";
            // 
            // labelCreatureTag
            // 
            this.labelCreatureTag.AutoSize = true;
            this.labelCreatureTag.Location = new System.Drawing.Point(6, 95);
            this.labelCreatureTag.Name = "labelCreatureTag";
            this.labelCreatureTag.Size = new System.Drawing.Size(29, 13);
            this.labelCreatureTag.TabIndex = 2;
            this.labelCreatureTag.Text = "Tag:";
            // 
            // labelCreatureName
            // 
            this.labelCreatureName.AutoSize = true;
            this.labelCreatureName.Location = new System.Drawing.Point(6, 68);
            this.labelCreatureName.Name = "labelCreatureName";
            this.labelCreatureName.Size = new System.Drawing.Size(38, 13);
            this.labelCreatureName.TabIndex = 1;
            this.labelCreatureName.Text = "Name:";
            // 
            // treeViewCreatures
            // 
            this.treeViewCreatures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewCreatures.Location = new System.Drawing.Point(4, 0);
            this.treeViewCreatures.Name = "treeViewCreatures";
            treeNode1.Name = "rootNode";
            treeNode1.Text = "Creatures";
            this.treeViewCreatures.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewCreatures.Size = new System.Drawing.Size(186, 423);
            this.treeViewCreatures.TabIndex = 4;
            // 
            // buttonAddCreatureCategory
            // 
            this.buttonAddCreatureCategory.Location = new System.Drawing.Point(5, 426);
            this.buttonAddCreatureCategory.Name = "buttonAddCreatureCategory";
            this.buttonAddCreatureCategory.ResourceType = WinterEngine.Toolset.Enumerations.ResourceTypeEnum.Creature;
            this.buttonAddCreatureCategory.Size = new System.Drawing.Size(185, 26);
            this.buttonAddCreatureCategory.TabIndex = 5;
            // 
            // CreatureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAddCreatureCategory);
            this.Controls.Add(this.treeViewCreatures);
            this.Controls.Add(this.tabControlCreatureSubProperties);
            this.Name = "CreatureView";
            this.Size = new System.Drawing.Size(570, 455);
            this.tabControlCreatureSubProperties.ResumeLayout(false);
            this.tabPageCreatureViewer.ResumeLayout(false);
            this.tabPageCreatureDetails.ResumeLayout(false);
            this.tabPageCreatureDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlCreatureSubProperties;
        private System.Windows.Forms.TabPage tabPageCreatureViewer;
        private System.Windows.Forms.Panel panelCreatureEditorControl;
        private System.Windows.Forms.TabPage tabPageCreatureDetails;
        private System.Windows.Forms.Button buttonDiscardChangesCreatureDetails;
        private System.Windows.Forms.Button buttonSaveChangesCreatureDetails;
        private System.Windows.Forms.Label labelCreatureDetailsHeader;
        private System.Windows.Forms.TextBox textBoxCreatureResref;
        private System.Windows.Forms.TextBox textBoxCreatureTag;
        private System.Windows.Forms.TextBox textBoxCreatureName;
        private System.Windows.Forms.Label labelCreatureResref;
        private System.Windows.Forms.Label labelCreatureTag;
        private System.Windows.Forms.Label labelCreatureName;
        private System.Windows.Forms.TreeView treeViewCreatures;
        private Controls.WinterEngineControls.TreeCategoryControl buttonAddCategory;
        private Controls.WinterEngineControls.TreeCategoryControl buttonAddCreatureCategory;

    }
}
