using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public abstract class Object : IDisposable
    {
        public readonly static List<Object> Objects = new List<Object>();

        public readonly Vector3 Position;

        public readonly Matrix4 modelView; //TODO find ud af om den skal være her

        public Object() 
        {
            Position = Vector3.Zero;
            SetObject(this);
        }
        public Object(Vector3 position)
        {
            Position = position;
            SetObject(this);
        }


        private void SetObject(Object obj)
        {
            Objects.Add(obj);
        }

        private void RemoveObject(Object obj)
        {
            Objects.Remove(obj);
        }

        public void Dispose()
        {
            RemoveObject(this);
        }
    }

}


