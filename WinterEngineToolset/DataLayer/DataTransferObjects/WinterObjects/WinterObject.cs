using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects
{
    /// <summary>
    /// Base abstract class for Winter Engine user interface data transfer objects. All UI object DTOs need to inherit this base class.
    /// </summary>
    [Serializable]
    public abstract class WinterObject
    {
        #region Fields

        string _name;
        string _tag;
        string _resref;
        int _resourceCategoryID;

        #endregion

        #region Properties

        /// <summary>
        /// Gets/Sets the publicly viewable name for an object.
        /// </summary>
        [MaxLength(64)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets/Sets a particular object's tag.
        /// </summary>
        [MaxLength(32)]
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Gets/Sets a particular object's resref.
        /// This is a unique identifier used as the primary key in the embedded database.
        /// Automatically converts all resrefs to lower case. This maintains consistency throughout the engine.
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string Resref
        {
            get 
            {
                if (_resref == null)
                {
                    return _resref;
                }
                else
                {
                    return _resref.ToLower(); 
                }
            }
            set { _resref = value.ToLower(); }
        }

        public int ResourceCategoryID
        {
            get { return _resourceCategoryID; }
            set { _resourceCategoryID = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
