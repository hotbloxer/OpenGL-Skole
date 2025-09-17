using OpenTK.GLControl;

namespace OpenGL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            glControl1 = new GLControl();
            Phong = new Button();
            hScrollBar1 = new HScrollBar();
            Blinn = new Button();
            RimLight = new CheckBox();
            checkBoxToon = new CheckBox();
            colorDialog1 = new ColorDialog();
            textBoxRed = new TextBox();
            textBoxGreen = new TextBox();
            textBoxBlue = new TextBox();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(12, 12);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl1.SharedContext = null;
            glControl1.Size = new Size(545, 426);
            glControl1.TabIndex = 0;
            glControl1.Load += glControl1_Load;
            glControl1.Paint += glControl1_Paint;
            // 
            // Phong
            // 
            Phong.Location = new Point(586, 12);
            Phong.Name = "Phong";
            Phong.Size = new Size(112, 34);
            Phong.TabIndex = 2;
            Phong.Text = "Phong";
            Phong.UseVisualStyleBackColor = true;
            Phong.Click += Phong_Click;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(586, 399);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(210, 39);
            hScrollBar1.TabIndex = 4;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // Blinn
            // 
            Blinn.Location = new Point(734, 12);
            Blinn.Name = "Blinn";
            Blinn.Size = new Size(112, 34);
            Blinn.TabIndex = 5;
            Blinn.Text = "Blinn";
            Blinn.UseVisualStyleBackColor = true;
            Blinn.Click += Blinn_Click;
            // 
            // RimLight
            // 
            RimLight.AutoSize = true;
            RimLight.Location = new Point(586, 87);
            RimLight.Name = "RimLight";
            RimLight.Size = new Size(116, 29);
            RimLight.TabIndex = 7;
            RimLight.Text = "RimLight?";
            RimLight.UseVisualStyleBackColor = true;
            RimLight.CheckedChanged += RimLightEnabled;
            // 
            // checkBoxToon
            // 
            checkBoxToon.AutoSize = true;
            checkBoxToon.Location = new Point(584, 132);
            checkBoxToon.Name = "checkBoxToon";
            checkBoxToon.Size = new Size(130, 29);
            checkBoxToon.TabIndex = 8;
            checkBoxToon.Text = "TooShader?";
            checkBoxToon.UseVisualStyleBackColor = true;
            checkBoxToon.CheckedChanged += ToonState;
            // 
            // textBoxRed
            // 
            textBoxRed.Location = new Point(595, 347);
            textBoxRed.Name = "textBoxRed";
            textBoxRed.Size = new Size(66, 31);
            textBoxRed.TabIndex = 9;
            // 
            // textBoxGreen
            // 
            textBoxGreen.Location = new Point(667, 347);
            textBoxGreen.Name = "textBoxGreen";
            textBoxGreen.Size = new Size(63, 31);
            textBoxGreen.TabIndex = 10;
            // 
            // textBoxBlue
            // 
            textBoxBlue.Location = new Point(736, 347);
            textBoxBlue.Name = "textBoxBlue";
            textBoxBlue.Size = new Size(60, 31);
            textBoxBlue.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1077, 490);
            Controls.Add(textBoxBlue);
            Controls.Add(textBoxGreen);
            Controls.Add(textBoxRed);
            Controls.Add(checkBoxToon);
            Controls.Add(RimLight);
            Controls.Add(Blinn);
            Controls.Add(hScrollBar1);
            Controls.Add(Phong);
            Controls.Add(glControl1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            Paint += Form1_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenTK.GLControl.GLControl glControl1;
        private Button Phong;
        private HScrollBar hScrollBar1;
        private Button Blinn;
        private CheckBox RimLight;
        private CheckBox checkBoxToon;
        private ColorDialog colorDialog1;
        private TextBox textBoxRed;
        private TextBox textBoxGreen;
        private TextBox textBoxBlue;
    }
}
