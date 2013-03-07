using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall.Input;
using WinterEngine.Editor.Services;

namespace WinterEngine.Editor.Controls
{
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
                int startPosition = SelectionStart;
                int length = SelectionLength;

                string newText = InputManager.Keyboard.GetStringTyped();
                Text += newText;
            }
        }
    }
}
