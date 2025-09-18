using Microsoft.VisualBasic.ApplicationServices;
using OpenGL.primitives;
using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Windows.Forms;



namespace OpenGL
{
    internal class OpenGLProcesses : IUseOpenGL
    {
        private GLControl glControl;

        private Shader lightingShader;
        private Shader rimShader;
        private Shader toonShader;

        ICamera camera;

        private int vertexBufferObject;
        private int vertexArrayobject;

        private float[] vertices;

        private Matrix4 viewModel;

        private Matrix4 projectionModel;

        public ILamp lamp;

        IPrimitive3d box;

        Object box1;
        Object box2;




        private bool rimShaderEnabled = false;
        private bool toonEnabled = false;


        public OpenGLProcesses (GLControl glControl, ICamera camera, ILamp lamp)
        {
            // fix til at vise lampe og kasse
            this.camera = camera;
            this.glControl = glControl;

            this.lamp = lamp;

            //box = new BoxGeometry();



            //lightingShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");


            lightingShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");
            toonShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");
            // shader will be null here
            box1 = new BoxFigure(1,1,1, new CustomColor(1,1,0,1), lightingShader);
            box1 = new BoxFigure(1,0.5f ,2, new CustomColor(1, 0, 0, 1), toonShader);

            
            //float[] kasse = box.GetShape(new Vector3(0f, 0f, 0f), 1f, 1f, 1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));
            //float[] lampHolder = box.GetShape(lamp.Position, 0.1f, 0.1f, 0.1f, new CustomColor(0.01f, 0.1f, 0.01f, 1f));

            //vertices = new float[lampHolder.Length + kasse.Length]; // showing box
            //Array.Copy(lampHolder, vertices, lampHolder.Length);
            //Array.Copy(kasse, 0, vertices, lampHolder.Length, kasse.Length);


        }

        public void Load ()
        {
            glControl.MakeCurrent();

            GL.ClearColor(0.0f, 0.4f, 0.6f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            toonShader.LoadNewFragmentShader("C:/UnityProjects/OpenGL-Skole/simpleShader2.frag");
            //toonShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader2.frag");


            LoadAllMeshes();

            //lightingShader.Use();

        }

        private void LoadAllMeshes ()
        {
            foreach (Object obj in Object.Objects)
            {
                if (obj is IHaveMesh mesh)
                {
                    mesh.LoadMesh();
                }
            }
        }


        public void Load2()
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

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 12 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 7 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, 12 * sizeof(float), 10 * sizeof(float));
            GL.EnableVertexAttribArray(3);

            viewModel = camera.GetViewMatrix();


            byte[] pixels = LoadTDA("C:/Users/p-hou/Desktop/Skole/Grafik/test.tga", 0 );

            CreateTexture(300, 300, false, pixels, 0);



            float w = glControl.ClientSize.Width;
            float h = glControl.ClientSize.Height;
            projectionModel = camera.GetProjectionMatrix(w, h);

            
            lightingShader = new("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/lighting.frag");
            rimShader = new("C:/UnityProjects/OpenGL-Skole/lighting_RimLight.vs", "C:/UnityProjects/OpenGL-Skole/lighting_RimLight.frag");
            toonShader = new("C:/UnityProjects/OpenGL-Skole/cellShaded.vs", "C:/UnityProjects/OpenGL-Skole/cellShaded.frag");


            lightingShader.Use();

            
        }

        public int CreateTexture (int width, int height, bool alpha, byte[] pixels, ushort unit)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + unit);

            int textureID = 0;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out textureID);

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, pixels);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return textureID;

        }


        public byte[] LoadTDA (string fileName, ushort unit)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            if (bytes!= null)
            {
                TDAHeader header        = new TDAHeader();
                header.identSize        = bytes[0];
                header.colorMapType     = bytes[1];
                header.imageType        = bytes[2];
                header.colorMapStart    = (ushort) (bytes[3]  + (bytes[4] <<  8));
                header.colorMapLength   = (ushort) (bytes[5]  + (bytes[6] <<  8));
                header.colorMapBits     = bytes[7];             
                header.startX           = (ushort) (bytes[8]  + (bytes[9] << 8));
                header.startY           = (ushort) (bytes[10] + (bytes[11] << 8));
                header.width            = (ushort) (bytes[12] + (bytes[13] << 8));
                header.height           = (ushort)(bytes[14]  + (bytes[15] << 8));
                header.bits             = bytes[16];
                header.descriptor       = bytes[17];
                byte colorChannels = (byte)(header.bits >> 3);
                bool alpha = colorChannels > 3;
                byte[] pixels = new byte[header.height * header.width * colorChannels];
                for (uint i = 0; i < header.height * header.width * colorChannels; i++)
                {
                    pixels[i] = (byte)bytes[i +18];
                }

                return pixels;
            }

            return null;

        }

        public struct TDAHeader
        {
            public byte     identSize;
            public byte     colorMapType;
            public byte     imageType;
            public ushort   colorMapStart;
            public ushort   colorMapLength;
            public byte     colorMapBits;
            public ushort   startX;
            public ushort   startY;
            public ushort   width;
            public ushort   height;
            public byte     bits;
            public byte     descriptor;
        }


        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            RenderAllMeshes();
        }

        private void RenderAllMeshes()
        {
            foreach (Object obj in Object.Objects)
            {
                if (obj is IHaveMesh mesh)
                {
                    mesh.RenderMesh();
                }
            }
            glControl.SwapBuffers();
        }

        public void Render2()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 model = Matrix4.Identity;

            if (rimShaderEnabled)
            {
                GL.DepthMask(false);

                rimShader.SetMatrix4("model", Matrix4.CreateScale(1.2f));
                rimShader.SetMatrix4("view", camera.GetViewMatrix());
                rimShader.SetMatrix4("projection", projectionModel);

                rimShader.Use();

                GL.BindVertexArray(vertexArrayobject);

                GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);

                GL.DepthMask(true);
            }


            if (toonEnabled)
            {

                toonShader.SetMatrix4("model", model);
                toonShader.SetMatrix4("view", camera.GetViewMatrix());
                toonShader.SetMatrix4("projection", projectionModel);

                toonShader.SetVec3("lightPosition", lamp.Position);

                toonShader.Use();

                GL.BindVertexArray(vertexArrayobject);

                GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
            }

            else
            {
                lightingShader.SetMatrix4("model", model);
                lightingShader.SetMatrix4("view", camera.GetViewMatrix());
                lightingShader.SetMatrix4("projection", projectionModel);

                lightingShader.SetVec3("objectColor", new Vector3(1f, 1f, 0.3f));

                lightingShader.SetVec3("lightColor", new Vector3(-1f, 1f, 2f));
                lightingShader.SetVec3("lightPosition", lamp.Position);

        

                try { lightingShader.SetVec3("viewPos", camera.GetPosition); }
                catch (Exception e) { }

                lightingShader.Use();

                GL.BindVertexArray(vertexArrayobject);

                GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
            }

            glControl.SwapBuffers();
        }


        public void ChangeLightingShader(Shader shaderName)
        {

            lightingShader = shaderName;
            lightingShader.Use();
            Render();
        }

        public void ToggleRimLight(bool light)
        {
            rimShaderEnabled = light;
            Render();
        }


        
        public void SetToonShading(bool enabled)
        {
            toonEnabled = enabled;
            Render();
        }

        public void SetColor(Vector3 color)
        {

        }

    }


    public interface IUseOpenGL 
    {
        void Render();

        void Load();

        void ChangeLightingShader(Shader shaderName);

        void ToggleRimLight(bool light);

        void SetToonShading(bool enabled);

        void SetColor(Vector3 color);
    }


}
