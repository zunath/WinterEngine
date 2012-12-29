using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;
using DejaVu;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class CreatureRepository
    {
        /// <summary>
        /// Returns all creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<CreatureDTO> GetAllCreatures()
        {
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                _creatureList.Clear();
                MessageBox.Show("Error retrieving all creatures.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return _creatureList;
        }

        /// <summary>
        /// Returns a specified creature by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public CreatureDTO GetCreatureByResref(string resref)
        {
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                retCreature = null;
                MessageBox.Show("Error retrieving specified creature (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return retCreature;
        }
    }
}
