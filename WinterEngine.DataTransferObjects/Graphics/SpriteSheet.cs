using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataTransferObjects.Graphics
{
    [Serializable]
    [Table("SpriteSheets")]
    public class SpriteSheet : GameResource, IEntity
    {
        #region Fields

        private string _resourcePackagePath;
        private string _resourceFileName;
        private string _temporaryDisplayName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the package containing this Sprite Sheet
        /// </summary>
        public string ResourcePackagePath
        {
            get { return _resourcePackagePath; }
            set { _resourcePackagePath = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource file for this Sprite Sheet
        /// </summary>
        public string ResourceFileName
        {
            get { return _resourceFileName; }
            set { _resourceFileName = value; }
        }

        /// <summary>
        /// Gets or sets the temporary display name. 
        /// If this is set, this object's ToString() method will return this value. 
        /// </summary>
        public string TemporaryDisplayName
        {
            get { return _temporaryDisplayName; }
            set { _temporaryDisplayName = value; }
        }

        #endregion

        #region Overrides
        
        /// <summary>
        /// Returns the temporary display name of the Graphic Resource, if available.
        /// Otherwise it returns the base ToString() return value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(TemporaryDisplayName))
            {
                return base.ToString();
            }
            else
            {
                return TemporaryDisplayName;
            }
        }

        #endregion

    }
}
