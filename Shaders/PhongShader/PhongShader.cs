using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenGL.Shaders.PhongShader
{
    internal class PhongShader : Shader
    {
        private static readonly string vertexShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/PhongShader/PhongShaderCode.vs";
        private static readonly string fragmentShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/PhongShader/PhongShaderCode.frag";
        private readonly Matrix4 view;
        private readonly Matrix4 projection;
        private readonly OpenGL.ILamp lamp;

        public PhongShader(ref Matrix4 view, ref Matrix4 projection, ILamp lamp) : base(vertexShaderPath, fragmentShaderPath)
        {
            this.view = view;
            this.projection = projection;
            this.lamp = lamp;
        }


        override public void Use()
        {
            System.Diagnostics.Debug.WriteLine(view.ToString(), projection.ToString());

            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
            SetVec3("lightPosition", lamp.Position);
            SetVec4("lightColor", lamp.Color); 


            base.Use();
        }

    }
}
