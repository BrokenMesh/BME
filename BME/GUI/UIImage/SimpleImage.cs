using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIImage {
    public class SimpleImage : Image {

        private Texture texture;

        public SimpleImage(Texture _texture) {
            texture = _texture;
        }
        
        public override Texture[] GetTextures() {
            Texture[] _t = { texture };
            return _t;
        }

    }
}
