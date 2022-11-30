using BME.ECS.Entitys;
using BME.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Entity.Components {
    public class DemoComponent : Component {

        public string name = "";

        public override void Load(DataFile _df) {
            name = _df.Get("name").GetString();
        }

        public override DataFile Save() {
            DataFile _df = new DataFile();
            _df.AddComment("This thing dont work");
            _df.Get("name").SetString(name);
            return _df;
            
        }

        public override void Start() {
        }

        public override void Update() {
        }
    }
}
