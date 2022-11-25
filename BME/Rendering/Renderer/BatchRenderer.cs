using System;
using System.Numerics;
using System.Collections.Generic;
using BME.Rendering.Shaders;
using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using BME.Rendering.Camera;
using BME.Rendering.Display;
using BME.Rendering.Geomerty;
using static OpenGL.OpenGL.GL;
using GLFW;

namespace BME.Rendering.Renderer
{
    public class BatchRenderer
    {
        public Camera2D camera;

        private Shader batchShader;
        private List<Sprite> spritesBatch;

        private uint vao;
        private uint vbo;

        private float[] vertexPreset;

        public unsafe BatchRenderer(Camera2D _camera, int _maxVerticis) {
            camera = _camera;

            spritesBatch = new List<Sprite>();

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

            glBufferData(GL_ARRAY_BUFFER, (sizeof(float) * 10) * _maxVerticis, (void*)0, GL_DYNAMIC_DRAW);

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
            
            batchShader = new Shader("./res/shaders/batchRenderer.vs", "./res/shaders/batchRenderer.fs");
            batchShader.Bind();
            int _uniformLocation = glGetUniformLocation(batchShader.programID, "u_Textures");


            int[] _index = new int[32];

            for (int i = 0; i < 32; i++)
                _index[i] = i;

            fixed (int* _ptrIndex = &_index[0]) {
                glUniform1iv(_uniformLocation, 32, _ptrIndex);
            }

        }

        public void AddSprite(Sprite _sprite) {
            spritesBatch.Add(_sprite);
            spritesBatch.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        public void AddRemove(Sprite _sprite) {
            spritesBatch.Remove(_sprite);
            spritesBatch.Sort((s1, s2) => s1.zDepth.CompareTo(s2.zDepth));
        }

        public unsafe void Render() {
            List<Texture> _texIDs = new List<Texture>();
            List<float> _vertexData = new List<float>();

            glUseProgram(batchShader.programID);

            for (int i = 0; i < spritesBatch.Count; i++) {

                // evaluate what texture will be used
                int _texIndex = _texIDs.Count;
                if (_texIDs.Contains(spritesBatch[i].GetTexture())) {
                    _texIndex = _texIDs.IndexOf(spritesBatch[i].GetTexture());
                }
                else {
                    _texIDs.Add(spritesBatch[i].GetTexture());

                    //batchShader.SetSlot("u_texture", _texIndex);
                    glActiveTexture(0x84C0 + _texIndex);
                    glBindTexture(GL_TEXTURE_2D, spritesBatch[i].GetTexture().textureID);
                }

                // Add verteces of current Sprite to vertexData
                for (int _vi = 0; _vi < 6; _vi++) {
                    // Position
                    Vector3 _vertexPos = new Vector3(vertexPreset[(_vi*10)], vertexPreset[(_vi * 10) + 1], vertexPreset[(_vi * 10) + 2]);
                    _vertexPos = Vector3.Transform(Vector3.Transform(_vertexPos, spritesBatch[i].GetModelMatrix()), camera.GetProjectionMatrix());

                    _vertexData.Add(_vertexPos.X);
                    _vertexData.Add(_vertexPos.Y);
                    _vertexData.Add(_vertexPos.Z);

                    // UV
                    _vertexData.Add(vertexPreset[(_vi * 10) + 3]);
                    _vertexData.Add(vertexPreset[(_vi * 10) + 4]);

                    // Texture Index
                    _vertexData.Add((float)_texIndex);

                    // Color
                    _vertexData.Add(spritesBatch[i].tint.X);
                    _vertexData.Add(spritesBatch[i].tint.Y);
                    _vertexData.Add(spritesBatch[i].tint.Z);
                    _vertexData.Add(spritesBatch[i].tint.W);
                }

                // if there are no texture units available we render;
                if (_texIDs.Count >= DisplayManager.textureUnitCount-1){
                    // Render
                    glBindVertexArray(vao);

                    float[] _vertices = _vertexData.ToArray();
                    fixed (float* _ptrVertices = &_vertices[0]) {
                        glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(float) * _vertexData.Count, _ptrVertices);
                    }

                    glDrawArrays(GL_TRIANGLES, 0, _vertices.Length/10);

                    // Clear 
                    _texIDs.Clear();
                    _vertexData.Clear();
                }
            }

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] _verticesi = _vertexData.ToArray();
            fixed (float* _ptrVertices = &_verticesi[0])
            {
                glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(float) * _verticesi.Length, _ptrVertices);
            }

            glDrawArrays(GL_TRIANGLES, 0, _verticesi.Length/10);
        }

        public void PresentRender() {
            Glfw.SwapBuffers(DisplayManager.window);
        }

        public void Delete() {
            batchShader.Delete();

            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);
        }

        public void DeleteAll() {
            batchShader.Delete();

            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);

            foreach (Sprite _s in spritesBatch)
                _s.Delete();
        }
    }
}
