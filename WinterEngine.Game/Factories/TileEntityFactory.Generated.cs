using WinterEngine.Game.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using WinterEngine.Game.Performance;

namespace WinterEngine.Game.Factories
{
	public class TileEntityFactory : IEntityFactory
	{
		public static TileEntity CreateNew ()
		{
			return CreateNew(null);
		}
		public static TileEntity CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			TileEntity instance = null;
			instance = new TileEntity(mContentManagerName, false);
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<TileEntity> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				TileEntity instance = new TileEntity(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (TileEntity objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (TileEntity objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<TileEntity> mScreenListReference;
			static PoolList<TileEntity> mPool = new PoolList<TileEntity>();
			public static Action<TileEntity> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return TileEntityFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return TileEntityFactory.CreateNew(layer);
			}
			public static PositionedObjectList<TileEntity> ScreenListReference
			{
				get
				{
					return mScreenListReference;
				}
				set
				{
					mScreenListReference = value;
				}
			}
			static TileEntityFactory mSelf;
			public static TileEntityFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new TileEntityFactory();
					}
					return mSelf;
				}
			}
	}
}
