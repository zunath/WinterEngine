using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class AreaRepository
    {
        public List<AreaDTO> GetAllAreas()
        {
            List<AreaDTO> _areaList = new List<AreaDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var areas = from area
                                        in context.Areas
                                        select area;
                    _areaList = Mapper.Map(areas.ToList<Area>(), _areaList);
                }
            }
            catch (Exception ex)
            {
                _areaList.Clear();
                MessageBox.Show("Error retrieving all areas.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _areaList;
        }
    }
}
