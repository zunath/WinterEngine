using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Contains Winter Engine-specific connection information for the database and data layer.
    /// </summary>
    public static class WinterConnectionInformation
    {
        private static string _connectionString;
        private static string _activeModuleDirectory;

        /// <summary>
        /// Gets or sets the active connection string. All database calls will utilize this 
        /// connection string.
        /// </summary>
        public static string ActiveConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Gets or sets the active module directory path.
        /// This is the directory which contains all database and other files related to the module.
        /// </summary>
        public static string ActiveModuleDirectoryPath
        {
            get { return _activeModuleDirectory; }
            set { _activeModuleDirectory = value; }
        }
    }
}
