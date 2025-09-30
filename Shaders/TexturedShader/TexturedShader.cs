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
        private readonly Matrix4 projection;
        private readonly OpenGL.ILamp lamp;
        private readonly OpenGL.ICamera camera;


        public TexturedShader(ILamp lamp, ref ICamera camera) : base(vertexShaderPath, fragmentShaderPath)
        {

            this.projection = projection;
            this.lamp = lamp;
            this.camera = camera;
        }


        override public void Use()
        {

      
            SetMatrix4("view", camera.GetViewMatrix());
            SetMatrix4("projection", camera.GetProjectionMatrix());
            SetVec3("lightPosition", lamp.Position);
            SetVec3("lightColor", lamp.Color);
            SetVec3("viewPosition", camera.GetPosition);

            base.Use();
        }


    }
}
