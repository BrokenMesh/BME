using BME.Util;
using BME.ECS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS
{
    public class Level
    {
        private EntityManager entityManager;

        public Level() {
            entityManager = new EntityManager();
        }

        public Level(DataFile _sceneData) {
            entityManager = new EntityManager();
        }



    }
}
