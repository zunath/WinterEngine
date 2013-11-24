using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class LevelRequirementRepository: RepositoryBase, IResourceRepository<LevelRequirement>
    {
        #region Constructors

        public LevelRequirementRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<LevelRequirement> GetAll()
        {

            return Context.LevelRequirements.ToList();
        }

        public LevelRequirement Add(LevelRequirement levelRequirement)
        {
            return Context.LevelRequirements.Add(levelRequirement);
        }

        public void Add(List<LevelRequirement> levelRequirementList)
        {
            Context.LevelRequirements.AddRange(levelRequirementList);
        }

        public void Update(LevelRequirement levelRequirement)
        {
            LevelRequirement dbLevelRequirement = Context.LevelRequirements.SingleOrDefault(x => x.ResourceID == levelRequirement.ResourceID);
            if (dbLevelRequirement == null) return;

            Context.Entry(dbLevelRequirement).CurrentValues.SetValues(levelRequirement);
        }

        public void Upsert(LevelRequirement levelRequirement)
        {
            if (levelRequirement.ResourceID <= 0)
            {
                Context.LevelRequirements.Add(levelRequirement);
            }
            else
            {
                Update(levelRequirement);
            }
        }

        public LevelRequirement GetByID(int levelRequirementID)
        {
            return Context.LevelRequirements.SingleOrDefault(x => x.ResourceID == levelRequirementID);
        }

        public bool Exists(LevelRequirement levelRequirement)
        {
            LevelRequirement dbLevelRequirement = Context.LevelRequirements.SingleOrDefault(x => x.ResourceID == levelRequirement.ResourceID);
            return !Object.ReferenceEquals(dbLevelRequirement, null);
        }

        public void Delete(LevelRequirement levelRequirement)
        {
            Context.LevelRequirements.Remove(levelRequirement);
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            throw new NotSupportedException();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
