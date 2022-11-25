using System;
using System.Numerics;

namespace BME.GameLoop.Scenes.Entitys
{
    public abstract class Entity {

        public Vector2 position { get; set; }
        public float zDepth { get; set; }
        public Vector2 scale { get; set; }
        public float rotation { get; set; }

        public DependencyType type { get; protected set; }

        protected Entity(Vector2 _position, float _zDepth, Vector2 _scale, float _rotation) {
            position = _position;
            zDepth = _zDepth;
            scale = _scale;
            rotation = _rotation;
        }

        public abstract void Init();
        public abstract void Update();
        public abstract void Destroy();
    }
}
