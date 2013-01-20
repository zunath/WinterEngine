using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataAccess.Repositories
{
    /// <summary>
    /// Handles data access to a specific ERF file database.
    /// </summary>
    public class ERFRepository : RepositoryBase, IDisposable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>
        /// Builds a new ERF repository object. The connection string passed in
        /// must be the connection string to the ERF file's database.
        /// </summary>
        /// <param name="connectionString"></param>
        public ERFRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Connects to the specified database and checks for duplicate resources.
        /// Returns a tuple containing the full list of resources and the list of duplicates
        /// Item1: Full list of resources
        /// Item2: Duplicate list of resources
        /// Item3: Non-duplicate list of resources
        /// </summary>
        /// <param name="databaseFilePath"></param>
        /// <returns></returns>
        public Tuple<List<GameObject>, List<GameObject>, List<GameObject>> GetDuplicateResources()
        {
            List<GameObject> fullList = new List<GameObject>();
            List<GameObject> duplicateList = new List<GameObject>();
            List<GameObject> nonDuplicateList = new List<GameObject>();

            // Connect to the ERF database
            using (WinterContext erfContext = new WinterContext(ConnectionString))
            {
                // Connect to the main application database.
                using (WinterContext mainContext = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    foreach (Area area in erfContext.Areas)
                    {
                        if (!Object.ReferenceEquals(mainContext.Areas.FirstOrDefault(x => x.Resref == area.Resref), null))
                        {
                            duplicateList.Add(area);
                        }
                        else
                        {
                            nonDuplicateList.Add(area);
                        }
                        fullList.Add(area);
                    }

                    foreach (Creature creature in erfContext.Creatures)
                    {
                        if (!Object.ReferenceEquals(mainContext.Creatures.FirstOrDefault(x => x.Resref == creature.Resref), null))
                        {
                            duplicateList.Add(creature);
                        }
                        else
                        {
                            nonDuplicateList.Add(creature);
                        }
                        fullList.Add(creature);
                    }

                    foreach (Item item in erfContext.Items)
                    {
                        if (!Object.ReferenceEquals(mainContext.Items.FirstOrDefault(x => x.Resref == item.Resref), null))
                        {
                            duplicateList.Add(item);
                        }
                        else
                        {
                            nonDuplicateList.Add(item);
                        }
                        fullList.Add(item);
                    }

                    foreach (Placeable placeable in erfContext.Placeables)
                    {
                        if (!Object.ReferenceEquals(mainContext.Placeables.FirstOrDefault(x => x.Resref == placeable.Resref), null))
                        {
                            duplicateList.Add(placeable);
                        }
                        else
                        {
                            nonDuplicateList.Add(placeable);
                        }
                        fullList.Add(placeable);
                    }
                }
            }

            return new Tuple<List<GameObject>,List<GameObject>, List<GameObject>>(fullList, duplicateList, nonDuplicateList);
        }

        public void Dispose()
        {
        }
        #endregion
    }
}
