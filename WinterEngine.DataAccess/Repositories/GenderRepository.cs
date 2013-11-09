using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class GenderRepository : RepositoryBase, IResourceRepository<Gender>
    {
        #region Constructors

        public GenderRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a gender to the database.
        /// </summary>
        /// <param name="gender">The gender to add to the database.</param>
        /// <returns></returns>
        public Gender Add(Gender gender)
        {
            return Context.GenderRepository.Add(gender);
        }

        /// <summary>
        /// Adds a list of genders to the database.
        /// </summary>
        /// <param name="genderList">The list of genders to add to the database.</param>
        public void Add(List<Gender> genderList)
        {
            Context.GenderRepository.AddList(genderList);
        }

        /// <summary>
        /// Updates an existing gender in the database with new values.
        /// </summary>
        /// <param name="newScript">The new gender that will replace the gender with the matching ID.</param>
        public void Update(Gender newGender)
        {
            Gender dbGender = Context.GenderRepository.Get(x => x.ResourceID == newGender.ResourceID).SingleOrDefault();
            if (dbGender == null) return;

            Context.Context.Entry(dbGender).CurrentValues.SetValues(newGender);
        }

        /// <summary>
        /// If an gender with the same resref is in the database, it will be replaced with newGender.
        /// If an gender does not exist by newGender's ID, it will be added to the database.
        /// </summary>
        /// <param name="gender">The new gender to upsert.</param>
        public void Upsert(Gender gender)
        {
            if (gender.ResourceID <= 0)
            {
                Context.GenderRepository.Add(gender);
            }
            else
            {
                Update(gender);
            }
        }

        /// <summary>
        /// Deletes a gender with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Gender gender = Context.GenderRepository.Get(c => c.ResourceID == resourceID).SingleOrDefault();
            Context.GenderRepository.Delete(gender);
        }

        /// <summary>
        /// Returns all of the genders from the database.
        /// </summary>
        /// <returns></returns>
        public List<Gender> GetAll()
        {
            return Context.GenderRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from gender
                                                in Context.GenderRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = gender.Name,
                                                    ResourceID = gender.ResourceID
                                                }).ToList();

            return items;
        }

        public Gender GetByID(int resourceID)
        {
            return Context.GenderRepository.Get(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void Delete(Gender gender)
        {
            Context.GenderRepository.Delete(gender);
        }

        public bool Exists(Gender gender)
        {
            Gender dbGender = Context.GenderRepository.Get(x => x.ResourceID == gender.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbGender, null);
        }

        public int GetDefaultResourceID()
        {
            Gender defaultObject = Context.GenderRepository.Get(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
