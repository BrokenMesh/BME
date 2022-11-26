using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIImage
{
    public abstract class Image
    {
        public Vector4 _tint;

        public abstract Texture[] GetTextures();
    }
}
