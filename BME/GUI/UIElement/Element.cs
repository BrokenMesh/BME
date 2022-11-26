using BME.GUI.UIImage;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIElement
{
    public abstract class Element
    {
        private Element? perent;
        private List<Element> children;

        public bool isEnabled;

        public Vector2 positon;
        public Vector2 scale;
        public Vector4 padding;

        public Allignment allignmentTop;
        public Allignment allignmentBottom;
        public Allignment allignmentLeft;
        public Allignment allignmentRight;

        public Element(Element? _perent,Vector2 _positon, Vector2 _scale) {
            isEnabled = true;
            positon = _positon;
            scale = _scale;
            padding = Vector4.Zero;

            children = new List<Element>();
            perent = _perent;

            allignmentTop = Allignment.Padding;
            allignmentBottom = Allignment.Padding;
            allignmentLeft = Allignment.Padding;
            allignmentRight = Allignment.Padding;
        }

        public Element? GetPerent() {
            return perent;
        }

        public List<Element> GetChildren() {
            return children;
        }

        public void AddChild(Element _child) {
            children.Add(_child);
        }

    }
}
