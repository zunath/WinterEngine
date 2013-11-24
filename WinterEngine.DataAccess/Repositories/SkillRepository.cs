using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class SkillRepository: RepositoryBase, IResourceRepository<Skill>
    {
        #region Constructors

        public SkillRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Skill> GetAll()
        {
            return Context.Skills.ToList();
        }

        public Skill Add(Skill skill)
        {
            return Context.Skills.Add(skill);
        }

        public void Add(List<Skill> skillList)
        {
            Context.Skills.AddRange(skillList);
        }

        public void Update(Skill skill)
        {
            Skill dbSkillList = Context.Skills.SingleOrDefault(x => x.ResourceID == skill.ResourceID);
            if (dbSkillList == null) return;

            Context.Entry(dbSkillList).CurrentValues.SetValues(skill);
        }

        public void Upsert(Skill skill)
        {
            if (skill.ResourceID <= 0)
            {
                Context.Skills.Add(skill);
            }
            else
            {
                Update(skill);
            }
        }

        public Skill GetByID(int skillID)
        {
            return Context.Skills.SingleOrDefault(x => x.ResourceID == skillID);
        }

        public bool Exists(Skill skill)
        {
            Skill dbSkill = Context.Skills.SingleOrDefault(x => x.ResourceID == skill.ResourceID);
            return !Object.ReferenceEquals(dbSkill, null);
        }

        public void Delete(Skill skill)
        {
            Context.Skills.Remove(skill);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
