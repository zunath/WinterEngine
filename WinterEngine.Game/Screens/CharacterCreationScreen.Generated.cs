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
using WinterEngine.Game.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Graphics;

namespace WinterEngine.Game.Screens
{
	public partial class CharacterCreationScreen : BaseScreen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private FlatRedBall.Graphics.Layer LayerInstance;
		private WinterEngine.Game.Entities.CharacterCreationUIEntity CharacterCreationUIEntityInstance;

		public CharacterCreationScreen()
			: base()
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			LayerInstance = new FlatRedBall.Graphics.Layer();
			LayerInstance.Name = "LayerInstance";
			CharacterCreationUIEntityInstance = new WinterEngine.Game.Entities.CharacterCreationUIEntity(ContentManagerName, false);
			CharacterCreationUIEntityInstance.Name = "CharacterCreationUIEntityInstance";
			
			
			base.Initialize(addToManagers);

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			SpriteManager.AddLayer(LayerInstance);
			LayerInstance.LayerCameraSettings = new LayerCameraSettings();
			LayerInstance.LayerCameraSettings.Orthogonal = false;
			base.AddToManagers();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				CharacterCreationUIEntityInstance.Activity();
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
			
			if (LayerInstance != null)
			{
				SpriteManager.RemoveLayer(LayerInstance);
			}
			if (CharacterCreationUIEntityInstance != null)
			{
				CharacterCreationUIEntityInstance.Destroy();
				CharacterCreationUIEntityInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (CharacterCreationUIEntityInstance.Parent == null)
			{
				CharacterCreationUIEntityInstance.CopyAbsoluteToRelative();
				CharacterCreationUIEntityInstance.RelativeZ += -40;
				CharacterCreationUIEntityInstance.AttachTo(SpriteManager.Camera, false);
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp ()
		{
			base.AddToManagersBottomUp();
			CharacterCreationUIEntityInstance.AddToManagers(LayerInstance);
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			CharacterCreationUIEntityInstance.ConvertToManuallyUpdated();
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			BaseScreen.LoadStaticContent(contentManagerName);
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
			WinterEngine.Game.Entities.CharacterCreationUIEntity.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			return null;
		}
		public static new object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
