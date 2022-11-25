using System;
using System.Collections.Generic;
using System.Numerics;
using BME.Rendering.Textures;

namespace BME.Rendering.Font
{
    public class FontData
    {
        // Info
        public string name;
        public bool isBold = false;
        public bool isItalic = false;
        public int aspectRatio;
        public Vector4 padding;
        public Vector2 spacing;
        
        // common
        public int lineHeight;
        public int baseHeight;
        public int imgWidth;
        public int imgHeight;

        public Texture atlas;

        public int charCount;
        public FontCharData[] charData = new FontCharData[256];

        public void Delete() {
            atlas.Delete();
        }
    }
}
