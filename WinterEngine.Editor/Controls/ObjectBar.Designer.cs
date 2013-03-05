namespace WinterEngine.Editor.Controls
{
    partial class ObjectBar
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
            this.radioButtonAreas = new System.Windows.Forms.RadioButton();
            this.radioButtonCreatures = new System.Windows.Forms.RadioButton();
            this.radioButtonItems = new System.Windows.Forms.RadioButton();
            this.radioButtonPlaceables = new System.Windows.Forms.RadioButton();
            this.radioButtonConversations = new System.Windows.Forms.RadioButton();
            this.radioButtonScripts = new System.Windows.Forms.RadioButton();
            this.radioButtonAdvanced = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonAreas
            // 
            this.radioButtonAreas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonAreas.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAreas.AutoSize = true;
            this.radioButtonAreas.Checked = true;
            this.radioButtonAreas.Location = new System.Drawing.Point(7, 3);
            this.radioButtonAreas.Name = "radioButtonAreas";
            this.radioButtonAreas.Size = new System.Drawing.Size(44, 23);
            this.radioButtonAreas.TabIndex = 13;
            this.radioButtonAreas.TabStop = true;
            this.radioButtonAreas.Text = "Areas";
            this.radioButtonAreas.UseVisualStyleBackColor = true;
            this.radioButtonAreas.CheckedChanged += new System.EventHandler(this.radioButtonAreas_CheckedChanged);
            // 
            // radioButtonCreatures
            // 
            this.radioButtonCreatures.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonCreatures.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonCreatures.AutoSize = true;
            this.radioButtonCreatures.Location = new System.Drawing.Point(57, 3);
            this.radioButtonCreatures.Name = "radioButtonCreatures";
            this.radioButtonCreatures.Size = new System.Drawing.Size(62, 23);
            this.radioButtonCreatures.TabIndex = 14;
            this.radioButtonCreatures.Text = "Creatures";
            this.radioButtonCreatures.UseVisualStyleBackColor = true;
            this.radioButtonCreatures.CheckedChanged += new System.EventHandler(this.radioButtonCreatures_CheckedChanged);
            // 
            // radioButtonItems
            // 
            this.radioButtonItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonItems.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonItems.AutoSize = true;
            this.radioButtonItems.Location = new System.Drawing.Point(125, 3);
            this.radioButtonItems.Name = "radioButtonItems";
            this.radioButtonItems.Size = new System.Drawing.Size(42, 23);
            this.radioButtonItems.TabIndex = 15;
            this.radioButtonItems.Text = "Items";
            this.radioButtonItems.UseVisualStyleBackColor = true;
            this.radioButtonItems.CheckedChanged += new System.EventHandler(this.radioButtonItems_CheckedChanged);
            // 
            // radioButtonPlaceables
            // 
            this.radioButtonPlaceables.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonPlaceables.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonPlaceables.AutoSize = true;
            this.radioButtonPlaceables.Location = new System.Drawing.Point(173, 3);
            this.radioButtonPlaceables.Name = "radioButtonPlaceables";
            this.radioButtonPlaceables.Size = new System.Drawing.Size(69, 23);
            this.radioButtonPlaceables.TabIndex = 16;
            this.radioButtonPlaceables.Text = "Placeables";
            this.radioButtonPlaceables.UseVisualStyleBackColor = true;
            this.radioButtonPlaceables.CheckedChanged += new System.EventHandler(this.radioButtonPlaceables_CheckedChanged);
            // 
            // radioButtonConversations
            // 
            this.radioButtonConversations.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonConversations.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonConversations.AutoSize = true;
            this.radioButtonConversations.Location = new System.Drawing.Point(248, 3);
            this.radioButtonConversations.Name = "radioButtonConversations";
            this.radioButtonConversations.Size = new System.Drawing.Size(84, 23);
            this.radioButtonConversations.TabIndex = 17;
            this.radioButtonConversations.Text = "Conversations";
            this.radioButtonConversations.UseVisualStyleBackColor = true;
            this.radioButtonConversations.CheckedChanged += new System.EventHandler(this.radioButtonConversations_CheckedChanged);
            // 
            // radioButtonScripts
            // 
            this.radioButtonScripts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonScripts.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonScripts.AutoSize = true;
            this.radioButtonScripts.Location = new System.Drawing.Point(338, 3);
            this.radioButtonScripts.Name = "radioButtonScripts";
            this.radioButtonScripts.Size = new System.Drawing.Size(49, 23);
            this.radioButtonScripts.TabIndex = 18;
            this.radioButtonScripts.Text = "Scripts";
            this.radioButtonScripts.UseVisualStyleBackColor = true;
            this.radioButtonScripts.CheckedChanged += new System.EventHandler(this.radioButtonScripts_CheckedChanged);
            // 
            // radioButtonAdvanced
            // 
            this.radioButtonAdvanced.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButtonAdvanced.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAdvanced.AutoSize = true;
            this.radioButtonAdvanced.Location = new System.Drawing.Point(393, 3);
            this.radioButtonAdvanced.Name = "radioButtonAdvanced";
            this.radioButtonAdvanced.Size = new System.Drawing.Size(66, 23);
            this.radioButtonAdvanced.TabIndex = 19;
            this.radioButtonAdvanced.Text = "Advanced";
            this.radioButtonAdvanced.UseVisualStyleBackColor = true;
            this.radioButtonAdvanced.CheckedChanged += new System.EventHandler(this.radioButtonAdvanced_CheckedChanged);
            // 
            // ObjectBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonAdvanced);
            this.Controls.Add(this.radioButtonScripts);
            this.Controls.Add(this.radioButtonConversations);
            this.Controls.Add(this.radioButtonPlaceables);
            this.Controls.Add(this.radioButtonItems);
            this.Controls.Add(this.radioButtonCreatures);
            this.Controls.Add(this.radioButtonAreas);
            this.Name = "ObjectBar";
            this.Size = new System.Drawing.Size(508, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonAreas;
        private System.Windows.Forms.RadioButton radioButtonCreatures;
        private System.Windows.Forms.RadioButton radioButtonItems;
        private System.Windows.Forms.RadioButton radioButtonPlaceables;
        private System.Windows.Forms.RadioButton radioButtonConversations;
        private System.Windows.Forms.RadioButton radioButtonScripts;
        private System.Windows.Forms.RadioButton radioButtonAdvanced;


    }
}
