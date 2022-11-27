using System;
using System.IO;
using System.Numerics;
using static OpenGL.OpenGL.GL;
using GLFW;
using BME.Rendering.Textures;
using BME.Util;

namespace BME.Rendering.Shaders {
    public class Shader
    {
        public string vertexFilepath;
        public string fragmentFilepath;

        private string vertexCode;
        private string fragmentCode;

        private Texture[] textures;

        public uint programID { get; private set; }

        public Shader(string _vertexFilepath, string _fragmenFilepath) {
            vertexFilepath = _vertexFilepath;
            fragmentFilepath = _fragmenFilepath;

            try {
                vertexCode = System.IO.File.ReadAllText(_vertexFilepath);
                fragmentCode = System.IO.File.ReadAllText(_fragmenFilepath);
            }
            catch(System.Exception e) {
                Console.WriteLine("Could not read shader files, program will exit\n\n{0}", e);
                Environment.Exit(0);
            }

            textures = new Texture[Display.DisplayManager.textureUnitCount];

            CreateShader();
        }

        private void CreateShader() {
            programID = glCreateProgram();
            uint _vs = Compile(vertexCode, GL_VERTEX_SHADER);
            uint _fs = Compile(fragmentCode, GL_FRAGMENT_SHADER);

            glAttachShader(programID, _vs);
            glAttachShader(programID, _fs);

            glLinkProgram(programID);

            glDetachShader(programID, _vs);
            glDetachShader(programID, _fs);

            glDeleteShader(_vs);
            glDeleteShader(_fs);
        }

        private unsafe uint Compile(string _code, int _type) {
            uint _id = glCreateShader(_type);

            glShaderSource(_id, _code);
            glCompileShader(_id);

            int _result;
            glGetShaderiv(_id, GL_COMPILE_STATUS, &_result);
            if (_result != GL_TRUE) {
                string _msg = glGetShaderInfoLog(_id);
                Console.WriteLine("shader error: " + _msg + "\n\n src: " + _code);
                return 0;
            }

            return _id;
        }
        public void Bind() {
            for (int i = 0; i < textures.Length; i++) {
                if (textures[i] == null) continue;
                glActiveTexture(0x84C0 + i);
                glBindTexture(GL_TEXTURE_2D, textures[i].textureID);
            }

            glUseProgram(programID);
        }

        public void Unbind() {
            glUseProgram(0);

            for (int i = 0; i < textures.Length; i++) { 
                glActiveTexture(0x84C0 + i);
                glBindTexture(GL_TEXTURE_2D, 0);
            }
        }

        public void SetMatrix4x4(string _uniformName, Matrix4x4 _mat) {
            int _location = glGetUniformLocation(programID, _uniformName);
            glUniformMatrix4fv(_location, 1, false, MathUtil.GetMatrix4x4Values(_mat));
        }

        public void SetVec4(string _uniformName, Vector4 _vec4) {
            int _location = glGetUniformLocation(programID, _uniformName);
            glUniform4f(_location, _vec4.X, _vec4.Y, _vec4.Z, _vec4.W);
        }

        public void SetVec3(string _uniformName, Vector3 _vec3) {
            int _location = glGetUniformLocation(programID, _uniformName);
            glUniform3f(_location, _vec3.X, _vec3.Y, _vec3.Z);
        }

        public void SetVec2(string _uniformName, Vector2 _vec2) {
            int _location = glGetUniformLocation(programID, _uniformName);
            glUniform2f(_location, _vec2.X, _vec2.Y);
        }

        public void SetFloat(string _uniformName, float _f) {
            int _location = glGetUniformLocation(programID, _uniformName);
            glUniform1f(_location, _f);
        }

        public void SetSlot(string _uniformName, int _textureSlot) {
            glUseProgram(programID);
            glUniform1i(glGetUniformLocation(programID, _uniformName), _textureSlot);
            glUseProgram(0);
        }

        public void SetTexture(Texture _texture, int _textureSlot) {
            textures[_textureSlot] = _texture;
        }

        public void Delete() {
            glDeleteProgram(programID);
        }
    }
}
