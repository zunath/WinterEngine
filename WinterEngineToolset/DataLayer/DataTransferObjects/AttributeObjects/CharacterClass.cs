using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.AttributeObjects
{
    [Serializable]
    [Table("CharacterClasses")]
    public class CharacterClass
    {
        #region Fields

        private int _characterClassID;
        private string _characterClassName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the character class ID.
        /// </summary>
        [Key]
        public int CharacterClassID
        {
            get { return _characterClassID; }
            set { _characterClassID = value; }
        }

        /// <summary>
        /// Gets or sets the name of a character class.
        /// </summary>
        [MaxLength(64)]
        public string CharacterClassName
        {
            get { return _characterClassName; }
            set { _characterClassName = value; }
        }

        #endregion

    }
}
