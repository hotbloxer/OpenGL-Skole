using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace OpenGL
{
    internal class Camera
    {
        public event System.Windows.Forms.KeyPressEventHandler? KeyPress;

        private Vector3 cameraPosition = Vector3.Zero;
        private Vector3 cameraDirection;
        private Vector3 cameraTarget;

        private Vector3 right = Vector3.UnitX;
        private Vector3 up = Vector3.UnitY;
        private Vector3 front = -Vector3.UnitZ;




        public Vector3 GetPosition { get => cameraPosition;}
        public Vector3 SetPosition { set => cameraPosition = value; }
        public Vector3 GetDirection { get => cameraDirection;}

        public Camera (Vector3 position)
        {
            cameraDirection = Vector3.Normalize(cameraPosition - cameraDirection); // actually pointing in the opposite way of view dir
            this.cameraPosition = position;

        }


        public Matrix4 GetViewMatrix()
        {
            Matrix4 test1 = Matrix4.CreateTranslation(cameraPosition);
            Matrix4 test2 =
            Matrix4.LookAt(cameraPosition, cameraPosition + front, up);
            return test1; //TODO find ud af dette
        }


        public void UpdateVectors ()
        {
            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

        public void UpdateMovement(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                cameraPosition.Z += 1f;
            }
            
        }


        public Matrix4 GetProjectionMatrix (float width, float height)
        {
            
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), width / height, 0.1f, 100.0f);
        }


    }



}
