using BME.ECS.Entity.Components;
using BME.ECS.Entitys.Components;
using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entitys
{
    public class EntityManager
    {
        private List<Entity> entities;
        private Func<string, Component?> customResolve;

        public EntityManager() {
            entities = new List<Entity>();
        }
        public EntityManager(Func<string, Component?> _customResolve) {
            entities = new List<Entity>();
            customResolve = _customResolve;
        }

        public void AddEntity(Entity _entity) {
            int _dupCount = 0;
            foreach (Entity _e in entities) {
                if (_entity.name == _e.name) {
                    _dupCount++;
                    _entity.name = _e.name + "(" + _dupCount + ")";
                }
            }

            entities.Add(_entity);
        }

        public void Setup() {
            entities.ForEach(e => { e.Start(); });
        }

        public void Update() {
            entities.ForEach(e => { e.Update(); });
        }

        public void Save(DataFile _df) {
            entities.ForEach(e => { e.Save(_df.Get("entities")); });
        }

        public void Load(DataFile _df) {
            DataFile _entitysDF = _df.Get("entities");
            Dictionary<string, DataFile> _children = _entitysDF.GetAllChildren();

            foreach (string _name in _children.Keys) {
                entities.Add(new Entity(_entitysDF.Get(_name), this));
            }
        }

        public Component? ResolveComponentName(string _name) {
            switch (_name) {
                case "Transform":               return new Transform();
                case "DemoComponent":           return new DemoComponent(); 
                case "SimpleSpriteComponent":   return new SimpleSpriteComponent();
                case "AnimatedSpriteComponent": return new AnimatedSpriteComponent();
            }

            if(customResolve != null)
                return customResolve(_name);

            return null;
        }

        public bool SignalByTag(string _tag, string _componentName, string _value) {
            bool _state = false;

            foreach (Entity _e in entities) {
                if (_e.tag == _tag) {
                    if (_e.Signal(_componentName, _value))
                        _state = true;
                }
            }

            return _state;
        }

        public bool SignalAll(string _componentName, string _value) {
            bool _state = false;
            
            foreach (Entity _e in entities) {
                if (_e.Signal(_componentName, _value))
                    _state = true;
            }

            return _state;
        }

        public bool SendSignal(string _entityName, string _componentName, string _value) {
            foreach (Entity _e in entities) {
                if(_e.name == _entityName)
                    return _e.Signal(_componentName, _value);
            }

            return false;
        }

        public void SetCustomeResolver(Func<string, Component?> _resolver) {
            customResolve = _resolver;
        }

    }
}
