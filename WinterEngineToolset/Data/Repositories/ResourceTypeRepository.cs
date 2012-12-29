using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;
using DejaVu;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ResourceTypeRepository: IDisposable
    {
        public List<ResourceTypeDTO> GetAllResourceTypes()
        {
            UndoRedoManager.StartInvisible("Data Access");
            List<ResourceTypeDTO> _resourceTypeList = new List<ResourceTypeDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from resourceType
                                in context.ResourceTypes
                                select resourceType;
                    _resourceTypeList = Mapper.Map(query.ToList<ResourceType>(), _resourceTypeList);
                }
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving all resource types.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return _resourceTypeList;
        }

        public void Dispose()
        {
        }
    }
}
