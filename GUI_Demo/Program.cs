using BME.GameLoop;
using BME.Util;
using BME.ECS;
using BME.ECS.Entitys;
using BME.ECS.Entitys.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using BME.ECS.Entity.Components;
using BME.Rendering.Textures;

namespace GUI_Demo
{
    class Program
    {
        public static void Main(string[] args) {

            Game _window = new GUI_Demo(1080, 720, "Window");
            _window.Run();
        }

    }
}
