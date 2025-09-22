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
        private readonly Vector3 lightPosition;



        public ToonShader() : base(vertexShaderPath, fragmentShaderPath)
        {

        }

        override public void Use()
        {

            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
            SetVec3("lightPosition", lightPosition);

            base.Use();

        }


    }
}





