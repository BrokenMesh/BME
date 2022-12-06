using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BME.ECS.Entity.Components;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.Rendering.Shaders;
using BME.Rendering.Textures;
using GLFW;
using static OpenGL.OpenGL.GL;

namespace BME.ECS.Rendering {
    internal class BaseRenderer {

        private Camera2D camera;
        private Shader shader;

        private uint vao;
        private uint vbo;

        private uint maxVerticis;
        private float[] vertexPreset;

        public unsafe BaseRenderer(Camera2D _camera, Shader _shader, uint _maxVerticis) {
            camera = _camera;
            shader = _shader;
            maxVerticis = _maxVerticis;

            float[] _vertexPreset = {
                -0.5f,  0.5f, 0.0f,    0.0f, 0.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // top left
                 0.5f,  0.5f, 0.0f,    1.0f, 0.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // top right
                -0.5f, -0.5f, 0.0f,    0.0f, 1.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // bottom left

                 0.5f,  0.5f, 0.0f,    1.0f, 0.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // top right
                 0.5f, -0.5f, 0.0f,    1.0f, 1.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,    0.0f, 1.0f,    0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  // bottom left
            };
            vertexPreset = _vertexPreset;

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            glBufferData(GL_ARRAY_BUFFER, (sizeof(float) * 10) * (int)_maxVerticis, (void*)0, GL_DYNAMIC_DRAW);

            // Vertex Position (3)
            glVertexAttribPointer(0, 3, GL_FLOAT, false, (sizeof(float) * 10), (void*)0);
            glEnableVertexAttribArray(0);

            // Vertex UV (2)
            glVertexAttribPointer(1, 2, GL_FLOAT, false, (sizeof(float) * 10), (void*)(3 * sizeof(float)));
            glEnableVertexAttribArray(1);

            // Vertex Texture Index (1)
            glVertexAttribPointer(2, 1, GL_FLOAT, false, (sizeof(float) * 10), (void*)(5 * sizeof(float)));
            glEnableVertexAttribArray(2);

            // Vertex Color (4)
            glVertexAttribPointer(3, 4, GL_FLOAT, false, (sizeof(float) * 10), (void*)(6 * sizeof(float)));
            glEnableVertexAttribArray(3);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            shader.Bind();
            int _uniformLocation = glGetUniformLocation(shader.programID, "u_Textures");

            int[] _index = new int[32];

            for (int i = 0; i < 32; i++)
                _index[i] = i;

            fixed (int* _ptrIndex = &_index[0]) {
                glUniform1iv(_uniformLocation, 32, _ptrIndex);
            }
        }

        public unsafe void Render(List<DrawableComponent> _drawables) {
            List<Texture> _texIDs = new List<Texture>();
            List<float> _vertexData = new List<float>();

            glUseProgram(shader.programID);

            for (int i = 0; i < _drawables.Count; i++) {

                // evaluate what texture will be used
                int _texIndex = _texIDs.Count;
                if (_texIDs.Contains(_drawables[i].GetTexture())) {
                    _texIndex = _texIDs.IndexOf(_drawables[i].GetTexture());
                } else {
                    _texIDs.Add(_drawables[i].GetTexture());

                    //batchShader.SetSlot("u_texture", _texIndex);
                    glActiveTexture((int)GL_TEXTURE0 + _texIndex);
                    glBindTexture(GL_TEXTURE_2D, _drawables[i].GetTexture().textureID);
                }

                // Add verteces of current Sprite to vertexData
                for (int _vi = 0; _vi < 6; _vi++) {
                    // Position
                    Vector3 _vertexPos = new Vector3(vertexPreset[(_vi * 10)], vertexPreset[(_vi * 10) + 1], vertexPreset[(_vi * 10) + 2]);
                    _vertexPos = Vector3.Transform(Vector3.Transform(_vertexPos, _drawables[i].GetModelMatrix()), camera.GetProjectionMatrix());

                    _vertexData.Add(_vertexPos.X);
                    _vertexData.Add(_vertexPos.Y);
                    _vertexData.Add(_vertexPos.Z);

                    // UV
                    _vertexData.Add(vertexPreset[(_vi * 10) + 3]);
                    _vertexData.Add(vertexPreset[(_vi * 10) + 4]);

                    // Texture Index
                    _vertexData.Add((float)_texIndex);

                    // Color
                    _vertexData.Add(_drawables[i].tint.X);
                    _vertexData.Add(_drawables[i].tint.Y);
                    _vertexData.Add(_drawables[i].tint.Z);
                    _vertexData.Add(_drawables[i].tint.W);
                }

                // if there are no texture units available we render;
                if (_texIDs.Count >= DisplayManager.textureUnitCount - 1) {
                    // Render
                    glBindVertexArray(vao);

                    float[] _vertices = _vertexData.ToArray();
                    fixed (float* _ptrVertices = &_vertices[0]) {
                        glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(float) * _vertexData.Count, _ptrVertices);
                    }

                    glDrawArrays(GL_TRIANGLES, 0, _vertices.Length / 10);

                    // Clear 
                    _texIDs.Clear();
                    _vertexData.Clear();
                }
            }

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] _verticesi = _vertexData.ToArray();
            fixed (float* _ptrVertices = &_verticesi[0]) {
                glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(float) * _verticesi.Length, _ptrVertices);
            }

            glDrawArrays(GL_TRIANGLES, 0, _verticesi.Length / 10);
        }

        public void Delete() {
            shader.Delete();

            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);
        }

        public Shader GetShader() {
            return shader;
        }

        internal uint GetMaxVertCount() {
            return maxVerticis;
        }
    }
}
