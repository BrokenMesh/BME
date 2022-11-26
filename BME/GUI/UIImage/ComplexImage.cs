using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIImage {
    public class ComplexImage : Image {
        private Texture[] textures;
        private Vector4 bounds;

        public ComplexImage(Texture[] _textures, Vector4 _bounds) {
            if (_textures.Length != 9)
                throw new ArgumentException("A Complex Image requires 9 Textures");

            textures = _textures;
            bounds = _bounds;
        }

        public Vector4 GetBounds() {
            return bounds;
        }

        public override Texture[] GetTextures() {
            return textures;
        }
    }
}
