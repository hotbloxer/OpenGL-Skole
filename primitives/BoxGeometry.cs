using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.primitives
{
    internal class BoxGeometry : IPrimitive3d
    {
        public float[] GetShape(Vector3 center, float height, float width, float depth, CustomColor color)
        {
            float w = width / 2;
            float h = height / 2;
            float d = depth / 2;

            float[] verts =
            {                                    //color                                          //Normal             //UV     
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  1.0f,     // Front face
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  0.0f,     
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  1.0f,     
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  1.0f,     
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  1.0f,     
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  0.0f,     
                                                                                                                            
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  1.0f,      // Back face
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  0.0f,       
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  1.0f,       
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  1.0f,       
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  0.0f,       
                                                                                                                            
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  1.0f,       // Left face
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  0.0f,       
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  1.0f,       
                                                                                                                            
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  1.0f,       // Right face
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  0.0f,       
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  1.0f,       
                                                                                                                            
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  0.0f,       // Bottom fac
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  0.0f,       
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  1.0f,       
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  1.0f,       
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  0.0f,       
                                                                                                                            
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  0.0f,       // Top face
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  0.0f,       
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  1.0f,       
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  1.0f,       
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  1.0f,       
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  0.0f      
                                                                                                                            
            };
            return verts;
        }
    }
}
