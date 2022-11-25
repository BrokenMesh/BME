using System;
using System.Numerics;
using System.Collections.Generic;
using BME.Rendering.Display;

namespace BME.GameLoop.Scenes
{
    class ReadUtil
    {
        public static string ReadValueName(string _line){
            return _line.Split("=")[0].Trim();
        }

        public static string ReadStrValue(string _line){
            return _line.Split("=")[1].Trim();
        }
        public static int ReadIntValue(string _line){
            string _value = _line.Split("=")[1].Trim();

            if (_value.StartsWith("[") && _value.EndsWith("]")) {
                switch (_value) {
                    case "[WindowWidth]": return (int)DisplayManager.windowSize.X;
                    case "[WindowHeight]": return (int)DisplayManager.windowSize.Y;
                }
            }

            return int.Parse(_value);
        }
        public static float ReadFloatValue(string _line){
            string _value = _line.Split("=")[1].Trim();

            if (_value.StartsWith("[") && _value.EndsWith("]")) {
                switch (_value) {
                    case "[WindowWidth]": return DisplayManager.windowSize.X;
                    case "[WindowHeight]": return DisplayManager.windowSize.Y;
                }
            }

            return float.Parse(_value);
        }
        public static Vector2 ReadVec2Value(string _line){
            string _value = _line.Split("=")[1].Trim();

            if (_value.StartsWith("[") && _value.EndsWith("]")) {
                switch (_value) {
                    case "[WindowSize]": return DisplayManager.windowSize;
                }
            }

            string[] _strVec = _value.Split(",");
            return new Vector2(float.Parse(_strVec[0]), float.Parse(_strVec[1]));
        }
        public static Vector3 ReadVec3Value(string _line){
            string[] _strVec = _line.Split("=")[1].Trim().Split(",");
            return new Vector3(float.Parse(_strVec[0]), float.Parse(_strVec[1]), float.Parse(_strVec[2]));
        }
        public static Vector4 ReadVec4Value(string _line) {
            string _value = _line.Split("=")[1].Trim();

            if (_value.StartsWith("[") && _value.EndsWith("]")) {
                switch (_value) {
                    case "[White]": return Vector4.One;
                    case "[Red]": return new Vector4(1,0,0,1);
                    case "[Green]": return new Vector4(0, 1, 0, 1);
                    case "[Blue]": return new Vector4(0, 0, 1, 1);
                }
            }

            string[] _strVec = _value.Split(",");
            return new Vector4(float.Parse(_strVec[0]), float.Parse(_strVec[1]), float.Parse(_strVec[2]), float.Parse(_strVec[3]));
        }
    }
}
