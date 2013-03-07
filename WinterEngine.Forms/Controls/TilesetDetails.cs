using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.Forms.Controls.Standard;
using WinterEngine.DataTransferObjects.Mapping;
using WinterEngine.Library.Factories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Forms.Shared;

namespace WinterEngine.Forms.Toolset
{
    public partial class TilesetDetails : UserControl
    {
        #region Fields

        private Tileset _activeTileset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the active tileset.
        /// </summary>
        private Tileset ActiveTileset
        {
            get { return _activeTileset; }
            set { _activeTileset = value; }
        }

        #endregion

        #region Constructors

        public TilesetDetails()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Prompts user to enter the name of the new tileset and then creates it once user is done.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTileset_Click(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Please enter the new tileset's name.", "New Tileset", 1, 64, TilesetNameValidation, CreateTileset, "New Tileset", "Invalid name.");
            inputBox.ShowDialog();
        }

        /// <summary>
        /// Prompts user to confirm deletion of tileset and disables controls once complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteTileset_Click(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(ActiveTileset, null))
            {
                if (MessageBox.Show("Are you sure you want to delete this tileset?", "Delete Tileset?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    listBoxTilesets.Items.Remove(listBoxTilesets.SelectedItem);
                    textBoxTilesetName.Text = String.Empty;
                    textBoxFilePath.Text = "No Graphic File Selected";

                    textBoxTilesetName.Enabled = false;
                    buttonBrowse.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Changes the active tileset and loads it into the form's controls.
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
                    buttonBrowse.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Handles prompting user to select a valid graphic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Tileset tileset = listBoxTilesets.SelectedItem as Tileset;
                tileset.GraphicFilePath = openFileDialog.FileName;
                textBoxFilePath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Handles initialization of the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TilesetDetailsControl_Load(object sender, EventArgs e)
        {
            InitializeFileBrowserFilters();
        }

        /// <summary>
        /// Updates the currently selected tileset's name as it's entered by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTilesetName_TextChanged(object sender, EventArgs e)
        {
            Tileset tileset = listBoxTilesets.SelectedItem as Tileset;

            if (!Object.ReferenceEquals(tileset, null))
            {
                tileset.Name = textBoxTilesetName.Text;

                // The list box items do not update on their own. We force them to do this by "replacing" the selected item with itself.
                // Weird, but it's a workaround!
                listBoxTilesets.Items[listBoxTilesets.SelectedIndex] = listBoxTilesets.SelectedItem;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the control's fields with data retrieved from the tileset object.
        /// </summary>
        /// <param name="tileset"></param>
        private void LoadTileset(Tileset tileset)
        {
            textBoxTilesetName.Text = tileset.Name;

            if (String.IsNullOrWhiteSpace(tileset.GraphicFilePath))
            {
                textBoxFilePath.Text = "No Graphic File Selected";
            }
            else
            {
                textBoxFilePath.Text = tileset.GraphicFilePath;
            }
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


        /// <summary>
        /// Creates a new tileset and adds it to the list box.
        /// </summary>
        /// <param name="tilesetName"></param>
        private void CreateTileset(string tilesetName)
        {
            Tileset tileset = new Tileset();
            tileset.Name = tilesetName;

            int index = listBoxTilesets.Items.Add(tileset);
            listBoxTilesets.SelectedIndex = index;
        }

        /// <summary>
        /// Returns the list of tilesets contained by this control.
        /// </summary>
        /// <returns></returns>
        public List<Tileset> GetTilesets()
        {
            List<Tileset> tilesetList = new List<Tileset>();

            foreach (Tileset currentTileset in listBoxTilesets.Items)
            {
                tilesetList.Add(currentTileset);
            }

            return tilesetList;
        }

        /// <summary>
        /// Sets the filters for all file browsers used by this control
        /// </summary>
        private void InitializeFileBrowserFilters()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            openFileDialog.Filter = factory.BuildGraphicFileFilter();
        }

        #endregion


    }
}
