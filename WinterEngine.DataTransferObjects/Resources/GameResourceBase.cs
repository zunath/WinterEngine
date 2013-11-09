﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    /// <summary>
    /// Base abstract class for Winter Engine game resources.
    /// </summary>
    [Serializable]
    public abstract class GameResourceBase : IEntity
    {
        #region Fields

        private int _resourceID;
        private ResourceTypeEnum _resourceType;
        private string _name;
        private string _comment;
        private bool _isSystemResource;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a resource's ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// Gets or sets the resource type of this object
        /// </summary>
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
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
        /// Gets or sets whether or not a resource is a system resource.
        /// System resources normally cannot be modified by the end user.
        /// </summary>
        public bool IsSystemResource
        {
            get { return _isSystemResource; }
            set { _isSystemResource = value; }
        }

        public bool IsDefault { get; set; }

        public DateTime CreateDate { get; set; }
        

        #endregion

        #region Constructors

        public GameResourceBase()
        {
            CreateDate = DateTime.UtcNow;
        }

        #endregion

        #region Methods
        #endregion

        #region Overrides

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion

    }
}
