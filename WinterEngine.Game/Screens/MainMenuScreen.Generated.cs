using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using WinterEngine.Game.Entities;
using FlatRedBall;
using FlatRedBall.Screens;

namespace WinterEngine.Game.Screens
{
	public partial class MainMenuScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private WinterEngine.Game.Entities.Button ServerListButton;
		private WinterEngine.Game.Entities.Button ExitButton;

		public MainMenuScreen()
			: base("MainMenuScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			ServerListButton = new WinterEngine.Game.Entities.Button(ContentManagerName, false);
			ServerListButton.Name = "ServerListButton";
			ExitButton = new WinterEngine.Game.Entities.Button(ContentManagerName, false);
			ExitButton.Name = "ExitButton";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				ServerListButton.Activity();
				ExitButton.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			if (ServerListButton != null)
			{
				ServerListButton.Destroy();
				ServerListButton.Detach();
			}
			if (ExitButton != null)
			{
				ExitButton.Destroy();
				ExitButton.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			ServerListButton.DisplayText = "Server List";
			if (ServerListButton.Parent == null)
			{
				ServerListButton.X = 0f;
			}
			else
			{
				ServerListButton.RelativeX = 0f;
			}
			if (ServerListButton.Parent == null)
			{
				ServerListButton.Y = 50f;
			}
			else
			{
				ServerListButton.RelativeY = 50f;
			}
			ExitButton.DisplayText = "Exit";
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			ServerListButton.AddToManagers(mLayer);
			ServerListButton.DisplayText = "Server List";
			if (ServerListButton.Parent == null)
			{
				ServerListButton.X = 0f;
			}
			else
			{
				ServerListButton.RelativeX = 0f;
			}
			if (ServerListButton.Parent == null)
			{
				ServerListButton.Y = 50f;
			}
			else
			{
				ServerListButton.RelativeY = 50f;
			}
			ExitButton.AddToManagers(mLayer);
			ExitButton.DisplayText = "Exit";
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			ServerListButton.ConvertToManuallyUpdated();
			ExitButton.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			WinterEngine.Game.Entities.Button.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
