using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using System.Windows.Forms;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ResourceTypeRepository : IDisposable
    {
        public List<ResourceType> GetAllResourceTypes()
        {
            List<ResourceType> _resourceTypeList = new List<ResourceType>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from resourceType
                                in context.ResourceTypes
                                select resourceType;
                    _resourceTypeList = query.ToList<ResourceType>();
                }
            }
            catch (Exception ex)
            {
                _resourceTypeList.Clear();
                ErrorHelper.ShowErrorDialog("Error retrieving all resource types.", ex);
            }

            return _resourceTypeList;
        }

        public void Dispose()
        {
        }
    }
}
