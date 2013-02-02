using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class TilesetViewerControl : UserControl
    {
        #region Fields

        private SpriteSheet _activeSpriteSheet;
        private Cell _selectedCell;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the active sprite sheet displayed in the picture box.
        /// </summary>
        public SpriteSheet ActiveSpriteSheet
        {
            get { return _activeSpriteSheet; }
            set { _activeSpriteSheet = value; }
        }

        /// <summary>
        /// Gets the selected cell in the active sprite sheet.
        /// </summary>
        public Cell SelectedCell
        {
            get { return _selectedCell; }
        }

        #endregion

        #region Constructors

        public TilesetViewerControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        private void pictureBoxTileset_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cell cell = new Cell();
                cell.CellX = e.X / cell.MaxTileWidth;
                cell.CellY = e.Y / cell.MaxTileHeight;
                _selectedCell = cell;

                MessageBox.Show("X = " + _selectedCell.CellX + ", Y = " + _selectedCell.CellY);
            }
        }

        #endregion

        #region Methods

        #endregion

    }
}
