using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.Services
{
    public static class WinterEditorServices
    {
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
