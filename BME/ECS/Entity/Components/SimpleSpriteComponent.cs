using BME.Rendering.Textures;
using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entity.Components {
    public class SimpleSpriteComponent : DrawableComponent {

        public Vector4 tint;

        private Texture spriteTexture;

        public void SetTexture(Texture _texture) {
            spriteTexture = _texture;
            tint = Vector4.One;
        }

        public void SetTint(float _r, float _g, float _b, float _a) {
            tint = new Vector4(_r,_g,_b,_a);
        }

        public override Texture GetTexture() {
            return spriteTexture; 
        }

        public override void Load(DataFile _df) {
            spriteTexture = TextureLoader.LoadTexture2DnoFlip_win(_df.Get("texture").GetString(), OpenGL.OpenGL.GL.GL_LINEAR);

            tint = Vector4.One;
            Vector4? _tint = MathUtil.DFGetColor(_df.Get("tint"));
            if (_tint != null) tint = (Vector4)_tint;
        }

        public override DataFile Save() {
            DataFile _df = new DataFile();
            _df.Get("texture").SetString(spriteTexture.source);
            MathUtil.DFSetColor(_df.Get("tint"), tint);
            return _df;
        }

        public override void Start() {
        }

        public override void Update() {
        }

        public override void Delete() {
        }
    }
}
