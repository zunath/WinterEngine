using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.Services
{
    public static class WinterEditorServices
    {
        #region Fields
        private static string _contentPackagesDirectoryName = "ContentPacks";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the directory which contains content packages.
        /// </summary>
        public static string ContentPackagesDirectoryName
        {
            get { return _contentPackagesDirectoryName; }
            set { _contentPackagesDirectoryName = value; }
        }

        #endregion

        #region Events / Delegates

        public static event EventHandler OnGameUpdate;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the OnGameUpdate event. This should ONLY be called
        /// in the main game loop's Update() method.
        /// </summary>
        public static void RaiseOnGameUpdateEvent()
        {
            if (!Object.ReferenceEquals(OnGameUpdate, null))
            {
                OnGameUpdate(null, new EventArgs());
            }
        }

        #endregion
    }
}
