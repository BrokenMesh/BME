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
        public List<Entity> entities;

        public EntityManager() {
            entities = new List<Entity>();
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
            Dictionary<string, DataFile> _children = _df.GetAllChildren();

            foreach (string _name in _children.Keys) {
                entities.Add(new Entity(_df.Get(_name)));
            }

        }

    }
}
