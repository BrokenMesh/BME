using System;
using System.Collections.Generic;
using System.Numerics;
using BME.Rendering.Textures;
using BME.Rendering.Animation;

namespace BME.Rendering.Sprites
{
    class AnimatedSprite : Sprite
    {
        public TextureAnimation animation { get; private set;}

        public AnimatedSprite(List<Texture> _animationTextures) : base(Vector2.Zero, 1.0f, new Vector2(100, 100), 0, Vector4.One) {
            animation = new TextureAnimation(_animationTextures);
        }

        public AnimatedSprite(string _textureAtlasFilename, int _textureCountX, int _textureCountY) : base(Vector2.Zero, 1.0f,new Vector2(100, 100), 0, Vector4.One) {
            animation = new TextureAnimation(_textureAtlasFilename, _textureCountX, _textureCountY);
        }

        public void Update() {
            animation.Update();
        }

        public void SetAnimationTime(float _time) {
            animation.animationTime = _time;
        }

        public override Texture GetTexture() {   
            return animation.GetTexture();
        }

        public override void Delete() {
            animation.Delete();
        }
    }


}
