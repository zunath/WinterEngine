namespace WinterEngine.Toolset.Controls.GenericControls
{
    partial class TagTextBox
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
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxTag
            // 
            this.textBoxTag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxTag.Location = new System.Drawing.Point(0, 8);
            this.textBoxTag.MaxLength = 32;
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(150, 20);
            this.textBoxTag.TabIndex = 0;
            this.textBoxTag.TextChanged += new System.EventHandler(this.textBoxResref_TextChanged);
            // 
            // TagTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxTag);
            this.Name = "TagTextBox";
            this.Size = new System.Drawing.Size(150, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTag;
    }
}
