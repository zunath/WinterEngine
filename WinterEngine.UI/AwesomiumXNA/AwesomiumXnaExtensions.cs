﻿// Retrieved from http://support.awesomium.com/discussions/suggestions/41-release-basic-awesomiumsharp-extensions-for-xna-40-windows on May 30, 2012

using System;
using Awesomium.Core;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomiumXNA
{
    public static class AwesomiumXnaExtensions
    {
        public static Texture2D RenderTexture2D(this BitmapSurface buffer, Texture2D texture)
        {
            TextureFormatConverter.DirectBlit(buffer, ref texture);
            return texture;
        }
    }
}
