using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.AttributeObjects;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Creatures")]
    public sealed class Creature : GameObject
    {
        #region Fields

        private Race _race;
        private string _description;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the race of a creature.
        /// </summary>
        public Race Race
        {
            get { return _race; }
            set { _race = value; }
        }

        /// <summary>
        /// Gets or sets the description of a creature.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
