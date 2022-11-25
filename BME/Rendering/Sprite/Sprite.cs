using System;
using System.Numerics;
using BME.Rendering.Textures;

namespace BME.Rendering.Sprites
{
    public abstract class Sprite
    {

        public Vector2 position { get; set; }
        public float zDepth { get; set; }
        public Vector2 scale { get; set; }
        public float rotation { get; set; }
        public Vector4 tint { get; set; }

        protected Sprite(Vector2 _position, float _zDepth, Vector2 _scale, float _rotation, Vector4 _tint) {
            position = _position;
            zDepth = _zDepth;
            scale = _scale;
            rotation = _rotation;
            tint = _tint;
        }


        public virtual Matrix4x4 GetModelMatrix() {
            Matrix4x4 _trans = Matrix4x4.CreateTranslation(position.X, position.Y, zDepth);
            Matrix4x4 _sca = Matrix4x4.CreateScale(scale.X, scale.Y, 0);
            Matrix4x4 _rot = Matrix4x4.CreateRotationZ(rotation);

            return _sca * _rot * _trans;
        }

        public abstract Texture GetTexture();

        public abstract void Delete();
    }
}
