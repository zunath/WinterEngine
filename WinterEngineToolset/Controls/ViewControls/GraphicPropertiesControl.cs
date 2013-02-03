using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
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

        private Tileset _backupTileset;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup tileset which is used to revert changes.
        /// </summary>
        public Tileset BackupTileset
        {
            get { return _backupTileset; }
            set { _backupTileset = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<TilesetEventArgs> OnTilesetChanged;

        #endregion

        #region Event Handling


        private void buttonNewTileset_Click(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Please enter the new tileset's name.", "New Tileset", 1, 64, TilesetNameValidation, CreateTileset, "New Tileset", "Invalid name.");
            inputBox.ShowDialog();
        }

        private void buttonDeleteTileset_Click(object sender, EventArgs e)
        {
            Tileset tileset = listBoxTilesets.SelectedItem as Tileset;

            if (!Object.ReferenceEquals(tileset, null))
            {
                if (MessageBox.Show("Are you sure you want to delete this tileset?", "Delete Tileset?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (TilesetRepository repo = new TilesetRepository())
                    {
                        repo.Delete(tileset);
                    }
                    listBoxTilesets.Items.Remove(listBoxTilesets.SelectedItem);
                    textBoxTilesetName.NameText = String.Empty;
                    comboBoxSpriteSheet.SelectedItem = null;

                    textBoxTilesetName.Enabled = false;
                    comboBoxSpriteSheet.Enabled = false;
                }
            }
        }

        private void comboBoxSpriteSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpriteSheet spriteSheet = comboBoxSpriteSheet.SelectedItem as SpriteSheet;
            Tileset tileset = listBoxTilesets.SelectedItem as Tileset;

            if (!Object.ReferenceEquals(tileset, null))
            {
                tileset.TilesetSpriteSheet = spriteSheet;

                using (TilesetRepository repo = new TilesetRepository())
                {
                    repo.Update(tileset);
                }

                if (!Object.ReferenceEquals(OnTilesetChanged, null))
                {
                    OnTilesetChanged(this, new TilesetEventArgs(tileset));
                }
            }
        }

        private void listBoxTilesets_SelectedValueChanged(object sender, EventArgs e)
        {
            Tileset tileset = listBoxTilesets.SelectedItem as Tileset;

            if (!Object.ReferenceEquals(tileset, null))
            {
                textBoxTilesetName.NameText = tileset.Name;
                BackupTileset = tileset;

                if (!Object.ReferenceEquals(tileset.TilesetSpriteSheet, null))
                {
                    comboBoxSpriteSheet.SelectedItem = tileset.TilesetSpriteSheet;
                }
                else
                {
                    comboBoxSpriteSheet.SelectedItem = comboBoxSpriteSheet.Items[0];
                }

                textBoxTilesetName.Enabled = true;
                comboBoxSpriteSheet.Enabled = true;
            }
            
        }

        private void GraphicPropertiesControl_Load(object sender, EventArgs e)
        {
            PopulateControls();
        }

        #endregion

        #region Constructors

        public GraphicPropertiesControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void CreateTileset(string tilesetName)
        {
            using (TilesetRepository repo = new TilesetRepository())
            {
                Tileset tileset = new Tileset();
                tileset.Name = tilesetName;

                repo.Add(tileset);
                listBoxTilesets.Items.Add(tileset);
            }
        }

        public void LoadTileset(Tileset tileset)
        {
            BackupTileset = tileset;
            textBoxTilesetName.Text = tileset.Name;

            if (!Object.ReferenceEquals(tileset.TilesetSpriteSheet, null))
            {
                comboBoxSpriteSheet.SelectedItem = tileset.TilesetSpriteSheet;
            }
        }

        private void PopulateControls()
        {
            
            using (SpriteSheetRepository repo = new SpriteSheetRepository())
            {
                comboBoxSpriteSheet.Items.Clear();
                comboBoxSpriteSheet.Items.Add("<No Sprite Sheet>");
                comboBoxSpriteSheet.Items.AddRange(repo.GetAll().ToArray());
            }
            
        }

        public void RefreshControls()
        {
        }

        public void UnloadControls()
        {
        }

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
