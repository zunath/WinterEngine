using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Categories")]
    public class Category : GameResourceBase
    {
        #region Fields

        private GameObjectTypeEnum _gameObjectType;

        #endregion

        #region Properties

        /// <summary>
        /// GameObjectTypeEnum in int format. This is done so that Entity Framework can store the value in the database correctly.
        /// It is not recommended to use this property in regular code.
        /// </summary>
        public int GameObjectTypeID
        {
            get { return (int)_gameObjectType; }
            set
            {
                _gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), Convert.ToString(value));
            }
        }

        /// <summary>
        /// Gets or sets the game object type this category is used for.
        /// </summary>
        [NotMapped]
        public GameObjectTypeEnum GameObjectType
        {
            // NOTE: XNA requires that .NET 4.0 be used. Entity Framework does not support storing enumerations in the database for versions less than 5.0
            // This field is not mapped to EF to prevent issues later on if the solution is upgraded to a later version of .NET
            get { return _gameObjectType; }
            set { _gameObjectType = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
