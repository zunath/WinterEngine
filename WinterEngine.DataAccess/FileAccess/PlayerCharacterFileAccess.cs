using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.DataAccess.FileAccess
{
    public class PlayerCharacterFileAccess : IDisposable
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
        /// Generates a list of PlayerCharacter objects which are tied to a specific player username.
        /// </summary>
        /// <param name="playerProfile"></param>
        /// <returns></returns>
        public List<PlayerCharacter> GetCharactersByUsername(string username)
        {
            List<PlayerCharacter> characters = new List<PlayerCharacter>();

            string extension = FileExtensionFactory.GetFileExtension(FileTypeEnum.PlayerCharacter);
            string path = DirectoryPaths.CharacterVaultDirectoryPath + username;

            if (Directory.Exists(path))
            {
                List<string> filePaths = Directory.GetFiles(path, "*" + extension).ToList();

                foreach (string file in filePaths)
                {
                    characters.Add(DeserializePlayerCharacterFile(file));
                }
            }

            return characters;
        }

        /// <summary>
        /// Serializes a specified player character object to the appropriate file path.
        /// </summary>
        /// <param name="character"></param>
        /// <returns>Returns the modified PlayerCharacter object</returns>
        public PlayerCharacter SerializePlayerCharacterFile(PlayerCharacter character, string username)
        {
            if (String.IsNullOrWhiteSpace(character.FileName))
            {
                character.FileName = CreateUniqueFileName(character, username);
            }

            string filePath = DirectoryPaths.CharacterVaultDirectoryPath + username + "/" + character.FileName;

            if (!Directory.Exists(DirectoryPaths.CharacterVaultDirectoryPath))
            {
                // Create the character vault directory
                Directory.CreateDirectory(DirectoryPaths.CharacterVaultDirectoryPath);
            }

            if (!Directory.Exists(DirectoryPaths.CharacterVaultDirectoryPath + username))
            {
                // Create the user's folder
                Directory.CreateDirectory(DirectoryPaths.CharacterVaultDirectoryPath + username);
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PlayerCharacter));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, character);
                }
            }
            catch
            {
                throw;
            }

            return character;
        }

        /// <summary>
        /// Deserializes a player character file at a specified file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public PlayerCharacter DeserializePlayerCharacterFile(string filePath)
        {
            try
            {
                PlayerCharacter character;
                XmlSerializer serializer = new XmlSerializer(typeof(PlayerCharacter));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    character = serializer.Deserialize(stream) as PlayerCharacter;
                }

                character.FileName = Path.GetFileName(filePath);

                return character;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a unique file name for a player character file.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="playerProfile"></param>
        /// <returns></returns>
        private string CreateUniqueFileName(PlayerCharacter character, string username)
        {
            string extension = FileExtensionFactory.GetFileExtension(FileTypeEnum.PlayerCharacter);
            string directoryPath = DirectoryPaths.CharacterVaultDirectoryPath + username + "/";
            string fileName = character.FirstName + character.LastName;

            foreach (char currentCharacter in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(currentCharacter.ToString(), "");
            }
            fileName = fileName.Replace(" ", "");

            string originalFileName = fileName;
            int index = 1;
            while (File.Exists(directoryPath + fileName + extension))
            {
                fileName = originalFileName + index;
                index++;
            }

            fileName += extension;
            return fileName;
        }

        /// <summary>
        /// Attempts to delete the specified player character file. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DeleteCharacterTypeEnum DeletePlayerCharacterFile(string filePath)
        {
            DeleteCharacterTypeEnum response = DeleteCharacterTypeEnum.Denied;

            string fileName = Path.GetFileName(filePath);
            string extension = Path.GetExtension(filePath);
            string path = Path.GetDirectoryName(filePath);

            if (extension == FileExtensionFactory.GetFileExtension(FileTypeEnum.PlayerCharacter))
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        // "Deleting" is really just changing the file extension.
                        string newPath = path + fileName + FileExtensionFactory.GetFileExtension(FileTypeEnum.DeletedPlayerCharacter);
                        File.Move(filePath, newPath);
                        response = DeleteCharacterTypeEnum.Accepted;
                    }
                    else
                    {
                        response = DeleteCharacterTypeEnum.FileNotFound;
                    }
                }
                catch
                {
                    response = DeleteCharacterTypeEnum.Error;
                }
            }

            return response;
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
