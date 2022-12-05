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

            return new Vector2((float)_x, (float)_y);
        }

        public static void DFSetColor(DataFile _df, Vector4 _col) {
            _df.GetPath("color/r").SetFloat(_col.X);
            _df.GetPath("color/g").SetFloat(_col.Y);
            _df.GetPath("color/b").SetFloat(_col.Z);
            _df.GetPath("color/a").SetFloat(_col.W);
        }
        public static Vector4? DFGetColor(DataFile _df) {
            float? _r = _df.GetPath("color/r").GetFloat(0);
            float? _g = _df.GetPath("color/g").GetFloat(0);
            float? _b = _df.GetPath("color/b").GetFloat(0);
            float? _a = _df.GetPath("color/a").GetFloat(0);

            if (_r == null || _g == null || _b == null || _a == null)
                return null;

            return new Vector4((float)_r,(float)_g, (float)_b, (float)_a);
        }
    }
}
