using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    /// <summary>
    /// Base abstract class for Winter Engine game resources.
    /// </summary>
    [Serializable]
    public abstract class GameResource
    {
        #region Fields

        private int _resourceID;
        private int _resourceTypeID;
        private string _name;
        private string _comment;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a resource's ID
        /// </summary>
        [Key]
        public int ResourceID
        {
            get { return _resourceID; }
            set { _resourceID = value; }
        }

        /// <summary>
        /// Gets/Sets the publicly viewable name for a resource.
        /// </summary>
        [MaxLength(64)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the resource type ID for this object.
        /// </summary>
        public int ResourceTypeID
        {
            get { return _resourceTypeID; }
            set { _resourceTypeID = value; }
        }

        /// <summary>
        /// Gets or sets the comment attached to an object in the toolset
        /// </summary>
        [MaxLength(4000)]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        #endregion

        #region Methods
        #endregion

        #region Overrides

        #endregion

    }
}
