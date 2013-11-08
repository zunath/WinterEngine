using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.FileAccess
{
    public class ERFFileAccess
    {
        #region Fields

        private FileExtensionFactory _fileExtensionFactory;

        #endregion

        #region Properties

        private FileExtensionFactory FileExtensionFactory
        {
            get
            {
                if (_fileExtensionFactory == null)
                {
                    _fileExtensionFactory = new FileExtensionFactory();
                }

                return _fileExtensionFactory;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes a list of game objects to the specified file path.
        /// </summary>
        /// <param name="filePath">Path to the file, including the file name and extension.</param>
        /// <param name="gameObjects">The list of game objects to serialize.</param>
        public void SerializeGameObjectList(string filePath, List<GameObjectBase> gameObjects)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<GameObjectBase>));
                using(FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, gameObjects);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deserializes a file to a list of game objects.
        /// </summary>
        /// <param name="filePath">Path to the file, including the file name and extension.</param>
        /// <returns></returns>
        public List<GameObjectBase> DeserializeERFFile(string filePath)
        {
            try
            {
                List<GameObjectBase> gameObjects;
                XmlSerializer serializer = new XmlSerializer(typeof(List<GameObjectBase>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    gameObjects = serializer.Deserialize(stream) as List<GameObjectBase>;
                }
                return gameObjects;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
