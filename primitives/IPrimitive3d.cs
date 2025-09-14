using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public interface IPrimitive3d
    {
        float[] GetShape(Vector3 center, float height, float width, float depth, CustomColor color);
    }

    public class CustomColor
    {
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;

        public CustomColor(float Red, float Green, float Blue, float Alpha)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
            this.Alpha = Alpha;
        }
    }

}
