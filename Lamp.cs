using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    internal class Lamp : ILamp
    {
        Vector3 position;
        private  Vector3 color;
       

        public Lamp (Vector3 position)
        {
            this.position = position;
        }

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Color { get => color; set => color = value; }
    }



    public interface ILamp
    {
        Vector3 Position { get; set; }

        Vector3 Color { get; set; }
    }
}
