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
    [ProtoContract]
    public class PlayerCharacter
    {
        #region Fields

        private int _playerID;
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _biography;
        private float _locationX;
        private float _locationY;
        private float _locationZ;
        private int _locationAreaID;
        private bool _isGameMaster;
        private List<LocalVariable> _localVariables;
        private string _fileName;

        #endregion

        #region Properties

        [ProtoMember(1)]
        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }

        [ProtoMember(2)]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [ProtoMember(3)]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [ProtoMember(4)]
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [ProtoMember(5)]
        public string Biography
        {
            get { return _biography; }
            set { _biography = value; }
        }

        [ProtoMember(6)]
        public bool IsGameMaster
        {
            get { return _isGameMaster; }
            set { _isGameMaster = value; }
        }

        [ProtoMember(7)]
        [XmlIgnore]
        public float X
        {
            get { return _locationX; }
            set { _locationX = value; }
        }

        [ProtoMember(8)]
        [XmlIgnore]
        public float Y
        {
            get { return _locationY; }
            set { _locationY = value; }
        }

        [ProtoMember(9)]
        [XmlIgnore]
        public float Z
        {
            get { return _locationZ; }
            set { _locationZ = value; }
        }

        [ProtoMember(10)]
        [XmlIgnore]
        public int AreaID
        {
            get { return _locationAreaID; }
            set { _locationAreaID = value; }
        }

        [ProtoMember(11)]
        public List<LocalVariable> LocalVariables
        {
            get { return _localVariables; }
            set { _localVariables = value; }
        }

        [ProtoMember(12)]
        [XmlIgnore]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        #endregion

        #region Constructors

        public PlayerCharacter()
        {
            LocalVariables = new List<LocalVariable>();
        }
     

        #endregion
    }
}
