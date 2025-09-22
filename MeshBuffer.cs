using OpenGL.Shaders;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public class MeshBuffer: IHaveMesh
    {
        int vertexBufferObject;
        int vertexArrayobject;

        Shader shader;

        float[] vertices;

        public MeshBuffer(float[] vertices)
        {
            this.vertices = vertices;

        }

        public void RenderMesh ( )
        {


            GL.BindVertexArray(vertexArrayobject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
        }

        public void LoadMesh ()
        {
            MakeVBO();
            MakeVAO();

        }
           

        private void MakeVBO ()
        {
            // lav en VBO
            vertexBufferObject = GL.GenBuffer();
            // bind bufferens type til VBO'en
            // det her er lidt ligesom at sætte et stik i en server, så der er forbindelse mellem VBO og OpenGL
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            // send dataen for VBO til OpenGL med BufferData
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }


        public void MakeVAO ()
        {
            // opret en VAO = vertexArrayobject
            // denne beskriver hvilke floats i VBO'en der repræsentere hvad.
            vertexArrayobject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayobject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 12 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 7 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, 12 * sizeof(float), 10 * sizeof(float));
            GL.EnableVertexAttribArray(3);
        }

    }

    public interface IHaveMesh
    {
        public void LoadMesh();
        public void RenderMesh();
    }
}
