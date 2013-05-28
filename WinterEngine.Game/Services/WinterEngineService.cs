using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.Game.Services
{
    public static class WinterEngineService
    {
        #region Fields

        private static UserProfile _userProfile;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently active user profile.
        /// </summary>
        public static UserProfile ActiveUserProfile
        {
            get { return _userProfile; }
        }

        #endregion

        #region Events / Delegates

        public static event EventHandler OnXNAUpdate;
        public static event EventHandler OnXNADraw;

        #endregion

        #region Methods

        /// <summary>
        /// Hooks into the raw XNA update method. 
        /// This is called before FlatRedBall and internal XNA methods.
        /// </summary>
        public static void Update()
        {
            if (!Object.ReferenceEquals(OnXNAUpdate, null))
            {
                OnXNAUpdate(null, new EventArgs());
            }
        }

        /// <summary>
        /// Hooks into the raw XNA draw method.
        /// This is called before FlatRedBall and internal XNA methods.
        /// </summary>
        public static void Draw()
        {
            if (!Object.ReferenceEquals(OnXNADraw, null))
            {
                OnXNADraw(null, new EventArgs());
            }
        }

        /// <summary>
        /// Replaces any existing user profile with a specified one.
        /// </summary>
        /// <param name="profile"></param>
        public static void InitializeUserProfile(UserProfile profile)
        {
            _userProfile = profile;
        }

        #endregion
    }
}
