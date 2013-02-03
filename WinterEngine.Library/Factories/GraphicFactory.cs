using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.Library.Factories
{
    public class GraphicFactory
    {
        /// <summary>
        /// Retrieves a sprite sheet graphic from a resource hakpak.
        /// </summary>
        /// <param name="content">The XNA content manager responsible for keeping track of this graphic.</param>
        /// <param name="spriteSheet">The sprite sheet object to retrieve</param>
        /// <returns></returns>
        public Texture2D GetSpriteSheet(ContentManager content, SpriteSheet spriteSheet)
        {
            Texture2D graphic;

            using (ZipFile zipFile = new ZipFile(spriteSheet.ResourcePackagePath))
            {
                string resourcePath = "./" + spriteSheet.ResourceFileName;

                ZipEntry entry = zipFile[spriteSheet.ResourceFileName];
                entry.Extract();
                graphic = content.Load<Texture2D>(Path.GetFileNameWithoutExtension(resourcePath));
                File.Delete("./" + entry.FileName);
            }

            return graphic;
        }

    }
}
