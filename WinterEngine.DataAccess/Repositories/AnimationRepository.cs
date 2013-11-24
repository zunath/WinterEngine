using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class AnimationRepository : RepositoryBase, IResourceRepository<Animation>
    {
        
        #region Constructors

        public AnimationRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Animation> GetAll()
        {

            return Context.Animations.ToList();
        }

        public Animation Add(Animation animation)
        {
            return Context.Animations.Add(animation);
        }

        public void Add(List<Animation> animationList)
        {
            Context.Animations.AddRange(animationList);
        }

        public void Update(Animation animation)
        {
            Animation dbAnimation = Context.Animations.SingleOrDefault(x => x.ResourceID == animation.ResourceID);
            if (dbAnimation == null) return;

            Context.Entry(dbAnimation).CurrentValues.SetValues(animation);
        }

        public void Upsert(Animation animation)
        {
            if (animation.ResourceID <= 0)
            {
                Context.Animations.Add(animation);
            }
            else
            {
                Update(animation);
            }
        }

        public Animation GetByID(int animationID)
        {
            return Context.Animations.SingleOrDefault(x => x.ResourceID == animationID);
        }

        public bool Exists(Animation animation)
        {
            Animation dbAnimation = Context.Animations.SingleOrDefault(x => x.ResourceID == animation.ResourceID);
            return !Object.ReferenceEquals(dbAnimation, null);
        }

        public void Delete(Animation animation)
        {
            Context.Animations.Remove(animation);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    
    }
}
