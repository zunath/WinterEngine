namespace WinterEngine.Toolset.Controls.WinterEngineControls
{
    partial class TreeCategoryControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Root");
            this.buttonAddCategory = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStripNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddCategory.Location = new System.Drawing.Point(3, 277);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(184, 23);
            this.buttonAddCategory.TabIndex = 0;
            this.buttonAddCategory.Text = "Add Category";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.buttonAddCategory_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.ContextMenuStrip = this.contextMenuStripNodes;
            this.treeView.Location = new System.Drawing.Point(3, 3);
            this.treeView.Name = "treeView";
            treeNode1.Name = "rootNode";
            treeNode1.Text = "Root";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView.Size = new System.Drawing.Size(186, 268);
            this.treeView.TabIndex = 5;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // contextMenuStripNodes
            // 
            this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultOptionToolStripMenuItem});
            this.contextMenuStripNodes.Name = "contextMenuStripNodes";
            this.contextMenuStripNodes.Size = new System.Drawing.Size(150, 26);
            this.contextMenuStripNodes.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripNodes_Opening);
            this.contextMenuStripNodes.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripNodes_ItemClicked);
            // 
            // defaultOptionToolStripMenuItem
            // 
            this.defaultOptionToolStripMenuItem.Name = "defaultOptionToolStripMenuItem";
            this.defaultOptionToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.defaultOptionToolStripMenuItem.Text = "DefaultOption";
            // 
            // TreeCategoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.buttonAddCategory);
            this.Name = "TreeCategoryControl";
            this.Size = new System.Drawing.Size(194, 309);
            this.contextMenuStripNodes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNodes;
        private System.Windows.Forms.ToolStripMenuItem defaultOptionToolStripMenuItem;
    }
}
