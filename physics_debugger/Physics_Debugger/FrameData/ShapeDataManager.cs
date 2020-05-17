using physics_debugger.FrameData.Shapes;
using System.Collections.Generic;

namespace physics_debugger.FrameData
{
    public class ShapeIterations
    {
        // frame ID that generated the iteration
        public Dictionary<int, BaseShape> Iterations = new Dictionary<int, BaseShape>();
    }

    public class ShapeDataManager
    {
        // shape id, set of iterations
        public Dictionary<int, ShapeIterations> ShapeData = new Dictionary<int, ShapeIterations>();
    }
}
