using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;

namespace WinterEngine.Forms.Controls.Standard
{
    public partial class ResrefTextBox : UserControl
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
        /// Gets or sets the text of the resref box.
        /// </summary>
        public string ResrefText
        {
            get { return textBoxResref.Text; }
            set { textBoxResref.Text = value; }
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
            get { return textBoxResref.SelectionStart; }
            set { textBoxResref.SelectionStart = value; }
        }

        /// <summary>
        /// Gets or sets the length of the text box selection
        /// </summary>
        public int SelectionLength
        {
            get { return textBoxResref.SelectionLength; }
            set { textBoxResref.SelectionLength = value; }
        }

        /// <summary>
        /// Gets whether the text box is focused.
        /// </summary>
        public override bool Focused
        {
            get { return textBoxResref.Focused; }
        }

        /// <summary>
        /// Gets or sets the text contained inside of the text box.
        /// </summary>
        public override string Text
        {
            get
            {
                return textBoxResref.Text;
            }
            set
            {
                textBoxResref.Text = value;
            }
        }

        #endregion

        #region Events / Delegates

        private event EventHandler OnValidationSucceeded;
        private event EventHandler OnValidationFailed;

        #endregion

        #region Constructors

        public ResrefTextBox()
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

            Regex resrefRegex = new Regex("^[a-zA-Z0-9_]*$");
            if (!resrefRegex.IsMatch(ResrefText) || ResrefText == "")
            {
                errorProvider.SetError(textBoxResref, "Invalid Resref");
                _isValid = false;
            }

            GameObjectFactory factory = new GameObjectFactory();
            if (factory.DoesObjectExistInDatabase(textBoxResref.Text, ResourceType))
            {
                errorProvider.SetError(textBoxResref, "This resref is already in use!");
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
            Tuple<string, int, int> retValue = textHelper.Validate(textBoxResref.Text, lettersOnly, textBoxResref.SelectionStart, textBoxResref.SelectionLength, false);

            textBoxResref.Text = retValue.Item1;
            textBoxResref.SelectionStart = retValue.Item2;
            textBoxResref.SelectionLength = retValue.Item3;
        }

        /// <summary>
        /// Handles validation of text entered when the control loses focus.
        /// Will check the database to see if the text entered matches the resref of another object.
        /// If so, the error provider will display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxResref_Leave(object sender, EventArgs e)
        {
            Validation();
        }

        #endregion
    }
}
