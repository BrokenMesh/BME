using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BME.ECS.Components;
using BME.Util;

namespace BME.ECS
{
    public class Entity 
    {
        public string name;

        Transform transform;
        List<Component> components;

        public Entity(string _name, Vector2 _position, Vector2 _scale, float _zDepth, float _rotation) {
            name = _name;

            transform = new Transform();
            transform.owner = this;
            transform.position = _position;
            transform.scale = _scale;
            transform.zDepth = _zDepth;
            transform.rotation = _rotation;
            components = new List<Component>();
        }

        public void AddComponent(Component _component) {
            _component.owner = this;
            components.Add(_component);
        }

        public void Start() {
            components.ForEach(c => c.Start());
        }

        public void Update() { 
            components.ForEach(c => c.Update());
        }

        public void Save(DataFile _df) {
            transform.Save(_df.Get(name));
            components.ForEach(c => c.Save(_df.Get(name)));
        }

    }
}
