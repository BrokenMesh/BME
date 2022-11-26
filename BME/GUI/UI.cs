using BME.GUI.UIElement;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.OpenGL.GL;

namespace BME.GUI {
    public class UI {

        public Texture standartTexture;

        private static string _UIResFilePath = "./res/UI/";

        List<Element> elemets;

        public UI() {
            if (!Directory.Exists(_UIResFilePath) || !File.Exists(_UIResFilePath + "box.bmp")) {
                throw new Exception($"File paths required for UI system were not found. {_UIResFilePath}");
            }

            standartTexture = TextureLoader.LoadTexture2DnoFlip_win(_UIResFilePath + "box.bmp", GL_LINEAR);

            elemets = new List<Element>();
        }

        public void AddElement(Element _element) {
            elemets.Add(_element);

            Element? _perent = _element.GetPerent();

            if (_perent == null)
                return;

            if (!elemets.Contains(_perent))
                throw new ArgumentException("The perent of the Element was not defined");

            _perent.AddChild(_element);
        }

        public void RemoveElement(Element _element) {
            foreach (Element _e in _element.GetChildren()) {
                RemoveElement(_e);
            }

            elemets.Remove(_element);
        }

    }
}
