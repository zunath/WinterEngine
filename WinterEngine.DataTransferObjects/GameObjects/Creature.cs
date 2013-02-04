using System;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Creatures")]
    public sealed class Creature : GameObject, IEntity
    {
        #region Fields

        public Race Race { get; set; }
        public string Description { get; set; }
        public SpriteSheet Model { get; set; }
        public SpriteSheet Portrait { get; set; }

        #endregion

    }
}
