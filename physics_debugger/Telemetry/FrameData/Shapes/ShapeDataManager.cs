using Telemetry.FrameData.Shapes;
using System.Collections.Generic;

namespace Telemetry.FrameData.Shapes
{
    public class ShapeIterations
    {
        // frame ID that generated the iteration
        public Dictionary<uint, BaseShape> Iterations = new Dictionary<uint, BaseShape>();
    }

    public class ShapeDataManager
    {
        // shape id, set of iterations
        public Dictionary<uint, ShapeIterations> ShapeData = new Dictionary<uint, ShapeIterations>();

        public void AddNewShape(uint frameId, BaseShape addedShape)
        {
            ShapeIterations iterations = new ShapeIterations();

            iterations.Iterations.Add(frameId, addedShape);

            ShapeData.Add(addedShape.Id, iterations);
        }
    }
}
