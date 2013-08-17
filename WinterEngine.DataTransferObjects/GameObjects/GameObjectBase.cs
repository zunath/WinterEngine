using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    /// <summary>
    /// Base abstract class for Winter Engine game objects.
    /// </summary>
    [Serializable]
    public abstract class GameObjectBase : GameResourceBase
    {
        #region Fields

        private string _tag;
        private string _resref;
        private GameObjectTypeEnum _gameObjectType;
        private int _resourceCategoryID;
        private Category _resourceCategory;
        private int? _graphicResourceID;
        private ContentPackageResource _graphicResource;

        // Temporary fields (not stored in database)
        private string _temporaryDisplayName;


        #endregion

        #region Properties

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
        /// Automatically converts all resrefs to lower case. This maintains consistency throughout the engine.
        /// </summary>
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
        
        [ForeignKey("ResourceCategoryID")]
        public virtual Category ResourceCategory
        {
            get { return _resourceCategory; }
            set { _resourceCategory = value; }
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

        [ForeignKey("GraphicResourceID")]
        public virtual ContentPackageResource GraphicResource
        {
            get { return _graphicResource; }
            set { _graphicResource = value; }
        }

        public int? GraphicResourceID
        {
            get { return _graphicResourceID; }
            set { _graphicResourceID = value; }
        }

        public virtual List<LocalVariable> LocalVariables { get; set; }

        #endregion

        #region Constructors

        public GameObjectBase()
        {
            this.ResourceType = ResourceTypeEnum.GameObject;
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
