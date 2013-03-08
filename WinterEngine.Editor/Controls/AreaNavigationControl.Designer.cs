namespace WinterEngine.Editor.Controls
{
    partial class AreaNavigationControl
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
            this.buttonCameraLeft = new System.Windows.Forms.Button();
            this.buttonCameraRight = new System.Windows.Forms.Button();
            this.buttonCameraUp = new System.Windows.Forms.Button();
            this.buttonCameraDown = new System.Windows.Forms.Button();
            this.buttonRotateClockwise = new System.Windows.Forms.Button();
            this.buttonRotateCounterclockwise = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCameraLeft
            // 
            this.buttonCameraLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCameraLeft.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_CameraLeft;
            this.buttonCameraLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCameraLeft.Location = new System.Drawing.Point(3, 10);
            this.buttonCameraLeft.Name = "buttonCameraLeft";
            this.buttonCameraLeft.Size = new System.Drawing.Size(35, 32);
            this.buttonCameraLeft.TabIndex = 0;
            this.buttonCameraLeft.UseVisualStyleBackColor = true;
            // 
            // buttonCameraRight
            // 
            this.buttonCameraRight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCameraRight.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_CameraRight;
            this.buttonCameraRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCameraRight.Location = new System.Drawing.Point(44, 10);
            this.buttonCameraRight.Name = "buttonCameraRight";
            this.buttonCameraRight.Size = new System.Drawing.Size(35, 32);
            this.buttonCameraRight.TabIndex = 1;
            this.buttonCameraRight.UseVisualStyleBackColor = true;
            // 
            // buttonCameraUp
            // 
            this.buttonCameraUp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCameraUp.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_CameraUp;
            this.buttonCameraUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCameraUp.Location = new System.Drawing.Point(85, 10);
            this.buttonCameraUp.Name = "buttonCameraUp";
            this.buttonCameraUp.Size = new System.Drawing.Size(35, 32);
            this.buttonCameraUp.TabIndex = 2;
            this.buttonCameraUp.UseVisualStyleBackColor = true;
            // 
            // buttonCameraDown
            // 
            this.buttonCameraDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCameraDown.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_CameraDown;
            this.buttonCameraDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCameraDown.Location = new System.Drawing.Point(126, 10);
            this.buttonCameraDown.Name = "buttonCameraDown";
            this.buttonCameraDown.Size = new System.Drawing.Size(35, 32);
            this.buttonCameraDown.TabIndex = 3;
            this.buttonCameraDown.UseVisualStyleBackColor = true;
            // 
            // buttonRotateClockwise
            // 
            this.buttonRotateClockwise.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonRotateClockwise.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_ObjectClockwise;
            this.buttonRotateClockwise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotateClockwise.Location = new System.Drawing.Point(191, 10);
            this.buttonRotateClockwise.Name = "buttonRotateClockwise";
            this.buttonRotateClockwise.Size = new System.Drawing.Size(35, 32);
            this.buttonRotateClockwise.TabIndex = 4;
            this.buttonRotateClockwise.UseVisualStyleBackColor = true;
            // 
            // buttonRotateCounterclockwise
            // 
            this.buttonRotateCounterclockwise.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonRotateCounterclockwise.BackgroundImage = global::WinterEngine.Editor.WinterEngine_Editor.Icon_ObjectCounterclockwise;
            this.buttonRotateCounterclockwise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotateCounterclockwise.Location = new System.Drawing.Point(230, 10);
            this.buttonRotateCounterclockwise.Name = "buttonRotateCounterclockwise";
            this.buttonRotateCounterclockwise.Size = new System.Drawing.Size(35, 32);
            this.buttonRotateCounterclockwise.TabIndex = 5;
            this.buttonRotateCounterclockwise.UseVisualStyleBackColor = true;
            // 
            // AreaNavigationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRotateCounterclockwise);
            this.Controls.Add(this.buttonRotateClockwise);
            this.Controls.Add(this.buttonCameraDown);
            this.Controls.Add(this.buttonCameraUp);
            this.Controls.Add(this.buttonCameraRight);
            this.Controls.Add(this.buttonCameraLeft);
            this.Name = "AreaNavigationControl";
            this.Size = new System.Drawing.Size(273, 45);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCameraLeft;
        private System.Windows.Forms.Button buttonCameraRight;
        private System.Windows.Forms.Button buttonCameraUp;
        private System.Windows.Forms.Button buttonCameraDown;
        private System.Windows.Forms.Button buttonRotateClockwise;
        private System.Windows.Forms.Button buttonRotateCounterclockwise;
    }
}
