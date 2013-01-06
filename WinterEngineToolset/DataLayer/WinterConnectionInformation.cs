using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Toolset.DataLayer
{
    /// <summary>
    /// Contains Winter Engine-specific connection information for the database and data layer.
    /// </summary>
    public static class WinterConnectionInformation
    {
        private static string _connectionString;

        /// <summary>
        /// Gets or sets the active connection string. All database calls will utilize this 
        /// connection string.
        /// </summary>
        public static string ActiveConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

    }
}
