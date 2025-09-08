using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenGL
{
    public partial class Form1 : Form
    {


        Shader shader;

        // We modify the vertex array to include four vertices for our rectangle.
        private readonly float[] vertices =
        {
             0.0f,   0.5f, 0.0f,  1.0f, 0.0f, 0.0f, // top 
            -0.5f,   0.5f, 0.0f,  1.0f, 1.0f, 0.0f, // top left
             0.5f,   0.5f, 0.0f,  1.0f, 0.0f, 1.0f,// top right
             0.5f,  -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,// bottom right
            -0.5f,  -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,// bottom left
        };


        private readonly uint[] indicies =
        {
            1,2,3,
            1,3,4,
        };

   

        private int vertexBufferObject;
        private int vertexArrayobject;
        private int elementArrayBuffer;


        public Form1()
        {
            InitializeComponent();

        }
 


        private void Render()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit); //| gl.DEPTH_BUFFER_BIT);

            shader.Use();

            GL.BindVertexArray(vertexArrayobject);

            GL.DrawElements(PrimitiveType.Triangles, indicies.Length, DrawElementsType.UnsignedInt, 0);
           

            glControl1.SwapBuffers();


        }




        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }



        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();
            GL.ClearColor(0.0f, 0.4f, 0.6f, 1.0f);

            // lav en VBO
            vertexBufferObject = GL.GenBuffer();

            // bind bufferens type til VBO'en
            // det her er lidt ligesom at sætte et stik i en server, så der er forbindelse mellem VBO og OpenGL
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);


            // send dataen for VBO til OpenGL med BufferData
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, vertices, BufferUsageHint.StaticDraw); previous



            // opret en VAO = vertexArrayobject
            // denne beskriver hvilke floats i VBO'en der repræsentere hvad.
            vertexArrayobject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayobject);

            elementArrayBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementArrayBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indicies.Length * sizeof(uint), indicies, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);





            shader = new Shader("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/shader.frag");

            shader.Use();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Render();
        }
    }
}
