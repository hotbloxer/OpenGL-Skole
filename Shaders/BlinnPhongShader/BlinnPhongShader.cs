using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenGL.Shaders.PhongShader
{
    internal class BlinnPhongShader : Shader
    {
        private static readonly string vertexShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/PhongShader/PhongShaderCode.vs";
        private static readonly string fragmentShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/PhongShader/PhongShaderCode.frag";
        private readonly Matrix4 view;
        private readonly Matrix4 projection;
        private readonly OpenGL.ILamp lamp;
        private readonly OpenGL.ICamera camera;


        public BlinnPhongShader(ref Matrix4 view, ref Matrix4 projection, ILamp lamp, ICamera camera) : base(vertexShaderPath, fragmentShaderPath)
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

    }
}
