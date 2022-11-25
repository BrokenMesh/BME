using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BME.Rendering.Geomerty
{
    struct Vertex
    {
        float x;
        float y;

        float u;
        float v;
    }

    struct BatchVertex {
        float x;
        float y;

        float u;
        float v;

        float texId;

        float r;
        float g;
        float b;
        float a;

    }
}
