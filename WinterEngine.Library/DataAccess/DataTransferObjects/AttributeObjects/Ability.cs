using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Library.DataAccess.DataTransferObjects.AttributeObjects
{
    [Serializable]
    [Table("Abilities")]
    public class Ability
    {
        #region Fields

        private int _abilityID;
        private string _abilityName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ID number of an ability.
        /// </summary>
        [Key]
        public int AbilityID
        {
            get { return _abilityID; }
            set { _abilityID = value; }
        }

        /// <summary>
        /// Gets or sets the name of an ability
        /// </summary>
        [MaxLength(32)]
        public string AbilityName
        {
            get { return _abilityName; }
            set { _abilityName = value; }
        }

        #endregion
    }
}
