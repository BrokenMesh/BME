using BME.Rendering.Renderer;
using BME.Rendering.Shaders;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.GameLoop.Scenes;


namespace BME.GameLoop.Scenes
{
    public class Scene {
        public string name;

        public Camera2D camera;
        public Shader shader;
        public SpriteRenderer renderer;
        public BatchRenderer batchRenderer;
        public TextRenderer textRenderer;
    }
}
