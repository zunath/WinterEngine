using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.Input;
using WinterEngine.Editor.Services;
using WinterEngine.Forms.Controls.Standard;

namespace WinterEngine.Editor.Controls
{
    public class FRBNameTextBox : NameTextBox
    {
        public FRBNameTextBox()
        {
            WinterEditorServices.OnGameUpdate += XNAUpdate; 
        }

        private void XNAUpdate(object sender, EventArgs e)
        {
            if (Focused)
            {
                string typedText = InputManager.Keyboard.GetStringTyped();

                if (!String.IsNullOrEmpty(typedText))
                {
                    int startPosition = SelectionStart;
                    int length = SelectionLength;

                    string modifiedText = Text.Remove(startPosition, length);
                    modifiedText = modifiedText.Insert(startPosition, typedText);

                    this.Text = modifiedText;
                    this.SelectionStart = startPosition + typedText.Length;
                }
            }
        }
    }
}
