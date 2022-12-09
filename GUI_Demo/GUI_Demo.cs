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
        SimpleSprite _box;

        Level _levle;

        public GUI_Demo(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        { 
        }

        protected override void Initalize() {
        }

        protected override void LoadContent() {
            DisplayManager.EnableVSync(true);
            GameManager.DefaultSceneSetup();

            // Saving    
            /*
            Level _levle = new Level();

            Entity _player = new Entity("Player", new Vector2(100, 100), new Vector2(2, 2), 1, 90);
            Entity _player2 = new Entity("Player", new Vector2(100, 200), new Vector2(2, 2), 1, 90);

            DemoComponent _demoComp = new DemoComponent();
            _demoComp.name = "Player1 demo Component";
            _player.AddComponent(_demoComp);

            SimpleSpriteComponent _sp = new SimpleSpriteComponent();
            _sp.SetTexture(TextureLoader.LoadTexture2D_win("./res/textures/Box.bmp", OpenGL.OpenGL.GL.GL_LINEAR));
            _sp.SetTint(1,0,0.2341f,1);
            _player.AddComponent(_sp);

            AnimatedSpriteComponent _asc = new AnimatedSpriteComponent();
            string[] _aniFilenames = new string[] {
                "./res/textures/walk/walk1.png",
                "./res/textures/walk/walk2.png",
                "./res/textures/walk/walk3.png",
                "./res/textures/walk/walk4.png",
                "./res/textures/walk/walk5.png",
                "./res/textures/walk/walk6.png",
                "./res/textures/walk/walk7.png",
                "./res/textures/walk/walk8.png",
                "./res/textures/walk/walk9.png",
                "./res/textures/walk/walk10.png",
            };
            _asc.animation = new TextureAnimation(TextureLoader.LoadTexture2DList_win(_aniFilenames, GL_LINEAR));

            _player2.AddComponent(_asc);

            _levle.entityManager.AddEntity(_player);
            _levle.entityManager.AddEntity(_player2);

            DataFile _demo = new DataFile();
            _levle.Save(_demo);
            DataFile.Write(_demo, "./demo.txt", "    ", ',');
            */

            // Load

            
            DataFile? _df = DataFile.Read("./demo.txt");
            if (_df == null) {
                Console.WriteLine("Demo");
                return;
            }
            _levle = new Level(_df);
            _levle.Start();
            
            
        }

        protected override void Render() {
            _levle.Render();
        }

        protected override void Update() {
            _levle.Update();
        }
        protected override void Close() {

        }
    }
}
