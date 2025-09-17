using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Camera;
using static OpenGL.ICamera;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace OpenGL
{
    public class Camera : ICamera
    {
        public event System.Windows.Forms.KeyPressEventHandler? KeyPress;

        private Vector3 cameraPosition = Vector3.Zero;
        private Vector3 cameraDirection;
        private Vector3 cameraTarget;

        private Vector3 right = Vector3.UnitX;
        private Vector3 up = Vector3.UnitY;
        private Vector3 front = -Vector3.UnitZ;



        public Vector3 GetPosition { get => cameraPosition;}
        public Vector3 SetPosition { get => cameraPosition; }

        public Vector3 GetDirection { get => cameraDirection;}
        Vector3 ICamera.SetPosition { set => cameraPosition = value; }

        public Camera (Vector3 position)
        {
            cameraDirection = Vector3.Normalize(cameraPosition - cameraDirection); // actually pointing in the opposite way of view dir
            this.cameraPosition = position;

        }


        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(cameraPosition, cameraPosition + front, Vector3.Normalize(up));
        }


        public void UpdateVectors ()
        {
            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }




        public Matrix4 GetProjectionMatrix (float width, float height)
        {
            
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), width / height, 0.1f, 100.0f);
        }

 

        public void UpdateCameraMovement(ICamera.CameraMovement movement)
        {
            if (movement == CameraMovement.UP)
            {
                cameraPosition.Z += 1f;
            }

            if (movement == CameraMovement.DOWN)
            {
                cameraPosition.Z -= 1f;
            }

            if (movement == CameraMovement.LEFT)
            {
                cameraPosition.X += 1f;
            }

            if (movement == CameraMovement.RIGHT)
            {
                cameraPosition.X -= 1f;
            }
        }
    }


    public interface ICamera
    {
        Vector3 GetPosition { get; }
        Vector3 SetPosition { set; }
        Vector3 GetDirection { get; }

        void UpdateCameraMovement(CameraMovement movement);
        Matrix4 GetProjectionMatrix(float width, float height);
        Matrix4 GetViewMatrix();

        public enum CameraMovement { UP, DOWN, LEFT, RIGHT };
    }


}
