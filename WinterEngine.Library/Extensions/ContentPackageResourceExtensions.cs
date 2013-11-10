using FlatRedBall;
using Ionic.Zip;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.Library.Extensions
{
    public static class ContentPackageResourceExtensions
    {
        public static Texture2D ToTexture2D(this ContentPackageResource resource)
        {
            MemoryStream stream = ToMemoryStream(resource);
            Texture2D texture = Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, stream);
            stream.Close();
            return texture;
        }

        public static MemoryStream ToMemoryStream(this ContentPackageResource resource)
        {
            string path = DirectoryPaths.ContentPackageDirectoryPath + resource.ContentPackage.FileName;
            MemoryStream stream = new MemoryStream();
            using (ZipFile zipFile = new ZipFile(path))
            {
                zipFile[resource.FileName].Extract(stream);
            }

            return stream;
            
        }

        public static string ToBase64String(this ContentPackageResource resource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Texture2D texture = ToTexture2D(resource);
                texture.SaveAsPng(stream, texture.Width, texture.Height);
                stream.Position = 0;
                byte[] imageBytes = stream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

    }
}
