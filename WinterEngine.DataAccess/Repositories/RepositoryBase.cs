using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataAccess.Repositories
{
    public class RepositoryBase
    {
        #region Fields

        private string _connectionString;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connection string local to this repository.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion
    }
}
