using BME.Rendering.Textures;
using System.Numerics;
using System;
using System.Collections.Generic;
using BME.Rendering.Font;
using static OpenGL.OpenGL.GL;


namespace BME.Rendering.Sprites
{
    public class TextSprite : Sprite
    {
        public float width = 0.5f;
        public float fadeWidth = 0.1f;

        public float borderWidth = 0.0f;
        public float borderFadeWidth = 0.5f;

        public Vector3 borderColor = Vector3.One;
        public Vector2 borderOffset = Vector2.Zero;

        public int kernning = 0;

        private uint vbo;
        private uint vao;

        private string sourceText;
        private FontData font;
        private uint vertexCount;

        public unsafe TextSprite(FontData _font) : base(new Vector2(100, 100), 10, Vector2.One, 0.0f, Vector4.One) {
            font = _font;
            vertexCount = 0;

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] _vertices = { 1 };

            fixed (float* _ptrVertices = &_vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * _vertices.Length, null, GL_DYNAMIC_DRAW);
            }

            glVertexAttribPointer(0, 3, GL_FLOAT, false, 9 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 2, GL_FLOAT, false, 9 * sizeof(float), (void*)(3 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glVertexAttribPointer(2, 4, GL_FLOAT, false, 9 * sizeof(float), (void*)(5 * sizeof(float)));
            glEnableVertexAttribArray(2);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        public unsafe void SetText(string _text) {
            sourceText = _text;
            vertexCount = 0;
            List<float> _vertexData = new List<float>();

            int _cursorX = 0;
            int _cursorY = font.baseHeight;

            foreach (char _char in _text) {
                FontCharData _charData = font.charData[_char];

                if (_charData.baseChar != _char) {
                    Console.WriteLine($"char '{_char}' dose not aline with chardata of {_charData.baseChar}. index={(int)_char}");
                    continue;
                }

                int charX = _cursorX + _charData.xOffset;
                int charY = _cursorY - _charData.yOffset;

                GenVertexFromData(charX, charY, _charData.x, _charData.y, ref _vertexData);                                                                            //-0.5f,  0.5f, 1.0f,     0.0f, 0.0f, // top left
                GenVertexFromData(charX + _charData.width, charY, _charData.x + _charData.width, _charData.y, ref _vertexData);                                        // 0.5f,  0.5f, 1.0f,     1.0f, 0.0f,// top right
                GenVertexFromData(charX, charY - _charData.height, _charData.x, _charData.y + _charData.height, ref _vertexData);                                       //-0.5f, -0.5f, 1.0f,     0.0f, 1.0f, // bottom left
                
                GenVertexFromData(charX + _charData.width, charY, _charData.x + _charData.width, _charData.y, ref _vertexData);                                        // 0.5f,  0.5f, 1.0f,     1.0f, 0.0f,// top right
                GenVertexFromData(charX + _charData.width, charY - _charData.height, _charData.x + _charData.width, _charData.y + _charData.height, ref _vertexData);  // 0.5f, -0.5f, 1.0f,     1.0f, 1.0f, // bottom right
                GenVertexFromData(charX, charY - _charData.height, _charData.x, _charData.y + _charData.height, ref _vertexData);                                       //-0.5f, -0.5f, 1.0f,     0.0f, 1.0f, // bottom left

                _cursorX += _charData.xAdvance - kernning;

                vertexCount += 6;

                if (_char == '\n') {
                    _cursorX = 0;
                    _cursorY -= font.lineHeight;
                }
            }


            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] _vertices = _vertexData.ToArray();
            fixed (float* _ptrVertices = &_vertices[0]) {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * _vertices.Length, _ptrVertices, GL_DYNAMIC_DRAW);
            }

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        private void GenVertexFromData(int _x, int _y, int _uvX, int _uvY, ref List<float> _vertexData) {
            // Pos
            _vertexData.Add((float)_x); // x
            _vertexData.Add((float)_y); // y
            _vertexData.Add(1);         // z

            // UV
            _vertexData.Add((float)(_uvX) / (float)(font.imgWidth));  // u
            _vertexData.Add((float)(font.imgHeight - _uvY) / (float)(font.imgHeight)); // v

            // TODO: Add subtext colors
            // Color
            _vertexData.Add(tint.X);
            _vertexData.Add(tint.Y);
            _vertexData.Add(tint.Z);
            _vertexData.Add(tint.W);

        }

        public uint GetVertexCount() {
            return vertexCount;
        }

        public uint GetVAO() { 
            return vao;
        }

        public override Texture GetTexture() {
            return font.atlas;
        }
        public override Matrix4x4 GetModelMatrix() {
            Matrix4x4 _trans = Matrix4x4.CreateTranslation(position.X, position.Y, zDepth);
            Matrix4x4 _sca = Matrix4x4.CreateScale(scale.X, -scale.Y, 0);
            Matrix4x4 _rot = Matrix4x4.CreateRotationZ(rotation);

            return _sca * _rot * _trans;
        }

        public override void Delete() {
            glDeleteBuffer(vao);
            glDeleteBuffer(vbo);
        }
    }
}
