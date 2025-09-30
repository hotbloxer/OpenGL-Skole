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

        protected Matrix4 modelView; 

        public Object() 
        {
            modelView = Matrix4.Identity;
            Position = Vector3.Zero;
            SetObject(this);
        }

        public void SetModelView(Matrix4 newView)
        {
            modelView = newView;    
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


