using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.Forms.Controls;

namespace WinterEngine.Editor.Graphics
{
    public partial class TilesetSpriteSheetControl : UserControl
    {
        public TilesetSpriteSheetControl()
        {
            bool debug = true; // Used to prevent FRB from loading in design time. Remove when running.

            InitializeComponent();

            if (!DesignMode && !debug)
            {
                FRBControl frbControl = new FRBControl();
                frbControl.Dock = DockStyle.Fill;
                panelSpriteSheet.Controls.Add(frbControl);
            }
        }
    }
}
