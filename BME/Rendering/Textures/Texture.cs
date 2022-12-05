using System;
using static OpenGL.OpenGL.GL;

namespace BME.Rendering.Textures
{
    public class Texture
    {
        public string source;
        public int width { get; private set; }
        public int height { get; private set; }
        public uint pixelformat { get; private set; }
        public uint textureID { get; private set; }

        public Texture(string _source, int _width, int _height, uint _pixelformat, int _textureFilter, IntPtr _data) {
            source = _source;
            
            width = _width;
            height = _height;
            pixelformat = _pixelformat;

            textureID = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, textureID);

            // TODO: use pixelformat to create correct Texture
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, _width, _height, 0, GL_BGRA, GL_UNSIGNED_BYTE, _data);
            
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, _textureFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, _textureFilter);

            glGenerateMipmap(GL_TEXTURE_2D);
        }

        public void Delete() { 
            glDeleteTexture(textureID);
        }

    }
}
