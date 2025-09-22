using OpenGL.Shaders;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.primitives
{

    

    public class BoxFigure : Object, IHaveMesh
    {
        private Shader shader;
        IHaveMesh meshBuffer;

        public BoxFigure(float height, float width, float depth, CustomColor color, Shader shader)
        {
            this.shader = shader;
            meshBuffer = new MeshBuffer(GetShape(width, height, depth, color));

        } 
      
        public void RenderMesh ()
        {

            shader.SetMatrix4("model", modelView);

            meshBuffer.RenderMesh();
            shader.Use();

        }

        public void LoadMesh ()
        {
            
            meshBuffer.LoadMesh();
            shader.Init();

        }


        public float[] GetShape(float height, float width, float depth, CustomColor color)
        {
            float w = width / 2;
            float h = height / 2;
            float d = depth / 2;

            float[] verts =
            {                                    //color                                          //Normal             //UV     
                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  1.0f,     // Front face
                  +w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  0.0f,
                  +w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
                  +w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
                  -w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  1.0f,
                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,  0.0f,  0.0f,

                  -w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  1.0f,      // Back face
                  +w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  0.0f,
                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
                  -w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  1.0f,
                  -w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,  0.0f,  0.0f,

                  -w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  1.0f,       // Left face
                  -w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  0.0f,
                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
                  -w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
                  -w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, -1.0f, 0.0f,  0.0f,  1.0f,  1.0f,

                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  1.0f,       // Right face
                  +w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
                  +w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
                  +w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
                  +w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,  1.0f,  1.0f,

                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  0.0f,       // Bottom fac
                  +w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  0.0f,
                  +w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  1.0f,
                  +w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  1.0f,  1.0f,
                  -w, -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  1.0f,
                  -w, -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,  0.0f,  0.0f,

                  -w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  0.0f,       // Top face
                  +w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  0.0f,
                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  1.0f,
                  +w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  1.0f,  1.0f,
                  -w, +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  1.0f,
                  -w, +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,  0.0f,  0.0f

            };
            return verts;
        }


    }
}
