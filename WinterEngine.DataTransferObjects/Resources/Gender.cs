using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Table("Genders")]
    public class Gender : GameResourceBase
    {
        public GenderTypeEnum GenderType { get; set; }
    }
}
