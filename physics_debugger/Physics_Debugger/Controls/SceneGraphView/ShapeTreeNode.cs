using System.Windows.Forms;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.SceneGraphView
{
    public class ShapeTreeNode : TreeNode
    {
        public BaseShape ShapeToDisplay { get; }

        public ShapeTreeNode(BaseShape shapeToDisplay)
        {
            ShapeToDisplay = shapeToDisplay;

            uint shapeId = shapeToDisplay.Id;

            switch (shapeToDisplay.ShapeType)
            {
                case ShapeType.eObb:
                    Text = $"OBB: {shapeId}";
                    break;
                case ShapeType.eSphere:
                    Text = $"Sphere: {shapeId}";
                    break;
                case ShapeType.eCone:
                    Text = $"Cone: {shapeId}";
                    break;
                case ShapeType.eConvexHull:
                    Text = $"Convex hull: {shapeId}";
                    break;
                case ShapeType.eTetrahedron:
                    Text = $"Tetrahedron: {shapeId}";
                    break;
                default:
                    Text = $"Unkown shape type: {shapeId}";
                    break;
            }
        }
    }
}
