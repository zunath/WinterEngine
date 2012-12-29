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
    public class PlaceableRepository
    {
        /// <summary>
        /// Returns all placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<PlaceableRepository> GetAllPlaceables()
        {
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving all placeables.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
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
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving specified placeable (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return retPlaceable;
        }
    }
}
