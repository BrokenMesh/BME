using BME.Rendering.Animation;
using BME.Rendering.Textures;
using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }

        public override DataFile Save() {
            DataFile _df = new DataFile();

            _df.GetPath("")

            return _df;
        }

        public override void Delete() {
            animation.Delete();
        }
    }
}
