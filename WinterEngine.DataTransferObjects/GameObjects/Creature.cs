using System;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Creatures")]
    public sealed class Creature : GameObjectBase
    {
        #region Fields

        public Race Race { get; set; }
        public string Description { get; set; }

        #endregion

    }
}
