using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Shaders
{
    internal class StandardShader : Shader
    {
        public StandardShader(string vertexShaderPath, string fragmentShaderPath) : base(vertexShaderPath, fragmentShaderPath)
        {
        }
    }
}
