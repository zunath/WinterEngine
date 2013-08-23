using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Repositories;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Ionic.Zip;

namespace WinterEngine.Library.Utility
{
    public class GraphicHelper
    {
        public Texture2D ContentPackageResourceToTexture2D(ContentPackageResource resource)
        {
            MemoryStream stream = ContentPackageResourceToMemoryStream(resource);

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


        /// <summary>
        /// Extracts a content builder resource from a content package to memory and returns the MemoryStream object.
        /// </summary>
        /// <param name="package"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public MemoryStream ContentPackageResourceToMemoryStream(ContentPackageResource resource, ContentPackage package = null)
        {
            string path = "";

            if (!Object.ReferenceEquals(package, null))
            {
                path = package.ContentPackagePath;
            }
            else
            {
                path = resource.Package.ContentPackagePath;
            }

            MemoryStream stream = new MemoryStream();
            using (ZipFile zipFile = new ZipFile(path))
            {
                zipFile[resource.FileName].Extract(stream);
            }

            return stream;
        }
    }
}
