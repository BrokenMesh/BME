using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS
{
    public abstract class Component
    {
        public Entity owner;

        public abstract void Update();
        public abstract void Start();

        public abstract void Save(DataFile _df);

    }
}
