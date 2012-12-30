using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class PlaceableRepository : IDisposable
    {
        /// <summary>
        /// Returns all placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<PlaceableRepository> GetAllPlaceables()
        {
            List<PlaceableRepository> _placeableList = new List<PlaceableRepository>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from placeable
                                in context.Placeables
                                select placeable;
                    _placeableList = Mapper.Map(query.ToList<Placeable>(), _placeableList);
                }
            }
            catch (Exception ex)
            {
                _placeableList.Clear();
                MessageBox.Show("Error retrieving all placeables.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _placeableList;
        }

        /// <summary>
        /// Returns a specified placeable by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public PlaceableDTO GetPlaceableByResref(string resref)
        {
            PlaceableDTO retPlaceable = new PlaceableDTO();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from placeable
                                in context.Placeables
                                where placeable.Resref.Equals(resref) // Must match the resref - primary key in database
                                select placeable;
                    List<Placeable> resultPlaceables = query.ToList<Placeable>();
                    retPlaceable = Mapper.Map(resultPlaceables[0], retPlaceable);
                }
            }
            catch (Exception ex)
            {
                retPlaceable = new PlaceableDTO();
                MessageBox.Show("Error retrieving specified placeable (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retPlaceable;
        }

        public void Dispose()
        {
        }
    }
}
