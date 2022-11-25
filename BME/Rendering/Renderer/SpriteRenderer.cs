using System;
using System.Numerics;
using System.Collections.Generic;
using BME.Rendering.Sprites;
using BME.Rendering.Shaders;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.Rendering.Textures;
using static OpenGL.OpenGL.GL;
using GLFW;

namespace BME.Rendering.Renderer
{
    public class SpriteRenderer
    {
        public Shader shader;
        public Camera2D camera;

        private List<Sprite> sprites;
        
        private uint vao;
        private uint vbo;

        public unsafe SpriteRenderer(Shader _shader, Camera2D _camera) {
            shader = _shader;
            camera = _camera;

            sprites = new List<Sprite>();
            
            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] _vertices = {
                -0.5f,  0.5f, 1.0f,     0.0f, 0.0f, // top left
                 0.5f,  0.5f, 1.0f,     1.0f, 0.0f,// top right
                -0.5f, -0.5f, 1.0f,     0.0f, 1.0f, // bottom left

                 0.5f,  0.5f, 1.0f,     1.0f, 0.0f,// top right
                 0.5f, -0.5f, 1.0f,     1.0f, 1.0f, // bottom right
                -0.5f, -0.5f, 1.0f,     0.0f, 1.0f, // bottom left
            };

            fixed (float* _ptrVertices = &_vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * _vertices.Length, _ptrVertices, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        public void Render() {
            foreach (Sprite _sprite in sprites) {
                RenderSprite(_sprite);
            }
        }

        public void PresentRender() {
            Glfw.SwapBuffers(DisplayManager.window);
        }

        private void RenderSprite(Sprite _sprite)  {
            Texture _texture = _sprite.GetTexture();

            shader.SetSlot("u_texture", 0);
            shader.SetTexture(_texture, 0);

            shader.Bind();
            shader.SetMatrix4x4("u_projection", camera.GetProjectionMatrix());
            shader.SetMatrix4x4("u_model", _sprite.GetModelMatrix());
            shader.SetVec4("u_tint", _sprite.tint);

            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);
        }
        
        public void AddSprite(Sprite _sprite) {
            sprites.Add(_sprite);
            sprites.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        public void AddRemove(Sprite _sprite) {
            sprites.Remove(_sprite);
            sprites.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        public void Delete() {
            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);
        }

        public void DeleteAll(){
            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);

            foreach(Sprite _s in sprites)
                _s.Delete();
        }

    }
}
