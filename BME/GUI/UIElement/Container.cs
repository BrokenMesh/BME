using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.GUI.UIElement {
    public class Container : Element 
    {
        public Container(Element? _perent, Vector2 _positon, Vector2 _scale) : base(_perent, _positon, _scale)  {
        }
    }
}
