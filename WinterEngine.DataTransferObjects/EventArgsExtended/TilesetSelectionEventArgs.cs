using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class TilesetSelectionEventArgs : EventArgs
    {
        public int TilesetResourceID { get; set; }
        public int GraphicResourceID { get; set; }

        public TilesetSelectionEventArgs(int tilesetResourceID, int graphicResourceID)
        {
            this.TilesetResourceID = tilesetResourceID;
            this.GraphicResourceID = graphicResourceID;
        }
    }
}
