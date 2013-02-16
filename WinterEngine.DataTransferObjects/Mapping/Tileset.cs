using System.ComponentModel;

namespace WinterEngine.DataTransferObjects.Mapping
{
    public class Tileset: IEntity
    {
        #region Properties
        public string Name { get; set; }
        public string GraphicFilePath { get; set; }
        public Tile[][] Tiles { get; set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
