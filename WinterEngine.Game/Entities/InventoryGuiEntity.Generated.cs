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
using FlatRedBall.Gui;
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
	public partial class InventoryGuiEntity : WinterEngine.Game.Entities.GuiBaseEntity, IDestroyable, IVisible, IWindow, IClickable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.GuiBaseEntity.ContentManagerName;}
            set{ Entities.GuiBaseEntity.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		public int Index { get; set; }
		public bool Used { get; set; }
		public event EventHandler BeforeVisibleSet;
		public event EventHandler AfterVisibleSet;
		protected bool mVisible = true;
		public virtual bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				if (BeforeVisibleSet != null)
				{
					BeforeVisibleSet(this, null);
				}
				mVisible = value;
				if (AfterVisibleSet != null)
				{
					AfterVisibleSet(this, null);
				}
			}
		}
		public bool IgnoresParentVisibility { get; set; }
		public bool AbsoluteVisible
		{
			get
			{
				return Visible && (Parent == null || IgnoresParentVisibility || Parent is IVisible == false || (Parent as IVisible).AbsoluteVisible);
			}
		}
		IVisible IVisible.Parent
		{
			get
			{
				if (this.Parent != null && this.Parent is IVisible)
				{
					return this.Parent as IVisible;
				}
				else
				{
					return null;
				}
			}
		}

        public InventoryGuiEntity(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public InventoryGuiEntity(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			this.Click += CallLosePush;
			this.RollOff += CallLosePush;
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			base.AddToManagers(layerToAddTo);
			CustomInitialize();
		}

		public override void Activity()
		{
			// Generated Activity
			mIsPaused = false;
			base.Activity();
			
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			


			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			ResourcePath = "file:///./Components/Inventory.html";
			X = 0f;
			Y = 0f;
			ScaleX = 1f;
			ScaleY = 1f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp (Layer layerToAddTo)
		{
			base.AddToManagersBottomUp(layerToAddTo);
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
			ResourcePath = "file:///./Components/Inventory.html";
			X = 0f;
			Y = 0f;
			ScaleX = 1f;
			ScaleY = 1f;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			GuiBaseEntity.LoadStaticContent(contentManagerName);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("InventoryGuiEntityStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("InventoryGuiEntityStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static new void UnloadStaticContent ()
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
		
    // DELEGATE START HERE
    

        #region IWindow methods and properties

        public event WindowEvent Click;
		public event WindowEvent ClickNoSlide;
		public event WindowEvent SlideOnClick;
        public event WindowEvent Push;
		public event WindowEvent DragOver;
		public event WindowEvent RollOn;
		public event WindowEvent RollOff;
		public event WindowEvent LosePush;

        System.Collections.ObjectModel.ReadOnlyCollection<IWindow> IWindow.Children
        {
            get { throw new NotImplementedException(); }
        }

        bool mEnabled = true;


		bool IWindow.Visible
        {
            get
            {
                return this.AbsoluteVisible;
            }
			set
			{
				this.Visible = value;
			}
        }

        bool IWindow.Enabled
        {
            get
            {
                return mEnabled;
            }
            set
            {
                mEnabled = value;
            }
        }

		public bool MovesWhenGrabbed
        {
            get;
            set;
        }

        bool IWindow.GuiManagerDrawn
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IgnoredByCursor
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotImplementedException();
            }
        }



        public System.Collections.ObjectModel.ReadOnlyCollection<IWindow> FloatingChildren
        {
            get { return null; }
        }

        public FlatRedBall.ManagedSpriteGroups.SpriteFrame SpriteFrame
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float IWindow.WorldUnitX
        {
            get
            {
                return Position.X;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float IWindow.WorldUnitY
        {
            get
            {
                return Position.Y;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float IWindow.WorldUnitRelativeX
        {
            get
            {
                return RelativePosition.X;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float IWindow.WorldUnitRelativeY
        {
            get
            {
                return RelativePosition.Y;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float IWindow.ScaleX
        {
            get;
            set;
        }

        float IWindow.ScaleY
        {
            get;
            set;
        }

        IWindow IWindow.Parent
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void IWindow.Activity(Camera camera)
        {

        }

        void IWindow.CallRollOff()
        {
			if(RollOff != null)
			{
				RollOff(this);
			}
        }

        void IWindow.CallRollOn()
        {
			if(RollOn != null)
			{
				RollOn(this);
			}
        }

		
		void CallLosePush(IWindow instance)
		{
			if(LosePush != null)
			{
				LosePush(instance);
			}
		}

        void IWindow.CloseWindow()
        {
            throw new NotImplementedException();
        }

		void IWindow.CallClick()
		{
			if(Click != null)
			{
				Click(this);
			}
		}

        public bool GetParentVisibility()
        {
            throw new NotImplementedException();
        }

        bool IWindow.IsPointOnWindow(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void OnDragging()
        {
			if(DragOver != null)
			{
				DragOver(this);
			}
        }

        public void OnResize()
        {
            throw new NotImplementedException();
        }

        public void OnResizeEnd()
        {
            throw new NotImplementedException();
        }

        public void OnLosingFocus()
        {
            // Do nothing
        }

        public bool OverlapsWindow(IWindow otherWindow)
        {
            return false; // we don't care about this.
        }

        public void SetScaleTL(float newScaleX, float newScaleY)
        {
            throw new NotImplementedException();
        }

        public void SetScaleTL(float newScaleX, float newScaleY, bool keepTopLeftStatic)
        {
            throw new NotImplementedException();
        }

        public virtual void TestCollision(FlatRedBall.Gui.Cursor cursor)
        {
            if (HasCursorOver(cursor))
            {
                cursor.WindowOver = this;

                if (cursor.PrimaryPush)
                {

                    cursor.WindowPushed = this;

                    if (Push != null)
                        Push(this);


					cursor.GrabWindow(this);

                }

                if (cursor.PrimaryClick) // both pushing and clicking can occur in one frame because of buffered input
                {
                    if (cursor.WindowPushed == this)
                    {
                        if (Click != null)
                        {
                            Click(this);
                        }
						if(cursor.PrimaryClickNoSlide && ClickNoSlide != null)
						{
							ClickNoSlide(this);
						}

                        // if (cursor.PrimaryDoubleClick && DoubleClick != null)
                        //   DoubleClick(this);
                    }
					else
					{
						if(SlideOnClick != null)
						{
							SlideOnClick(this);
						}
					}
                }
            }
        }

        void IWindow.UpdateDependencies()
        {
            // do nothing
        }

        Layer ILayered.Layer
        {
            get
            {
				return LayerProvidedByContainer;
            }
        }


        #endregion

		public virtual bool HasCursorOver (FlatRedBall.Gui.Cursor cursor)
		{
			if (mIsPaused)
			{
				return false;
			}
			if (!AbsoluteVisible)
			{
				return false;
			}
			if (LayerProvidedByContainer != null && LayerProvidedByContainer.Visible == false)
			{
				return false;
			}
			if (!cursor.IsOn(LayerProvidedByContainer))
			{
				return false;
			}
			return false;
		}
		public virtual bool WasClickedThisFrame (FlatRedBall.Gui.Cursor cursor)
		{
			return cursor.PrimaryClick && HasCursorOver(cursor);
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
		}
		public void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class InventoryGuiEntityExtensionMethods
	{
		public static void SetVisible (this PositionedObjectList<InventoryGuiEntity> list, bool value)
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Visible = value;
			}
		}
	}
	
}
