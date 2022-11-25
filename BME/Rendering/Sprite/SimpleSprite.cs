using System;
using System.Numerics;
using BME.Rendering.Textures;

namespace BME.Rendering.Sprites
{
    public class SimpleSprite : Sprite
    {
        public Texture spriteTexture { get; private set; }

        public SimpleSprite(Texture _spriteTextute) : base(Vector2.Zero, 1, new Vector2(100, 100), 0, Vector4.One) { 
            spriteTexture = _spriteTextute;
        }

        public override Texture GetTexture() {
            return spriteTexture;
        }

        public override void Delete() {
            spriteTexture.Delete();
        }
    }
}
