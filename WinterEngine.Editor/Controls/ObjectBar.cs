using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Editor.ExtendedEventArgs;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.Controls
{
    public partial class ObjectBar : UserControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public ObjectBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<ObjectSelectionEventArgs> OnObjectSelected;

        #endregion

        #region Event Handling


        private void radioButtonAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonAreas.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Area));
                }
            }
        }

        private void radioButtonCreatures_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonCreatures.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Creature));
                }
            }
        }

        private void radioButtonItems_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonItems.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Item));
                }
            }
        }

        private void radioButtonPlaceables_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonPlaceables.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Placeable));
                }
            }
        }

        private void radioButtonConversations_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonConversations.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Conversation));
                }
            }
        }

        private void radioButtonScripts_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonScripts.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Script));
                }
            }
        }

        private void radioButtonAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectSelected, null))
            {
                if (radioButtonGraphics.Checked)
                {
                    OnObjectSelected(this, new ObjectSelectionEventArgs(ObjectSelectionTypeEnum.Graphics));
                }
            }
        }

        #endregion

        #region Methods


        #endregion
    }
}
