using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telemetry.FrameData;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.SceneGraphView
{
    public partial class SceneGraphView : UserControl
    {
        public FrameData FrameData { get; private set; } = null;

        private int frameIdToDisplay = 0;
        public int FrameIdToDisplay 
        { 
            get { return frameIdToDisplay; } 
            set 
            {
                if (FrameData != null)
                {
                    if (value < 0 || (value >= FrameData.Frames.Count && FrameData.Frames.Count != 0 ))
                    {
                        throw new ArgumentOutOfRangeException("FrameIdToDisplay", $"Tried to pass a frame ID \'{value}\' that was out of the range of frames. Frame Count '{FrameData.Frames.Count}\'.");
                    }

                    frameIdToDisplay = value;
                    DisplayNewFrame();
                }
                else
                {
                    frameIdToDisplay = 0;
                }
            } 
        }

        public SceneGraphView()
        {
            InitializeComponent();
        }

        private void DisplayNewFrame()
        {
            treeView.Nodes.Clear();
            treeView.Nodes.Add(BuildDisplayTree());
        }

        public void SetFrameData(FrameData frameData, int frameIdToDisplay)
        {
            FrameData = frameData;
            FrameIdToDisplay = frameIdToDisplay;

            DisplayNewFrame();
        }

        private TreeNode BuildDisplayTree()
        {
            TreeNode rootNode = null;

            if (FrameData != null && FrameData.Frames.Count > 0)
            {
                rootNode = new TreeNode($"Frame ID: {frameIdToDisplay}");
                TreeNode worldRoot = rootNode.Nodes.Add("World 1");

                {
                    //////////////////////////////////////////////////////////////////
                    // rigid body data
                    TreeNode rigidBodyRoot = worldRoot.Nodes.Add("Rigid Bodies");

                    foreach (RigidBody body in FrameData.Frames[frameIdToDisplay].RigidBodies.Values)
                    {
                        RigidBodyTreeNode bodyNode = new RigidBodyTreeNode(body);
                        rigidBodyRoot.Nodes.Add(bodyNode);

                        foreach (uint shapeId in body.CollisionShapeIds)
                        {
                            ShapeFrameIdPair actualShapePair = FrameData.ShapeData.RetrieveShapeForFrame(shapeId, FrameData.Frames[FrameIdToDisplay].FrameId);

                            ShapeTreeNode shapeNode = new ShapeTreeNode(actualShapePair.Shape);
                            bodyNode.Nodes.Add(shapeNode);
                        }
                    }
                }

                {
                    //////////////////////////////////////////////////////////////////
                    // Shape iteration data
                    TreeNode shapeIterationRoot = rootNode.Nodes.Add("Shape Iterations");

                    foreach (KeyValuePair<uint, ShapeIterations> iterationPair in FrameData.ShapeData.ShapeData)
                    {
                        TreeNode iterationNode = shapeIterationRoot.Nodes.Add($"Shape ID: {iterationPair.Key}");

                        foreach (ShapeFrameIdPair shapePair in iterationPair.Value.Iterations)
                        {
                            ShapeFrameIdPairTreeNode shapeIterationPairNode = new ShapeFrameIdPairTreeNode(shapePair);
                            iterationNode.Nodes.Add(shapeIterationPairNode);

                            ShapeTreeNode shapeNode = new ShapeTreeNode(shapePair.Shape);
                            shapeIterationPairNode.Nodes.Add(shapeNode);
                        }
                    }
                }
            }
            else
            {
                rootNode = new TreeNode("No data");
            }

            return rootNode;
        }

    }
}
