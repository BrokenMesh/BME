using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Components
{
    public class Transform : Component
    {
        public Vector2 position;
        public Vector2 scale;
        public float zDepth;
        public float rotation;

        public override void Save(DataFile _df) {
            MathUtil.DFSetVec2(_df.GetPath("Transform/position/"), position);
            MathUtil.DFSetVec2(_df.GetPath("Transform/scale/"), scale);
            _df.GetPath("Transform/zDepth/").SetFloat(zDepth);
            _df.GetPath("Transform/rotation/").SetFloat(rotation);
        }

        public override void Start() {
        }

        public override void Update() {
        }
    }
}
