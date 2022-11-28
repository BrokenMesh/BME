using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.Util
{
    internal class MathUtil
    {
        public static float[] GetMatrix4x4Values(Matrix4x4 m)
        {
            return new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }

        public static void DFSetVec2(DataFile _df, Vector2 _vec) {
            _df.GetPath("vector/x").SetFloat(_vec.X);
            _df.GetPath("vector/y").SetFloat(_vec.Y);
        }
        public static Vector2? DFGetVec2(DataFile _df) {
            float? _x = _df.GetPath("vector/x").GetFloat(0);
            float? _y = _df.GetPath("vector/y").GetFloat(0);

            if (_x == null || _y == null)
                return null;

            return new Vector2((float)_x,(float)_y);
        }
    }
}
