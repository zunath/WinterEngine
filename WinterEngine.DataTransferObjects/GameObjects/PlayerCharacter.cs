using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ProtoBuf;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [NotMapped]
    [ProtoContract]
    public class PlayerCharacter: Creature
    {
        #region Properties

        [ProtoMember(1)]
        public int PlayerID { get; set; }

        [ProtoMember(2)]
        public int Age { get; set; }

        [ProtoMember(3)]
        public bool IsGameMaster { get; set; }

        [ProtoMember(4)]
        [XmlIgnore]
        public string FileName { get; set; }

        #endregion

        #region Constructors

        public PlayerCharacter()
        {
            LocalVariables = new List<LocalVariable>();
        }
     

        #endregion
    }
}
