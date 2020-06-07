using System.ComponentModel;
using System.Numerics;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class BaseShapePropertyWrapper
    {
        [BrowsableAttribute(false)]
        public BaseShape BaseShape { get; }

        [Category("Base")]
        public uint Id { get { return BaseShape.Id; } }

        [Category("Base")]
        public Matrix4x4 LocalMatrix { get { return BaseShape.LocalMatrix; } }

        [Category("Base")]
        public ShapeType ShapeType { get { return BaseShape.ShapeType; } }

        public BaseShapePropertyWrapper(BaseShape baseShape)
        {
            BaseShape = baseShape;
        }
    }
}
