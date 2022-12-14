using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BME.GameLoop;
using BME.Rendering.Display;
using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using BME.Rendering.Animation;
using static OpenGL.OpenGL.GL; // TODO: This is bad
using BME.ECS.Entity.Components;
using BME.ECS.Entitys;
using BME.Util;
using BME.ECS;

namespace GUI_Demo
{
    class GUI_Demo : Game
    {
        Level levle;

        public GUI_Demo(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        { 
        }

        protected override void Initalize() {
        }

        protected override void LoadContent() {
            DisplayManager.EnableVSync(true);
            GameManager.DefaultSceneSetup();
            
            levle = new Level();

            levle.entityManager.SetCustomeResolver((string _name) => {
                switch (_name) {
                    case "PlayerComponent": return new PlayerComponent();
                }
                return null;
            });

            DataFile? _df = DataFile.Read("./demo.txt");
            if (_df == null) {
                Console.WriteLine("Demo");
                return;
            }

            levle.Load(_df);
            levle.Start(); 
        }

        protected override void Render() {
            levle.Render();
        }

        protected override void Update() {
            levle.Update();
        }
        protected override void Close() {

        }
    }
}
