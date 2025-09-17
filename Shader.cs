using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OpenGL
{
    public class Shader
    {
        public int Handle;

        Dictionary<string, int>  uniformsInShader;

        public Shader (string vertexShaderPath, string fragmentShaderPath)
        {
            string vertexShaderSource = File.ReadAllText(vertexShaderPath);
            string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);

            

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            GL.CompileShader(vertexShader);
            GL.CompileShader(fragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);


            LinkProgram(Handle);


            // when attached, the shaders are no longer needed
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);



            // also activate uniforms in the shader automatically
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var antaluniforms);

            uniformsInShader = new Dictionary<string, int>();

            for (int i = 0; i < antaluniforms; i++)
            {
                string key = GL.GetActiveUniform(Handle, i, out _, out _);
                int location = GL.GetUniformLocation(Handle, key);

                uniformsInShader.Add(key, location);
            }
        }

        public void SetVec3 (string name, Vector3 value)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(uniformsInShader[name], ref value);
        }

        public void CompileShader (int shader)
        {
            // try to compile shader
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int) All.True) // Todo, spørg søren om den her all
            {
                
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }



        private static void LinkProgram(int program)
        {
            // We link the program
            GL.LinkProgram(program);  // todo spørg søren

            // Check for linking errors
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(uniformsInShader[name], true, ref data);
        }

    }
}
