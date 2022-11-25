using System;
using System.Numerics;
using BME.Rendering.Sprites;

namespace BME.GameLoop.Scenes.Entitys
{
    class SpriteEntity : Entity
    {
        private SimpleSprite sprite;

        public SpriteEntity(SimpleSprite _sprite, Vector2 _position, float _zDepth, Vector2 _scale, float _rotation, Vector4 _tint) : base(_position, _zDepth, _scale, _rotation){
            sprite = _sprite;
            sprite.position = _position; 
            sprite.rotation = _rotation;
            sprite.scale = _scale;
            sprite.zDepth = _zDepth;
            sprite.tint = _tint;

            type = DependencyType.Sprite;
        }


        public override void Init()
        {
        }

        public override void Update() {
            sprite.position = position;
            sprite.rotation = rotation;
            sprite.zDepth = zDepth;
            sprite.scale = scale;
        }

        public override void Destroy() {
        }

        public void setTint(Vector4 _tint) { 
            sprite.tint = _tint;
        }
    }
}
