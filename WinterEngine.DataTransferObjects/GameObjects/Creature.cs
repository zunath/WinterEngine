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

        private Race _race;
        private string _description;
        private SpriteSheet _modelGraphic;
        private SpriteSheet _portraitGraphic;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the race of a creature.
        /// </summary>
        public Race Race
        {
            get { return _race; }
            set { _race = value; }
        }

        /// <summary>
        /// Gets or sets the description of a creature.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the model of a creature.
        /// </summary>
        public SpriteSheet ModelGraphic
        {
            get { return _modelGraphic; }
            set { _modelGraphic = value; }
        }

        /// <summary>
        /// Gets or sets the portrait of a creature.
        /// </summary>
        public SpriteSheet PortraitGraphic
        {
            get { return _portraitGraphic; }
            set { _portraitGraphic = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
