using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class CreatureRepository
    {
        public List<CreatureDTO> GetAllCreatures()
        {
            List<CreatureDTO> _creatureList = new List<CreatureDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var creatures = from creature
                                    in context.Creatures
                                    select creature;
                    _creatureList = Mapper.Map(creatures.ToList<Creature>(), _creatureList);
                }
            }
            catch (Exception ex)
            {
                _creatureList.Clear();
                MessageBox.Show("Error retrieving all creatures.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _creatureList;
        }
    }
}
