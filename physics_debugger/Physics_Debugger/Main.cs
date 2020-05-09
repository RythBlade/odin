using physics_debugger.FrameData;
using Renderer;
using SharpDX;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
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

        private Particle[] testParticleBuffer = new Particle[10];

        private Network.DataStream dataStream = new Network.DataStream("localhost", 27015);

        private Network.PacketTranslator translator = new Network.PacketTranslator();

        private FrameData.FrameData frameData = new FrameData.FrameData();

        private int CubeMeshId = 0;

        public Main()
        {
            InitializeComponent();

            lastMousePosition = Control.MousePosition;

            CubeMeshId = mainViewport.Renderer.Meshes.AddCubeMesh();

            for(int i = 0; i < testParticleBuffer.Length; ++i)
            {
                testParticleBuffer[i] = new Particle();
                mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), CubeMeshId));
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

            int frameIndexToRender = 0;

            if (frameData != null && frameData.Frames != null && frameData.Frames.Count > 0 )
            {
                frameIndexToRender = frameData.Frames.Count - 1;
            }

            RenderFrame(frameIndexToRender);
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

            if (Keyboard.IsKeyDown(Key.E))
            {
                mainViewport.Renderer.Camera.MoveCameraUp(cameraSpeed);
            }
            else if (Keyboard.IsKeyDown(Key.Q))
            {
                mainViewport.Renderer.Camera.MoveCameraUp(-cameraSpeed);
            }
        }

        private void UpdateTelemetry()
        {
            if (dataStream.Connected)
            {
                Network.BasePacketHeader basePacket = dataStream.ReceiveData();

                if (translator.TranslatePacket(basePacket))
                {
                    // todo: error - don't pull out packets that aren't complete
                    foreach (Tuple<bool, FrameSnapshot> snapshot in translator.ConstructedSnapehots.Values)
                    {
                        frameData.Frames.Add(snapshot.Item2);
                    }

                    translator.ConstructedSnapehots.Clear();
                }
                else
                {
                    Console.WriteLine($"Error: read unknown packet type: {basePacket.PacketType}");
                }
            }
        }

        private void RenderFrame(int frameIndex)
        {
            float time = clock.ElapsedMilliseconds / 1000.0f;

            FrameSnapshot latestFrame = frameData.Frames != null && frameData.Frames.Count > 0 ? frameData.Frames[frameIndex] : null;

            if (latestFrame != null)
            {
                int differenceInRenderInstances = mainViewport.Renderer.InstanceList.Count - latestFrame.RigidBodies.Count;

                if (differenceInRenderInstances > 0)
                {
                    mainViewport.Renderer.InstanceList.RemoveRange(0, differenceInRenderInstances);
                }
                else if (differenceInRenderInstances < 0)
                {
                    for (int i = 0; i < -differenceInRenderInstances; ++i)
                    {
                        mainViewport.Renderer.InstanceList.Add(new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), CubeMeshId));
                    }
                }

                int instance = 0;

                foreach (RigidBody rigidBody in latestFrame.RigidBodies.Values)
                {
                    Matrix translationMatrix = Matrix.Translation(
                        rigidBody.WorldMatrix.Translation.X
                        , rigidBody.WorldMatrix.Translation.Y
                        , rigidBody.WorldMatrix.Translation.Z);

                    mainViewport.Renderer.InstanceList[instance].WorldMatrix = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * 0.7f) * translationMatrix;
                    ++instance;
                }
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDialogue connectionDialogue = new ConnectionDialogue(dataStream.HostName, dataStream.Port);

            if (connectionDialogue.ShowDialog(this) == DialogResult.OK)
            {
                dataStream.HostName = connectionDialogue.HostName;
                dataStream.Port = connectionDialogue.Port;

                dataStream.Reconnect();                
            }
        }
    }
}
