using SharpDX;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;

namespace Renderer
{
    public partial class DirectXControl : UserControl
    {
        private static float s_cameraMoveSpeed = 0.1f;
        private static float s_cameraSpeedModifier = 3.0f;
        private static float s_cameraScrollSpeed = (MathUtil.Pi / 360.0f);
        
        private MainRenderer renderer = new MainRenderer();

        private System.Drawing.Point lastMousePosition = new System.Drawing.Point(0, 0);

        private Stopwatch clock = new Stopwatch();

        public DirectXControl()
        {
            InitializeComponent();

            clock.Start();

            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                lastMousePosition = Control.MousePosition;

                GraphicsDevice.Instance.Initialise(Handle, ClientRectangle);
                renderer.Initialise();
                timer.Start();

                Resize += DirectXControl_Resize;

                int cubeMesh = renderer.Meshes.AddCubeMesh();

                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 5.0f, 0.0f,  5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 5.0f, 0.0f, -5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f,  5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, -5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 0.0f, 0.0f, -5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f,  0.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 5.0f, 0.0f,  0.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 0.0f, 0.0f,  5.0f), cubeMesh));
                renderer.InstanceList.Add(new RenderInstance(Matrix.Translation( 0.0f, 0.0f,  0.0f), cubeMesh));
            }
        }
        
        private void DirectXControl_Resize(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                GraphicsDevice.Instance.ResizeRenderTarget(ClientSize.Width, ClientSize.Height);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                Update();

                renderer.Render();
            }
        }

        private void Update()
        {
            UpdateInput();

            float time = clock.ElapsedMilliseconds / 1000.0f;

            for (int i = 0; i < renderer.InstanceList.Count; ++i)
            {
                Matrix translationMatrix = Matrix.Translation(
                    renderer.InstanceList[i].WorldMatrix.Row4.X
                    , renderer.InstanceList[i].WorldMatrix.Row4.Y
                    , renderer.InstanceList[i].WorldMatrix.Row4.Z);

                // Update world matrix
                renderer.InstanceList[i].WorldMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * 0.7f) * translationMatrix;
            }
        }

        private void UpdateInput()
        {
            System.Drawing.Point currentMousePosition = Control.MousePosition;
            System.Drawing.Point mouseDifference = new System.Drawing.Point(
                    currentMousePosition.X - lastMousePosition.X
                    , currentMousePosition.Y - lastMousePosition.Y);

            lastMousePosition = currentMousePosition;
            //Console.WriteLine($"Mouse position. X: {currentMousePosition.X}  Y: {currentMousePosition.Y}");

            if (Control.MouseButtons == MouseButtons.Right)
            {
                Console.WriteLine($"Mouse difference. X: {mouseDifference.X}  Y: {mouseDifference.Y} Pitch: {mouseDifference.Y * s_cameraScrollSpeed} Yaw: {mouseDifference.X * s_cameraScrollSpeed}");

                renderer.Camera.Pitch -= mouseDifference.Y * s_cameraScrollSpeed;
                renderer.Camera.Yaw += mouseDifference.X * s_cameraScrollSpeed;
            }

            float modifier = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) ? s_cameraSpeedModifier : 1.0f;
            float cameraSpeed = s_cameraMoveSpeed * modifier;

            if (Keyboard.IsKeyDown(Key.W))
            {
                renderer.Camera.MoveCameraLongitudinal(cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                renderer.Camera.MoveCameraLongitudinal(-cameraSpeed);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                renderer.Camera.MoveCameraLateral(-cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                renderer.Camera.MoveCameraLateral(cameraSpeed);
            }

            if (Keyboard.IsKeyDown(Key.Q))
            {
                renderer.Camera.MoveCameraUp(cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.Z))
            {
                renderer.Camera.MoveCameraUp(-cameraSpeed);
            }
        }
    }
}
