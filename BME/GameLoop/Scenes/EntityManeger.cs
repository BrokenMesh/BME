using System;
using System.Collections.Generic;
using BME.GameLoop.Scenes.Entitys;

namespace BME.GameLoop.Scenes
{
    public class EntityManeger {

        public List<Entity> entities { get; private set; }

        public EntityManeger() {
            entities = new List<Entity>();
        }

        public void InitAll() {
            foreach (Entity _e in entities) _e.Init();  
        }

        public void UpdateAll() {
            foreach (Entity _e in entities) _e.Update();
        }

        public void DestroyAll() {
            foreach (Entity _e in entities) {
                _e.Destroy();
                
            }
            entities.Clear();
        }

        public void AddEntity(Entity _entity) {
            entities.Add(_entity);
        }

        public void RemoveEntity(Entity _entity) { 
            entities.Remove(_entity);
        }
    }
}
