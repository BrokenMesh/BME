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

namespace GUI_Demo
{
    class Program
    {
        public static void Main(string[] args) {

            EntityManager _em = new EntityManager();
            Entity _player = new Entity("Player", new Vector2(100,100), new Vector2(2,2), 1, 90);
            Entity _player2 = new Entity("Player", new Vector2(100,200), new Vector2(2,2), 1, 90);

            _player.AddComponent(new DemoComponent("iashfuhasf"));

            _em.AddEntity(_player);
            _em.AddEntity(_player2);

            DataFile _demo = new DataFile();
            _em.Save(_demo.Get("EM"));

            DataFile.Write(_demo, "./demo.txt", "    ", ',');

            //Game _window = new GUI_Demo(1080, 720, "Window");
            //_window.Run();

            /*DataFile _df = new DataFile();

            DataFile _demo = _df.Get("demo");
            _demo.AddComment("This is the Username");
            _demo.Get("name").SetString("Simon");
            _demo.Get("age").SetInt(24);
            _demo.Get("height").SetFloat(1.66f);

            DataFile _code = _demo.Get("code");
            _code.SetString("c++", 0);
            _code.SetString("vhdl",1);
            _code.SetString("lua", 2);

            DataFile _pc = _demo.Get("pc");
            _pc.Get("processor").SetString("intel");
            _pc.Get("ram").SetInt(32);

            _df.GetPath("demo/pc/windows/version").SetString("11");
            _df.GetPath("demo/pc/windows/bit").SetFloat(32);
            _df.GetPath("demo/pc/windows/age").SetString("1 year");
            _df.GetPath("demo/pc/2ram").SetString("2 GB");

            _df.GetPath("user1/pc/windows/version").SetString("11");
            _df.GetPath("user1/pc/windows/bit").SetFloat(32);
            _df.GetPath("user1/pc/windows/age").SetString("1 year");

            DataFile.Write(_df, "./demo.txt", _indent: "    ");

            DataFile? _df2 = DataFile.Read("./demo.txt");
            if (_df2 == null) return;

            Console.WriteLine(_df2.GetPath("demo/name").GetString());
            */

        }

    }
}
