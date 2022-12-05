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
            entityManager = new EntityManager();
        }



    }
}
