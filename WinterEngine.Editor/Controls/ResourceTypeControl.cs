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
            ChangeResourceType(SpriteSheetTypeEnum.Audio, false);
        }

        private void radioButtonCharacter_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(SpriteSheetTypeEnum.Character, false);
        }

        private void radioButtonTileset_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(SpriteSheetTypeEnum.Tileset, false);
        }

        private void radioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeResourceType(SpriteSheetTypeEnum.Item, false);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Changes the selected radio button based on the resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="changeRadioButton">If true, the radio button will be selected. If false, only the OnResourceChanged event will be raised.</param>
        public void ChangeResourceType(SpriteSheetTypeEnum resourceType, bool changeRadioButton = false)
        {
            ResourceTypeChangedEventArgs eventArgs = new ResourceTypeChangedEventArgs();
            eventArgs.ResourceType = resourceType;

            if (!Object.ReferenceEquals(OnResourceChanged, null))
            {
                OnResourceChanged(this, eventArgs);
            }

            if (changeRadioButton)
            {
                switch (resourceType)
                {
                    case SpriteSheetTypeEnum.Audio:
                        radioButtonAudio.Checked = true;
                        break;
                    case SpriteSheetTypeEnum.Character:
                        radioButtonCharacter.Checked = true;
                        break;
                    case SpriteSheetTypeEnum.Tileset:
                        radioButtonTileset.Checked = true;
                        break;
                    case SpriteSheetTypeEnum.Item:
                        radioButtonItem.Checked = true;
                        break;
                }
            }

        }
        #endregion


    }
}
