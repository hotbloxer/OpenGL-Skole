using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OpenGL.Shaders
{
    public abstract class Shader
    {
        public int Handle;

        Dictionary<string, int>  uniformsInShader;
        private string vertexShaderPath;
        private string fragmentShaderPath;

 


        // use first since GL operations cannot happen in the constructor of the Form
 
        public Shader (string vertexShaderPath, string fragmentShaderPath)
        {
            this.vertexShaderPath = vertexShaderPath;
            this.fragmentShaderPath = fragmentShaderPath;
        }

        private int LoadFragmentShader ()
        {
            string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            GL.AttachShader(Handle, fragmentShader);

            return fragmentShader;
        }

        private int LoadVertexShader()
        {
            string vertexShaderSource = File.ReadAllText(vertexShaderPath);
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            GL.AttachShader(Handle, vertexShader);

            return vertexShader;
        }




        public void LoadNewFragmentShader(string fragmentShaderPath)
        {
            this.fragmentShaderPath = fragmentShaderPath;
            Init();
        }

        private void DetacthAndDelete (int handle, int shader)
        {
            GL.DetachShader(handle, shader);
            GL.DeleteShader(shader);
        }

        public void Init()
        {
            Handle = GL.CreateProgram();
            int vertexShader = LoadVertexShader();
            int fragmentShader = LoadFragmentShader();

            LinkProgram(Handle);

            DetacthAndDelete(Handle, vertexShader);
            DetacthAndDelete(Handle, fragmentShader);


            //// when attached, the shaders are no longer needed
            //GL.DetachShader(Handle, vertexShader);
            //GL.DetachShader(Handle, fragmentShader);
            //GL.DeleteShader(vertexShader);
            //GL.DeleteShader(fragmentShader);



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

        public void SetVec4(string name, Vector4 value)
        {
            GL.UseProgram(Handle);
            GL.Uniform4(uniformsInShader[name], ref value);
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


        
        public virtual void Use()
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

    public interface IShader
    {
        void Use();
    }
}
