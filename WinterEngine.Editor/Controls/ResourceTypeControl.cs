using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Editor.Controls
{
    public partial class ResourceTypeControl : UserControl
    {
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
            ChangeResourceType(ContentPackageResourceTypeEnum.Audio, false);
        }

        private void radioButtonCharacter_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(ContentPackageResourceTypeEnum.Character, false);
        }

        private void radioButtonTileset_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(ContentPackageResourceTypeEnum.Tileset, false);
        }

        private void radioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(ContentPackageResourceTypeEnum.Item, false);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Changes the selected radio button based on the resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="changeRadioButton">If true, the radio button will be selected. If false, only the OnResourceChanged event will be raised.</param>
        public void ChangeResourceType(ContentPackageResourceTypeEnum resourceType, bool changeRadioButton = true)
        {
            ResourceTypeChangedEventArgs eventArgs = new ResourceTypeChangedEventArgs();
            eventArgs.GameObjectType = resourceType;

            if (!Object.ReferenceEquals(OnResourceChanged, null))
            {
                OnResourceChanged(this, eventArgs);
            }

            if (changeRadioButton)
            {
                switch (resourceType)
                {
                    case ContentPackageResourceTypeEnum.Audio:
                        radioButtonAudio.Checked = true;
                        break;
                    case ContentPackageResourceTypeEnum.Character:
                        radioButtonCharacter.Checked = true;
                        break;
                    case ContentPackageResourceTypeEnum.Tileset:
                        radioButtonTileset.Checked = true;
                        break;
                    case ContentPackageResourceTypeEnum.Item:
                        radioButtonItem.Checked = true;
                        break;
                }
            }

        }
        #endregion


    }
}
