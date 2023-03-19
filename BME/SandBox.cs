using System;
using System.Numerics;
using System.Collections.Generic;
using BME.GameLoop;
using BME.Rendering.Display;
using BME.Rendering.Shaders;
using BME.Rendering.Camera;
using BME.Rendering.Textures;
using BME.Rendering.Sprites;
using BME.Rendering.Renderer;
using BME.Rendering.Animation;
using BME.Rendering.Font;
using static OpenGL.OpenGL.GL;
using GLFW;

namespace BME {
    public class SandBox : Game
    {
        SimpleSprite sprite1;
        SimpleSprite sprite2;
        SimpleSprite bg_sprite;
        AnimatedSprite animatedSprite1;
        AnimatedSprite animatedSprite2;
        TextSprite text1;
        
        List<SimpleSprite> sprites = new List<SimpleSprite>(); 

        private readonly Random random = new Random();

        float timer;

        public SandBox(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {  
        }

        protected override void Initalize() {

        }

        protected unsafe override void LoadContent() {

            GameManager.DeleteGLScene();

            GameManager.currentScene.shader = new Shader("./res/shaders/spriteShader.vs", "./res/shaders/spriteShader.fs");
            GameManager.currentScene.camera = new Camera2D(DisplayManager.windowSize / 2f, 1f);
            GameManager.currentScene.textRenderer = new TextRenderer(GameManager.currentScene.camera);

            //CurrentScene.renderer = new SpriteRenderer(CurrentScene.shader, CurrentScene.camera);
            GameManager.currentScene.batchRenderer = new BatchRenderer(GameManager.currentScene.camera, 10_000);

            Texture texture1 = TextureLoader.LoadTexture2D_win("./res/textures/image.bmp", GL_LINEAR);
            Texture texture2 = TextureLoader.LoadTexture2D_win("./res/textures/image2.bmp", GL_LINEAR);
            
            
            for (int i = 0; i < 100; i++) {
                SimpleSprite _s = new SimpleSprite(random.Next(10)>5 ? texture1 : texture2);
                _s.position = new Vector2(random.Next(initialWindowWidth), random.Next(initialWindowHeight));
                _s.tint = new Vector4((float)(random.Next(255))/255.0f, (float)(random.Next(255)) / 255.0f, (float)(random.Next(255)) / 255.0f, (float)(random.Next(255)) / 255.0f);
                _s.zDepth = random.NextSingle()*9.0f -10.0f;
                sprites.Add(_s);

                //CurrentScene.renderer.AddSprite(_s);
                GameManager.currentScene.batchRenderer.AddSprite(_s);
            }

            sprite1 = new SimpleSprite(texture1);
            sprite2 = new SimpleSprite(texture2);

            sprite1.scale = new Vector2(300,-200);
            sprite1.position = new Vector2(800/3, initialWindowHeight / 2);
            //sprite1.position = new Vector2(0.2f, 0.2f);

            sprite2.scale = new Vector2(100, -100);
            //sprite2.position = new Vector2(0.0f, 0.0f);
            sprite2.position = new Vector2(800/3*2, initialWindowHeight / 2);
            sprite2.rotation = MathF.PI;
            sprite2.tint = Vector4.One;

            bg_sprite = new SimpleSprite(TextureLoader.LoadTexture2D_win("./res/textures/BG.png", GL_LINEAR));
            bg_sprite.scale = new Vector2(initialWindowWidth, initialWindowHeight);
            bg_sprite.position = new Vector2(initialWindowWidth / 2, initialWindowHeight / 2);
            bg_sprite.zDepth = -10;

            //CurrentScene.renderer.AddSprite(sprite1);
            //CurrentScene.renderer.AddSprite(bg_sprite);
            //CurrentScene.renderer.AddSprite(sprite2);
            GameManager.currentScene.batchRenderer.AddSprite(bg_sprite);
            GameManager.currentScene.batchRenderer.AddSprite(sprite2);
            GameManager.currentScene.batchRenderer.AddSprite(sprite1);


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
            List<Texture> _texAnimation = TextureLoader.LoadTexture2DList_win(_aniFilenames, GL_LINEAR);
            animatedSprite2 = new AnimatedSprite(_texAnimation);
            animatedSprite2.position = new Vector2(800 / 3 * 2, (initialWindowHeight / 2)-200);
            animatedSprite2.SetAnimationTime(1);
            animatedSprite2.zDepth = 2;

            animatedSprite1 = new AnimatedSprite("./res/textures/textureAtlasAnimation.bmp", 8, 2);
            animatedSprite1.SetAnimationTime(4);
            animatedSprite1.animation.type = AnimationType.PingPong | AnimationType.LoopFlag;
            animatedSprite1.position = new Vector2(800 / 3 * 2, (initialWindowHeight / 2)+200);
            animatedSprite1.zDepth = -1;

            //CurrentScene.renderer.AddSprite(animatedSprite1);
            //CurrentScene.renderer.AddSprite(animatedSprite2);
            GameManager.currentScene.batchRenderer.AddSprite(animatedSprite1);
            GameManager.currentScene.batchRenderer.AddSprite(animatedSprite2);


            float _time = 10.0f;
            TaskSystem.AddTimedTask((float _t) => {
                animatedSprite1.position = Vector2.Lerp(new Vector2(0, initialWindowHeight -100), new Vector2(initialWindowWidth, initialWindowHeight -100), _t/_time);
            }, _time, AnimationType.PingPong | AnimationType.LoopFlag);

            float _2time = 30.0f;
            TaskSystem.AddTimedTask((float _t) => {
                animatedSprite2.position = Vector2.Lerp(animatedSprite1.position, new Vector2(800 / 3 * 2, (initialWindowHeight / 2) - 200), _t / _2time);
            }, _2time, AnimationType.Linear);

            FontData _fontSegoeDF = FontLoader.LoadFont("./res/fonts/Segoe_UI_DF.fnt", "./res/fonts/Segoe_UI_DF.png");
            text1 = new TextSprite(_fontSegoeDF);
            text1.position = new Vector2(10,25);
            text1.scale = new Vector2(0.25f, 0.25f);
            text1.tint = new Vector4(0,1,0,1);
            text1.width = 0.40f;
            text1.fadeWidth = 0.25f;
            text1.kernning = 12;
            text1.SetText("FPS: 100");

            FontData _fontComic = FontLoader.LoadFont("./res/fonts/ComicSans_DF.fnt", "./res/fonts/ComicSans_DF.png");
            TextSprite text2 = new TextSprite(_fontComic);
            text2.position = new Vector2(500, 500);
            text2.borderWidth = 0.4f;
            text2.borderFadeWidth = 0.4f;
            text2.borderColor = new Vector3(1, 1, 0);
            text2.kernning = 12;
            text2.SetText("Hello World! \nthis is a meme");

            TextSprite text3 = new TextSprite(_fontSegoeDF);
            text3.position = new Vector2(50, 300);
            text3.scale = new Vector2(1f, 1f);
            text3.tint = new Vector4(0,1,1,1);
            text3.borderOffset = new Vector2(.006f, .006f);
            text3.borderWidth = 0.2f;
            text3.borderFadeWidth = 0.5f;
            text3.borderColor = new Vector3(0,0,0);
            text3.kernning = 12;
            text3.SetText("This is A Gaming Moment");

            GameManager.currentScene.textRenderer.AddText(text1);
            GameManager.currentScene.textRenderer.AddText(text2);
            GameManager.currentScene.textRenderer.AddText(text3);
        }

        protected override void Update() {      
             timer += GameTime.deltaTime;

            sprite1.rotation += GameTime.deltaTime*5;
            if (sprite1.rotation > 360) sprite1.rotation = 0;

            for (int i = 0; i < sprites.Count; i++) {
                sprites[i].rotation += GameTime.deltaTime * 15 * ((float)i / (float)sprites.Count);
                if (sprites[i].rotation > 360) sprites[i].rotation = 0;
            }
            
            sprite2.position = new Vector2((800 / 3 * 2) + (MathF.Sin(GameTime.totalElapsedSeconds*4) * 50), sprite2.position.Y);
            sprite2.tint = new Vector4((MathF.Sin(GameTime.totalElapsedSeconds * 4)/2)+0.5f, 1, 1, 1);

            animatedSprite1.Update();
            animatedSprite2.Update();

            text1.SetText($"FPS: {(int)(1 / frameTime)}");

            TaskSystem.Update();
        }

        protected override void Render() {
            // TODO: offload this to the game class

            //CurrentScene.renderer.Render();
            //CurrentScene.renderer.PresentRender();

            GameManager.currentScene.batchRenderer.Render();
            GameManager.currentScene.textRenderer.Render();
            GameManager.currentScene.batchRenderer.PresentRender();
        }

        protected override void Close() {
            
        }
    }
}