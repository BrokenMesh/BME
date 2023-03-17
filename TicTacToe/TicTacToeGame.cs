using BME.GameLoop;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.Rendering.Font;
using BME.Rendering.Renderer;
using BME.Rendering.Shaders;
using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using static OpenGL.OpenGL.GL; // TODO: This is bad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using BME.GameLoop.Input;
using BME.Rendering.Animation;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        TextSprite fpsCounter;
        TextSprite playerDisplay;

        SimpleSprite[] crossSprite;
        SimpleSprite[] circleSprite;
        int[,] grid = new int[3, 3];

        int currentPlayer = 1;
        bool hasWon = false;

        bool isDown = true;


        public TicTacToeGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle){
            Console.WriteLine("TicTacToe Game ------------------------");
        }

        protected override void Initalize() {
        }

        protected override void LoadContent() {
            
            // Setting Up Engine ------------------------
            DisplayManager.EnableVSync(false); // VSync 
            
            GameManager.DeleteGLScene();

            GameManager.currentScene.shader = new Shader("./res/shaders/spriteShader.vs", "./res/shaders/spriteShader.fs");
            GameManager.currentScene.camera = new Camera2D(DisplayManager.windowSize / 2f, 1f);
            GameManager.currentScene.textRenderer = new TextRenderer(GameManager.currentScene.camera);
            GameManager.currentScene.batchRenderer = new BatchRenderer(GameManager.currentScene.camera, 10_000);

            // Loading ----------------------------------
            Texture _boxTexture = TextureLoader.LoadTexture2D_win("./res/textures/Box.bmp", GL_LINEAR);

            SimpleSprite _bgSprite = new SimpleSprite(_boxTexture);
            _bgSprite.tint = new Vector4(0.3f, 0.3f, 0.3f, 1f);
            _bgSprite.scale = new Vector2(initialWindowWidth, initialWindowHeight);
            _bgSprite.position = new Vector2(initialWindowWidth / 2, initialWindowHeight / 2);
            _bgSprite.zDepth = -10;

            GameManager.currentScene.batchRenderer.AddSprite(_bgSprite);

            // Set Up Cells
            crossSprite = new SimpleSprite[9];
            circleSprite = new SimpleSprite[9];

            Texture _crossTexture = TextureLoader.LoadTexture2DnoFlip_win("./res/textures/Cross.png", GL_LINEAR);
            Texture _circleTexture = TextureLoader.LoadTexture2DnoFlip_win("./res/textures/Circle.png", GL_LINEAR);
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    grid[y, x] = 0;

                    SimpleSprite _cell = new SimpleSprite(_boxTexture);
                    _cell.tint = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _cell.scale = new Vector2(180, 180);
                    _cell.position = new Vector2(300 + (200 * x), 100 + (200 * y));
                    _cell.zDepth = 1;

                    SimpleSprite _cross= new SimpleSprite(_crossTexture);
                    _cross.tint = new Vector4(0.2f, 0.2f, 0.9f, 0f);
                    _cross.scale = new Vector2(150, 150);
                    _cross.position = new Vector2(300 + (200 * x), 100 + (200 * y));
                    _cross.zDepth = 2;
                    crossSprite[(y * 3) + x] = _cross;

                    SimpleSprite _circle = new SimpleSprite(_circleTexture);
                    _circle.tint = new Vector4(0.8f, 0.2f, 0.1f, 0f);
                    _circle.scale = new Vector2(150, 150);
                    _circle.position = new Vector2(300 + (200 * x), 100 + (200 * y));
                    _circle.zDepth = 3;
                    circleSprite[(y * 3) + x] = _circle;

                    GameManager.currentScene.batchRenderer.AddSprite(_cell);
                    GameManager.currentScene.batchRenderer.AddSprite(_cross);
                    GameManager.currentScene.batchRenderer.AddSprite(_circle);
                }
            }

            // Text
            FontData _fontSegoeDF = FontLoader.LoadFont("./res/fonts/Segoe_UI_DF.fnt", "./res/fonts/Segoe_UI_DF.png");
            fpsCounter = new TextSprite(_fontSegoeDF);
            fpsCounter.position = new Vector2(10, 25);
            fpsCounter.scale = new Vector2(0.25f, 0.25f);
            fpsCounter.tint = new Vector4(0, 1, 0, 1);
            fpsCounter.width = 0.40f;
            fpsCounter.fadeWidth = 0.25f;
            fpsCounter.kernning = 12;
            fpsCounter.SetText("FPS: 100");

            GameManager.currentScene.textRenderer.AddText(fpsCounter);

            playerDisplay = new TextSprite(_fontSegoeDF);
            playerDisplay.position = new Vector2(10, 100);
            playerDisplay.scale = new Vector2(0.40f, 0.40f);
            playerDisplay.tint = new Vector4(1f, 1f, 1f, 1);
            playerDisplay.width = 0.40f;
            playerDisplay.fadeWidth = 0.20f;
            playerDisplay.kernning = 12;
            playerDisplay.SetText("Player: X");

            GameManager.currentScene.textRenderer.AddText(playerDisplay);

            TextSprite _ContText = new TextSprite(_fontSegoeDF);
            _ContText.position = new Vector2(10, 550);
            _ContText.scale = new Vector2(0.30f, 0.30f);
            _ContText.tint = new Vector4(.8f, .8f, .8f, 1);
            _ContText.width = 0.40f;
            _ContText.fadeWidth = 0.20f;
            _ContText.kernning = 12;
            _ContText.SetText("(R)Restart");

            GameManager.currentScene.textRenderer.AddText(_ContText);

        }
        
        protected override void Render() {
            GameManager.currentScene.batchRenderer.Render();
            GameManager.currentScene.textRenderer.Render();
            GameManager.currentScene.batchRenderer.PresentRender();
        }

        protected override void Update() {
            fpsCounter.SetText($"FPS: {(int)(1 / frameTime)}");
            TaskSystem.Update();

            if (InputManager.GetKey(KeyCode.R)) {
                Restart();
                return;
            }

            if (hasWon) return;

            if (InputManager.GetMouseButton(MouseButton.Button1) && isDown == true) {
                isDown = false;

                Vector2? _ncell = CheckForOverlap(InputManager.GetMousePosition());
                if (_ncell != null) {
                    Vector2 _cell = (Vector2)_ncell;
                    if (grid[(int)_cell.Y, (int)_cell.X] == 0) {

                        grid[(int)_cell.Y, (int)_cell.X] = currentPlayer;

                        SimpleSprite _s = crossSprite[(int)(_cell.Y)*3 + (int)_cell.X];
                        if(currentPlayer != 1)
                            _s = circleSprite[(int)(_cell.Y)*3 + (int)_cell.X];

                        float _time = .5f;
                        TaskSystem.AddTimedTask((float _t) => {
                            _s.tint = new Vector4(_s.tint.X, _s.tint.Y, _s.tint.Z, _t / _time);
                        }, _time, AnimationType.Linear);

                        if (currentPlayer == 1) {
                            currentPlayer = 2;
                            playerDisplay.SetText("Player: O");
                        } else {
                            currentPlayer = 1;
                            playerDisplay.SetText("Player: X");
                        }

                    }
                }
            }
            if (!InputManager.GetMouseButton(MouseButton.Button1)) isDown = true;

            if (IsWinState(grid)) {
                hasWon = true;
                if (currentPlayer == 2) {
                    playerDisplay.SetText("Player: X\nHasWon");
                } else {
                    playerDisplay.SetText("Player: O\nHas Won");
                }

            }

        }

        protected override void Close() {
        }

        public void Restart() {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    grid[y, x] = 0;
                    crossSprite[y*3+x].tint = new Vector4(0.2f, 0.2f, 0.9f, 0f);
                    circleSprite[y*3+x].tint = new Vector4(0.8f, 0.2f, 0.1f, 0f);
                }
            }
            currentPlayer = 1;
            playerDisplay.SetText("Player: X");
            hasWon = false;
        }

        public static Vector2? CheckForOverlap(Vector2 _mousePosition) {
            Vector2 _harea = new Vector2(90,90);

            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    Vector2 _pos = new Vector2(300 + (200 * x), 100 + (200 * y));

                    Vector2 _curTop = _pos + _harea;
                    Vector2 _curBottom = _pos - _harea;
                    if (_mousePosition.X < _curTop.X && _mousePosition.X > _curBottom.X &&
                        _mousePosition.Y < _curTop.Y && _mousePosition.Y > _curBottom.Y) {
                        return new Vector2(x,y);
                    }
                }
            }

            return null;
        }

        public static bool IsWinState(int[,] _grid){
            for (int x = 0; x < 3; x++)
            {
                if (_grid[0, x] == 0) continue;
                if (_grid[0, x] == _grid[1, x] && _grid[1, x] == _grid[2, x]) return true;
            }

            for (int y = 0; y < 3; y++)
            {
                if (_grid[y, 0] == 0) continue;
                if (_grid[y, 0] == _grid[y, 1] && _grid[y, 1] == _grid[y, 2]) return true;
            }

            if (_grid[1, 1] == 0) return false;
            if (_grid[0, 0] == _grid[1, 1] && _grid[1, 1] == _grid[2, 2]) return true;
            if (_grid[0, 2] == _grid[1, 1] && _grid[1, 1] == _grid[2, 0]) return true;

            return false;
        }
    }
}
