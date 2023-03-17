using BME.ECS.Entity.Components;
using BME.Rendering.Display;
using GLFW;
using static OpenGL.OpenGL.GL;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BME.Rendering.Camera;
using BME.Rendering.Shaders;
using BME.Util;
using System.Numerics;
using System.IO;

namespace BME.ECS.Rendering {
    public class RenderManager 
    {
        public Camera2D camera;

        private BaseRenderer baseRenderer;
        private List<DrawableComponent> drawables;

        public RenderManager() {
            camera = new Camera2D(DisplayManager.windowSize /2, 1.0f);
            baseRenderer = new BaseRenderer(camera, new Shader("./res/shaders/batchRenderer.vs", "./res/shaders/batchRenderer.fs"), 10_000);
            drawables = new List<DrawableComponent>();
        }

        public RenderManager(DataFile _df) {
            drawables = new List<DrawableComponent>();
            Load(_df);
        }

        public void Load(DataFile _df) {
            // camera
            Vector2? _focusPoint = MathUtil.DFGetVec2(_df.GetPath("camera/focusPoint"));
            float? _zoom = _df.GetPath("camera/zoom").GetFloat(0);

            if (_zoom != null && _focusPoint != null) {
                camera = new Camera2D((Vector2)_focusPoint / 2, (float)_zoom);
            } else {
                camera = new Camera2D(DisplayManager.windowSize / 2, 1f);
            }

            // renderer
            string vShaderPath = _df.GetPath("renderer/vertexShader").GetString();
            string fShaderPath = _df.GetPath("renderer/fragmentShader").GetString();
            int? maxVertex = _df.GetPath("renderer/maxVertexCount").GetInt(0);

            if (File.Exists(vShaderPath) && File.Exists(fShaderPath) && maxVertex != null) {
                baseRenderer = new BaseRenderer(camera, new Shader(vShaderPath, fShaderPath), (uint)maxVertex);
            } else {
                baseRenderer = new BaseRenderer(camera, new Shader("./res/shaders/batchRenderer.vs", "./res/shaders/batchRenderer.fs"), 10_000);
            }
        }

        public void Save(DataFile _df) {
            MathUtil.DFSetVec2(_df.GetPath("camera/focusPoint"), camera.focusPosition);
            _df.GetPath("camera/zoom").SetFloat(camera.Zoom);

            Shader _s = baseRenderer.GetShader();
            _df.GetPath("renderer/vertexShader").SetString(_s.vertexFilepath);
            _df.GetPath("renderer/fragmentShader").SetString(_s.fragmentFilepath);
            _df.GetPath("renderer/maxVertexCount").SetInt((int)baseRenderer.GetMaxVertCount());
        }

        public void Render() {
            baseRenderer.Render(drawables);
        }

        public void AddDrawable(DrawableComponent _drawable) {
            drawables.Add(_drawable);
            drawables.Sort((s1, s2) => s1.owner.transform.zDepth.CompareTo(s2.owner.transform.zDepth));
        }

        public void RemoveDrawable(DrawableComponent _drawable) {
            drawables.Remove(_drawable);
        }

        public void PresentRender() {
            Glfw.SwapBuffers(DisplayManager.window);
        }


    }
}
