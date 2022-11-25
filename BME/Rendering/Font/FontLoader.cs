using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;

namespace BME.Rendering.Font
{
    public class FontLoader
    {
        public static FontData LoadFont(string _file, string _textureFile) {
            string _rawData = File.ReadAllText(_file);
            FontData _fontData = new FontData();

            String[] _lines = _rawData.Split("\n");

            // info
            string[] _infoStr = _lines[0].Split(" ");
            _fontData.name = GetStrFromParam(_infoStr[1]);
            _fontData.isBold = GetIntFromParam(_infoStr[3]).Equals(1);
            _fontData.isItalic = GetIntFromParam(_infoStr[4]).Equals(1);
            _fontData.aspectRatio = GetIntFromParam(_infoStr[9]);

            string[] _pStr = _infoStr[10].Split("=")[1].Split(",");
            _fontData.padding = new Vector4(int.Parse(_pStr[0]), int.Parse(_pStr[1]), int.Parse(_pStr[2]), int.Parse(_pStr[3]));

            string[] _sStr = _infoStr[11].Split("=")[1].Split(",");
            _fontData.spacing = new Vector2(int.Parse(_sStr[0]), int.Parse(_sStr[1]));

            // commons
            string[] _commonsStr = _lines[1].Split(" ");
            _fontData.lineHeight = GetIntFromParam(_commonsStr[1]);
            _fontData.baseHeight = GetIntFromParam(_commonsStr[2]);
            _fontData.imgWidth = GetIntFromParam(_commonsStr[3]);
            _fontData.imgHeight = GetIntFromParam(_commonsStr[4]);

            _fontData.atlas = Textures.TextureLoader.LoadTexture2D_win(_textureFile, 0x2601);

            // chars (ascii only)
            string[] _charInfoStr = _lines[3].Split(" ");
            _fontData.charCount = GetIntFromParam(_charInfoStr[1]);

            for (int i = 0; i < _fontData.charCount; i++) {
                string[] _charStr = _lines[i+4].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                FontCharData _charData = new FontCharData();
                _charData.baseChar = (char)GetIntFromParam(_charStr[1]);
                _charData.x = GetIntFromParam(_charStr[2]);
                _charData.y = GetIntFromParam(_charStr[3]);
                _charData.width = GetIntFromParam(_charStr[4]);
                _charData.height = GetIntFromParam(_charStr[5]);
                _charData.xOffset = GetIntFromParam(_charStr[6]);
                _charData.yOffset = GetIntFromParam(_charStr[7]);
                _charData.xAdvance = GetIntFromParam(_charStr[8]);

                _fontData.charData[(int)_charData.baseChar] = _charData;
            }

            return _fontData;
        }

        public static int GetIntFromParam(string _param) {
            return int.Parse(_param.Split("=")[1]);
        }

        public static string GetStrFromParam(string _param){
            string _rawStr = _param.Split("=")[1];      
            return _rawStr.Substring(1, _rawStr.Length-2);
        }
    }
}
