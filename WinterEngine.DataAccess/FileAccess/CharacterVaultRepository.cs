using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.DataAccess.FileAccess
{
    public class CharacterVaultRepository
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

        public List<PlayerCharacter> GetCharactersByAccount(string accountName)
        {
            string extension = FileExtensionFactory.GetFileExtension(FileTypeEnum.PlayerCharacter);
            List<string> filePaths = Directory.GetFiles(DirectoryPaths.CharacterVaultDirectoryPath + accountName, "*." + extension).ToList();

            List<PlayerCharacter> characters = new List<PlayerCharacter>();
            

            return characters;
        }

        #endregion
    }
}
