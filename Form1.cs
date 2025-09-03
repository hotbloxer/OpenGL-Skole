using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenGL
{
    public partial class Form1 : Form
    {


        Shader shader;

        // We modify the vertex array to include four vertices for our rectangle.
        private readonly float[] vertices =
        {
             0.5f,  0.5f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, // top left
        };

        // Then, we create a new array: indices.
        // This array controls how the EBO will use those vertices to create triangles
        private readonly uint[] indices =
        {
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the top-right half of the triangle
            1, 2, 3  // Then the second will be the bottom-left half of the triangle
        };

        //int textureGL = 0;
        //float[] display = [1.0f, 1.0f, 1.0f, 1.0f];
        //int displayGL = 0;


        //int proGL = 0;

        //// Projection Matrix
        //float[] projection = [
        //   0.0f, 0.0f, 0.0f, 0.0f,
        //    0.0f, 0.0f, 0.0f, 0.0f,
        //    0.0f, 0.0f, 0.0f, 0.0f,
        //    0.0f, 0.0f, 0.0f, 0.0f
        //   ];

        //int modGL = 0; // Uniform Location


        //// Model View Matrix
        //private float[] modelView = {
        //1.0f, 0.0f, 0.0f, 0.0f,
        //0.0f, 1.0f, 0.0f, 0.0f,
        //0.0f, 0.0f, 1.0f, 0.0f,
        //0.0f, 0.0f,-1.2f, 1.0f
        //};




        private int vertexBufferObject;
        private int vertexArrayobject;


        public Form1()
        {
            InitializeComponent();



        }
        public void InitShaders()
        {


           
            shader.Use();

            //// Compile vertex & fragment shaders
            //int vertexShader = InitVertexShader();


            //int fragmentShader = InitFragmentShader();

            //// Link two shaders in a shader program
            //int program = InitShaderProgram(vertexShader, fragmentShader);



            // Create GPU buffers for geometry
            CreateGeometryBuffers(shader.Handle);
        }

        public void CreateGeometryBuffers(int program)
        {


            //// Create GPU buffer (VBO)
            //CreateVBO(program, vertices);

            //// uniform shader inform
            //angleGL = gl.getUniformLocation(program, 'Angle');
            //proGL = gl.getUniformLocation(program, 'Projection');
            //modGL = gl.getUniformLocation(program, 'ModelView');


            ////CreateTexture(program, 'images/tekstur.jpg')
            //CreateTexture(program, 'images/testtex.jpg')

            // Activate shader program

            //GL.UseProgram(program);



            ////update display options
            //gl.uniform4fv(displayGL, new Float32Array(display))

            // Display geometri on screen
            Render();
        }


        private void Render()
        {
            //GL.ClearColor(0.0f, 0.4f, 0.6f, 1.0f);

           


            GL.Clear(ClearBufferMask.ColorBufferBit); //| gl.DEPTH_BUFFER_BIT);

            shader.Use();

            GL.BindVertexArray(vertexArrayobject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, indices.Length);

            //const zoom  = document.getElementById('zoom').value;
            //modelView[14] = -zoom;

            //// perspective projection
            //const fov  = document.getElementById('fov').value;
            //const aspect = gl.canvas.width / gl.canvas.height;
            //Perspective(fov, aspect, 1.0, 2000.0, projection);

            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);




        }


        private void CreateVBO(int program, float[] vertices)
        {
            //int vbo = GL.CreateBuffers();
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // create position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // create color
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            //TODO

            //// Create shader attribute: Color
            //const o = 3 * Float32Array.BYTES_PER_ELEMENT;
            //let c = gl.getAttribLocation(program, 'Color');
            //gl.vertexAttribPointer(c, 3, gl.FLOAT, gl.FALSE, s, o);
            //gl.enableVertexAttribArray(c);

            //// create shader uv
            //const o2 = 6 * Float32Array.BYTES_PER_ELEMENT;
            //let u = gl.getAttribLocation(program, 'UV');
            //gl.vertexAttribPointer(u, 2, gl.FLOAT, gl.FALSE, s, o2);
            //gl.enableVertexAttribArray(u);

            //// create shader normals
            //const o3 = 8 * Float32Array.BYTES_PER_ELEMENT;
            //let n = gl.getAttribLocation(program, 'Normal');
            //gl.vertexAttribPointer(n, 3, gl.FLOAT, gl.FALSE, s, o3);
            //gl.enableVertexAttribArray(n);

        }




        private int InitShaderProgram(int vertexShader, int fragmentShader)
        {
            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            // Check if shaders were linked successfully
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
            return program;

        }

        private int InitVertexShader()
        {
            string vertexShaderSource = File.ReadAllText("C://Users//p-hou//source//repos//OpenGL//shader.vs");

            // oprete ny vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);

            // bind vertex shaderen med dens source
            GL.ShaderSource(vertexShader, vertexShaderSource);

            CompileShader(vertexShader);

            return vertexShader;
        }


        private int InitFragmentShader()
        {
            string fragmentShaderSource = File.ReadAllText("C://Users//p-hou//source//repos//OpenGL//shader.vs");

            // oprete ny fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            // bind fragment shaderen med dens source
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            CompileShader(fragmentShader);

            return fragmentShader;
        }


        private void CompileShader(int shader)
        {

            // check for compiler erros
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }






        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }



        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();


            // lav en VBO
            vertexBufferObject = GL.GenBuffer();

            // bind bufferens type til VBO'en
            // det her er lidt ligesom at sætte et stik i en server, så der er forbindelse mellem VBO og OpenGL
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);


            // send dataen for VBO til OpenGL med BufferData
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, vertices, BufferUsageHint.StaticDraw);



            // opret en VAO = vertexArrayobject
            // denne beskriver hvilke floats i VBO'en der repræsentere hvad.
            vertexArrayobject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayobject);

            GL.VertexAttribPointer(vertexArrayobject, 3, VertexAttribPointerType.Float, false, vertices.Length, 0);

            shader = new Shader("C://Users//p-hou//source//repos//OpenGL//shader.vs", "C://Users//p-hou//source//repos//OpenGL//shader.frag");

            shader.Use();

        }
    }
}
