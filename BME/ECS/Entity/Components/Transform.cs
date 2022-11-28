using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entitys.Components
{
    public class Transform : Component
    {
        public Vector2 position;
        public Vector2 scale;
        public float zDepth;
        public float rotation;

        public override DataFile Save() {
            DataFile _df = new DataFile();

            MathUtil.DFSetVec2(_df.Get("position"), position);
            MathUtil.DFSetVec2(_df.Get("scale"), scale);
            _df.Get("/zDepth").SetFloat(zDepth);
            _df.Get("/rotation").SetFloat(rotation);

            return _df;
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
