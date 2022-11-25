using System;
using BME.GameLoop;
using BME.GameLoop.Scenes;


namespace BME
{
    class SceneLoadDemo : Game
    {
        public SceneLoadDemo(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) {
        }

        protected override void Initalize(){
        }

        protected override void LoadContent() {
            GameManager.DeleteGLScene(); 
            GameManager.currentScene = SceneLoader.LoadScene("./res/scenes/mainScene/scene.scn");
            GameManager.currentScene.entityManeger.InitAll();
        }
        protected override void Update() {
            GameManager.currentScene.entityManeger.UpdateAll();
            TaskSystem.Update();
        }

        protected override void Render() {
            GameManager.currentScene.batchRenderer.Render();
            GameManager.currentScene.textRenderer.Render();
            GameManager.currentScene.batchRenderer.PresentRender();
        }


        protected override void Close() {
            GameManager.currentScene.entityManeger.DestroyAll();
        }

    }
}
