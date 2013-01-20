namespace WinterEngine.Toolset.GUI.Views
{
    partial class AdvancedView
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
            this.panelAdvancedDock = new System.Windows.Forms.Panel();
            this.dataGridViewAdvanced = new System.Windows.Forms.DataGridView();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.panelAdvancedDock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdvanced)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAdvancedDock
            // 
            this.panelAdvancedDock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAdvancedDock.Controls.Add(this.dataGridViewAdvanced);
            this.panelAdvancedDock.Location = new System.Drawing.Point(0, 36);
            this.panelAdvancedDock.Name = "panelAdvancedDock";
            this.panelAdvancedDock.Size = new System.Drawing.Size(465, 219);
            this.panelAdvancedDock.TabIndex = 0;
            // 
            // dataGridViewAdvanced
            // 
            this.dataGridViewAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAdvanced.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewAdvanced.Name = "dataGridViewAdvanced";
            this.dataGridViewAdvanced.Size = new System.Drawing.Size(465, 219);
            this.dataGridViewAdvanced.TabIndex = 0;
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyChanges.Location = new System.Drawing.Point(239, 263);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(103, 23);
            this.buttonApplyChanges.TabIndex = 1;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.Location = new System.Drawing.Point(3, 9);
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(247, 21);
            this.comboBoxTable.Sorted = true;
            this.comboBoxTable.TabIndex = 3;
            this.comboBoxTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable_SelectedIndexChanged);
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDiscardChanges.Location = new System.Drawing.Point(348, 263);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(103, 23);
            this.buttonDiscardChanges.TabIndex = 4;
            this.buttonDiscardChanges.Text = "Discard Changes";
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            // 
            // AdvancedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.comboBoxTable);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.panelAdvancedDock);
            this.Name = "AdvancedView";
            this.Size = new System.Drawing.Size(468, 289);
            this.Load += new System.EventHandler(this.AdvancedView_Load);
            this.panelAdvancedDock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdvanced)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAdvancedDock;
        private System.Windows.Forms.DataGridView dataGridViewAdvanced;
        private System.Windows.Forms.Button buttonApplyChanges;
        private System.Windows.Forms.ComboBox comboBoxTable;
        private System.Windows.Forms.Button buttonDiscardChanges;


    }
}
