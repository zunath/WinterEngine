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

namespace WinterEngine.Editor.Extensions
{
    public static class ContentPackageResourceExtensions
    {
        public static Texture2D ToTexture2D(this ContentPackageResource resource)
        {
            MemoryStream stream = ToMemoryStream(resource);

            // Reference: http://stackoverflow.com/questions/2869801/is-there-a-fast-alternative-to-creating-a-texture2d-from-a-bitmap-object-in-xna
            Bitmap bitmap = new Bitmap(stream);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int bufferSize = data.Height * data.Stride;
            byte[] bytes = new byte[bufferSize];

            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            Texture2D texture = new Texture2D(FlatRedBallServices.GraphicsDevice, bitmap.Width, bitmap.Height);
            texture.SetData(bytes);
            bitmap.UnlockBits(data);

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
            MemoryStream stream = new MemoryStream();
            Texture2D texture = ToTexture2D(resource);
            texture.SaveAsPng(stream, texture.Width, texture.Height);
            stream.Position = 0;
            byte[] imageBytes = stream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

    }
}
