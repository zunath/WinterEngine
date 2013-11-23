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

using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Diagnostics;
using FlatRedBall.Screens;
using WinterEngine.Game.Screens;
using WinterEngine.Network;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Game.Services;
using WinterEngine.Network.Clients;
using System.Threading.Tasks;

namespace WinterEngine.Game.Entities
{
	public partial class MainMenuUIEntity
    {
        #region Fields

        private WebServiceClientUtility _webClientUtility;

        #endregion

        #region Properties

        private WebServiceClientUtility WebUtility
        {
            get
            {
                if (_webClientUtility == null)
                {
                    _webClientUtility = new WebServiceClientUtility();
                }

                return _webClientUtility;
            }
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

            EntityJavascriptObject.Bind("LoginButtonClick", false, LoginButtonClickAsync);
            EntityJavascriptObject.Bind("LogoutButtonClick", true, LogoutButtonClick);
            EntityJavascriptObject.Bind("FindServerButtonClick", false, FindServerButtonClick);
            EntityJavascriptObject.Bind("ToolsetButtonClick", false, ToolsetButtonClick);
            EntityJavascriptObject.Bind("SettingsButtonClick", false, SettingsButtonClick);
            EntityJavascriptObject.Bind("WebsiteButtonClick", false, WebsiteButtonClick);
            EntityJavascriptObject.Bind("ForumsButtonClick", false, ForumsButtonClick);
            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButtonClick);

            EntityJavascriptObject.Bind("SaveUserProfileClick", false, SaveUserProfileClickAsync);
            EntityJavascriptObject.Bind("ResendAccountActivationEmail", false, ResendAccountActivationEmailAsync);
        
            // User profile data binding
            EntityJavascriptObject.Bind("GetUserName", true, GetUserName);
            EntityJavascriptObject.Bind("GetPassword", true, GetPassword);
            EntityJavascriptObject.Bind("GetEmail", true, GetEmail);
            EntityJavascriptObject.Bind("GetFirstName", true, GetFirstName);
            EntityJavascriptObject.Bind("GetLastName", true, GetLastName);
            EntityJavascriptObject.Bind("GetDateOfBirth", true, GetDateOfBirth);
            EntityJavascriptObject.Bind("GetIsLoggedIn", true, GetIsLoggedIn);

            // Logo Links
            EntityJavascriptObject.Bind("FlatRedBallLogoLinkClick", false, FlatRedBallLogoLinkClick);
            EntityJavascriptObject.Bind("XNALogoLinkClick", false, XNALogoLinkClick);


            // This method fires after the client side OnDocumentReady - must call it here to 
            // toggle logged-in options.
            RunJavaScriptMethod("CheckIfLoggedIn();");
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
        private async void LoginButtonClickAsync(object sender, JavascriptMethodEventArgs args)
        {
            string username = args.Arguments[0];
            string password = args.Arguments[1];
            LoginCredentials loginCredentials = new LoginCredentials(username, password);
            UserProfileResponseTypeEnum responseType = UserProfileResponseTypeEnum.Failure;

            await Task.Factory.StartNew(() =>
            {
                WinterEngineService.InitializeUserProfile(WebUtility.AttemptUserLogin(loginCredentials));
                    
                if (WinterEngineService.ActiveUserProfile.UserID > 0 && WinterEngineService.ActiveUserProfile.IsEmailVerified)
                {
                    WinterEngineService.ActiveUserProfile.IsLoggedIn = true;
                    responseType = UserProfileResponseTypeEnum.Successful;
                }
                else if (WinterEngineService.ActiveUserProfile.UserID > 0 && !WinterEngineService.ActiveUserProfile.IsEmailVerified)
                {
                    responseType = UserProfileResponseTypeEnum.AccountNotActivated;
                }
                else
                {
                    responseType = UserProfileResponseTypeEnum.InvalidPassword;
                }

                loginCredentials.UserName = "";
                loginCredentials.Password = "";
            });

            AsyncJavascriptCallback("DoLogin_Callback", (int)responseType);
        }

        /// <summary>
        /// Removes user's profile from memory, effectively logging them out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LogoutButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            WinterEngineService.InitializeUserProfile(new UserProfile());
        }

        /// <summary>
        /// Changes active screen to the Server List screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FindServerButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(ServerListScreen)));
        }

        /// <summary>
        /// Changes active screen to the toolset screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ToolsetButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(ToolsetScreen)));
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
        private async void SaveUserProfileClickAsync(object sender, JavascriptMethodEventArgs args)
        {
            UserProfileResponseTypeEnum responseType = UserProfileResponseTypeEnum.Failure;

            string password = args.Arguments[1];
            string confirmPassword = args.Arguments[2];
            bool isCreatingNewProfile = (bool)args.Arguments[7];

            await Task.Factory.StartNew(() =>
            {
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

                    responseType = WebUtility.SendUserProfile(profile, isCreatingNewProfile);
                }
                else
                {
                    responseType = UserProfileResponseTypeEnum.PasswordMismatch;
                }
            });
            AsyncJavascriptCallback("SaveProfileButton_Callback", Convert.ToInt32(responseType));
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
        private async void ResendAccountActivationEmailAsync(object sender, JavascriptMethodEventArgs args)
        {
            await Task.Factory.StartNew(() => WebUtility.RequestActivationEmailResend(WinterEngineService.ActiveUserProfile.UserEmail));
        }

        #endregion

        #region Profile Javascript Data Retrieval Methods

        private void GetUserName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserName;
            }
        }

        private void GetPassword(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserPassword;
            }
        }

        private void GetEmail(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserEmail;
            }
        }

        private void GetFirstName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserFirstName;
            }
        }

        private void GetLastName(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserLastName;
            }
        }

        private void GetDateOfBirth(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.UserDOB.ToString("MM/dd/yyyy");
            }
        }

        private void GetIsLoggedIn(object sender, JavascriptMethodEventArgs e)
        {
            e.Result = false;

            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                e.Result = WinterEngineService.ActiveUserProfile.IsLoggedIn;
            }
        }

        #endregion
    }
}
