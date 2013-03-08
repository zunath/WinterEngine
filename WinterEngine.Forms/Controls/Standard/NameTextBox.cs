using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.Utility;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Forms.Controls.Standard
{
    public partial class NameTextBox : UserControl
    {
        #region Fields

        private bool _isValid;
        private Regex _validCharactersRegex;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the regex used by this control
        /// </summary>
        public Regex ValidCharactersRegex
        {
            get { return _validCharactersRegex; }
            set { _validCharactersRegex = value; }
        }

        /// <summary>
        /// Gets or sets whether or not the data entered is valid.
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        /// <summary>
        /// Gets or sets the text of the name box.
        /// </summary>
        public string NameText
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        /// <summary>
        /// Gets or sets the start position of the text box selection
        /// </summary>
        public int SelectionStart
        {
            get { return textBoxName.SelectionStart; }
            set { textBoxName.SelectionStart = value; }
        }

        /// <summary>
        /// Gets or sets the length of the text box selection
        /// </summary>
        public int SelectionLength
        {
            get { return textBoxName.SelectionLength; }
            set { textBoxName.SelectionLength = value; }
        }

        /// <summary>
        /// Gets whether the text box is focused.
        /// </summary>
        public override bool Focused
        {
            get { return textBoxName.Focused; }
        }

        /// <summary>
        /// Gets or sets the text contained inside of the text box.
        /// </summary>
        public override string Text
        {
            get
            {
                return textBoxName.Text;
            }
            set
            {
                textBoxName.Text = value;
            }
        }

        #endregion

        #region Events / Delegates

        private event EventHandler OnValidationSucceeded;
        private event EventHandler OnValidationFailed;

        #endregion

        #region Constructors

        public NameTextBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Handles validating the text entered to the text box.
        /// </summary>
        /// <returns></returns>
        public void Validation()
        {
            errorProvider.Clear();

            _isValid = true;

            if (NameText == "")
            {
                errorProvider.SetError(textBoxName, "Invalid Name");
                _isValid = false;
            }

            // Handle firing events for all subscribers.
            if (_isValid)
            {
                if (!Object.ReferenceEquals(OnValidationSucceeded, null))
                {
                    OnValidationSucceeded(this, new EventArgs());
                }
            }
            else
            {
                if (!Object.ReferenceEquals(OnValidationFailed, null))
                {
                    OnValidationFailed(this, new EventArgs());
                }
            }
        }

        #endregion

        #region Event Handlers


        private void textBoxName_Leave(object sender, EventArgs e)
        {
            Validation();
        }

        #endregion

    }
}
