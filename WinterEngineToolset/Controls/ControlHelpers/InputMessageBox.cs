using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    public partial class InputMessageBox : Form
    {
        #region Fields

        private int _minimumLength;
        ValidationMethod _validationMethod;
        SuccessMethod _successMethod;

        #endregion

        #region Properties

        /// <summary>
        /// Gets/Sets the minimum length of characters that must be entered.
        /// </summary>
        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        #endregion

        #region Delegates

        // ValidationMethod runs when "OK" button is clicked by user.
        public delegate bool ValidationMethod(string inputText);
        // SuccessMethod runs when validation succeeds
        public delegate void SuccessMethod(string inputText);

        #endregion

        #region Methods

        public InputMessageBox(string message, string title, int inputMinimumLength, int inputMaximumLength, ValidationMethod validationMethod, SuccessMethod successMethod, string defaultText = "Enter Text", string errorMessage = "Invalid entry.")
        {
            InitializeComponent();

            labelMessage.Text = message;
            this.Text = title;

            // Must be a valid range.
            if (inputMinimumLength > inputMaximumLength)
            {
                inputMaximumLength = inputMinimumLength;
            }

            textBoxInput.MaxLength = inputMaximumLength;
            textBoxInput.Text = defaultText;
            textBoxInput.SelectAll();

            labelErrorMessage.Text = errorMessage;

            this._validationMethod = validationMethod;
            this._successMethod = successMethod;
        }
        #endregion

        /// <summary>
        /// Dispose of this form when user clicks Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Validation succeeded. 
            if (_validationMethod(textBoxInput.Text))
            {
                // Run success method.
                _successMethod(textBoxInput.Text);
                
                // Dispose of window.
                this.Dispose();
            }

            else
            {
                labelErrorMessage.Visible = true;
            }
        }

        /// <summary>
        /// Press the OK button if the enter key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK.PerformClick();
            }
        }

    }
}
