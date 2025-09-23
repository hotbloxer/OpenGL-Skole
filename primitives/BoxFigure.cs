using OpenGL.Shaders;
using OpenGL.Shaders.PhongShader;
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

            Matrix4 tes1 = Matrix4.Identity;
            Matrix4 test2 = Matrix4.Identity;
            Lamp test4 = new Lamp(new Vector3(1, 1, 1));
            ICamera test5 = new Camera(new(1,1,1)); 

            if (shader.GetType() == new TexturedShader(ref tes1, ref test2,  test4, ref test5).GetType())
            {
                TexturedShader test = (TexturedShader) shader;
                test.Use(modelView);
            }

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
            {     // position //color                                          //Normal             //UV     
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
