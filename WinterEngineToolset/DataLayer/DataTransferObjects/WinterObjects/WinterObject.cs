using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DejaVu;
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

        readonly UndoRedo<string> _name = new UndoRedo<string>();
        readonly UndoRedo<string> _tag = new UndoRedo<string>();
        readonly UndoRedo<string> _resref = new UndoRedo<string>();
        readonly UndoRedo<int> _resourceCategoryID = new UndoRedo<int>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets/Sets the publicly viewable name for an object.
        /// </summary>
        [MaxLength(64)]
        public string Name
        {
            get { return _name.Value; }
            set { _name.Value = value; }
        }

        /// <summary>
        /// Gets/Sets a particular object's tag.
        /// </summary>
        [MaxLength(32)]
        public string Tag
        {
            get { return _tag.Value; }
            set { _tag.Value = value; }
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
                if (_resref.Value == null)
                {
                    return _resref.Value;
                }
                else
                {
                    return _resref.Value.ToLower(); 
                }
            }
            set { _resref.Value = value.ToLower(); }
        }

        public int ResourceCategoryID
        {
            get { return _resourceCategoryID.Value; }
            set { _resourceCategoryID.Value = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
