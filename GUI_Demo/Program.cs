using BME.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
