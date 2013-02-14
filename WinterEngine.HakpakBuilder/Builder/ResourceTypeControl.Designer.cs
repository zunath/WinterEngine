namespace WinterEngine.HakpakBuilder.Builder
{
    partial class ResourceTypeControl
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
            this.radioButtonAudio = new System.Windows.Forms.RadioButton();
            this.radioButtonCharacter = new System.Windows.Forms.RadioButton();
            this.radioButtonTileset = new System.Windows.Forms.RadioButton();
            this.radioButtonItem = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonAudio
            // 
            this.radioButtonAudio.AutoSize = true;
            this.radioButtonAudio.Location = new System.Drawing.Point(141, 3);
            this.radioButtonAudio.Name = "radioButtonAudio";
            this.radioButtonAudio.Size = new System.Drawing.Size(52, 17);
            this.radioButtonAudio.TabIndex = 16;
            this.radioButtonAudio.Text = "Audio";
            this.radioButtonAudio.UseVisualStyleBackColor = true;
            this.radioButtonAudio.CheckedChanged += new System.EventHandler(this.radioButtonAudio_CheckedChanged);
            // 
            // radioButtonCharacter
            // 
            this.radioButtonCharacter.AutoSize = true;
            this.radioButtonCharacter.Location = new System.Drawing.Point(65, 3);
            this.radioButtonCharacter.Name = "radioButtonCharacter";
            this.radioButtonCharacter.Size = new System.Drawing.Size(71, 17);
            this.radioButtonCharacter.TabIndex = 15;
            this.radioButtonCharacter.Text = "Character";
            this.radioButtonCharacter.UseVisualStyleBackColor = true;
            this.radioButtonCharacter.CheckedChanged += new System.EventHandler(this.radioButtonCharacter_CheckedChanged);
            // 
            // radioButtonTileset
            // 
            this.radioButtonTileset.AutoSize = true;
            this.radioButtonTileset.Location = new System.Drawing.Point(3, 3);
            this.radioButtonTileset.Name = "radioButtonTileset";
            this.radioButtonTileset.Size = new System.Drawing.Size(56, 17);
            this.radioButtonTileset.TabIndex = 14;
            this.radioButtonTileset.Text = "Tileset";
            this.radioButtonTileset.UseVisualStyleBackColor = true;
            this.radioButtonTileset.CheckedChanged += new System.EventHandler(this.radioButtonTileset_CheckedChanged);
            // 
            // radioButtonItem
            // 
            this.radioButtonItem.AutoSize = true;
            this.radioButtonItem.Checked = true;
            this.radioButtonItem.Location = new System.Drawing.Point(199, 3);
            this.radioButtonItem.Name = "radioButtonItem";
            this.radioButtonItem.Size = new System.Drawing.Size(45, 17);
            this.radioButtonItem.TabIndex = 17;
            this.radioButtonItem.TabStop = true;
            this.radioButtonItem.Text = "Item";
            this.radioButtonItem.UseVisualStyleBackColor = true;
            this.radioButtonItem.CheckedChanged += new System.EventHandler(this.radioButtonItem_CheckedChanged);
            // 
            // ResourceTypeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonItem);
            this.Controls.Add(this.radioButtonAudio);
            this.Controls.Add(this.radioButtonCharacter);
            this.Controls.Add(this.radioButtonTileset);
            this.Name = "ResourceTypeControl";
            this.Size = new System.Drawing.Size(292, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonAudio;
        private System.Windows.Forms.RadioButton radioButtonCharacter;
        private System.Windows.Forms.RadioButton radioButtonTileset;
        private System.Windows.Forms.RadioButton radioButtonItem;
    }
}
