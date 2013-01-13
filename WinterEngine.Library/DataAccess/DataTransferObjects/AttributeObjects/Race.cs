using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Library.DataAccess.DataTransferObjects.AttributeObjects
{
    [Serializable]
    [Table("Races")]
    public class Race
    {
        #region Fields

        private int _raceID;
        private string _raceName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the race ID for a particular race.
        /// </summary>
        [Key]
        public int RaceID
        {
            get { return _raceID; }
            set { _raceID = value; }
        }

        /// <summary>
        /// Gets or sets the name of a race.
        /// </summary>
        [MaxLength(64)]
        public string RaceName
        {
            get { return _raceName; }
            set { _raceName = value; }
        }

        #endregion
    }
}
