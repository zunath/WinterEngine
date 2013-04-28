using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;

using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.Resources;
using System.IO;

namespace WinterEngine.Editor.Controls
{
    public partial class AreaPropertiesControl : UserControl, IPropertyControl
    {
        #region Fields

        private Area _backupArea;
        private PictureBox _selectedTilePictureBox;

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

        /// <summary>
        /// Gets or sets the picture box displaying the selected tile.
        /// </summary>
        private PictureBox SelectedTile
        {
            get {  return _selectedTilePictureBox; }
            set { _selectedTilePictureBox = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnSaveArea;

        #endregion

        #region Constructors

        public AreaPropertiesControl()
        {
            InitializeComponent();
            SelectedTile = new PictureBox();
            SelectedTile.Image = WinterEngine_Editor.Icon_SelectedTile;
            SelectedTile.Size = new Size((int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
            SelectedTile.BackColor = Color.Transparent;
            SelectedTile.Visible = false;
            pictureBoxTileset.Controls.Add(SelectedTile);
        }

        #endregion

        #region Event Handling


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
        /// Refreshes all data bindings.
        /// </summary>
        public void RefreshAllControls()
        {
            using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
            {
                listBoxTilesets.DataSource = repo.GetAllByResourceType(ContentPackageResourceTypeEnum.Tileset);
            }
        }

        /// <summary>
        /// Removes all active data bindings.
        /// </summary>
        public void UnloadAllControls()
        {
            listBoxTilesets.DataSource = null;

            nameTextBoxArea.Text = "";
            tagTextBoxArea.Text = "";
            resrefTextBoxArea.Text = "";
            textBoxAreaComments.Text = "";
        }

        #endregion

        #region Tileset Events

        private void pictureBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            int xPosition = e.X / (int)MappingEnum.TileWidth;
            int yPosition = e.Y / (int)MappingEnum.TileHeight;

            pictureBoxTileset.Controls[0].Location = new Point(xPosition * (int)MappingEnum.TileWidth, yPosition * (int)MappingEnum.TileHeight);
        }

        private void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContentPackageResource resource = listBoxTilesets.SelectedItem as ContentPackageResource;

            if (!Object.ReferenceEquals(resource, null))
            {
                using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
                {
                    MemoryStream stream = repo.ExtractResourceToMemory(resource, resource.Package);
                    pictureBoxTileset.Image = Image.FromStream(stream);
                }
            }
        }

        #endregion

    }
}
