using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoMapper;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class PlaceableRepository : IWinterObjectRepository
    {
        /// <summary>
        /// Returns all placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjects()
        {
            List<PlaceableDTO> _placeableList = new List<PlaceableDTO>();

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

            return _placeableList.ConvertAll(x => (WinterObjectDTO)x);
        }

        /// <summary>
        /// Returns all areas that match a particular ResourceCategoryDTO's CategoryID field.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjectsByResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            List<PlaceableDTO> _placeableRepository = new List<PlaceableDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from placeable
                                in context.Placeables
                                where placeable.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                select placeable;
                    _placeableRepository = Mapper.Map(query.ToList<Placeable>(), _placeableRepository);
                }
            }
            catch (Exception ex)
            {
                _placeableRepository.Clear();
                MessageBox.Show("Error retrieving placeables by resource category.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _placeableRepository.ConvertAll(x => (WinterObjectDTO)x);
        }

        /// <summary>
        /// Returns a specified placeable by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public WinterObjectDTO GetObjectByResref(string resref)
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

            return retPlaceable as WinterObjectDTO;
        }

        public void Dispose()
        {
        }
    }
}
