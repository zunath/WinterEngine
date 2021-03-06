﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Network.Clients;

namespace WinterEngine.Game.Services
{
    public static class WinterEngineService
    {
        #region Fields

        private static UserProfile _userProfile;
        private static GameNetworkClient _networkClient;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently active user profile.
        /// </summary>
        public static UserProfile ActiveUserProfile
        {
            get { return _userProfile; }
        }

        /// <summary>
        /// Gets the active network client which contains data about the current connection.
        /// </summary>
        public static GameNetworkClient NetworkClient
        {
            get { return _networkClient; }
        }

        #endregion

        #region Events / Delegates

        #endregion

        #region Methods

        /// <summary>
        /// Replaces any existing user profile with a specified one.
        /// </summary>
        /// <param name="profile"></param>
        public static void InitializeUserProfile(UserProfile profile)
        {
            _userProfile = profile;
        }

        /// <summary>
        /// Replaces any existing game network client with a specified one.
        /// </summary>
        /// <param name="client"></param>
        public static void InitializeNetworkClient()
        {
            GameNetworkClient client = new GameNetworkClient();
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                client.Username = WinterEngineService.ActiveUserProfile.UserName;
            }

            _networkClient = client;
        }

        #endregion
    }
}
