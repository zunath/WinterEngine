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
        /// Gets or sets the game object type this category is used for.
        /// </summary>
        public GameObjectTypeEnum GameObjectType
        {
            get { return _gameObjectType; }
            set { _gameObjectType = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
