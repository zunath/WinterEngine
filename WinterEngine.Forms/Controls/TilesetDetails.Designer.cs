namespace WinterEngine.Forms.Toolset
{
    partial class TilesetDetails
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
            this.tabPageTilesets = new System.Windows.Forms.TabPage();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.textBoxTilesetName = new System.Windows.Forms.TextBox();
            this.buttonDeleteTileset = new System.Windows.Forms.Button();
            this.buttonNewTileset = new System.Windows.Forms.Button();
            this.listBoxTilesets = new System.Windows.Forms.ListBox();
            this.labelTilesetHeader = new System.Windows.Forms.Label();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.labelTilesetName = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControlProperties.SuspendLayout();
            this.tabPageTilesets.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabPageTilesets);
            this.tabControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProperties.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(292, 414);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageTilesets
            // 
            this.tabPageTilesets.Controls.Add(this.buttonBrowse);
            this.tabPageTilesets.Controls.Add(this.textBoxFilePath);
            this.tabPageTilesets.Controls.Add(this.textBoxTilesetName);
            this.tabPageTilesets.Controls.Add(this.buttonDeleteTileset);
            this.tabPageTilesets.Controls.Add(this.buttonNewTileset);
            this.tabPageTilesets.Controls.Add(this.listBoxTilesets);
            this.tabPageTilesets.Controls.Add(this.labelTilesetHeader);
            this.tabPageTilesets.Controls.Add(this.labelFilePath);
            this.tabPageTilesets.Controls.Add(this.labelTilesetName);
            this.tabPageTilesets.Location = new System.Drawing.Point(4, 22);
            this.tabPageTilesets.Name = "tabPageTilesets";
            this.tabPageTilesets.Size = new System.Drawing.Size(284, 388);
            this.tabPageTilesets.TabIndex = 0;
            this.tabPageTilesets.Text = "Tilesets";
            this.tabPageTilesets.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Enabled = false;
            this.buttonBrowse.Location = new System.Drawing.Point(242, 90);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(37, 23);
            this.buttonBrowse.TabIndex = 18;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilePath.Location = new System.Drawing.Point(80, 92);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(156, 20);
            this.textBoxFilePath.TabIndex = 17;
            this.textBoxFilePath.Text = "No Graphic File Selected";
            // 
            // textBoxTilesetName
            // 
            this.textBoxTilesetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTilesetName.Enabled = false;
            this.textBoxTilesetName.Location = new System.Drawing.Point(80, 61);
            this.textBoxTilesetName.MaxLength = 64;
            this.textBoxTilesetName.Name = "textBoxTilesetName";
            this.textBoxTilesetName.Size = new System.Drawing.Size(199, 20);
            this.textBoxTilesetName.TabIndex = 16;
            this.textBoxTilesetName.TextChanged += new System.EventHandler(this.textBoxTilesetName_TextChanged);
            // 
            // buttonDeleteTileset
            // 
            this.buttonDeleteTileset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDeleteTileset.Location = new System.Drawing.Point(139, 135);
            this.buttonDeleteTileset.Name = "buttonDeleteTileset";
            this.buttonDeleteTileset.Size = new System.Drawing.Size(81, 23);
            this.buttonDeleteTileset.TabIndex = 15;
            this.buttonDeleteTileset.Text = "Delete Tileset";
            this.buttonDeleteTileset.UseVisualStyleBackColor = true;
            this.buttonDeleteTileset.Click += new System.EventHandler(this.buttonDeleteTileset_Click);
            // 
            // buttonNewTileset
            // 
            this.buttonNewTileset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonNewTileset.Location = new System.Drawing.Point(58, 135);
            this.buttonNewTileset.Name = "buttonNewTileset";
            this.buttonNewTileset.Size = new System.Drawing.Size(75, 23);
            this.buttonNewTileset.TabIndex = 14;
            this.buttonNewTileset.Text = "New Tileset";
            this.buttonNewTileset.UseVisualStyleBackColor = true;
            this.buttonNewTileset.Click += new System.EventHandler(this.buttonNewTileset_Click);
            // 
            // listBoxTilesets
            // 
            this.listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTilesets.FormattingEnabled = true;
            this.listBoxTilesets.Location = new System.Drawing.Point(0, 170);
            this.listBoxTilesets.Name = "listBoxTilesets";
            this.listBoxTilesets.Size = new System.Drawing.Size(284, 212);
            this.listBoxTilesets.TabIndex = 13;
            this.listBoxTilesets.SelectedValueChanged += new System.EventHandler(this.listBoxTilesets_SelectedValueChanged);
            // 
            // labelTilesetHeader
            // 
            this.labelTilesetHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTilesetHeader.AutoSize = true;
            this.labelTilesetHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTilesetHeader.Location = new System.Drawing.Point(60, 19);
            this.labelTilesetHeader.Name = "labelTilesetHeader";
            this.labelTilesetHeader.Size = new System.Drawing.Size(186, 31);
            this.labelTilesetHeader.TabIndex = 11;
            this.labelTilesetHeader.Text = "Tileset Details";
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Location = new System.Drawing.Point(25, 95);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(47, 13);
            this.labelFilePath.TabIndex = 10;
            this.labelFilePath.Text = "Graphic:";
            // 
            // labelTilesetName
            // 
            this.labelTilesetName.AutoSize = true;
            this.labelTilesetName.Location = new System.Drawing.Point(6, 68);
            this.labelTilesetName.Name = "labelTilesetName";
            this.labelTilesetName.Size = new System.Drawing.Size(72, 13);
            this.labelTilesetName.TabIndex = 4;
            this.labelTilesetName.Text = "Tileset Name:";
            // 
            // TilesetDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 414);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "TilesetDetailsControl";
            this.Load += new System.EventHandler(this.TilesetDetailsControl_Load);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageTilesets.ResumeLayout(false);
            this.tabPageTilesets.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageTilesets;
        private System.Windows.Forms.Button buttonDeleteTileset;
        private System.Windows.Forms.Button buttonNewTileset;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.Label labelTilesetHeader;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label labelTilesetName;
        private System.Windows.Forms.TextBox textBoxTilesetName;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
