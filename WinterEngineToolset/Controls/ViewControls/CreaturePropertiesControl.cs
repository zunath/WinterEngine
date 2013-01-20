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
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class CreaturePropertiesControl : UserControl
    {
        #region Fields

        private ObjectViewer3D _objectViewer;
        private Creature _backupCreature;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup creature which is used to revert changes.
        /// </summary>
        public Creature BackupCreature
        {
            get { return _backupCreature; }
            set { _backupCreature = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnSaveCreature;

        #endregion

        #region Constructors

        public CreaturePropertiesControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _objectViewer = new ObjectViewer3D();
            _objectViewer.Dock = DockStyle.Fill;
            panelCreatureObjectViewer.Controls.Add(_objectViewer);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates all controls and fields with the creature passed in.
        /// </summary>
        /// <param name="creature"></param>
        public void LoadCreature(Creature creature)
        {
            BackupCreature = creature;

            // Re-enable controls
            tabControlProperties.Enabled = true;
            buttonApplyChanges.Enabled = true;
            buttonDiscardChanges.Enabled = true;

            // Load data into controls
            nameTextBoxCreature.NameText = creature.Name;
            tagTextBoxCreature.TagText = creature.Tag;
            resrefTextBoxCreature.ResrefText = creature.Resref;

            textBoxCreatureComments.Text = creature.Comment;
        }

        /// <summary>
        /// Handles updating a creature's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (nameTextBoxCreature.IsValid && tagTextBoxCreature.IsValid)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    Creature creature = new Creature();
                    creature.Name = nameTextBoxCreature.NameText;
                    creature.Tag = tagTextBoxCreature.TagText;
                    creature.Resref = resrefTextBoxCreature.ResrefText;
                    creature.Description = textBoxCreatureDescription.Text;
                    creature.Comment = textBoxCreatureComments.Text;
                    creature.ResourceCategoryID = BackupCreature.ResourceCategoryID;

                    repo.Update(resrefTextBoxCreature.ResrefText, creature);
                    BackupCreature = creature;

                    GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                    eventArgs.GameObject = creature;
                    OnSaveCreature(this, eventArgs);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name and tag.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!nameTextBoxCreature.IsValid)
                {
                    nameTextBoxCreature.Focus();
                }
                else
                {
                    tagTextBoxCreature.Focus();
                }
            }
        }

        /// <summary>
        /// Handles reverting all input fields to the backup creature's values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            nameTextBoxCreature.NameText = BackupCreature.Name;
            tagTextBoxCreature.TagText = BackupCreature.Tag;
            resrefTextBoxCreature.ResrefText = BackupCreature.Resref;
            textBoxCreatureComments.Text = BackupCreature.Comment;
            textBoxCreatureDescription.Text = BackupCreature.Description;
        }

        #endregion
    }
}
