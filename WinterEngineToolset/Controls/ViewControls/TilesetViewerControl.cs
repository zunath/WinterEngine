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
using WinterEngine.Toolset.Controls.XnaControls.TilesetEditor;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class TilesetViewerControl : UserControl
    {
        #region Fields

        private XNATilesetControl _tilesetControlXNA;
        private SpriteSheet _activeSpriteSheet;

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
        /// Gets or sets the XNA control used by this control.
        /// </summary>
        public XNATilesetControl TilesetControlXNA
        {
            get { return _tilesetControlXNA; }
            set { _tilesetControlXNA = value; }
        }

        #endregion

        #region Constructors

        public TilesetViewerControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles loading the Tileset XNA control in the appropriate panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TilesetViewerControl_Load(object sender, EventArgs e)
        {
            TilesetControlXNA = new XNATilesetControl();
            TilesetControlXNA.Dock = DockStyle.Fill;
            panelTilesetViewer.Controls.Add(TilesetControlXNA);
        }


        private void radioButtonPassage_CheckedChanged(object sender, EventArgs e)
        {
            TilesetControlXNA.Mode = TilesetEditorModeTypeEnum.Passage;
        }

        #endregion

        #region Methods

        #endregion

    }
}
