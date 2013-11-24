using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("LevelRequirements")]
    public class LevelRequirement : GameResourceBase
    {
        #region Fields

        #endregion

        #region Properties

        public int Level { get; set; }
        public int ExperienceRequired { get; set; }

        #endregion
    }
}
