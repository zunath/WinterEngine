using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageResourceRepository : RepositoryBase
    {
        #region Constructors

        public ContentPackageResourceRepository(string connectionString = "")
            : base(connectionString)
        {
        }

        #endregion

        #region Methods

        public void Dispose()
        {
        }

        #endregion
    }
}
