using BME.GUI.UIElement;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.OpenGL.GL;

namespace BME.GUI {
    public class ElementFactory {

        public static Container BuildContainer(Element? _perent, Vector4 _padding) {
            Vector2 _scale = new Vector2(100,100);

            if (_perent != null)
                _scale = _perent.scale;

            Container _container = new Container(_perent, Vector2.Zero, _scale);
            _container.padding = _padding;
            return _container;
        }

        public static ImageElement BuildImage(Element? _perent, Vector4 _padding, string _filename) {
            Vector2 _scale = new Vector2(100, 100);

            if (_perent != null)
                _scale = _perent.scale;

            ImageElement _image = new ImageElement(TextureLoader.LoadTexture2DnoFlip_win(_filename, GL_LINEAR) ,_perent, Vector2.Zero, _scale);
            _image.padding = _padding;
            return _image;
        }

    }
}
