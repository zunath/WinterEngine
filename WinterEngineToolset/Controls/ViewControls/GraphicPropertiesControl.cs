using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AutoMapper;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Mapping;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class GraphicPropertiesControl : UserControl
    {
        #region Fields

        private Tileset _activeTileset;

        #endregion

        #region Properties

        /// <summary>
        ///  Gets or sets the active tileset.
        /// </summary>
        private Tileset ActiveTileset
        {
            get { return _activeTileset; }
            set { _activeTileset = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<TilesetEditorEventArgs> OnTilesetChanged;
        public event EventHandler OnUnloadTileset;

        #endregion

        #region Event Handling


        /// <summary>
        /// Handles updating the database and the selected item in the list box with changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            using (TilesetRepository repo = new TilesetRepository())
            {
                ActiveTileset.Name = textBoxTilesetName.NameText;

                SpriteSheet spriteSheet = comboBoxSpriteSheet.SelectedItem as SpriteSheet;

                if (!Object.ReferenceEquals(spriteSheet, null))
                {
                    ActiveTileset.SpriteSheetID = spriteSheet.ResourceID;
                }
                else
                {
                    ActiveTileset.SpriteSheetID = 0;
                }

                repo.Update(ActiveTileset);
            }
        }

        /// <summary>
        /// Reverts changes to the active item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiscardChanges_Click(object sender, EventArgs e)
        {
            textBoxTilesetName.NameText = ActiveTileset.Name;
            SelectMatchingSpritesheetItem(ActiveTileset.SpriteSheetID);
        }

        /// <summary>
        /// Prompts users to enter the name of a tileset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTileset_Click(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Please enter the new tileset's name.", "New Tileset", 1, 64, TilesetNameValidation, CreateTileset, "New Tileset", "Invalid name.");
            inputBox.ShowDialog();
        }

        /// <summary>
        /// Prompts user to confirm deletion of a tileset. If user chooses yes, the tileset is removed from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteTileset_Click(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(ActiveTileset, null))
            {
                if (MessageBox.Show("Are you sure you want to delete this tileset?", "Delete Tileset?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (TilesetRepository repo = new TilesetRepository())
                    {
                        repo.Delete(ActiveTileset);
                    }
                    listBoxTilesets.Items.Remove(listBoxTilesets.SelectedItem);
                    textBoxTilesetName.NameText = String.Empty;
                    comboBoxSpriteSheet.SelectedItem = null;

                    textBoxTilesetName.Enabled = false;
                    comboBoxSpriteSheet.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Updates the temporary tileset object's sprite sheet selection.
        /// This is not permanent until the ApplyChanges button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSpriteSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpriteSheet spriteSheet = comboBoxSpriteSheet.SelectedItem as SpriteSheet;

            if (!Object.ReferenceEquals(ActiveTileset, null))
            {
                if (!Object.ReferenceEquals(OnTilesetChanged, null))
                {
                    // Note: The sprite sheet is passed separately from the tileset since we don't want
                    // to actually modify the object until the user presses Apply Changes.
                    OnTilesetChanged(this, new TilesetEditorEventArgs(ActiveTileset, spriteSheet));
                }
            }
            else
            {
                OnUnloadTileset(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles changing the current tileset and populating the form's controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxTilesets_SelectedValueChanged(object sender, EventArgs e)
        {
            // This check prevents the active tileset from changing if the user doesn't actually
            // select a new object.
            if (!Object.Equals(ActiveTileset, listBoxTilesets.SelectedItem as Tileset))
            {
                ActiveTileset = listBoxTilesets.SelectedItem as Tileset;

                if (!Object.ReferenceEquals(ActiveTileset, null))
                {
                    LoadTileset(ActiveTileset);

                    textBoxTilesetName.Enabled = true;
                    comboBoxSpriteSheet.Enabled = true;
                }
            }
        }

        #endregion

        #region Constructors

        public GraphicPropertiesControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void SelectMatchingSpritesheetItem(int spriteSheetID)
        {
            // To-Do: Optimize the search algorithm.
            foreach (var current in comboBoxSpriteSheet.Items)
            {
                SpriteSheet spriteSheet = current as SpriteSheet;
                if (!Object.ReferenceEquals(spriteSheet, null))
                {
                    if (spriteSheet.ResourceID == spriteSheetID)
                    {
                        comboBoxSpriteSheet.SelectedItem = current;
                        break;
                    }
                }
            }
        }

        private void LoadTileset(Tileset tileset)
        {
            textBoxTilesetName.NameText = tileset.Name;

            if (tileset.SpriteSheetID > 0)
            {
                SelectMatchingSpritesheetItem(tileset.SpriteSheetID);
            }
            else
            {
                comboBoxSpriteSheet.SelectedItem = comboBoxSpriteSheet.Items[0];
            }

        }

        /// <summary>
        /// Creates a new tileset and adds it to the database. Also adds it to the list box.
        /// </summary>
        /// <param name="tilesetName"></param>
        private void CreateTileset(string tilesetName)
        {
            using (TilesetRepository repo = new TilesetRepository())
            {
                Tileset tileset = new Tileset();
                tileset.Name = tilesetName;

                repo.Add(tileset);
                int index = listBoxTilesets.Items.Add(tileset);
                listBoxTilesets.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Populates form controls using data retrieved from the database.
        /// </summary>
        private void PopulateControls()
        {
            if (!DesignMode)
            {
                using (SpriteSheetRepository repo = new SpriteSheetRepository())
                {
                    comboBoxSpriteSheet.Items.Clear();
                    comboBoxSpriteSheet.Items.Add("<No Sprite Sheet>");
                    comboBoxSpriteSheet.Items.AddRange(repo.GetAll().ToArray());
                }

                using (TilesetRepository repo = new TilesetRepository())
                {
                    listBoxTilesets.Items.AddRange(repo.GetAll().ToArray());
                }

            }
        }

        /// <summary>
        /// Handles unloading control data and then populating them again.
        /// </summary>
        public void RefreshControls()
        {
            UnloadControls();
            PopulateControls();
        }

        /// <summary>
        /// Handles unloading control data.
        /// </summary>
        public void UnloadControls()
        {
            listBoxTilesets.Items.Clear();
            comboBoxSpriteSheet.Items.Clear();
        }

        /// <summary>
        /// Validation method used to ensure proper data is entered for the tileset's name.
        /// </summary>
        /// <param name="tilesetName"></param>
        /// <returns></returns>
        private bool TilesetNameValidation(string tilesetName)
        {
            bool success = true;

            if (tilesetName.Length <= 0)
            {
                success = false;
            }

            return success;
        }

        #endregion
    }
}
