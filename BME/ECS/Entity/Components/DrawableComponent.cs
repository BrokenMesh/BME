using BME.ECS.Entitys;
using BME.ECS.Entitys.Components;
using BME.Rendering.Textures;
using GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entity.Components {
    public abstract class DrawableComponent : Component {

        public Vector4 tint;

        public virtual Matrix4x4 GetModelMatrix() {
            Matrix4x4 _trans = Matrix4x4.CreateTranslation(owner.transform.position.X, owner.transform.position.Y, owner.transform.zDepth);
            Matrix4x4 _sca = Matrix4x4.CreateScale(owner.transform.scale.X, owner.transform.scale.Y, 0);
            Matrix4x4 _rot = Matrix4x4.CreateRotationZ(owner.transform.rotation);

            return _sca * _rot * _trans;
        }

        public void SetTint(float _r, float _g, float _b, float _a) {
            tint = new Vector4(_r, _g, _b, _a);
        }



        public override void Start() {
            Level.current.renderManager.AddDrawable(this);
        }

        public abstract Texture GetTexture();
        public abstract void Delete();

    }
}
