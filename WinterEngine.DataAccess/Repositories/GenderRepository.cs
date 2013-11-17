using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class GenderRepository : IGenericRepository<Gender>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public GenderRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Gender InternalSave(Gender gender, bool saveChanges)
        {
            Gender retGender;
            if (gender.ResourceID <= 0)
            {
                retGender = _context.Genders.Add(gender);
            }
            else
            {
                retGender = _context.Genders.SingleOrDefault(x => x.ResourceID == gender.ResourceID);
                if (retGender == null) return null;
                _context.Entry(retGender).CurrentValues.SetValues(gender);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retGender;
        }

        /// <summary>
        /// If an gender with the same resref is in the database, it will be replaced with newGender.
        /// If an gender does not exist by newGender's ID, it will be added to the database.
        /// </summary>
        /// <param name="gender">The new gender to upsert.</param>
        public Gender Save(Gender gender)
        {
            return InternalSave(gender, true);
        }

        public void Save(IEnumerable<Gender> entityList)
        {
            if(entityList != null)
            {
                foreach(var gender in entityList)
                {
                    InternalSave(gender, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing gender in the database with new values.
        /// </summary>
        /// <param name="newScript">The new gender that will replace the gender with the matching ID.</param>
        public void Update(Gender newGender)
        {
            Gender dbGender = _context.Genders.Where(x => x.ResourceID == newGender.ResourceID).SingleOrDefault();
            if (dbGender == null) return;

            _context.Entry(dbGender).CurrentValues.SetValues(newGender);
        }

        private void DeleteInternal(Gender gender, bool saveChanges = true)
        {
            var dbGender = _context.Genders.SingleOrDefault(x => x.ResourceID == gender.ResourceID);
            if (dbGender == null) return;

            _context.Genders.Remove(dbGender);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Gender gender)
        {
            DeleteInternal(gender);
        }

        public void Delete(int resourceID)
        {
            var gender = _context.Genders.Find(resourceID);
            DeleteInternal(gender);
        }

        public void Delete(IEnumerable<Gender> genderList)
        {
            foreach (var gender in genderList)
            {
                DeleteInternal(gender, false);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the genders from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gender> GetAll()
        {
            return _context.Genders.ToList();
        }

        //todo: move this logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from gender
        //                                        in Context.GenderRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = gender.Name,
        //                                            ResourceID = gender.ResourceID
        //                                        }).ToList();

        //    return items;
        //}

        public Gender GetByID(int resourceID)
        {
            return _context.Genders.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        //public bool Exists(Gender gender)
        //{
        //    Gender dbGender = _context.Genders.Where(x => x.ResourceID == gender.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbGender, null);
        //}

        //public int GetDefaultResourceID()
        //{
        //    Gender defaultObject = _context.Genders.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}


        #endregion
    }
}
