using System.Windows.Forms;
using WinterEngine.Forms.Controls.FlatRedBall;

namespace WinterEngine.Editor.Graphics
{
    public partial class TilesetSpriteSheetControl : UserControl
    {
        public TilesetSpriteSheetControl()
        {
            bool debug = false; // Used to prevent FRB from loading in design time. Set to false when running.

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
