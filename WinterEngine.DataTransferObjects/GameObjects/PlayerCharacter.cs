using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataTransferObjects.GameObjects
{
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

        #endregion

        #region Properties

        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Biography
        {
            get { return _biography; }
            set { _biography = value; }
        }

        public bool IsGameMaster
        {
            get { return _isGameMaster; }
            set { _isGameMaster = value; }
        }

        public float X
        {
            get { return _locationX; }
            set { _locationX = value; }
        }

        public float Y
        {
            get { return _locationY; }
            set { _locationY = value; }
        }

        public float Z
        {
            get { return _locationZ; }
            set { _locationZ = value; }
        }

        public int AreaID
        {
            get { return _locationAreaID; }
            set { _locationAreaID = value; }
        }

        public List<LocalVariable> LocalVariables
        {
            get { return _localVariables; }
            set { _localVariables = value; }
        }

        #endregion

        #region Constructors

        #endregion
    }
}
