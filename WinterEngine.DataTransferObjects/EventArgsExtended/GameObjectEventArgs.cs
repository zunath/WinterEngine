using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class GameObjectEventArgs : EventArgs
    {
        private GameObject _gameObject;

        /// <summary>
        /// Gets or sets the game object returned in the event args
        /// </summary>
        public GameObject GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }
    }
}
