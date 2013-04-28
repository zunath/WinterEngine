using System.Text.RegularExpressions;
namespace WinterEngine.Editor.Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaPropertiesControl));
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageTiles = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxTileset = new System.Windows.Forms.PictureBox();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.labelTileset = new System.Windows.Forms.Label();
            this.listBoxTilesets = new System.Windows.Forms.ListBox();
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
            this.resrefTextBoxArea = new WinterEngine.Editor.Controls.FRBResrefTextBox();
            this.tagTextBoxArea = new WinterEngine.Editor.Controls.FRBTagTextBox();
            this.nameTextBoxArea = new WinterEngine.Editor.Controls.FRBNameTextBox();
            this.tabControlProperties.SuspendLayout();
            this.tabPageTiles.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTileset)).BeginInit();
            this.tabPageDetails.SuspendLayout();
            this.tabPageComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageTiles);
            this.tabControlProperties.Controls.Add(this.tabPageDetails);
            this.tabControlProperties.Controls.Add(this.tabPageAudio);
            this.tabControlProperties.Controls.Add(this.tabPageEvents);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(335, 417);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageTiles
            // 
            this.tabPageTiles.Controls.Add(this.panel1);
            this.tabPageTiles.Location = new System.Drawing.Point(4, 22);
            this.tabPageTiles.Name = "tabPageTiles";
            this.tabPageTiles.Size = new System.Drawing.Size(327, 391);
            this.tabPageTiles.TabIndex = 5;
            this.tabPageTiles.Text = "Tiles";
            this.tabPageTiles.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBoxTileset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 391);
            this.panel1.TabIndex = 1;
            // 
            // pictureBoxTileset
            // 
            this.pictureBoxTileset.BackColor = System.Drawing.Color.White;
            this.pictureBoxTileset.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxTileset.Image")));
            this.pictureBoxTileset.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxTileset.InitialImage")));
            this.pictureBoxTileset.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTileset.Name = "pictureBoxTileset";
            this.pictureBoxTileset.Size = new System.Drawing.Size(176, 160);
            this.pictureBoxTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxTileset.TabIndex = 0;
            this.pictureBoxTileset.TabStop = false;
            this.pictureBoxTileset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDown);
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.labelTileset);
            this.tabPageDetails.Controls.Add(this.listBoxTilesets);
            this.tabPageDetails.Controls.Add(this.labelAreaDetailsHeader);
            this.tabPageDetails.Controls.Add(this.labelItemResref);
            this.tabPageDetails.Controls.Add(this.labelItemTag);
            this.tabPageDetails.Controls.Add(this.labelItemName);
            this.tabPageDetails.Controls.Add(this.resrefTextBoxArea);
            this.tabPageDetails.Controls.Add(this.tagTextBoxArea);
            this.tabPageDetails.Controls.Add(this.nameTextBoxArea);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(327, 391);
            this.tabPageDetails.TabIndex = 1;
            this.tabPageDetails.Text = "Details";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // labelTileset
            // 
            this.labelTileset.AutoSize = true;
            this.labelTileset.Location = new System.Drawing.Point(6, 241);
            this.labelTileset.Name = "labelTileset";
            this.labelTileset.Size = new System.Drawing.Size(41, 13);
            this.labelTileset.TabIndex = 12;
            this.labelTileset.Text = "Tileset:";
            // 
            // listBoxTilesets
            // 
            this.listBoxTilesets.FormattingEnabled = true;
            this.listBoxTilesets.Location = new System.Drawing.Point(81, 146);
            this.listBoxTilesets.Name = "listBoxTilesets";
            this.listBoxTilesets.Size = new System.Drawing.Size(182, 108);
            this.listBoxTilesets.TabIndex = 11;
            this.listBoxTilesets.SelectedIndexChanged += new System.EventHandler(this.listBoxTilesets_SelectedIndexChanged);
            // 
            // labelAreaDetailsHeader
            // 
            this.labelAreaDetailsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tabPageAudio.Size = new System.Drawing.Size(327, 391);
            this.tabPageAudio.TabIndex = 4;
            this.tabPageAudio.Text = "Audio";
            this.tabPageAudio.UseVisualStyleBackColor = true;
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Size = new System.Drawing.Size(327, 391);
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
            this.tabPageComments.Size = new System.Drawing.Size(327, 391);
            this.tabPageComments.TabIndex = 2;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // labelAreaComments
            // 
            this.labelAreaComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.textBoxAreaComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAreaComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxAreaComments.MaxLength = 4000;
            this.textBoxAreaComments.Multiline = true;
            this.textBoxAreaComments.Name = "textBoxAreaComments";
            this.textBoxAreaComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAreaComments.Size = new System.Drawing.Size(346, 293);
            this.textBoxAreaComments.TabIndex = 13;
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDiscardChanges.Enabled = false;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(167, 426);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChanges.TabIndex = 13;
            this.buttonDiscardChanges.Text = "Discard Changes";
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            this.buttonDiscardChanges.Click += new System.EventHandler(this.buttonDiscardChanges_Click);
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonApplyChanges.Enabled = false;
            this.buttonApplyChanges.Location = new System.Drawing.Point(51, 426);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChanges.TabIndex = 12;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // resrefTextBoxArea
            // 
            this.resrefTextBoxArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resrefTextBoxArea.Enabled = false;
            this.resrefTextBoxArea.IsValid = false;
            this.resrefTextBoxArea.Location = new System.Drawing.Point(81, 112);
            this.resrefTextBoxArea.Name = "resrefTextBoxArea";
            this.resrefTextBoxArea.ResourceType = WinterEngine.DataTransferObjects.Enumerations.GameObjectTypeEnum.Area;
            this.resrefTextBoxArea.ResrefText = "";
            this.resrefTextBoxArea.SelectionLength = 0;
            this.resrefTextBoxArea.SelectionStart = 0;
            this.resrefTextBoxArea.Size = new System.Drawing.Size(192, 28);
            this.resrefTextBoxArea.TabIndex = 10;
            // 
            // tagTextBoxArea
            // 
            this.tagTextBoxArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTextBoxArea.IsValid = true;
            this.tagTextBoxArea.Location = new System.Drawing.Point(81, 86);
            this.tagTextBoxArea.Name = "tagTextBoxArea";
            this.tagTextBoxArea.ResourceType = WinterEngine.DataTransferObjects.Enumerations.GameObjectTypeEnum.Area;
            this.tagTextBoxArea.SelectionLength = 0;
            this.tagTextBoxArea.SelectionStart = 0;
            this.tagTextBoxArea.Size = new System.Drawing.Size(192, 28);
            this.tagTextBoxArea.TabIndex = 9;
            this.tagTextBoxArea.TagText = "";
            // 
            // nameTextBoxArea
            // 
            this.nameTextBoxArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBoxArea.IsValid = false;
            this.nameTextBoxArea.Location = new System.Drawing.Point(81, 58);
            this.nameTextBoxArea.Name = "nameTextBoxArea";
            this.nameTextBoxArea.NameText = "";
            this.nameTextBoxArea.SelectionLength = 0;
            this.nameTextBoxArea.SelectionStart = 0;
            this.nameTextBoxArea.Size = new System.Drawing.Size(192, 28);
            this.nameTextBoxArea.TabIndex = 8;
            this.nameTextBoxArea.ValidCharactersRegex = null;
            // 
            // AreaPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "AreaPropertiesControl";
            this.Size = new System.Drawing.Size(341, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageTiles.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTileset)).EndInit();
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            this.tabPageComments.ResumeLayout(false);
            this.tabPageComments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.Label labelAreaDetailsHeader;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.TabPage tabPageEvents;
        private System.Windows.Forms.TabPage tabPageComments;
        private System.Windows.Forms.Label labelAreaComments;
        private System.Windows.Forms.TextBox textBoxAreaComments;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonApplyChanges;
        private System.Windows.Forms.TabPage tabPageAudio;
        private WinterEngine.Editor.Controls.FRBResrefTextBox resrefTextBoxArea;
        private WinterEngine.Editor.Controls.FRBTagTextBox tagTextBoxArea;
        private WinterEngine.Editor.Controls.FRBNameTextBox nameTextBoxArea;
        private System.Windows.Forms.Label labelTileset;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.TabPage tabPageTiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxTileset;

    }
}
