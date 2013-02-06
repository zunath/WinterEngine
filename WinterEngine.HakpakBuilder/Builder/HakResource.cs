using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.HakpakBuilder.Builder
{
    class HakResource
    {
        #region Fields

        private string _resourceName;
        private string _resourcePath;
        private HakResourceTypeEnum _resourceType;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        /// <summary>
        /// Gets or sets the path to the resource.
        /// </summary>
        public string ResourcePath
        {
            get { return _resourcePath; }
            set { _resourcePath = value; }
        }

        /// <summary>
        /// Gets or sets the hak resource type of the hak resource.
        /// </summary>
        public HakResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        #endregion

        #region Constructors

        public HakResource(string resourcePath, HakResourceTypeEnum resourceType)
        {
            this.ResourcePath = resourcePath;
            this.ResourceType = resourceType;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// ToString override for use with the list boxes and other WinForms controls.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retValue = ResourcePath;

            if (!String.IsNullOrWhiteSpace(ResourceName))
            {
                retValue = ResourceName;
            }

            return retValue;
        }

        #endregion

    }
}
