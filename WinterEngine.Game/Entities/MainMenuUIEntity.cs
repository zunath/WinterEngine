using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Diagnostics;
using FlatRedBall.Screens;
using WinterEngine.Game.Screens;
using WinterEngine.Network;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Enums;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class MainMenuUIEntity
    {

        #region Fields

        private UserProfile _userProfile;

        #endregion

        #region Properties

        private UserProfile Profile
        {
            get { return _userProfile; }
            set { _userProfile = value; }
        }

        #endregion

        #region FRB Events

        private void CustomInitialize()
		{
            AwesomiumWebView.DocumentReady += OnDocumentReady;
		}

		private void CustomActivity()
		{
            

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        
        #endregion

        #region Awesomium Event Handling

        /// <summary>
        /// Handles binding the global javascript object to C# methods.
        /// Enables javascript to call this entity's methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OnDocumentReady(object sender, EventArgs eventArgs)
        {
            AwesomiumWebView.DocumentReady -= OnDocumentReady;

            GlobalJavascriptObject.Bind("LoginButtonClick", true, LoginButtonClick);
            GlobalJavascriptObject.Bind("FindServerButtonClick", false, FindServerButtonClick);
            GlobalJavascriptObject.Bind("ToolsetButtonClick", false, ToolsetButtonClick);
            GlobalJavascriptObject.Bind("SettingsButtonClick", false, SettingsButtonClick);
            GlobalJavascriptObject.Bind("WebsiteButtonClick", false, WebsiteButtonClick);
            GlobalJavascriptObject.Bind("ForumsButtonClick", false, ForumsButtonClick);
            GlobalJavascriptObject.Bind("ExitButtonClick", false, ExitButtonClick);

            GlobalJavascriptObject.Bind("CreateProfileButtonClick", true, CreateProfileButtonClick);
            GlobalJavascriptObject.Bind("UpdateProfileButtonClick", true, UpdateProfileButtonClick);
            GlobalJavascriptObject.Bind("ResendAccountActivationEmail", true, ResendAccountActivationEmail);
        
            // User profile data binding
            GlobalJavascriptObject.Bind("GetUserName", true, GetUserName);
            GlobalJavascriptObject.Bind("GetPassword", true, GetPassword);
            GlobalJavascriptObject.Bind("GetEmail", true, GetEmail);
            GlobalJavascriptObject.Bind("GetFirstName", true, GetFirstName);
            GlobalJavascriptObject.Bind("GetLastName", true, GetLastName);
            GlobalJavascriptObject.Bind("GetDateOfBirth", true, GetDateOfBirth);

            // Logo Links
            GlobalJavascriptObject.Bind("FlatRedBallLogoLinkClick", false, FlatRedBallLogoLinkClick);
            GlobalJavascriptObject.Bind("XNALogoLinkClick", false, XNALogoLinkClick);

        }

        #endregion

        #region UI Methods

        /// <summary>
        /// Sends login credentials to master server and attempts to log in.
        /// Returns true if log in is successful.
        /// Returns false if log in is unsuccessful.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LoginButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            string username = args.Arguments[0].ToString();
            string password = args.Arguments[1].ToString();
            LoginCredentials loginCredentials = new LoginCredentials { UserName = username, Password = password};

            WebServiceClientUtility utility = new WebServiceClientUtility();
            Profile = utility.AttemptUserLogin(loginCredentials);

            if (!Profile.IsEmailVerified)
            {
                args.Result = (int)UserProfileResponseTypeEnum.AccountNotActivated;
            }
            else if (Profile.UserID > 0)
            {
                args.Result = (int)UserProfileResponseTypeEnum.Successful;
            }
            else
            {
                args.Result = (int)UserProfileResponseTypeEnum.InvalidPassword;
            }
        }

        /// <summary>
        /// Changes active screen to the Server List screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FindServerButtonClick(object sender, JavascriptMethodEventArgs args)
        {

        }

        /// <summary>
        /// Changes active screen to the toolset screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ToolsetButtonClick(object sender, JavascriptMethodEventArgs args)
        {
        }

        private void SettingsButtonClick(object sender, JavascriptMethodEventArgs args)
        {
        }

        /// <summary>
        /// Opens user's default browser and navigates to the Winter Engine website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebsiteButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("https://www.winterengine.com/");
        }

        /// <summary>
        /// Opens user's default browser and navigates to the Winter Engine forums.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ForumsButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("https://www.winterengine.com/forum");
        }

        /// <summary>
        /// Attempts to create a new user profile on the master server.
        /// Can fail for any of the following reasons:
        ///     - Username already exists
        ///     - Invalid password
        ///     - Password mismatch
        ///     - Email address already in use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CreateProfileButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            UserProfileResponseTypeEnum responseType = UserProfileResponseTypeEnum.Failure;

            string password = args.Arguments[1];
            string confirmPassword = args.Arguments[2];

            if (password == confirmPassword)
            {
                UserProfile profile = new UserProfile
                {
                    UserName = args.Arguments[0],
                    UserPassword = args.Arguments[1],
                    UserEmail = args.Arguments[3],
                    UserFirstName = args.Arguments[4],
                    UserLastName = args.Arguments[5],
                };

                DateTime parsedDOB;
                DateTime.TryParse(args.Arguments[6], out parsedDOB);
                profile.UserDOB = parsedDOB;

                WebServiceClientUtility utility = new WebServiceClientUtility();
                responseType = utility.SendUserProfile(profile, true);
            }
            else
            {
                responseType = UserProfileResponseTypeEnum.PasswordMismatch;
            }
            
            args.Result = Convert.ToInt32(responseType);
        }

        private void UpdateProfileButtonClick(object sender, JavascriptMethodEventArgs args)
        {
        }

        /// <summary>
        /// Closes the game window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ExitButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            FlatRedBallServices.Game.Exit();
        }

        /// <summary>
        /// Opens user's default browser and navigates to the FlatRedBall website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FlatRedBallLogoLinkClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("http://www.flatredball.com/");
        }

        /// <summary>
        /// Opens user's default browser and navigates to the XNA website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void XNALogoLinkClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("http://msdn.microsoft.com/en-us/centrum-xna.aspx");
        }

        /// <summary>
        /// Sends request to master server to resend the activation email for a user's account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ResendAccountActivationEmail(object sender, JavascriptMethodEventArgs args)
        {
            WebServiceClientUtility utility = new WebServiceClientUtility();
            utility.RequestActivationEmailResend(Profile.UserEmail);
        }

        #endregion

        #region Profile Javascript Data Retrieval Methods

        private void GetUserName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserName;
            }
        }

        private void GetPassword(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserPassword;
            }
        }

        private void GetEmail(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserEmail;
            }
        }

        private void GetFirstName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserFirstName;
            }
        }

        private void GetLastName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserLastName;
            }
        }

        private void GetDateOfBirth(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(Profile, null))
            {
                e.Result = Profile.UserDOB.ToString("MM/dd/yyyy");
            }
        }
        #endregion
    }
}
