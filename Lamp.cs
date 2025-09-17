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
        Vector3 position ;

        public Lamp (Vector3 position)
        {
            this.position = position;
        }

        public Vector3 Position { get => position; set => position = value; }
    }



    public interface ILamp
    {
        Vector3 Position { get; set; }
    }
}
