using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoMapper;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class CreatureRepository : IWinterObjectRepository
    {
        /// <summary>
        /// Returns all creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjects()
        {
            List<CreatureDTO> _creatureList = new List<CreatureDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from creature
                                in context.Creatures
                                select creature;
                    _creatureList = Mapper.Map(query.ToList<Creature>(), _creatureList);
                }
            }
            catch (Exception ex)
            {
                _creatureList.Clear();
                MessageBox.Show("Error retrieving all creatures.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _creatureList.ConvertAll(x => (WinterObjectDTO)x);;
        }

        /// <summary>
        /// Returns all creatures that match a particular ResourceCategoryDTO's CategoryID field.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjectsByResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            List<CreatureDTO> _creatureList = new List<CreatureDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from creature
                                in context.Creatures
                                where creature.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                select creature;
                    _creatureList = Mapper.Map(query.ToList<Creature>(), _creatureList);
                }
            }
            catch (Exception ex)
            {
                _creatureList.Clear();
                MessageBox.Show("Error retrieving creatures by resource category.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _creatureList.ConvertAll(x => (WinterObjectDTO)x);
        }

        /// <summary>
        /// Returns a specified creature by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public WinterObjectDTO GetObjectByResref(string resref)
        {
            CreatureDTO retCreature = new CreatureDTO();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from creature
                                in context.Creatures
                                where creature.Resref.Equals(resref) // Must match the resref - primary key in database
                                select creature;
                    List<Creature> resultCreatures = query.ToList<Creature>();
                    retCreature = Mapper.Map(resultCreatures[0], retCreature);
                }
            }
            catch (Exception ex)
            {
                retCreature = new CreatureDTO();
                MessageBox.Show("Error retrieving specified creature (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retCreature as WinterObjectDTO;
        }

        public void Dispose()
        {
        }
    }
}
