using FlatRedBall.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Paths
{
    public static class FilePaths
    {
        #region Constants

        private const string _serverSettingsFileName = "ServerSettings.xml";

        #endregion

        #region Properties

        public static string ServerSettingsPath
        {
            get
            {
                return FileManager.RelativeDirectory + @"Content/Settings/" + _serverSettingsFileName;
            }
        }

        #endregion
    }
}
