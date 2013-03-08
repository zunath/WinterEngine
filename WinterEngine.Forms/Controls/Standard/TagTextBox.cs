﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.Utility;
using WinterEngine.Library.Factories;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Forms.Controls.Standard
{
    public partial class TagTextBox : UserControl
    {

        #region Fields

        private bool _isValid;
        private ResourceTypeEnum _resourceType = ResourceTypeEnum.Area;

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
        /// Gets or sets the text of the tag box.
        /// </summary>
        public string TagText
        {
            get { return textBoxTag.Text; }
            set { textBoxTag.Text = value; }
        }

        /// <summary>
        /// Gets or sets the resource type for this text box.
        /// </summary>
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        /// <summary>
        /// Gets or sets the start position of the text box selection
        /// </summary>
        public int SelectionStart
        {
            get { return textBoxTag.SelectionStart; }
            set { textBoxTag.SelectionStart = value; }
        }

        /// <summary>
        /// Gets or sets the length of the text box selection
        /// </summary>
        public int SelectionLength
        {
            get { return textBoxTag.SelectionLength; }
            set { textBoxTag.SelectionLength = value; }
        }

        /// <summary>
        /// Gets whether the text box is focused.
        /// </summary>
        public override bool Focused
        {
            get { return textBoxTag.Focused; }
        }

        /// <summary>
        /// Gets or sets the text contained inside of the text box.
        /// </summary>
        public override string Text
        {
            get
            {
                return textBoxTag.Text;
            }
            set
            {
                textBoxTag.Text = value;
            }
        }


        #endregion

        #region Events / Delegates

        private event EventHandler OnValidationSucceeded;
        private event EventHandler OnValidationFailed;

        #endregion

        #region Constructors

        public TagTextBox()
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

            Regex tagRegex = new Regex("^[a-zA-Z0-9_]*$");
            _isValid = true;

            if (!tagRegex.IsMatch(TagText) || TagText == "")
            {
                errorProvider.SetError(textBoxTag, "Invalid Tag");
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

        #region Event handlers

        /// <summary>
        /// Handles validation whenever text in the resref text box is changed.
        /// Resrefs can only contain lower case letters and numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxResref_TextChanged(object sender, EventArgs e)
        {
            Regex lettersOnly = new Regex("^[a-zA-Z0-9_]*$");
            TextHelper textHelper = new TextHelper();
            Tuple<string, int, int> retValue = textHelper.Validate(textBoxTag.Text, lettersOnly, textBoxTag.SelectionStart, textBoxTag.SelectionLength, true);

            textBoxTag.Text = retValue.Item1;
            textBoxTag.SelectionStart = retValue.Item2;
            textBoxTag.SelectionLength = retValue.Item3;
        }

        /// <summary>
        /// Handles validation of text entered when the control loses focus.
        /// If data is invalid, the error provider will display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTag_Leave(object sender, EventArgs e)
        {
            Validation();
        }

        #endregion


    }
}
