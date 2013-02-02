using System;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Mapping;
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

        public event EventHandler<GameObjectEventArgs> OnSaveTileset;

        #endregion

        #region Event Handling

        private void buttonAddTileset_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeleteTileset_Click(object sender, EventArgs e)
        {

        }

        private void buttonApplyChangesTilesetDetails_Click(object sender, EventArgs e)
        {

        }

        private void buttonDiscardChangesTilesetDetails_Click(object sender, EventArgs e)
        {

        }

        private void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Constructors

        public GraphicPropertiesControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods


        public void LoadTileset(Tileset tileset)
        {
            BackupTileset = tileset;

            textBoxTilesetName.Text = tileset.Name;

        }

        #endregion
    }
}
