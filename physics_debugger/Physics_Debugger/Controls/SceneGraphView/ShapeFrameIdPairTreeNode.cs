using System.Windows.Forms;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.SceneGraphView
{
    public class ShapeFrameIdPairTreeNode : TreeNode
    {
        public ShapeFrameIdPair PairToDisplay { get; }

        public ShapeFrameIdPairTreeNode(ShapeFrameIdPair pairToDisplay)
        {
            PairToDisplay = pairToDisplay;

            Text = $"FrameId: {pairToDisplay.FrameId}";
        }
    }
}
