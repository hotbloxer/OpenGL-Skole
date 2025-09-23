using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenGL.Shaders.PhongShader
{
    internal class TexturedShader : Shader
    {
        private static readonly string vertexShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/TexturedShader/TexturedShaderCode.vs";
        private static readonly string fragmentShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/TexturedShader/TexturedShaderCode.frag";
        private readonly Matrix4 view;
        private readonly Matrix4 projection;
        private readonly OpenGL.ILamp lamp;
        private readonly OpenGL.ICamera camera;


        public TexturedShader(ref Matrix4 view, ref Matrix4 projection, ILamp lamp, ref ICamera camera) : base(vertexShaderPath, fragmentShaderPath)
        {
            this.view = view;
            this.projection = projection;
            this.lamp = lamp;
            this.camera = camera;
        }


        override public void Use()
        {
            System.Diagnostics.Debug.WriteLine(view.ToString(), projection.ToString());

            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
            SetVec3("lightPosition", lamp.Position);
            SetVec3("lightColor", lamp.Color);
            SetVec3("viewPosition", camera.GetPosition);



            base.Use();
        }

        public void Use( Matrix4 model)
        {
            Matrix4 transformMatrix;


            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
            SetVec3("lightPosition", lamp.Position);
            SetVec3("lightColor", lamp.Color);
            SetVec3("viewPosition", camera.GetPosition);

            //transformMatrix = projection * view * model;
            transformMatrix = model * view * projection;
            Vector4 test = transformMatrix * new Vector4(0.5f, 0.5f, 0.5f, 1);


            base.Use();
        }


    }
}
