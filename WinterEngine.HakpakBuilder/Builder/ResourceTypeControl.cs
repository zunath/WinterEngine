using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinterEngine.HakpakBuilder.Builder
{
    public partial class ResourceTypeControl : UserControl
    {
        #region Fields

        private bool _changingSelectedRadioButton;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether this control is changing the selected radio button.
        /// </summary>
        private bool ChangingSelectedRadioButton
        {
            get { return _changingSelectedRadioButton; }
            set { _changingSelectedRadioButton = value; }
        }

        #endregion

        #region Events

        public event EventHandler<ResourceTypeChangedEventArgs> OnResourceChanged;

        #endregion

        #region Constructors

        public ResourceTypeControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        private void radioButtonAudio_CheckedChanged(object sender, EventArgs e)
        {
            ChangingSelectedRadioButton = true;
            ChangeResourceType(HakResourceTypeEnum.Audio);
            ChangingSelectedRadioButton = false;
        }

        private void radioButtonCharacter_CheckedChanged(object sender, EventArgs e)
        {
            ChangingSelectedRadioButton = true;
            ChangeResourceType(HakResourceTypeEnum.Character);
            ChangingSelectedRadioButton = false;
        }

        private void radioButtonTileset_CheckedChanged(object sender, EventArgs e)
        {
            ChangingSelectedRadioButton = true;
            ChangeResourceType(HakResourceTypeEnum.Tileset);
            ChangingSelectedRadioButton = false;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Changes the selected radio button based on the resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        public void ChangeResourceType(HakResourceTypeEnum resourceType)
        {
            if (!ChangingSelectedRadioButton)
            {
                if (resourceType == HakResourceTypeEnum.Audio)
                {
                    radioButtonAudio.Checked = true;
                }
                else if (resourceType == HakResourceTypeEnum.Character)
                {
                    radioButtonCharacter.Checked = true;
                }
                else if (resourceType == HakResourceTypeEnum.Tileset)
                {
                    radioButtonTileset.Checked = true;
                }
                else
                {
                    radioButtonTileset.Checked = true;
                }

                ResourceTypeChangedEventArgs eventArgs = new ResourceTypeChangedEventArgs();
                eventArgs.ResourceType = resourceType;
                OnResourceChanged(this, eventArgs);
            }
        }
        #endregion

    }
}
