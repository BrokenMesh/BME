using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using BME.GameLoop.Scenes.Entitys;
using BME.Rendering.Sprites;
using BME.Rendering.Textures;
using static OpenGL.OpenGL.GL;

namespace BME.GameLoop.Scenes
{
    class DependencyLoader
    {
        private Dictionary<int, Action<string[], Scene>> depLoaders;

        public DependencyLoader() {
            depLoaders = new Dictionary<int, Action<string[], Scene>>();

            depLoaders.Add((int)DependencyType.Struct, LoadStruct);
            depLoaders.Add((int)DependencyType.Sprite, LoadSprite);
        }

        public void LoadDependency(string _line, Scene _scene) {
            string[] _sLine = _line.Split("=");
            
            Console.WriteLine($"Loading Dependency '{_sLine[0]}'");

            string[] _strVal = _sLine[1].Trim().Split(",");

            int _depId = -1;
            _strVal[0] = _strVal[0].Trim();
            if (_strVal[0].StartsWith("[") && _strVal[0].EndsWith("]")) {

                for (int i = 0; i < (int)DependencyType.Count; i++) {
                    if (_strVal[0].Equals($"[{((DependencyType)(i)).ToString()}]"))
                        _depId = i;
                }

            } else {
                _depId = int.Parse(_strVal[0]);
            }

            string _depPath = _strVal[1].Trim();

            string _rawDepFile = File.ReadAllText(_depPath);
            depLoaders[_depId](_rawDepFile.Split("\n"), _scene);
        }
        
        private void LoadStruct(string[] _lines, Scene _scene) {
            for (int i = 0; i < _lines.Length; i++) {
                if (_lines[i].StartsWith("D"))
                   LoadDependency(_lines[i], _scene);
            }
        }

        /* - Sprite Dep File ----------
         * Line 1: Texture Path:      S texturePath=<Name>
         * Line 2: Position:          V position=<float,float>
         * Line 3: Z Depth:           F zDepth=<float>
         * Line 4: Scale:             V scale=<float,float>
         * Line 5: Rotation:          F rotation=<float>
         * Line 6: Tint:              V tint:<float>,<float>,<float>,<float>
         */
        private void LoadSprite(string[] _lines, Scene _scene) {
            string _texturePath = ReadUtil.ReadStrValue(_lines[0]);
            Vector2 _position = ReadUtil.ReadVec2Value(_lines[1]);
            float _zDepth = ReadUtil.ReadFloatValue(_lines[2]);
            Vector2 _scale = ReadUtil.ReadVec2Value(_lines[3]);
            float _rotation = ReadUtil.ReadFloatValue(_lines[4]);
            Vector4 _tint = ReadUtil.ReadVec4Value(_lines[5]);

            SimpleSprite _sprite = new SimpleSprite(TextureLoader.LoadTexture2D_win(_texturePath, GL_LINEAR));
            SpriteEntity _spriteEntity = new SpriteEntity(_sprite, _position, _zDepth, _scale, _rotation, _tint);

            _scene.batchRenderer.AddSprite(_sprite);
            _scene.entityManeger.AddEntity(_spriteEntity);
        }

        /* - Sprite Dep File ----------
         * Line 1: Position:          V position=<float,float>
         * Line 2: Z Depth:           F zDepth=<float>
         * Line 3: Scale:             V scale=<float,float>
         * Line 4: Rotation:          F rotation=<float>
         * Line 5: Tint:              V tint:<float>,<float>,<float>,<float>
         * Line 6: AnimationTime:     F animationTime=<float>
         * Line 7: Texture Path:      S texturePath=<type>,(<path>,<int>,<int>)/(<int>) / type: A/F, Shows if frames are in Atlas(one file) or FileArray(list of files).
         */
        private void LoadAnimatedSprite(string[] _lines, Scene _scene) {
            Vector2 _position = ReadUtil.ReadVec2Value(_lines[0]);
            float _zDepth = ReadUtil.ReadFloatValue(_lines[1]);
            Vector2 _scale = ReadUtil.ReadVec2Value(_lines[2]);
            float _rotation = ReadUtil.ReadFloatValue(_lines[3]);
            Vector4 _tint = ReadUtil.ReadVec4Value(_lines[4]);
            float _animationTime = ReadUtil.ReadFloatValue(_lines[5]);
            string _texturePathStr = ReadUtil.ReadStrValue(_lines[6]);

            AnimatedSprite _sprite = null;

            if (_texturePathStr.StartsWith('A')) {
                string[] _texturePath = _texturePathStr.Split(",");
                _sprite = new AnimatedSprite(_texturePath[1].Trim(), int.Parse(_texturePath[2]), int.Parse(_texturePath[3]));
            }
            if (_texturePathStr.StartsWith('F')) {
                int _frameCount = int.Parse(_texturePathStr.Split(",")[1]);

                string[] _framePaths = new string[_frameCount];
                for (int i = 7; i < _frameCount+7; i++) {
                    _framePaths[i-7] = _lines[i].Trim();
                }

                List<Texture> _frameTextures = TextureLoader.LoadTexture2DList_win(_framePaths, GL_LINEAR);
                _sprite = new AnimatedSprite(_frameTextures);
            }

            if (_sprite == null) {
                Console.WriteLine("Error: Animated Sprite dep failed to load");
                return;
            }
            
            AnimatedSpriteEntity _spriteEntity = new AnimatedSpriteEntity(_sprite, _animationTime, _position, _zDepth, _scale, _rotation, _tint);

            _scene.batchRenderer.AddSprite(_sprite);
            _scene.entityManeger.AddEntity(_spriteEntity);
        }
    }
}
