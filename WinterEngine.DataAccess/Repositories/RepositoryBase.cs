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

        private UnitOfWork _unitOfWork;
        private string _connectionString;
        private bool _autoSaveChanges;

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

        public UnitOfWork Context
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// Gets or sets whether the repository will save all changes before being disposed.
        /// This is used only if the repository is called in a using statement.
        /// </summary>
        public bool AutoSaveChanges
        {
            get { return _autoSaveChanges; }
            set { _autoSaveChanges = value; }
        }

        #endregion

        #region Constructors

        public RepositoryBase(string connectionString = "", bool autoSaveChanges = true)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;
            _unitOfWork = new UnitOfWork(ConnectionString);
            _autoSaveChanges = autoSaveChanges;
        }

        #endregion

        #region Methods

        public void SaveChanges()
        {
            Context.Save();
        }

        #endregion
    }
}
