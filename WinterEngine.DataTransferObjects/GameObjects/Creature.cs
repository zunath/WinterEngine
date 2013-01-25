using System;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Creatures")]
    public sealed class Creature : GameObject
    {
        #region Fields

        private Race _race;
        private string _description;
        private GraphicResource _modelGraphic;
        private GraphicResource _portraitGraphic;

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
        public GraphicResource ModelGraphic
        {
            get { return _modelGraphic; }
            set { _modelGraphic = value; }
        }

        /// <summary>
        /// Gets or sets the portrait of a creature.
        /// </summary>
        public GraphicResource PortraitGraphic
        {
            get { return _portraitGraphic; }
            set { _portraitGraphic = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
