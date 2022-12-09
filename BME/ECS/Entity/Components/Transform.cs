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

        public override void Load(DataFile _df) {
            Vector2? _pos = MathUtil.DFGetVec2(_df.Get("position"));
            if (_pos != null) position = (Vector2)_pos;
            
            Vector2? _scl = MathUtil.DFGetVec2(_df.Get("scale"));
            if (_scl != null) scale = (Vector2)_scl;

            float? _zDepth = _df.Get("zDepth").GetFloat(0);
            if(_zDepth != null) zDepth = (float)_zDepth;

            float? _rotation = _df.Get("rotation").GetFloat(0);
            if (_rotation != null) rotation = (float)_rotation * ((float)Math.PI / 180);
        }

        public override DataFile Save() {
            DataFile _df = new DataFile();

            MathUtil.DFSetVec2(_df.Get("position"), position);
            MathUtil.DFSetVec2(_df.Get("scale"), scale);
            _df.Get("zDepth").SetFloat(zDepth);
            _df.Get("rotation").SetFloat(rotation * (180 / (float)Math.PI));

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
