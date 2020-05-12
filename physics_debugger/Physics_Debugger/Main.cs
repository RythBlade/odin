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

        private int m_frameToDisplay = 0;
        private bool m_playLiveData = true;

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

            if (m_playLiveData)
            {
                int frameIndexToRender = m_frameToDisplay;

                if (frameData != null && frameData.Frames != null && frameData.Frames.Count > 0)
                {
                    frameIndexToRender = frameData.Frames.Count - 1;
                }

                goToFrame(frameIndexToRender);
            }

            RenderFrame(m_frameToDisplay);
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

                frameTrackBar.Maximum = frameData.Frames.Count;
                frameTrackBar.TickFrequency = frameData.Frames.Count / 10;
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

        private void frameTrackBar_Scroll(object sender, EventArgs e)
        {
            goToFrame(frameTrackBar.Value);
        }

        void goToFrame(int frameIndex)
        {
            int firstFrame = 0;
            int lastFrame = frameData.Frames.Count - 1;

            // going to any other frame than the last one cancels tracking the live data
            m_playLiveData = false;

            if (frameIndex >= lastFrame)
            {
                frameIndex = lastFrame;

                // are we going to the last frame - resume playing live data
                m_playLiveData = true;
            }

            if (frameIndex <= firstFrame)
            {
                frameIndex = firstFrame;
            }
            
            m_frameToDisplay = frameIndex;

            frameCounterTextBox.Text = m_frameToDisplay.ToString();
            frameTrackBar.Value = m_frameToDisplay;
        }

        private void goToFirstFrameButton_Click(object sender, EventArgs e)
        {
            goToFrame(0);
        }

        private void previousFrameButton_Click(object sender, EventArgs e)
        {
            goToFrame(m_frameToDisplay - 1);
        }

        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            goToFrame(m_frameToDisplay + 1);
        }

        private void goToLastFrameButton_Click(object sender, EventArgs e)
        {
            goToFrame(frameData.Frames.Count - 1);
        }
    }
}
