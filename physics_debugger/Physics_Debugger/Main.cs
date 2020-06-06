using physics_debugger.FrameControl;
using Renderer;
using Renderer.Buffers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using Telemetry.FrameData;
using Telemetry.FrameData.Shapes;
using Telemetry.Network;

namespace physics_debugger
{
    public partial class Main : Form
    {
        private static float s_cameraMoveSpeed = 0.2f;
        private static float s_cameraSpeedModifier = 3.0f;
        private static float s_cameraScrollSpeed = (MathUtil.Pi / 360.0f);

        private System.Drawing.Point lastMousePosition = new System.Drawing.Point(0, 0);
        private Stopwatch clock = new Stopwatch();

        private Telemetry.Network.DataStream dataStream = new Telemetry.Network.DataStream("localhost", 27015);
        private Telemetry.Network.PacketTranslator translator = new Telemetry.Network.PacketTranslator();

        private TelemetryReceiver receiver = null;

        private FrameData frameData = new FrameData();
        private FrameController controller = new FrameController();

        private int CubeMeshId = 0;
        private int TetrahedronMeshId = 0;
        private int PlaneMeshId = 0;

        // shape/frame id pair, render mesh handle
        private Dictionary<ShapeFrameIdPair, int> shapeRenderMeshBindings = new Dictionary<ShapeFrameIdPair, int>();

        private uint selectedShapeId = uint.MaxValue;

        public Main()
        {
            InitializeComponent();

            receiver = new TelemetryReceiver(dataStream, translator);

            lastMousePosition = MousePosition;

            PlaneMeshId = mainViewport.Renderer.Meshes.AddPlane(10, 10, new Vector3(-10.0f, 0.0f, -10.0f), new Vector3(10.0f, 0.0f, 10.0f));
            CubeMeshId = mainViewport.Renderer.Meshes.AddCubeMesh();
            TetrahedronMeshId = mainViewport.Renderer.Meshes.AddTetrahedron();

            mainViewport.Renderer.Camera.CameraPosition = new Vector3(0.0f, 0.0f, 15.0f);

            RenderInstance instanceToRender = new RenderInstance(Matrix.Translation(0.0f, 0.0f, 0.0f), PlaneMeshId);
            instanceToRender.Fill = RenderInstance.FillMode.eWireFrame;
            instanceToRender.Material.ColourTint = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            mainViewport.Renderer.InstanceList.Add(instanceToRender);

            clock.Start();
            updateTimer.Start();

            controller.FrameChanged += Controller_FrameChanged;
            controller.MaxFrameChanged += Controller_MaxFrameChanged;
            controller.StateChanged += Controller_StateChanged;

            controller.State = PlayBackState.eStaticFrame;   
        }

        private void Controller_StateChanged(object sender, EventArgs e)
        {
            UpdateButtonText();
        }

        private void Controller_MaxFrameChanged(object sender, EventArgs e)
        {
            frameTrackBar.Maximum = controller.MaxFrameId;
            frameTrackBar.TickFrequency = controller.MaxFrameId;
        }

        private void Controller_FrameChanged(object sender, EventArgs e)
        {
            frameCounterTextBox.Text = controller.CurrentFrameId.ToString();
            frameTrackBar.Value = controller.CurrentFrameId;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            MainLoop();
        }

        private void MainLoop()
        {
            UpdateInput();

            UpdateTelemetry();

            controller.Update();

            RenderFrame(controller.CurrentFrameId);
        }
        
        private void UpdateInput()
        {
            System.Drawing.Point currentMousePosition = Control.MousePosition;
            System.Drawing.Point mouseDifference = new System.Drawing.Point(
                    currentMousePosition.X - lastMousePosition.X
                    , currentMousePosition.Y - lastMousePosition.Y);

            lastMousePosition = currentMousePosition;

            System.Drawing.Point pixelCooord = mainViewport.PointToClient(Control.MousePosition);
            if (pixelCooord.X >= 0 && pixelCooord.Y >= 0 && pixelCooord.X < mainViewport.Size.Width && pixelCooord.Y < mainViewport.Size.Height)
            {
                selectedShapeId = GraphicsDevice.Instance.PixelUserData[pixelCooord.X, pixelCooord.Y];
            }
            else
            {
                selectedShapeId = uint.MaxValue;
            }

            mainViewport.Renderer.SelectedInstance = selectedShapeId;

            if (MouseButtons == MouseButtons.Right)
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

        private void FramesAdded()
        {
            if (frameData != null && frameData.Frames != null && frameData.Frames.Count > 0)
            {
                controller.MaxFrameId = frameData.Frames.Count - 1;
            }
        }

        private int GenerateMeshForConvexHull(ConvexHullShape shape)
        {
            List<Vertex> vertices = new List<Vertex>(shape.Faces.Count * 3);
            
            foreach(ConvexHullShape.Face face in shape.Faces)
            {
                for (int i = 0; i < 3; ++i)
                {
                    vertices.Add(new Vertex(shape.Vertices[face.Index[i]].Point, shape.Vertices[face.Index[i]].Normal));
                }
            }
            
            return mainViewport.Renderer.Meshes.AddMesh(vertices);
        }

        private void UpdateTelemetry()
        {
            if (dataStream.Connected)
            {
                // lock the queues and flush their contents to our render data
                lock(receiver.LockObject)
                {
                    while(receiver.ReceivedFrameSnapshots.Count > 0)
                    {
                        frameData.Frames.Add(receiver.ReceivedFrameSnapshots.Dequeue());
                    }

                    while(receiver.ReceivedShapes.Count > 0)
                    {
                        PacketTranslator.CollectedFrameShapes collectedShapes = receiver.ReceivedShapes.Dequeue();

                        foreach (BaseShape addedShape in collectedShapes.Shapes)
                        {
                            switch (addedShape.ShapeType)
                            {
                                case ShapeType.eConvexHull:
                                    ConvexHullShape convexShape = (ConvexHullShape)addedShape;
                                    int shapeRenderHandle = GenerateMeshForConvexHull(convexShape);

                                    ShapeFrameIdPair pair = frameData.ShapeData.AddNewShape(collectedShapes.FrameId, convexShape);

                                    // store a binding for this mesh version
                                    shapeRenderMeshBindings.Add(pair, shapeRenderHandle);
                                    break;
                                case ShapeType.eObb:
                                case ShapeType.eSphere:
                                case ShapeType.eCone:
                                case ShapeType.eTetrahedron:
                                default:
                                    frameData.ShapeData.AddNewShape(collectedShapes.FrameId, addedShape);
                                    break;
                            }
                        }
                    }    
                }

                FramesAdded();
            }
        }

        bool animate = true;

        private void RenderFrame(int frameIndex)
        {
            float time = animate ? clock.ElapsedMilliseconds / 1000.0f : 0.0f;

            FrameSnapshot latestFrame = frameData.Frames != null && frameData.Frames.Count > 0 ? frameData.Frames[frameIndex] : null;

            if (latestFrame != null)
            {
                int nextRenderInstanceId = 1; // the first instance is the world reference plane

                foreach (RigidBody rigidBody in latestFrame.RigidBodies.Values)
                {
                    foreach(uint shapeId in rigidBody.CollisionShapeIds)
                    {
                        RenderInstance instanceToRender = null;

                        ShapeFrameIdPair actualShapePair = frameData.ShapeData.RetrieveShapeForFrame(shapeId, frameData.Frames[frameIndex].FrameId);

                        if (actualShapePair != null)
                        {
                            if (nextRenderInstanceId < mainViewport.Renderer.InstanceList.Count)
                            {
                                instanceToRender = mainViewport.Renderer.InstanceList[nextRenderInstanceId];
                            }
                            else
                            {
                                instanceToRender = new RenderInstance(Matrix.Translation(5.0f, 0.0f, 5.0f), TetrahedronMeshId);
                                mainViewport.Renderer.InstanceList.Add(instanceToRender);
                            }

                            // todo - do the rest of the shape types and properly setup the position of the shapes
                            switch (actualShapePair.Shape.ShapeType)
                            {
                                case ShapeType.eObb:
                                    instanceToRender.MeshId = CubeMeshId;
                                    break;
                                case ShapeType.eSphere:
                                    break;
                                case ShapeType.eCone:
                                    break;
                                case ShapeType.eConvexHull:
                                    instanceToRender.MeshId = shapeRenderMeshBindings[actualShapePair];
                                    break;
                                case ShapeType.eTetrahedron:
                                    instanceToRender.MeshId = TetrahedronMeshId;
                                    break;
                                default:
                                    break;
                            }

                            instanceToRender.UserDataValue = actualShapePair.Shape.Id;

                            Matrix rotationAnimation = Matrix.RotationX(time) * Matrix.RotationY(time * 2.0f) * Matrix.RotationZ(time * 0.7f);

                            Matrix translationMatrix = Matrix.Translation(
                                rigidBody.WorldMatrix.Translation.X
                                , rigidBody.WorldMatrix.Translation.Y
                                , rigidBody.WorldMatrix.Translation.Z);

                            if(actualShapePair.Shape.HasLocalMatrix)
                            {
                                Matrix localMatrix = Matrix.Translation(
                                    actualShapePair.Shape.LocalMatrix.Translation.X
                                    , actualShapePair.Shape.LocalMatrix.Translation.Y
                                    , actualShapePair.Shape.LocalMatrix.Translation.Z);

                                instanceToRender.WorldMatrix = localMatrix * rotationAnimation * translationMatrix;
                            }
                            else
                            {
                                instanceToRender.WorldMatrix = rotationAnimation * translationMatrix; 
                            }

                            if( actualShapePair.Shape.Id == selectedShapeId)
                            {
                                instanceToRender.Material.ColourTint = new Vector4(1.0f, 0.5f, 0.5f, 1.0f);
                            }
                            else
                            {
                                instanceToRender.Material.ColourTint = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                            }

                            ++nextRenderInstanceId;
                        }
                    }
                }

                int differenceInRenderInstances = mainViewport.Renderer.InstanceList.Count - nextRenderInstanceId;
                mainViewport.Renderer.InstanceList.RemoveRange(mainViewport.Renderer.InstanceList.Count - differenceInRenderInstances, differenceInRenderInstances);
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDialogue connectionDialogue = new ConnectionDialogue(dataStream.HostName, dataStream.Port);

            if (connectionDialogue.ShowDialog(this) == DialogResult.OK)
            {
                receiver.StopThread();

                // todo - needs a file save warning
                // clear the telemetry we currently have
                DisplayLoadedTelemetry(new FrameData());

                dataStream.HostName = connectionDialogue.HostName;
                dataStream.Port = connectionDialogue.Port;
                
                dataStream.Reconnect();

                receiver.StartReceiverThread();
                controller.State = PlayBackState.eLive;
            }
        }

        private void frameTrackBar_Scroll(object sender, EventArgs e)
        {
            controller.State = PlayBackState.eStaticFrame;
            controller.CurrentFrameId = frameTrackBar.Value;
        }


        private void goToFirstFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToFirstFrame();
        }

        private void previousFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToPreviousFrame();
        }

        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToNextFrame();
        }

        private void goToLastFrameButton_Click(object sender, EventArgs e)
        {
            controller.GoToLastFrame();
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

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            receiver.StopThread();

            dataStream.Disconnect();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DisplayLoadedTelemetry(FrameData readFrameData)
        {
            frameData = readFrameData;

            shapeRenderMeshBindings.Clear();

            foreach (ShapeIterations iterations in frameData.ShapeData.ShapeData.Values)
            {
                foreach (ShapeFrameIdPair pair in iterations.Iterations)
                {
                    if (pair.Shape.ShapeType == ShapeType.eConvexHull)
                    {
                        ConvexHullShape convexShape = (ConvexHullShape)pair.Shape;
                        int shapeRenderHandle = GenerateMeshForConvexHull(convexShape);

                        shapeRenderMeshBindings.Add(pair, shapeRenderHandle);
                    }
                    else
                    {
                        // all iterations of a shape are still the same shape
                        break;
                    }
                }
            }

            FramesAdded();
        }

        private void openTelemetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Physics Telemetry | *.ptm";
            openDialog.Title = "Open Telemetry";

            if( openDialog.ShowDialog() == DialogResult.OK)
            {
                DataSerialiser serialiser = new DataSerialiser();
                serialiser.Filename = openDialog.FileName;

                FrameData readFrameData = new FrameData();
                bool success = serialiser.OpenTelemetry(readFrameData);

                if( success)
                {
                    DisplayLoadedTelemetry(readFrameData);
                }
                else 
                {
                    string errorMessage = "Failed to properly read in the telemetry file. It could be corrupt. Would you like to try and use what data was successfully read?\n";
                    errorMessage += $"Potentially corrupt frames read: {readFrameData.Frames.Count}";

                    DialogResult result = MessageBox.Show(errorMessage, "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

                    if(result == DialogResult.Yes)
                    {
                        DisplayLoadedTelemetry(readFrameData);
                    }
                }
            }
        }

        private void saveTelemetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Physics Telemetry | *.ptm";
            saveDialog.Title = "Save Telemetry";            

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                DataSerialiser serialiser = new DataSerialiser();
                serialiser.Filename = saveDialog.FileName;

                serialiser.SaveTelemetry(frameData);
            }
        }
    }
}
