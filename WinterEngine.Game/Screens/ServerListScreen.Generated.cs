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
using FlatRedBall.Graphics;

namespace WinterEngine.Game.Screens
{
	public partial class ServerListScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private FlatRedBall.Graphics.Layer GUILayer;
		private WinterEngine.Game.Entities.ActionBarGuiEntity ActionBarGuiEntityInstance;
		private WinterEngine.Game.Entities.PartyGuiEntity PartyGuiEntityInstance;

		public ServerListScreen()
			: base("ServerListScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			GUILayer = new FlatRedBall.Graphics.Layer();
			GUILayer.Name = "GUILayer";
			ActionBarGuiEntityInstance = new WinterEngine.Game.Entities.ActionBarGuiEntity(ContentManagerName, false);
			ActionBarGuiEntityInstance.Name = "ActionBarGuiEntityInstance";
			PartyGuiEntityInstance = new WinterEngine.Game.Entities.PartyGuiEntity(ContentManagerName, false);
			PartyGuiEntityInstance.Name = "PartyGuiEntityInstance";
			
			
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
			SpriteManager.AddLayer(GUILayer);
			GUILayer.UsePixelCoordinates();
			if (SpriteManager.Camera.Orthogonal)
			{
				GUILayer.LayerCameraSettings.OrthogonalWidth = FlatRedBall.SpriteManager.Camera.OrthogonalWidth;
				GUILayer.LayerCameraSettings.OrthogonalHeight = FlatRedBall.SpriteManager.Camera.OrthogonalHeight;
			}
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				ActionBarGuiEntityInstance.Activity();
				PartyGuiEntityInstance.Activity();
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
			
			if (GUILayer != null)
			{
				SpriteManager.RemoveLayer(GUILayer);
			}
			if (ActionBarGuiEntityInstance != null)
			{
				ActionBarGuiEntityInstance.Destroy();
				ActionBarGuiEntityInstance.Detach();
			}
			if (PartyGuiEntityInstance != null)
			{
				PartyGuiEntityInstance.Destroy();
				PartyGuiEntityInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			ActionBarGuiEntityInstance.AddToManagers(GUILayer);
			PartyGuiEntityInstance.AddToManagers(GUILayer);
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			ActionBarGuiEntityInstance.ConvertToManuallyUpdated();
			PartyGuiEntityInstance.ConvertToManuallyUpdated();
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
			WinterEngine.Game.Entities.ActionBarGuiEntity.LoadStaticContent(contentManagerName);
			WinterEngine.Game.Entities.PartyGuiEntity.LoadStaticContent(contentManagerName);
			WinterEngine.Game.Entities.ServerListGuiEntity.LoadStaticContent(contentManagerName);
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
