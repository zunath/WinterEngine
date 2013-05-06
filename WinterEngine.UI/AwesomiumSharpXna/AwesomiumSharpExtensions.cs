using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Awesomium.Core;

namespace WinterEngine.UI.AwesomiumSharpXna
{
    public static class AwesomiumXnaExtensions
    {
        //     public static Texture2D RenderTexture2D(this RenderBuffer Buffer, Texture2D Texture)
        //    {
        //       TextureFormatConverter.DirectBlit(Buffer, ref Texture);
        //       return Texture;			
        //   }


        public static Texture2D RenderTexture2D(this BitmapSurface Buffer, Texture2D Texture)
        {
            TextureFormatConverter.DirectBlit(Buffer, ref Texture);
            return Texture;
        }
    }
}
