using BME.GameLoop;
using BME.GUI.UIImage;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIElement {
    public class ImageElement : Element {

        public Image image;
        
        public ImageElement(Image _image, Element _perent, Vector2 _positon, Vector2 _scale) : base(_perent, _positon, _scale) {
            image = _image;
        }

        public ImageElement(Element _perent, Vector2 _positon, Vector2 _scale) : base(_perent, _positon, _scale) {
            image = new SimpleImage(GameManager.currentScene.ui.standartTexture);
        }

        public ImageElement(Texture _texture,Element _perent, Vector2 _positon, Vector2 _scale) : base(_perent, _positon, _scale) {
            image = new SimpleImage(_texture);
        }

    }
}
