using OpenGL.primitives;
using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenGL
{
    public partial class Form1 : Form
    {

        IUseOpenGL openGL;

        ICamera camera;


        public Form1()
        {
            InitializeComponent();

            openGL = new OpenGLProcesses();

            camera = new Camera(new Vector3(0.0f, 2.0f, 5.0f));

            openGL.Initialize(glControl1, camera);

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


    }
}
  
  