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
using WinterEngine.Network;
using Awesomium.Core;
using WinterEngine.Network.Entities;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class ServerListGuiEntity
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            InitializeAwesomiumEventSubscriptions();

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

        private void OnDocumentReady(object sender, EventArgs e)
        {
            BuildServerList();
        }

        public void InitializeAwesomiumEventSubscriptions()
        {
            GuiEntity.AwesomiumWebView.DocumentReady += OnDocumentReady;
        }

        #endregion

        #region Methods


        private void BuildServerList()
        {
            WebServiceClientUtility utility = new WebServiceClientUtility();
            List<ServerDetails> serverList = utility.GetAllActiveServers();

            JSObject jObject = GuiEntity.AwesomiumWebView.CreateGlobalJavascriptObject("ServerList");

            foreach (ServerDetails server in serverList)
            {
                /*
                JSValue val = jobject.Invoke("AddServerDetailsToTable",
                    new JSValue(server.ServerName),
                    new JSValue(""),
                    new JSValue(""),
                    //new JSValue(server.Connection.ServerIPAddress.ToString()),
                    //new JSValue(server.Connection.ServerPort),
                    new JSValue(server.ServerMaxLevel),
                    new JSValue(server.ServerMaxPlayers),
                    new JSValue(server.ServerMaxPlayers),
                    new JSValue(server.GameType.ToString()),
                    new JSValue(server.PVPType.ToString()));
                */
            }

        }

        #endregion

    }
}
