using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    /// <summary>
    /// Base abstract class for Winter Engine game objects.
    /// </summary>
    [Serializable]
    public abstract class GameObjectBase : IEntity
    {
        #region Fields

        private string _name;
        private string _tag;
        private string _resref;
        private GameObjectTypeEnum _gameObjectType;
        private int _resourceCategoryID;
        private string _comment;

        // Temporary fields (not stored in database)
        private string _temporaryDisplayName;

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

        /// <summary>
        /// GameObjectTypeEnum in int format. This is done so that Entity Framework can store the value in the database correctly.
        /// It is not recommended to use this property in regular code.
        /// </summary>
        public int GameObjectTypeID
        {
            get { return (int)_gameObjectType; }
            set 
            {
                _gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), Convert.ToString(value));
            }
        }

        /// <summary>
        /// Gets or sets the resource type ID for this object.
        /// </summary>
        [NotMapped]
        public GameObjectTypeEnum GameObjectType
        {
            // NOTE: XNA requires that .NET 4.0 be used. Entity Framework does not support storing enumerations in the database for versions less than 5.0
            // This field is not mapped to EF to prevent issues later on if the solution is upgraded to a later version of .NET
            get { return _gameObjectType; }
            set { _gameObjectType = value; }
        }

        /// <summary>
        /// Gets or sets the resource category this object belongs to.
        /// </summary>
        public int ResourceCategoryID
        {
            get { return _resourceCategoryID; }
            set { _resourceCategoryID = value; }
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

        /// <summary>
        /// Gets or sets the temporary display name, for use with the ToString method.
        /// </summary>
        [NotMapped]
        public string TemporaryDisplayName
        {
            get { return _temporaryDisplayName; }
            set { _temporaryDisplayName = value; }
        }

        #endregion

        #region Methods
        #endregion

        #region Overrides

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
