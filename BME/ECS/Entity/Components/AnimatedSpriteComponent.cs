using BME.Rendering.Animation;
using BME.Rendering.Textures;
using BME.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entity.Components {
    public class AnimatedSpriteComponent : DrawableComponent {

        public TextureAnimation animation;

        public override Texture GetTexture() {
            return animation.GetTexture();
        }

        public override void Start() {
            base.Start();
        }

        public override void Update() {
            animation.Update();
        }
        public override void Load(DataFile _df) {
            bool _isAtlas = _df.GetPath("animation/textures/isAtlas").GetString() == "true" ? true : false;

            if (_isAtlas) {
                string _path = _df.GetPath("animation/textures/path").GetString();
                Vector2? _rawsize = MathUtil.DFGetVec2(_df.GetPath("animation/textures/atlasSize"));

                if (!File.Exists(_path) || _rawsize == null) {
                    Console.WriteLine("Was not able to load AnimatedSpriteComponent.");
                    return;
                }
                Vector2 _size = (Vector2)_rawsize;
                animation = new TextureAnimation(_path, (int)_size.X, (int)_size.Y);
            }
            else {
                int? _count = _df.GetPath("animation/textures/count").GetInt(0);

                if (_count == null) {
                    Console.WriteLine("Was not able to load AnimatedSpriteComponent.");
                    return;
                }

                string[] _filepaths = new string[(int)_count];

                for (int i = 0; i < _count; i++) {
                    _filepaths[i] = _df.GetPath($"animation/textures/path/texture[{i}]").GetString();
                }
                
                List<Texture> _texAnimation = TextureLoader.LoadTexture2DList_win(_filepaths, OpenGL.OpenGL.GL.GL_LINEAR);
                animation = new TextureAnimation(_texAnimation);
            }

            float? _animationTime = _df.GetPath("animation/time").GetFloat(0);
            float? _flags = _df.GetPath("animation/flags").GetInt(0);
            Vector4? _tint = MathUtil.DFGetColor(_df.GetPath("tint"));

            if (_tint == null || _flags == null || _animationTime == null) {
                Console.WriteLine("Was not able to load AnimatedSpriteComponent.");
                return;
            }

            animation.animationTime = (float)_animationTime;
            animation.type = (AnimationType)_flags;
            tint = (Vector4)_tint;
        }

        public override DataFile Save() {
            DataFile _df = new DataFile();

            _df.GetPath("animation/time").SetFloat(animation.animationTime, 0);
            _df.GetPath("animation/flags").SetInt((int)animation.type , 0);

            List<Texture> _textures = animation.GetAllTextures();

            if (animation.isAtlas) {
                _df.GetPath("animation/textures/isAtlas").SetString("true");
                MathUtil.DFSetVec2(_df.GetPath("animation/textures/atlasSize"), animation.atlasSize);
                _df.GetPath("animation/textures/path").SetString(_textures[0].source);
            }
            else { 
                _df.GetPath("animation/textures/isAtlas").SetString("false");
                _df.GetPath("animation/textures/count").SetInt(_textures.Count);
                for (int i = 0; i < _textures.Count; i++) {
                    _df.GetPath($"animation/textures/path/texture[{i}]").SetString(_textures[i].source);
                }
            }

            MathUtil.DFSetColor(_df.GetPath("tint"), tint);

            return _df;
        }

        public override void Delete() {
            animation.Delete();
        }
    }
}
