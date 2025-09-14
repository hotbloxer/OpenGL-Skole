using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.primitives
{
    internal class Square: IPrimitive2d
    {
        public float[] GetShape(Vector3 center, float width, float height, CustomColor color)
        {
            float w = width / 2;
            float h = height / 2;

            float[] verts =
            {
              center.X +  h, center.Y -  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // top left
              center.X +  h, center.Y +  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // top right
              center.X -  h, center.Y +  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // bottom right
              center.X -  h, center.Y -  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // bottom left
              center.X +  h, center.Y -  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // top left
              center.X + -h, center.Y +  w, center.Z, color.Red, color.Green, color.Blue, color.Alpha, // bottom right
            };
            return verts;
        }
    }
}
