using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using WinterEngine.Game.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using WinterEngine.Game.Entities;
using FlatRedBall;
using FlatRedBall.Screens;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace WinterEngine.Game.Entities
{
	public partial class TableRow : PositionedObject, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		private WinterEngine.Game.Entities.TableCell ServerNameCell;
		private WinterEngine.Game.Entities.TableCell AddressCell;
		private WinterEngine.Game.Entities.TableCell MaxLevelCell;
		private WinterEngine.Game.Entities.TableCell PlayerCountCell;
		private WinterEngine.Game.Entities.TableCell GameTypeCell;
		private WinterEngine.Game.Entities.TableCell PVPTypeCell;
		public string AddressText
		{
			get
			{
				return AddressCell.DisplayText;
			}
			set
			{
				AddressCell.DisplayText = value;
			}
		}
		public string GameTypeText
		{
			get
			{
				return GameTypeCell.DisplayText;
			}
			set
			{
				GameTypeCell.DisplayText = value;
			}
		}
		public string MaxLevelText
		{
			get
			{
				return MaxLevelCell.DisplayText;
			}
			set
			{
				MaxLevelCell.DisplayText = value;
			}
		}
		public string PlayerCountText
		{
			get
			{
				return PlayerCountCell.DisplayText;
			}
			set
			{
				PlayerCountCell.DisplayText = value;
			}
		}
		public string PVPTypeText
		{
			get
			{
				return PVPTypeCell.DisplayText;
			}
			set
			{
				PVPTypeCell.DisplayText = value;
			}
		}
		public string ServerNameText
		{
			get
			{
				return ServerNameCell.DisplayText;
			}
			set
			{
				ServerNameCell.DisplayText = value;
			}
		}
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public TableRow(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public TableRow(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			ServerNameCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			ServerNameCell.Name = "ServerNameCell";
			AddressCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			AddressCell.Name = "AddressCell";
			MaxLevelCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			MaxLevelCell.Name = "MaxLevelCell";
			PlayerCountCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			PlayerCountCell.Name = "PlayerCountCell";
			GameTypeCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			GameTypeCell.Name = "GameTypeCell";
			PVPTypeCell = new WinterEngine.Game.Entities.TableCell(ContentManagerName, false);
			PVPTypeCell.Name = "PVPTypeCell";
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			ServerNameCell.Activity();
			AddressCell.Activity();
			MaxLevelCell.Activity();
			PlayerCountCell.Activity();
			GameTypeCell.Activity();
			PVPTypeCell.Activity();
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (ServerNameCell != null)
			{
				ServerNameCell.Destroy();
				ServerNameCell.Detach();
			}
			if (AddressCell != null)
			{
				AddressCell.Destroy();
				AddressCell.Detach();
			}
			if (MaxLevelCell != null)
			{
				MaxLevelCell.Destroy();
				MaxLevelCell.Detach();
			}
			if (PlayerCountCell != null)
			{
				PlayerCountCell.Destroy();
				PlayerCountCell.Detach();
			}
			if (GameTypeCell != null)
			{
				GameTypeCell.Destroy();
				GameTypeCell.Detach();
			}
			if (PVPTypeCell != null)
			{
				PVPTypeCell.Destroy();
				PVPTypeCell.Detach();
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (ServerNameCell.Parent == null)
			{
				ServerNameCell.CopyAbsoluteToRelative();
				ServerNameCell.AttachTo(this, false);
			}
			if (ServerNameCell.Parent == null)
			{
				ServerNameCell.X = -150f;
			}
			else
			{
				ServerNameCell.RelativeX = -150f;
			}
			ServerNameCell.DisplayText = "Server";
			if (AddressCell.Parent == null)
			{
				AddressCell.CopyAbsoluteToRelative();
				AddressCell.AttachTo(this, false);
			}
			if (AddressCell.Parent == null)
			{
				AddressCell.X = -50f;
			}
			else
			{
				AddressCell.RelativeX = -50f;
			}
			AddressCell.DisplayText = "Address";
			if (MaxLevelCell.Parent == null)
			{
				MaxLevelCell.CopyAbsoluteToRelative();
				MaxLevelCell.AttachTo(this, false);
			}
			if (MaxLevelCell.Parent == null)
			{
				MaxLevelCell.X = 50f;
			}
			else
			{
				MaxLevelCell.RelativeX = 50f;
			}
			MaxLevelCell.DisplayText = "Max Level";
			if (PlayerCountCell.Parent == null)
			{
				PlayerCountCell.CopyAbsoluteToRelative();
				PlayerCountCell.AttachTo(this, false);
			}
			if (PlayerCountCell.Parent == null)
			{
				PlayerCountCell.X = 150f;
			}
			else
			{
				PlayerCountCell.RelativeX = 150f;
			}
			PlayerCountCell.DisplayText = "Players";
			if (GameTypeCell.Parent == null)
			{
				GameTypeCell.CopyAbsoluteToRelative();
				GameTypeCell.AttachTo(this, false);
			}
			if (GameTypeCell.Parent == null)
			{
				GameTypeCell.X = 250f;
			}
			else
			{
				GameTypeCell.RelativeX = 250f;
			}
			GameTypeCell.DisplayText = "Game Type";
			if (PVPTypeCell.Parent == null)
			{
				PVPTypeCell.CopyAbsoluteToRelative();
				PVPTypeCell.AttachTo(this, false);
			}
			if (PVPTypeCell.Parent == null)
			{
				PVPTypeCell.X = 350f;
			}
			else
			{
				PVPTypeCell.RelativeX = 350f;
			}
			PVPTypeCell.DisplayText = "PVP Type";
			AddressText = "Address";
			GameTypeText = "Game Type";
			MaxLevelText = "Max Level";
			PlayerCountText = "Players";
			PVPTypeText = "PVP Type";
			ServerNameText = "Server";
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			// We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldRotationX = RotationX;
			float oldRotationY = RotationY;
			float oldRotationZ = RotationZ;
			
			float oldX = X;
			float oldY = Y;
			float oldZ = Z;
			
			X = 0;
			Y = 0;
			Z = 0;
			RotationX = 0;
			RotationY = 0;
			RotationZ = 0;
			ServerNameCell.AddToManagers(layerToAddTo);
			if (ServerNameCell.Parent == null)
			{
				ServerNameCell.X = -150f;
			}
			else
			{
				ServerNameCell.RelativeX = -150f;
			}
			ServerNameCell.DisplayText = "Server";
			AddressCell.AddToManagers(layerToAddTo);
			if (AddressCell.Parent == null)
			{
				AddressCell.X = -50f;
			}
			else
			{
				AddressCell.RelativeX = -50f;
			}
			AddressCell.DisplayText = "Address";
			MaxLevelCell.AddToManagers(layerToAddTo);
			if (MaxLevelCell.Parent == null)
			{
				MaxLevelCell.X = 50f;
			}
			else
			{
				MaxLevelCell.RelativeX = 50f;
			}
			MaxLevelCell.DisplayText = "Max Level";
			PlayerCountCell.AddToManagers(layerToAddTo);
			if (PlayerCountCell.Parent == null)
			{
				PlayerCountCell.X = 150f;
			}
			else
			{
				PlayerCountCell.RelativeX = 150f;
			}
			PlayerCountCell.DisplayText = "Players";
			GameTypeCell.AddToManagers(layerToAddTo);
			if (GameTypeCell.Parent == null)
			{
				GameTypeCell.X = 250f;
			}
			else
			{
				GameTypeCell.RelativeX = 250f;
			}
			GameTypeCell.DisplayText = "Game Type";
			PVPTypeCell.AddToManagers(layerToAddTo);
			if (PVPTypeCell.Parent == null)
			{
				PVPTypeCell.X = 350f;
			}
			else
			{
				PVPTypeCell.RelativeX = 350f;
			}
			PVPTypeCell.DisplayText = "PVP Type";
			AddressText = "Address";
			GameTypeText = "Game Type";
			MaxLevelText = "Max Level";
			PlayerCountText = "Players";
			PVPTypeText = "PVP Type";
			ServerNameText = "Server";
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			ServerNameCell.ConvertToManuallyUpdated();
			AddressCell.ConvertToManuallyUpdated();
			MaxLevelCell.ConvertToManuallyUpdated();
			PlayerCountCell.ConvertToManuallyUpdated();
			GameTypeCell.ConvertToManuallyUpdated();
			PVPTypeCell.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TableRowStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			WinterEngine.Game.Entities.TableCell.LoadStaticContent(contentManagerName);
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TableRowStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
			}
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
		protected bool mIsPaused;
		public override void Pause (InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			InstructionManager.IgnorePausingFor(this);
			ServerNameCell.SetToIgnorePausing();
			AddressCell.SetToIgnorePausing();
			MaxLevelCell.SetToIgnorePausing();
			PlayerCountCell.SetToIgnorePausing();
			GameTypeCell.SetToIgnorePausing();
			PVPTypeCell.SetToIgnorePausing();
		}
		public void MoveToLayer (Layer layerToMoveTo)
		{
			ServerNameCell.MoveToLayer(layerToMoveTo);
			AddressCell.MoveToLayer(layerToMoveTo);
			MaxLevelCell.MoveToLayer(layerToMoveTo);
			PlayerCountCell.MoveToLayer(layerToMoveTo);
			GameTypeCell.MoveToLayer(layerToMoveTo);
			PVPTypeCell.MoveToLayer(layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class TableRowExtensionMethods
	{
	}
	
}
