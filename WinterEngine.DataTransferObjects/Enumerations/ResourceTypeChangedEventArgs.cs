using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public class ResourceTypeChangedEventArgs : EventArgs
    {
        #region Fields

        private ContentPackageResourceTypeEnum _resourceType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource type passed via event args
        /// </summary>
        public ContentPackageResourceTypeEnum GameObjectType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        #endregion


    }
}
