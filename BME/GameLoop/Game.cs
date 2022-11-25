using System;
using System.Threading;
using BME.Rendering.Display;
using BME.GameLoop.Input;
using static OpenGL.OpenGL.GL;
using GLFW;

namespace BME.GameLoop
{
    public abstract class Game
    {

        public float frameTime = 0;

        public InputManager input;

        protected int initialWindowWidth { get; set; }
        protected int initialWindowHeight { get; set; }
        protected string initialWindowTitle { get; set; } = String.Empty;

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) {
            this.initialWindowWidth = initialWindowWidth;
            this.initialWindowHeight = initialWindowHeight;
            this.initialWindowTitle = initialWindowTitle;
        }

        public void Run() {
            Initalize();

            DisplayManager.CreateWindow(initialWindowWidth, initialWindowHeight, initialWindowTitle);

            GameManager.DefaultSceneSetup();
            input = new InputManager();

            LoadContent();

            float _frameTimeTimer = 0;
            float _frameTimeSum = 0;
            float _frameTimeSteps = 0;

            while (!Glfw.WindowShouldClose(DisplayManager.window)) {

                GameTime.deltaTime = (float)Glfw.Time - GameTime.totalElapsedSeconds;
                GameTime.totalElapsedSeconds = (float)Glfw.Time;

                _frameTimeSum += GameTime.deltaTime;
                _frameTimeSteps++;
                _frameTimeTimer += GameTime.deltaTime;

                if (_frameTimeTimer > .1f) {
                    frameTime = _frameTimeSum / _frameTimeSteps;

                    Glfw.SetWindowTitle(DisplayManager.window, $"Game   {(int)(1/ frameTime)} FPS - {(int)(frameTime * 1_000_000)} µs");
                    _frameTimeTimer = 0;
                    _frameTimeSum = 0;
                    _frameTimeSteps = 0;
                }

                input.Update();
                Update();

                Glfw.PollEvents();

                glClearColor(DisplayManager.clearColor.X, DisplayManager.clearColor.Y, DisplayManager.clearColor.Z, DisplayManager.clearColor.W);
                glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

                Render();
            }

            Close();

            GameManager.DeleteGLScene();

            DisplayManager.CloseWindow();
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();
        protected abstract void Update();
        protected abstract void Render();
        protected abstract void Close();

    }
}
