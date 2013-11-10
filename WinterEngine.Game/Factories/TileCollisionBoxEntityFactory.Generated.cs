using WinterEngine.Game.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using WinterEngine.Game.Performance;

namespace WinterEngine.Game.Factories
{
	public class TileCollisionBoxEntityFactory : IEntityFactory
	{
		public static TileCollisionBoxEntity CreateNew ()
		{
			return CreateNew(null);
		}
		public static TileCollisionBoxEntity CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			TileCollisionBoxEntity instance = null;
			instance = mPool.GetNextAvailable();
			if (instance == null)
			{
				mPool.AddToPool(new TileCollisionBoxEntity(mContentManagerName, false));
				instance =  mPool.GetNextAvailable();
			}
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
		
		public static void Initialize (PositionedObjectList<TileCollisionBoxEntity> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
			FactoryInitialize();
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
				TileCollisionBoxEntity instance = new TileCollisionBoxEntity(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (TileCollisionBoxEntity objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (TileCollisionBoxEntity objectToMakeUnused, bool callDestroy)
		{
			mPool.MakeUnused(objectToMakeUnused);
			
			if (callDestroy)
			{
				objectToMakeUnused.Destroy();
			}
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<TileCollisionBoxEntity> mScreenListReference;
			static PoolList<TileCollisionBoxEntity> mPool = new PoolList<TileCollisionBoxEntity>();
			public static Action<TileCollisionBoxEntity> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return TileCollisionBoxEntityFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return TileCollisionBoxEntityFactory.CreateNew(layer);
			}
			public static PositionedObjectList<TileCollisionBoxEntity> ScreenListReference
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
			static TileCollisionBoxEntityFactory mSelf;
			public static TileCollisionBoxEntityFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new TileCollisionBoxEntityFactory();
					}
					return mSelf;
				}
			}
	}
}
