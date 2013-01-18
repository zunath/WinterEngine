using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.Helpers;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Toolset.Controls.GenericControls
{
    public partial class NameTextBox : UserControl
    {
        #region Fields

        private bool _isValid;
        private ResourceTypeEnum _resourceType;

        #endregion

        #region Properties

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
