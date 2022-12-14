using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BME.ECS.Entitys;
using BME.GameLoop;
using BME.GameLoop.Input;
using BME.Util;

namespace GUI_Demo {
    public class PlayerComponent : Component {

        public float speed = 5;
        
        public override void Load(DataFile _df) {
            float? _speed = _df.Get("speed").GetFloat(0);

            if(_speed != null)
                speed = _speed.Value;
        }

        public override DataFile Save() {
            DataFile _df =  new DataFile();

            _df.Get("speed").SetFloat(speed);

            return _df;
        }

        public override void Start() {
        }

        public override void Update() {
            Vector2 _move = Vector2.Zero;

            if (InputManager.GetKey(KeyCode.S)) _move.Y =+ 1;
            if (InputManager.GetKey(KeyCode.W)) _move.Y =- 1;
            if (InputManager.GetKey(KeyCode.D)) _move.X =+ 1;
            if (InputManager.GetKey(KeyCode.A)) _move.X =- 1;

            owner.transform.position += _move * speed * GameTime.deltaTime;
        }
    }
}
