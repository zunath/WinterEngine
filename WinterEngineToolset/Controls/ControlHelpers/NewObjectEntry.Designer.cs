namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    partial class NewObjectEntry
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resrefTextBoxEntry = new WinterEngine.Toolset.Controls.Controls.ResrefTextBox();
            this.resrefTextBox1 = new WinterEngine.Toolset.Controls.Controls.ResrefTextBox();
            this.SuspendLayout();
            // 
            // resrefTextBoxEntry
            // 
            this.resrefTextBoxEntry.Location = new System.Drawing.Point(40, 49);
            this.resrefTextBoxEntry.Name = "resrefTextBoxEntry";
            this.resrefTextBoxEntry.Size = new System.Drawing.Size(232, 24);
            this.resrefTextBoxEntry.TabIndex = 0;
            // 
            // resrefTextBox1
            // 
            this.resrefTextBox1.Location = new System.Drawing.Point(30, 71);
            this.resrefTextBox1.Name = "resrefTextBox1";
            this.resrefTextBox1.Size = new System.Drawing.Size(230, 28);
            this.resrefTextBox1.TabIndex = 0;
            // 
            // NewObjectEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.resrefTextBoxEntry);
            this.Name = "NewObjectEntry";
            this.Text = "NewObjectEntry";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ResrefTextBox resrefTextBox1;
        private Controls.ResrefTextBox resrefTextBoxEntry;
    }
}