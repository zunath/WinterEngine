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
    public class PlaceableDTO
    {
        public List<PlaceableDTO> GetAllPlaceables()
        {
            List<PlaceableDTO> _placeableList = new List<PlaceableDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var placeables = from placeable
                                in context.Placeables
                                select placeable;
                    _placeableList = Mapper.Map(placeables.ToList<Placeable>(), _placeableList);
                }
            }
            catch (Exception ex)
            {
                _placeableList.Clear();
                MessageBox.Show("Error retrieving all placeables.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _placeableList;
        }
    }
}
