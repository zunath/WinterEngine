using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.HakpakBuilder.Builder
{
    public class ResourceTypeChangedEventArgs : EventArgs
    {
        #region Fields

        private HakResourceTypeEnum _resourceType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource type passed via event args
        /// </summary>
        public HakResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        #endregion


    }
}
