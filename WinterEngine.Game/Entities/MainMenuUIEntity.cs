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

            GlobalJavascriptObject.Bind("SaveProfileButtonClick", true, SaveProfileButtonClick);
        
            // User profile data binding
            GlobalJavascriptObject.Bind("GetUserName", true, GetUserName);
            GlobalJavascriptObject.Bind("GetPassword", true, GetPassword);
            GlobalJavascriptObject.Bind("GetEmail", true, GetEmail);
            GlobalJavascriptObject.Bind("GetFirstName", true, GetFirstName);
            GlobalJavascriptObject.Bind("GetLastName", true, GetLastName);
            GlobalJavascriptObject.Bind("GetDateOfBirth", true, GetDateOfBirth);
        }

        #endregion

        #region UI Methods

        private void LoginButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            string username = args.Arguments[0].ToString();
            string password = args.Arguments[1].ToString();
            LoginCredentials loginCredentials = new LoginCredentials { UserName = username, Password = password};

            WebServiceClientUtility utility = new WebServiceClientUtility();
            Profile = utility.AttemptUserLogin(loginCredentials);

            if (!Object.ReferenceEquals(Profile, null))
            {
                args.Result = true;
            }
            else
            {
                args.Result = false;
            }
        }

        private void FindServerButtonClick(object sender, JavascriptMethodEventArgs args)
        {

        }

        private void ToolsetButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            ScreenManager.CurrentScreen.MoveToScreen(typeof(ToolsetScreen).FullName);
        }

        private void SettingsButtonClick(object sender, JavascriptMethodEventArgs args)
        {
        }

        private void WebsiteButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("https://www.winterengine.com/");
        }

        private void ForumsButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("https://www.winterengine.com/forum");
        }

        private void SaveProfileButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            if(!Object.ReferenceEquals(Profile, null))
            {   
                UserProfile profile = new UserProfile
                {
                    UserName = args.Arguments[0],
                    UserPassword = args.Arguments[1],
                    UserEmail = args.Arguments[2],
                    UserFirstName = args.Arguments[3],
                    UserLastName = args.Arguments[4],
                    UserDOB = DateTime.Parse(args.Arguments[5])
                };
                WebServiceClientUtility utility = new WebServiceClientUtility();
                args.Result = utility.SendUserProfile(profile);
            }
        }

        private void ExitButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            FlatRedBallServices.Game.Exit();
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
