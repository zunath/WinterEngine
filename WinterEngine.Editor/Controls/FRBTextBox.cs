using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall.Input;
using WinterEngine.Editor.Services;

namespace WinterEngine.Editor.Controls
{
    /// <summary>
    /// A basic text box to be used in XNA windows.
    /// </summary>
    public class FRBTextBox : TextBox
    {
        /// <summary>
        /// Constructs a new FRB text box control and subscribes it to the 
        /// WinterEditorServices.OnGameUpdate event.
        /// </summary>
        public FRBTextBox()
        {
            WinterEditorServices.OnGameUpdate += XNAUpdate;
        }

        /// <summary>
        /// Handles updating text entered by user every frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void XNAUpdate(object sender, EventArgs e)
        {
            if (Focused)
            {
                string typedText = InputManager.Keyboard.GetStringTyped();

                // Make sure the user typed something in before modifying anything.
                if(!String.IsNullOrEmpty(typedText))
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
