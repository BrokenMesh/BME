using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entitys
{
    public abstract class Component
    {
        public Entity owner;

        public abstract void Update();
        public abstract void Start();

        public abstract DataFile Save();

    }
}
