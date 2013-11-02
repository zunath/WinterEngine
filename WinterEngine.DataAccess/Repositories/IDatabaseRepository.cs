using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataAccess
{
    public interface IDatabaseRepository
    {
        void ChangeDatabaseConnection(string databaseFilePath);
        string BuildConnectionString(string databaseFilePath);
        string CreateNewDatabase(string databaseFilePath, string databaseFileName, bool changeApplicationConnection);
    }
}
