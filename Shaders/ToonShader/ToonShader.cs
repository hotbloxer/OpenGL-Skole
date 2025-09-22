using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Shaders.ToonShader
{
    internal class ToonShader : Shader
    {
        private static readonly string vertexShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/ToonShader/ToonShader.vs";
        private static readonly string fragmentShaderPath = "C:/UnityProjects/OpenGL-Skole/Shaders/ToonShader/ToonShader.frag";
        private readonly Matrix4 view;
        private readonly Matrix4 projection;
        private readonly ILamp lamp;


        public ToonShader(ref Matrix4 view, ref Matrix4 projection, ILamp lamp) : base(vertexShaderPath, fragmentShaderPath)
        {
            this.view = view;
            this.projection = projection;
            this.lamp = lamp;


        }

        override public void Use()
        {

            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
            SetVec3("lightPosition", lamp.Position);

            base.Use();

        }


    }
}





