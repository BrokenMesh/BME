using System;
using System.Numerics;
using BME.Rendering.Sprites;

namespace BME.GameLoop.Scenes.Entitys
{
    class AnimatedSpriteEntity : Entity
    {
        private AnimatedSprite animatedSprite;

        public AnimatedSpriteEntity(AnimatedSprite _sprite, float _animationTime, Vector2 _position, float _zDepth, Vector2 _scale, float _rotation, Vector4 _tint) : base(_position, _zDepth, _scale, _rotation) {
            animatedSprite = _sprite;
            _sprite.SetAnimationTime(_animationTime);
            animatedSprite.position = _position;
            animatedSprite.rotation = _rotation;
            animatedSprite.scale = _scale;
            animatedSprite.zDepth = _zDepth;
            animatedSprite.tint = _tint;
        }

        public override void Init()
        {
        }

        public override void Update() {
            animatedSprite.position = position;
            animatedSprite.rotation = rotation;
            animatedSprite.zDepth = zDepth;
            animatedSprite.scale = scale;
            animatedSprite.Update();
        }

        public override void Destroy()
        {
        }

        public void SetTint(Vector4 _tint) {
            animatedSprite.tint = _tint;
        }

        public void SetAnimationTime(float _animationTime) {
            animatedSprite.SetAnimationTime(_animationTime);
        }
        

    }
}
