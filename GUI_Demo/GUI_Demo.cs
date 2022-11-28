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
