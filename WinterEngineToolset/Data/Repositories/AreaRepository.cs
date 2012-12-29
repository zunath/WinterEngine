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
        /// <summary>
        /// Returns all areas from the database.
        /// </summary>
        /// <returns></returns>
        public List<AreaDTO> GetAllAreas()
        {
            List<AreaDTO> _areaList = new List<AreaDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from area
                                in context.Areas
                                select area;
                    _areaList = Mapper.Map(query.ToList<Area>(), _areaList);
                }
            }
            catch (Exception ex)
            {
                _areaList.Clear();
                MessageBox.Show("Error retrieving all areas.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _areaList;
        }

        /// <summary>
        /// Returns a specified area by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public AreaDTO GetAreaByResref(string resref)
        {
            AreaDTO retArea = new AreaDTO();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from area
                                in context.Areas
                                where area.Resref.Equals(resref) // Must match the resref - primary key in database
                                select area;
                    List<Area> resultAreas = query.ToList<Area>();
                    retArea = Mapper.Map(resultAreas[0], retArea);
                }
            }
            catch (Exception ex)
            {
                retArea = null;
                MessageBox.Show("Error retrieving specified area (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retArea;
        }
    }
}
