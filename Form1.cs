
using OpenGL.Shaders;
using OpenTK.GLControl;
using OpenTK.Mathematics;
using System;


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

            bool BlinnEnabled = true;
            Blinn.Enabled = false;

            camera = new Camera(new Vector3(0.0f, 1.0f, 5.0f));
            lamp = new Lamp(new Vector3(0, 2f, -2));


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
            openGL.Render();
        }


        private void Load()
        {
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
            Phong.Enabled = false;
            Blinn.Enabled = true;

            StandardShader phongShader = new("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/lightingPhong.frag");

            //Shader phongShader = new("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/cellShaded.frag");

            openGL.ChangeLightingShader(phongShader);
        }

        private void Blinn_Click(object sender, EventArgs e)
        {
            Phong.Enabled = true;
            Blinn.Enabled = false;
            StandardShader blinnShader = new("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/lighting.frag");

            openGL.ChangeLightingShader(blinnShader);
        }

        private void ToonShader_Click(object sender, EventArgs e)
        {
            Phong.Enabled = false;
            Blinn.Enabled = true;


            StandardShader CellShading = new("C:/UnityProjects/OpenGL-Skole/shader.vs", "C:/UnityProjects/OpenGL-Skole/cellShaded.frag");

            openGL.ChangeLightingShader(CellShading);
        }

        private void RimLightEnabled(object sender, EventArgs e)
        {
            openGL.ToggleRimLight(RimLight.Checked);
        }

        private void ToonState(object sender, EventArgs e)
        {
            openGL.SetToonShading(checkBoxToon.Checked);
        }
    }
}
  
  