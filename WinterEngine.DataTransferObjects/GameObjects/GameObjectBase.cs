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

        [MaxLength(4000)]
        public string Description { get; set; }

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
        /// Gets or sets the resource type ID for this object.
        /// </summary>
        public GameObjectTypeEnum GameObjectType
        {
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
