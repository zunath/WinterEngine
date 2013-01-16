using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : IDisposable
    {
        /// <summary>
        /// Changes the database connection to the specified path. All subsequent database calls
        /// will utilize this connection until changed.
        /// </summary>
        /// <param name="databaseFilePath">The full path to the database file to which the conection will be changed.</param>
        public void ChangeDatabaseConnection(string databaseFilePath)
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            // Change the active connection to point to this new database.
            WinterConnectionInformation.ActiveConnectionString = "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
        }

        /// <summary>
        /// Creates a new database file at the specified path with the specified file name.
        /// The ".sdf" extension will be added to the file. Do not pass it in the file name.
        /// </summary>
        /// <param name="databaseFilePath">The path to the file, excluding the file's name</param>
        /// <param name="databaseFileName">The name of the new database file. Exclude the .sdf extension - it will be added automatically.</param>
        public void CreateNewDatabase(string databaseFilePath, string databaseFileName)
        {
            string fullPath = databaseFilePath + "\\" + databaseFileName + ".sdf";

            // Update connection settings and the active module directory path
            ChangeDatabaseConnection(fullPath);
            WinterConnectionInformation.ActiveModuleDirectoryPath = fullPath;

            // Initialize the database - will create the database file at the specified location.
            // Also creates tables based on the code-first model.
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Database.Initialize(true);
            }
        }

        /// <summary>
        /// Adds a new module detail to the active module.
        /// If the detail already exists, an exception will be thrown.
        /// </summary>
        /// <param name="detailName">The unique string used to identify the detail. (Max: 32 characters)</param>
        /// <param name="detailValue">The value to store in the database. (Max: 64 characters)</param>
        public void AddModuleDetail(string detailName, string detailValue)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ModuleDetail detail = new ModuleDetail();
                detail.DetailName = detailName;
                detail.DetailValue = detailValue;
                
                context.ModuleDetails.Add(detail);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing detail in the database.
        /// If the detail does not exist, an exception will be thrown.
        /// </summary>
        /// <param name="detailName">The unique string used to identify the detail. (Max: 32 characters)</param>
        /// <param name="detailNewValue">The value to store in the database. (Max: 64 characters)</param>
        public void UpdateModuleDetail(string detailName, string detailNewValue)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ModuleDetail detail = context.ModuleDetails.SingleOrDefault(x => x.DetailName == detailName);
                detail.DetailValue = detailNewValue;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// If a module detail exists, its value will be updated.
        /// If a module detail does not exist, a new one will be created with the specified value.
        /// </summary>
        /// <param name="detailName">The unique string used to identify the detail. (Max: 32 characters)</param>
        /// <param name="detailNewValue">The value to store in the database. (Max: 64 characters)</param>
        public void UpsertModuleDetail(string detailName, string detailNewValue)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ModuleDetail detail = context.ModuleDetails.SingleOrDefault(x => x.DetailName == detailName);

                // Detail does not exist - create a new one with the specified value.
                if (Object.ReferenceEquals(detail, null))
                {
                    detail = new ModuleDetail();
                    detail.DetailName = detailName;
                    detail.DetailValue = detailNewValue;
                }
                // Detail exists - update the existing one.
                else
                {
                    detail.DetailValue = detailNewValue;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing detail from the database.
        /// If the detail does not exist, nothing will happen.
        /// </summary>
        /// <param name="detailName">The unique string used to identify the details. (Max: 32 characters)</param>
        public void DeleteModuleDetail(string detailName)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ModuleDetail detail = context.ModuleDetails.First(x => x.DetailName == detailName);
                context.ModuleDetails.Remove(detail);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns true if a module detail exists in the database with the specified name.
        /// </summary>
        /// <param name="detailName">The unique string used to identify the details. (Max: 32 characters)</param>
        /// <returns></returns>
        public bool DoesModuleDetailExist(string detailName)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ModuleDetail detail = context.ModuleDetails.SingleOrDefault(x => x.DetailName == detailName);
                if (Object.ReferenceEquals(detail, null))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void Dispose()
        {
        }

    }
}
