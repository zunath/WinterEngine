using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.Controls.XnaControls.Shared;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class AreaPropertiesControl : UserControl
    {
        #region Fields

        private ObjectViewer3D _objectViewer;
        private Area _backupArea;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup item which is used to revert changes.
        /// </summary>
        public Area BackupArea
        {
            get { return _backupArea; }
            set { _backupArea = value; }
        }
        
        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnSaveArea;

        #endregion

        #region Constructors

        public AreaPropertiesControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _objectViewer = new ObjectViewer3D();
            _objectViewer.Dock = DockStyle.Fill;
            panelAreaObjectViewer.Controls.Add(_objectViewer);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates all controls and fields with the area passed in.
        /// </summary>
        /// <param name="area"></param>
        public void LoadArea(Area area)
        {
            BackupArea = area;

            // Re-enable controls
            tabControlProperties.Enabled = true;
            buttonApplyChanges.Enabled = true;
            buttonDiscardChanges.Enabled = true;

            // Load data into controls
            nameTextBoxArea.NameText = area.Name;
            tagTextBoxArea.TagText = area.Tag;
            resrefTextBoxArea.ResrefText = area.Resref;

            textBoxAreaComments.Text = area.Comment;
        }

        /// <summary>
        /// Handles updating an area's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (nameTextBoxArea.IsValid && tagTextBoxArea.IsValid)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    Area area = new Area();
                    area.Comment = textBoxAreaComments.Text;
                    area.Name = nameTextBoxArea.NameText;
                    area.Tag = tagTextBoxArea.TagText;
                    area.Resref = resrefTextBoxArea.ResrefText;
                    area.ResourceCategoryID = BackupArea.ResourceCategoryID;

                    repo.Update(BackupArea.Resref, area);
                    BackupArea = area;

                    GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                    eventArgs.GameObject = area;
                    OnSaveArea(this, eventArgs);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name and tag.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!nameTextBoxArea.IsValid)
                {
                    nameTextBoxArea.Focus();
                }
                else
                {
                    tagTextBoxArea.Focus();
                }
            }
        }

        /// <summary>
        /// Handles reverting all input fields to the backup area's values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            textBoxAreaComments.Text = BackupArea.Comment;
            nameTextBoxArea.NameText = BackupArea.Name;
            tagTextBoxArea.TagText = BackupArea.Tag;
            resrefTextBoxArea.ResrefText = BackupArea.Resref;

        }

        #endregion
    }
}
