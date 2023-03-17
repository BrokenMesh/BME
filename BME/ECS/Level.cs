using BME.Util;
using BME.ECS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BME.ECS.Rendering;

namespace BME.ECS
{
    public class Level
    {
        public static Level current;

        public EntityManager entityManager;
        public RenderManager renderManager; 

        public Level() {
            if (current != null) throw new Exception("Cannot load more then one level.");

            current = this;
            entityManager = new EntityManager();
            renderManager = new RenderManager();
        }

        public Level(DataFile _sceneData) {
            if (current != null)
                throw new Exception("Cannot load more then one level.");

            current = this;
            entityManager = new EntityManager();
            renderManager = new RenderManager();

            Load(_sceneData);
        }

        public void Load(DataFile _sceneData) {
            entityManager.Load(_sceneData.Get("em"));
            renderManager.Load(_sceneData.Get("rm"));
        }

        public void Save(DataFile _sceneData) {
            entityManager.Save(_sceneData.Get("em"));
            renderManager.Save(_sceneData.Get("rm"));
        }

        public void Start() {
            entityManager.Setup();
        }

        public void Update() {
            entityManager.Update();
        }
        public void Render() {
            renderManager.Render();
            renderManager.PresentRender();
        }

    }
}
