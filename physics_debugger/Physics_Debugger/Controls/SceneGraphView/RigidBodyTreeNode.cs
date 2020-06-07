using System.Windows.Forms;
using Telemetry.FrameData;

namespace physics_debugger.Controls.SceneGraphView
{
    public class RigidBodyTreeNode : TreeNode
    {
        public RigidBody BodyToDisplay { get; }

        public RigidBodyTreeNode(RigidBody bodyToDisplay)
        {
            BodyToDisplay = bodyToDisplay;

            Text = $"Rigid Body: {bodyToDisplay.Id}";
        }
    }
}
