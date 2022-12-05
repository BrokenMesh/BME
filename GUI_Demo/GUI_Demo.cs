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

namespace GUI_Demo
{
    class GUI_Demo : Game
    {
        SimpleSprite _box;

        public GUI_Demo(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        { 
        }

        protected override void Initalize() {
        }

        protected override void LoadContent() {
            DisplayManager.EnableVSync(true);
            GameManager.DefaultSceneSetup();

            Texture _boxTexture = TextureLoader.LoadTexture2D_win("./res/textures/Box.bmp", GL_LINEAR);

            SimpleSprite _bgSprite = new SimpleSprite(_boxTexture);
            _bgSprite.tint = new Vector4(0.3f, 0.3f, 0.3f, 1f);
            _bgSprite.scale = new Vector2(initialWindowWidth, initialWindowHeight);
            _bgSprite.position = new Vector2(initialWindowWidth / 2, initialWindowHeight / 2);
            _bgSprite.zDepth = -10;

            GameManager.currentScene.batchRenderer.AddSprite(_bgSprite);

            _box = new SimpleSprite(_boxTexture);
            _box.tint = new Vector4(0.0f, 0.0f, 1f, 1f);
            _box.scale = new Vector2(100,100);
            _box.position = new Vector2(initialWindowWidth / 4, initialWindowHeight / 2);
            _box.zDepth = -1;

            GameManager.currentScene.batchRenderer.AddSprite(_box);

            float _time = 5f;
            TaskSystem.AddTimedTask((float _t) => {
                _box.position = new Vector2((initialWindowWidth / 4) + (initialWindowWidth /2)*(_t/_time), initialWindowHeight / 2);
            }, _time, AnimationType.PingPong | AnimationType.LoopFlag);


            // Saving
            /*
            EntityManager _em = new EntityManager();
            Entity _player = new Entity("Player", new Vector2(100, 100), new Vector2(2, 2), 1, 90);
            Entity _player2 = new Entity("Player", new Vector2(100, 200), new Vector2(2, 2), 1, 90);

            DemoComponent _demoComp = new DemoComponent();
            _demoComp.name = "Player1 demo Component";
            _player.AddComponent(_demoComp);

            SimpleSpriteComponent _sp = new SimpleSpriteComponent();
            _sp.SetTexture(TextureLoader.LoadTexture2D_win("./res/textures/Box.bmp", OpenGL.OpenGL.GL.GL_LINEAR));
            _sp.SetTint(1,0,0.2341f,1);
            _player.AddComponent(_sp);

            _em.AddEntity(_player);
            _em.AddEntity(_player2);

            DataFile _demo = new DataFile();
            _em.Save(_demo.Get("EM"));

            DataFile.Write(_demo, "./demo.txt", "    ", ',');
            */

            // Load
            /*
            EntityManager _em = new EntityManager();
            DataFile? _df = DataFile.Read("./demo.txt");
            if (_df == null) {
                Console.WriteLine("Demo");
                return;
            }
            _em.Load(_df.Get("EM"));
            */
        }

        protected override void Render() {
            GameManager.currentScene.batchRenderer.Render();
            GameManager.currentScene.textRenderer.Render();
            GameManager.currentScene.batchRenderer.PresentRender();
        }

        protected override void Update() { 

        }
        protected override void Close() {

        }
    }
}
