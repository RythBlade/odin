using Renderer;
using SharpDX;
using System;
using System.ComponentModel;
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

        private DataStream dataStream = new DataStream();

        private Particle[] testParticleBuffer = new Particle[10];

        public Main()
        {
            InitializeComponent();

            lastMousePosition = Control.MousePosition;

            int cubeMesh = mainViewport.Renderer.Meshes.AddCubeMesh();

            for(int i = 0; i < testParticleBuffer.Length; ++i)
            {
                testParticleBuffer[i] = new Particle();
                mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), cubeMesh));
            }

//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, -5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, -5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, -5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(-5.0f, 0.0f, 0.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 0.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 5.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 0.0f), cubeMesh));
//             mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(0.0f, 0.0f, 0.0f), cubeMesh));

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

            UpdateTelemetry();

            float time = clock.ElapsedMilliseconds / 1000.0f;

            for (int i = 0; i < mainViewport.Renderer.InstanceList.Count; ++i)
            {
                Matrix translationMatrix = Matrix.Translation(
                      testParticleBuffer[i].position.X
                    , testParticleBuffer[i].position.Y
                    , testParticleBuffer[i].position.Z);

                // Update world matrix
                mainViewport.Renderer.InstanceList[i].WorldMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * 0.7f) * translationMatrix;
            }
        }

        private void UpdateTelemetry()
        {
            if(dataStream.Connected)
            {
                Byte[] readData = new Byte[512];
                int numberOfReadBytes = 0;
                dataStream.ReadBytes(out readData, out numberOfReadBytes);
                //Console.WriteLine($"numberOfBytes: {numberOfReadBytes}");

                if(numberOfReadBytes > 0)
                {
                    Console.WriteLine($"bytes, {readData[0]}, {readData[1]}, {readData[2]}, {readData[3]}, {readData[4]}, {readData[5]}, {readData[6]}, {readData[7]}");

                    int byteIndex = 0;

                    int startBytes = BitConverter.ToInt32(readData, byteIndex); byteIndex += 4;

                    if(startBytes == 999999)
                    {
                        //Console.WriteLine("Found the start");
                    }

                    for (int i = 0; i < testParticleBuffer.Length; ++i)
                    {
                        testParticleBuffer[i].position.X = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                        testParticleBuffer[i].position.Y = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                        testParticleBuffer[i].position.Z = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                        testParticleBuffer[i].velocity.X = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                        testParticleBuffer[i].velocity.Y = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                        testParticleBuffer[i].velocity.Z = BitConverter.ToSingle(readData, byteIndex); byteIndex += 4;
                    }

                    int endBytes = BitConverter.ToInt32(readData, byteIndex); byteIndex += 4;

                    if (endBytes == -999999)
                    {
                        //Console.WriteLine("Found the end");
                    }
                }
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

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDialogue connectionDialogue = new ConnectionDialogue(dataStream.HostName, dataStream.Port);

            if (connectionDialogue.ShowDialog(this) == DialogResult.OK)
            {
                dataStream.Disconnect();

                dataStream.HostName = connectionDialogue.HostName;
                dataStream.Port = connectionDialogue.Port;

                dataStream.Connect();
            }
        }
    }
}
