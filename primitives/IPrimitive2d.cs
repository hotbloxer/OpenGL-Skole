using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.primitives
{
    interface IPrimitive2d
    {
        float[] GetShape(Vector3 center, float width, float height, CustomColor color);
    }
}
