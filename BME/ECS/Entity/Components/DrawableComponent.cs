using BME.ECS.Entitys;
using BME.ECS.Entitys.Components;
using BME.Rendering.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entity.Components {
    public abstract class DrawableComponent : Component {
        
        public virtual Matrix4x4 GetModelMatrix() {
            Matrix4x4 _trans = Matrix4x4.CreateTranslation(owner.transform.position.X, owner.transform.position.Y, owner.transform.zDepth);
            Matrix4x4 _sca = Matrix4x4.CreateScale(owner.transform.scale.X, owner.transform.scale.Y, 0);
            Matrix4x4 _rot = Matrix4x4.CreateRotationZ(owner.transform.rotation);

            return _sca * _rot * _trans;
        }

        public override void Start() {
            Level.current.renderManager.
        }

        public abstract Texture GetTexture();
        public abstract void Delete();

    }
}
