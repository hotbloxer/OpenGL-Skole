using OpenGL.primitives;
using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Windows.Forms;



namespace OpenGL
{
    internal class OpenGLProcesses: IUseOpenGL
    {
        GLControl glControl;

        Shader lightingShader;

        ICamera camera;

        private int vertexBufferObject;
        private int vertexArrayobject;

        private float[] vertices;

        private Matrix4 viewModel;

        private Matrix4 projectionModel;

        private Vector3 lamp;

        IPrimitive3d box;
        IPrimitive2d quad;

        public void Render()
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

            glControl.SwapBuffers();
        }


        public void Initialize(GLControl glControl,  ICamera camera)
        {
            // fix til at vise lampe og kasse
            this.camera = camera;
            this. glControl = glControl;

            lamp = new Vector3(0, 1.5f, 2);

            box = new BoxGeometry();
            quad = new Square();

            float[] kasse = box.GetShape(new Vector3(0f, 0f, 0f), 1f, 1f, 1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));
            float[] lampHolder = box.GetShape(lamp, 0.1f, 0.1f, 0.1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));

            vertices = new float[lampHolder.Length + kasse.Length]; // showing box
            Array.Copy(lampHolder, vertices, lampHolder.Length);
            Array.Copy(kasse, 0, vertices, lampHolder.Length, kasse.Length);

        }

        public void Load()
        {
            glControl.MakeCurrent();

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

            float w = glControl.ClientSize.Width;
            float h = glControl.ClientSize.Height;
            projectionModel = camera.GetProjectionMatrix(w, h);

            //shader = new Shader("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/shader.frag");
            lightingShader = new Shader("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/cellShaded.frag");


            lightingShader.Use();
        }


    }


    public interface IUseOpenGL 
    {
        void Render();

        void Initialize(GLControl glControl, ICamera camera);

        void Load();


    }
}
