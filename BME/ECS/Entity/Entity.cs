using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BME.Util;
using BME.ECS.Entitys.Components;

namespace BME.ECS.Entitys
{
    public class Entity
    {
        public bool isEnabled = true;
        public string name;
        public Transform transform;

        private List<Component> components;

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
        public Entity(DataFile _df, EntityManager _manager) {
            name = _df.Get("name").GetString();
            isEnabled = _df.Get("isEnabled").GetInt(0) == 1;
            
            components = new List<Component>();
            List<string> _components = _df.Get("components").GetDataList();

            foreach (string _componentname in _components) {
                Component? _component = _manager.ResolveComponentName(_componentname);
                if (_component == null) return;

                _component.Load(_df.Get(_componentname));
                _component.owner = this;
                components.Add(_component);
            }

            transform = new Transform();
            transform.owner = this;
            transform.Load(_df.Get("Transfrom"));
        }

        public void AddComponent(Component _component) {
            _component.owner = this;
            components.Add(_component);
        }

        public void Start() {
            components.ForEach(c => c.Start());
        }

        public void Update() {
            if (!isEnabled) return;
            components.ForEach(c => c.Update());
        }

        public void Save(DataFile _df) {
            DataFile _ce = _df.Get(name);
            _ce.Get("name").SetString(name);
            _ce.Get("isEnabled").SetInt(isEnabled ? 1 : 0);

            foreach (Component _c in components) {
                _ce.Get("components").SetString(_c.GetType().Name);
                _ce.Get(_c.GetType().Name).Set(_c.Save());
            }

            _ce.Get("Transfrom").Set(transform.Save());
        }

    }
}
