
using OpenGL.Shaders;
using OpenTK.GLControl;
using OpenTK.Mathematics;
using System;
using Timer = System.Windows.Forms.Timer;


namespace OpenGL
{
    public partial class Form1 : Form
    {

        IUseOpenGL openGL;

        ICamera camera;
        ILamp lamp;


        public Form1()
        {
            InitializeComponent();

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = 360;

            camera = new Camera(new Vector3(0.0f, 1.0f, 5.0f));
            lamp = new Lamp(new Vector3(0, 2f, -2));
            lamp.Color = new Vector3(1, 1f, 1f);


            openGL = new OpenGLProcesses(glControl1, camera, lamp);

            this.KeyPreview = true; // Let the form receive key events
            this.KeyPress += new KeyPressEventHandler(Keypressed);
        }


        private void Keypressed(System.Object o, KeyPressEventArgs e)
        {
            // The keypressed method uses the KeyChar property to check 
            // whether the ENTER key is pressed. 

            if (e.KeyChar == 'w') camera.UpdateCameraMovement(ICamera.CameraMovement.UP);
            else if (e.KeyChar == 'a') camera.UpdateCameraMovement(ICamera.CameraMovement.LEFT);
            else if (e.KeyChar == 'd') camera.UpdateCameraMovement(ICamera.CameraMovement.RIGHT);
            if (e.KeyChar == 's') camera.UpdateCameraMovement(ICamera.CameraMovement.DOWN);

            openGL.Render();

            // If the ENTER key is pressed, the Handled property is set to true, 
            // to indicate the event is handled.
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }




        }




        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void Load()
        {

            // timer was Copilots idea
            Timer timer = new Timer();
            timer.Interval = 16; // ~60 FPS
            timer.Tick += (s, e) =>
            {
                Update();
                camera.UpdateCameraRotation(new Vector2(Cursor.Position.X, Cursor.Position.Y));

                openGL.Render();
            };


            timer.Start();

            openGL.Load();
        }




        private void glControl1_Load(object sender, EventArgs e)
        {
            Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openGL.Render();
        }


        private void glControl1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            openGL.Render();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            float progressValue = hScrollBar1.Value; // vinkel degree 0 - 360

            float xLocation = (float)Math.Cos(MathHelper.DegreesToRadians(progressValue));
            float yLocation = (float)Math.Sin(MathHelper.DegreesToRadians(progressValue));

            Vector3 newLocation = new(xLocation, lamp.Position.Y, yLocation);

            lamp.Position = newLocation;

            openGL.Render();
        }

        bool BlinnEnabled = true;

        private void Phong_Click(object sender, EventArgs e)
        {


        }

        private void Blinn_Click(object sender, EventArgs e)
        {


        }

        private void ToonShader_Click(object sender, EventArgs e)
        {




        }

        private void RimLightEnabled(object sender, EventArgs e)
        {

        }

        private void ToonState(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
  
  