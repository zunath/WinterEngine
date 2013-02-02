using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class PlaceablePropertiesControl : UserControl
    {

        #region Fields

        private Placeable _backupPlaceable;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup placeable which is used to revert changes.
        /// </summary>
        public Placeable BackupPlaceable
        {
            get { return _backupPlaceable; }
            set { _backupPlaceable = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnSavePlaceable;

        #endregion

        #region Constructors

        public PlaceablePropertiesControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates all controls and fields with the placeable passed in.
        /// </summary>
        /// <param name="placeable"></param>
        public void LoadPlaceable(Placeable placeable)
        {
            BackupPlaceable = placeable;

            // Re-enable controls
            tabControlProperties.Enabled = true;
            buttonApplyChanges.Enabled = true;
            buttonDiscardChanges.Enabled = true;

            // Load data into controls
            nameTextBoxPlaceable.NameText = placeable.Name;
            tagTextBoxPlaceable.TagText = placeable.Tag;
            resrefTextBoxPlaceable.ResrefText = placeable.Resref;

            textBoxPlaceableComments.Text = placeable.Comment;
            textBoxPlaceableDescription.Text = placeable.Description;
            checkBoxHasInventory.Checked = placeable.HasInventory;
            checkBoxUseable.Checked = placeable.IsUseable;
        }

        /// <summary>
        /// Handles updating a placeable's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (nameTextBoxPlaceable.IsValid && tagTextBoxPlaceable.IsValid)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    Placeable placeable = new Placeable();
                    placeable.Name = nameTextBoxPlaceable.NameText;
                    placeable.Tag = tagTextBoxPlaceable.TagText;
                    placeable.Resref = resrefTextBoxPlaceable.ResrefText;
                    placeable.Description = textBoxPlaceableDescription.Text;
                    placeable.Comment = textBoxPlaceableComments.Text;
                    placeable.HasInventory = checkBoxHasInventory.Checked;
                    placeable.IsUseable = checkBoxUseable.Checked;
                    placeable.ResourceCategoryID = BackupPlaceable.ResourceCategoryID;

                    repo.Update(resrefTextBoxPlaceable.ResrefText, placeable);
                    BackupPlaceable = placeable;

                    GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                    eventArgs.GameObject = placeable;
                    OnSavePlaceable(this, eventArgs);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name and tag.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!nameTextBoxPlaceable.IsValid)
                {
                    nameTextBoxPlaceable.Focus();
                }
                else
                {
                    tagTextBoxPlaceable.Focus();
                }
            }
        }

        /// <summary>
        /// Handles reverting all input fields to the backup placeable's values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            nameTextBoxPlaceable.NameText = BackupPlaceable.Name;
            tagTextBoxPlaceable.TagText = BackupPlaceable.Tag;
            resrefTextBoxPlaceable.ResrefText = BackupPlaceable.Resref;
            textBoxPlaceableComments.Text = BackupPlaceable.Comment;
            textBoxPlaceableDescription.Text = BackupPlaceable.Description;
            checkBoxHasInventory.Checked = BackupPlaceable.HasInventory;
            checkBoxUseable.Checked = BackupPlaceable.IsUseable;
        }

        #endregion
    }
}
