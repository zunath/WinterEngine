﻿namespace WinterEngine.Toolset.Controls.Controls
{
    partial class ResrefTextBox
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
            this.textBoxResref = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxResref
            // 
            this.textBoxResref.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxResref.Location = new System.Drawing.Point(0, 8);
            this.textBoxResref.MaxLength = 32;
            this.textBoxResref.Name = "textBoxResref";
            this.textBoxResref.Size = new System.Drawing.Size(150, 20);
            this.textBoxResref.TabIndex = 0;
            this.textBoxResref.TextChanged += new System.EventHandler(this.textBoxResref_TextChanged);
            // 
            // ResrefTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxResref);
            this.Name = "ResrefTextBox";
            this.Size = new System.Drawing.Size(150, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxResref;
    }
}
