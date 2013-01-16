using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.ERF
{
    public class ERFResource
    {
        #region Fields

        private ResourceTypeEnum _resourceType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource type of an ERF Resource.
        /// </summary>
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        #endregion

        #region Constructors

        public ERFResource(ResourceTypeEnum resourceType)
        {
            this.ResourceType = resourceType;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return ResourceType.ToString();
        }

        #endregion
    }
}
