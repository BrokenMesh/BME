using System;
using BME.Rendering.Renderer;
using BME.Rendering.Shaders;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.GameLoop.Scenes;

namespace BME.GameLoop
{
    public static class GameManager {
         
        public static Scene currentScene = new Scene();

        public static void DefaultSceneSetup() {
            currentScene = new Scene();

            currentScene.shader = new Shader("./res/shaders/spriteShader.vs", "./res/shaders/spriteShader.fs");
            currentScene.camera = new Camera2D(DisplayManager.windowSize / 2f, 1f);
            currentScene.renderer = new SpriteRenderer(currentScene.shader, currentScene.camera);
            currentScene.batchRenderer = new BatchRenderer(currentScene.camera, 10_000);
            currentScene.textRenderer = new TextRenderer(currentScene.camera);
            currentScene.entityManeger = new EntityManeger();
        }

        public static void CleanScene() {
            currentScene.shader?.Delete();
            currentScene.renderer?.Delete();
            currentScene.batchRenderer?.Delete();
            currentScene.textRenderer?.Delete();

            DefaultSceneSetup();
            GC.Collect();
        }

        public static void DeleteGLScene() {
            currentScene.shader?.Delete();
            currentScene.renderer?.Delete();
            currentScene.batchRenderer?.Delete();
            currentScene.textRenderer?.Delete();
        }

    }
}
