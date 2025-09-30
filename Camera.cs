using Microsoft.VisualBasic.Devices;
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
using Vector2 = OpenTK.Mathematics.Vector2;
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

        private int width;
        private int height;



        public Vector3 GetPosition { get => cameraPosition;}
        public Vector3 SetPosition { get => cameraPosition; }

        public Vector3 GetDirection { get => cameraDirection;}
        Vector3 ICamera.SetPosition { set => cameraPosition = value; }

        public Camera (Vector3 position, int width, int height)
        {
            cameraDirection = Vector3.Normalize(cameraPosition - cameraDirection); // actually pointing in the opposite way of view dir
            this.cameraPosition = position;
            this.width = width;
            this.height = height;
        }


        public Matrix4 GetViewMatrix()
        {
            Matrix4 test = Matrix4.LookAt(cameraPosition, cameraPosition + front, Vector3.Normalize(up));
            return Matrix4.LookAt(cameraPosition, cameraPosition + front, Vector3.Normalize(up));
        }


        public void UpdateVectors ()
        {
            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }




        public Matrix4 GetProjectionMatrix ()
        {
            
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), width / height, 0.1f, 10000.0f);
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

        private bool firstMove = true;
        private Vector2 lastMove;
        private float cameraYaw = 0f;
        private float cameraPitch = 0f;
        private float cameraRoll = 0f;
        private float cameraRoationSensitivity = 1;

        public void UpdateCameraRotation(Vector2 cursorCoordinates)
        {

           if (firstMove)
            {
                lastMove = cursorCoordinates;
                firstMove = false;
            }

           else
            {
                float deltaX = cursorCoordinates.X - lastMove.X;
                float deltaY = cursorCoordinates.Y - lastMove.Y;
                lastMove = new Vector2(cursorCoordinates.X, cursorCoordinates.Y);

                cameraYaw += deltaX * 0.01F;
                if (cameraPitch > 89.0f)
                {
                    cameraPitch = 89.0f;
                }
                else if (cameraPitch < -89.0f)
                {
                    cameraPitch = -89.0f;
                }
                else
                {
                    cameraPitch -= deltaX * cameraRoationSensitivity;
                }

                front.X = (float)Math.Cos(MathHelper.DegreesToRadians(cameraPitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(cameraYaw));
                front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(cameraPitch));
                front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(cameraPitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(cameraYaw));
                front = Vector3.Normalize(front);
            }
        }

    }


    public interface ICamera
    {
        Vector3 GetPosition { get; }
        Vector3 SetPosition { set; }
        Vector3 GetDirection { get; }

        void UpdateCameraMovement(CameraMovement movement);
        void UpdateCameraRotation(Vector2 cursorCoordinates);
        Matrix4 GetProjectionMatrix();
        Matrix4 GetViewMatrix();

        public enum CameraMovement { UP, DOWN, LEFT, RIGHT };
    }


}
