using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.DataLayer.Repositories;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class PlaceableViewControl : UserControl
    {

        #region Fields

        private ObjectViewer3D _objectViewer;
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

        public PlaceableViewControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _objectViewer = new ObjectViewer3D();
            _objectViewer.Dock = DockStyle.Fill;
            panelPlaceableObjectViewer.Controls.Add(_objectViewer);
        }

        /// <summary>
        /// Handles updating a placeable's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChanges_Click(object sender, EventArgs e)
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

                repo.Update(resrefTextBoxPlaceable.ResrefText, placeable);
                BackupPlaceable = placeable;
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
    }
}
