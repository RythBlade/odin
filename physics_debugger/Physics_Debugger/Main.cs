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

        private Network.DataStream dataStream = new Network.DataStream("localhost", 27015);
        private Network.PacketTranslator translator = new Network.PacketTranslator();

        private FrameData.FrameData frameData = new FrameData.FrameData();
        private FrameController controller = new FrameController();

        private int CubeMeshId = 0;



        public Main()
        {
            InitializeComponent();

            lastMousePosition = Control.MousePosition;

            CubeMeshId = mainViewport.Renderer.Meshes.AddCubeMesh();

            clock.Start();
            updateTimer.Start();

            controller.State = PlayBackState.eStaticFrame;
            UpdateButtonText();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            MainLoop();
        }

        private void UpdateFrameController()
        {
            controller.Update();

            frameCounterTextBox.Text = controller.CurrentFrameId.ToString();

            frameTrackBar.Value = controller.CurrentFrameId;
        }

        private void MainLoop()
        {
            UpdateInput();

            UpdateTelemetry();

            UpdateFrameController();


            RenderFrame(controller.CurrentFrameId);
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

                FramesAdded();
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
            controller.CurrentFrameId = frameTrackBar.Value;
            UpdateButtonText();
        }


        private void goToFirstFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToFirstFrame();
            UpdateButtonText();
        }

        private void previousFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToPreviousFrame();
            UpdateButtonText();
        }

        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToNextFrame();
            UpdateButtonText();
        }

        private void goToLastFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToLastFrame();
            UpdateButtonText();
        }

        private void playBackwardsButton_Click(object sender, EventArgs e)
        {
            if( controller.State == PlayBackState.eBackwards)
            {
                controller.State = PlayBackState.eStaticFrame;
            }
            else
            {
                controller.State = PlayBackState.eBackwards;
            }

            UpdateButtonText();
        }

        private void playForwardsButton_Click(object sender, EventArgs e)
        {
            if (controller.State == PlayBackState.eForwards || controller.State == PlayBackState.eLive)
            {
                controller.State = PlayBackState.eStaticFrame;
            }
            else
            {
                controller.State = PlayBackState.eForwards;
            }

            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            switch (controller.State)
            {
                case PlayBackState.eStaticFrame:
                    playForwardsButton.Text = ">";
                    playBackwardsButton.Text = "<";
                    break;
                case PlayBackState.eLive:
                    playForwardsButton.Text = "P";
                    playBackwardsButton.Text = "<";
                    break;
                case PlayBackState.eForwards:
                    playForwardsButton.Text = "P";
                    playBackwardsButton.Text = "<";
                    break;
                case PlayBackState.eBackwards:
                    playForwardsButton.Text = ">";
                    playBackwardsButton.Text = "P";
                    break;
                default:
                    break;
            }
        }

        private void FramesAdded()
        {
            if (frameData != null && frameData.Frames != null && frameData.Frames.Count > 0)
            {
                frameTrackBar.Maximum = frameData.Frames.Count;
                frameTrackBar.TickFrequency = frameData.Frames.Count / 10;

                controller.MaxFrameId = frameData.Frames.Count - 1;
            }
        }
    }
}
