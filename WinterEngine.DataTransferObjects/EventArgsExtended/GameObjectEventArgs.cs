using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;


namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class GameObjectEventArgs : EventArgs
    {
        private GameObjectBase _gameObject;

        /// <summary>
        /// Gets or sets the game object returned in the event args
        /// </summary>
        public GameObjectBase GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }
    }
}
