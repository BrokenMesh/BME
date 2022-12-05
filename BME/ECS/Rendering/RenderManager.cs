using BME.ECS.Entity.Components;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.ECS.Rendering {
    public class RenderManager {

        private List<DrawableComponent> drawables;

        public RenderManager() {
            drawables = new List<DrawableComponent>();
        }

        public void AddDrawable(DrawableComponent _drawable) {
            drawables.Add(_drawable);
        }



    }
}
