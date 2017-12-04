using Renderer;
using SharpDX;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;

namespace physics_debugger
{
    public partial class Main : Form
    {
        private static float s_cameraMoveSpeed = 0.1f;
        private static float s_cameraSpeedModifier = 3.0f;
        private static float s_cameraScrollSpeed = (MathUtil.Pi / 360.0f);

        private System.Drawing.Point lastMousePosition = new System.Drawing.Point(0, 0);
        private Stopwatch clock = new Stopwatch();

        public Main()
        {
            InitializeComponent();

            lastMousePosition = Control.MousePosition;

            int cubeMesh = mainViewport.Renderer.Meshes.AddCubeMesh();

            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, -5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, -5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, -5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 0.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 0.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 5.0f), cubeMesh));
            mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 0.0f), cubeMesh));

            clock.Start();
            updateTimer.Start();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            MainLoop();
        }

        private void MainLoop()
        {
            UpdateInput();

            float time = clock.ElapsedMilliseconds / 1000.0f;

            for (int i = 0; i < mainViewport.Renderer.InstanceList.Count; ++i)
            {
                Matrix translationMatrix = Matrix.Translation(
                    mainViewport.Renderer.InstanceList[i].WorldMatrix.Row4.X
                    , mainViewport.Renderer.InstanceList[i].WorldMatrix.Row4.Y
                    , mainViewport.Renderer.InstanceList[i].WorldMatrix.Row4.Z);

                // Update world matrix
                mainViewport.Renderer.InstanceList[i].WorldMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * 0.7f) * translationMatrix;
            }
        }

        private void UpdateInput()
        {
            System.Drawing.Point currentMousePosition = Control.MousePosition;
            System.Drawing.Point mouseDifference = new System.Drawing.Point(
                    currentMousePosition.X - lastMousePosition.X
                    , currentMousePosition.Y - lastMousePosition.Y);

            lastMousePosition = currentMousePosition;

            if (Control.MouseButtons == MouseButtons.Right)
            {
                mainViewport.Renderer.Camera.Pitch -= mouseDifference.Y * s_cameraScrollSpeed;
                mainViewport.Renderer.Camera.Yaw += mouseDifference.X * s_cameraScrollSpeed;
            }

            float modifier = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) ? s_cameraSpeedModifier : 1.0f;
            float cameraSpeed = s_cameraMoveSpeed * modifier;

            if (Keyboard.IsKeyDown(Key.W))
            {
                mainViewport.Renderer.Camera.MoveCameraLongitudinal(cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                mainViewport.Renderer.Camera.MoveCameraLongitudinal(-cameraSpeed);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                mainViewport.Renderer.Camera.MoveCameraLateral(-cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                mainViewport.Renderer.Camera.MoveCameraLateral(cameraSpeed);
            }

            if (Keyboard.IsKeyDown(Key.Q))
            {
                mainViewport.Renderer.Camera.MoveCameraUp(cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.Z))
            {
                mainViewport.Renderer.Camera.MoveCameraUp(-cameraSpeed);
            }
        }
    }
}
