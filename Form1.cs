using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenGL
{
    public partial class Form1 : Form
    {

        Camera camera;

        Shader lightingShader;

        private float[] vertices;

        private int vertexBufferObject;
        private int vertexArrayobject;
        //private int elementArrayBuffer;



        private Matrix4 viewModel;

        private Matrix4 projectionModel;

        private Vector3 lamp;

        public Form1()
        {
            InitializeComponent();

            camera = new Camera(new Vector3(0.0f, 1.0f, -5.0f));

            // fix til at vise lampe og kasse

            lamp = new Vector3(0, 1.5f, 2);

            float[] kasse = DrawBox(new Vector3(0f, 0f, 0f), 1f, 1f, 1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));
            float[] lampHolder = DrawBox(lamp, 0.1f, 0.1f, 0.1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));

            vertices = new float[lampHolder.Length + kasse.Length]; // showing box
            Array.Copy(lampHolder, vertices, lampHolder.Length);
            Array.Copy(kasse, 0, vertices, lampHolder.Length, kasse.Length);


            this.KeyPreview = true; // Let the form receive key events
            this.KeyPress += new KeyPressEventHandler(Keypressed);


        }



        private void Render()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            lightingShader.Use();

            GL.BindVertexArray(vertexArrayobject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);

            Matrix4 model = Matrix4.Identity;
            //Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(45)) * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(10));

            lightingShader.SetMatrix4("model", model);
            lightingShader.SetMatrix4("view", camera.GetViewMatrix());
            lightingShader.SetMatrix4("projection", projectionModel);

            lightingShader.SetVec3("objectColor", new Vector3(1f, 1f, 0.3f));

            lightingShader.SetVec3("lightColor", new Vector3(-1f, 1f, 2f));
            lightingShader.SetVec3("lightPosition", lamp);

            
            lightingShader.SetVec3("viewPos", camera.GetPosition);

            glControl1.SwapBuffers();

        }




        private void Keypressed(System.Object o, KeyPressEventArgs e)
        {
            // The keypressed method uses the KeyChar property to check 
            // whether the ENTER key is pressed. 
            camera.UpdateMovement(o, e);
            // If the ENTER key is pressed, the Handled property is set to true, 
            // to indicate the event is handled.
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                      
            Render();
        }



        private void glControl1_Load(object sender, EventArgs e)
        {

            glControl1.MakeCurrent();
            
            GL.ClearColor(0.0f, 0.4f, 0.6f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            // lav en VBO
            vertexBufferObject = GL.GenBuffer();

            // bind bufferens type til VBO'en
            // det her er lidt ligesom at sætte et stik i en server, så der er forbindelse mellem VBO og OpenGL
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            // send dataen for VBO til OpenGL med BufferData
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);


            // opret en VAO = vertexArrayobject
            // denne beskriver hvilke floats i VBO'en der repræsentere hvad.
            vertexArrayobject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayobject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 10 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 7 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            
            viewModel = camera.GetViewMatrix();

            float w = glControl1.ClientSize.Width;
            float h = glControl1.ClientSize.Height;
            projectionModel = camera.GetProjectionMatrix(w, h);

            //shader = new Shader("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/shader.frag");
            lightingShader = new Shader("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/lighting.frag");


            lightingShader.Use();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Render();
        }

        public float[] DrawSquare(Vector3 center, float width, float height, CustomColor color)
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

        public float[] DrawBox(Vector3 center, float width, float height, float depth, CustomColor color)
        {
            float w = width / 2;
            float h = height / 2;
            float d = depth / 2;




            float[] verts =
            {                                    //color                                          //Normal
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f, // Front face
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f, -1.0f,
                                                                                               
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f, // Back face
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  0.0f,  1.0f,
                                                                                               
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f, // Left face
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                                                                                               
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f, // Right face
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 1.0f,  0.0f,  0.0f,
                                                                                               
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f, // Bottom fac
                  center.X  +w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,
                  center.X  +w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,
                  center.X  -w, center.Y -h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,
                  center.X  -w, center.Y -h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f, -1.0f,  0.0f,
                                                                                               
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f, // Top face
                  center.X  +w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,
                  center.X  +w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,
                  center.X  -w, center.Y +h,  d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f,
                  center.X  -w, center.Y +h, -d, color.Red, color.Green, color.Blue, color.Alpha, 0.0f,  1.0f,  0.0f

            };
            return verts;
        }

        public struct CustomColor
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
}
  
  