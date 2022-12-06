using BME.GameLoop;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Numerics;
using static OpenGL.OpenGL.GL;

namespace BME.Rendering.Animation
{
    public class TextureAnimation
    {
        public float animationTime = 1.0f;
        public AnimationType type = AnimationType.Linear | AnimationType.LoopFlag;

        private bool typeArray = false;

        private List<Texture> animationTextures = new List<Texture>();

        private int frameCount;
        public int currentIndex;

        private float animationPassedTime = 0.0f;

        public TextureAnimation(List<Texture> _animationTextures) {
            typeArray = true;
            animationTextures = _animationTextures;
            frameCount = animationTextures.Count;
        }

        public TextureAnimation(string _textureAtlasFilename, int _textureCountX, int _textureCountY) {
            typeArray = false;

            frameCount = _textureCountX * _textureCountY;

            // Generate UV Cords
            float _texSizeX = 1 / (float)_textureCountX;
            float _texSizeY = 1 / (float)_textureCountY;

            for (int iy = 0; iy < _textureCountY; iy++) {
                for (int ix = 0; ix < _textureCountX; ix++) {

                    animationTextures.Add(TextureLoader.LoadTexture2DPart_win(
                        _textureAtlasFilename, 
                        GL_LINEAR, 
                        _texSizeX*ix, 
                        _texSizeY*iy, 
                        (_texSizeX*ix)+_texSizeX, 
                        (_texSizeY * iy) + _texSizeY
                        )
                    );

                }
            }
        }

        // TODO: Allow function pointer for animation updates so custome animation types can be used.
        public void Update() {
            animationPassedTime += GameTime.deltaTime;
            if (animationPassedTime >= animationTime && (type & AnimationType.LoopFlag) == AnimationType.LoopFlag) 
                animationPassedTime = 0.0f;

            switch (type & ~AnimationType.LoopFlag)
            {
                case AnimationType.Linear:
                        currentIndex = (int)((animationPassedTime / animationTime) * (float)frameCount);
                    break;
                case AnimationType.Reverse:
                        currentIndex = (int)((1-(animationPassedTime / animationTime)) * (float)frameCount);
                    break;
                case AnimationType.PingPong:
                    if (animationPassedTime / animationTime < 0.5f) {
                        currentIndex = (int)((animationPassedTime / animationTime)*2 * (float)frameCount);
                    } else { 
                        currentIndex = (int)((1-(animationPassedTime / animationTime)) * 2 * (float)frameCount);
                    }
                    break;
                default:
                    break;
            }

            currentIndex = Math.Clamp(currentIndex, 0, frameCount-1);
        }

        public Texture GetTexture() {
            return animationTextures[currentIndex];
        }

        public void Delete() {
            foreach (Texture _t in animationTextures)
                _t.Delete();
        }

    }
}
