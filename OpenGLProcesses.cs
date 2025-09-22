using Microsoft.VisualBasic.ApplicationServices;
using OpenGL.primitives;
using OpenGL.Shaders;
using OpenGL.Shaders.PhongShader;
using OpenGL.Shaders.ToonShader;
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

        private StandardShader rimShader;
        private StandardShader toonShader;
        private Shader CurrentShader;

        ICamera camera;

        private int vertexBufferObject;
        private int vertexArrayobject;

        private float[] vertices;

        private Matrix4 viewModel;

        private Matrix4 projectionModel;


        public OpenGL.ILamp lamp;

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

            //lightingShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");

            //lightingShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");
            //toonShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader.frag");

            //CurrentShader = new ToonShader();

        }

        public void Load ()
        {
            glControl.MakeCurrent();

            GL.ClearColor(0.0f, 0.4f, 0.6f, 1.0f);
            GL.Enable(EnableCap.DepthTest);


            //// set values for Toon shader
            //toonTestShader.SetMatrix4("view", camera.GetViewMatrix());
            //toonTestShader.SetMatrix4("projection", projectionModel);
            //toonTestShader.SetVec3("lightPosition", lamp.Position);



            //toonShader.LoadNewFragmentShader("C:/UnityProjects/OpenGL-Skole/simpleShader2.frag");
            //toonShader = new("C:/UnityProjects/OpenGL-Skole/simpleShader.vs", "C:/UnityProjects/OpenGL-Skole/simpleShader2.frag");
            float w = glControl.ClientSize.Width;
            float h = glControl.ClientSize.Height;
            
            projectionModel = camera.GetProjectionMatrix(w, h);
            viewModel = camera.GetViewMatrix();

            CurrentShader = new PhongShader(ref viewModel, ref projectionModel, lamp, camera);



            // shader will be null here
            box1 = new BoxFigure(1, 1, 1, new CustomColor(1, 1, 0, 1), CurrentShader);
            box1 = new BoxFigure(1, 0.5f, 2, new CustomColor(1, 0, 0, 1), CurrentShader);

            LoadAllMeshes();
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
        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            CurrentShader.SetMatrix4("view", camera.GetViewMatrix());
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

        void ToggleRimLight(bool light);

        void SetToonShading(bool enabled);

        void SetColor(Vector3 color);
    }


}
