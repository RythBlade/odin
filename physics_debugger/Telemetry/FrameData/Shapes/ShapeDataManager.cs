using Telemetry.FrameData.Shapes;
using System.Collections.Generic;
using System;

namespace Telemetry.FrameData.Shapes
{
    public class ShapeFrameIdPair : IComparable<ShapeFrameIdPair>
    {
        public uint FrameId = 0;
        public BaseShape Shape = null;

        public ShapeFrameIdPair(uint frameId, BaseShape shape)
        {
            FrameId = frameId;
            Shape = shape;
        }

        public int CompareTo(ShapeFrameIdPair other)
        {
            return other.FrameId < FrameId ? -1 : other.FrameId > FrameId ? 1 : 0;
        }
    }

    public class ShapeIterations
    {
        // frame ID that generated the iteration
        public List<ShapeFrameIdPair> Iterations = new List<ShapeFrameIdPair>();
    }

    public class ShapeDataManager
    {
        // shape id, set of iterations
        public Dictionary<uint, ShapeIterations> ShapeData = new Dictionary<uint, ShapeIterations>();

        public void AddNewShape(uint frameId, BaseShape addedShape)
        {
            ShapeIterations iterations = new ShapeIterations();

            iterations.Iterations.Add(new ShapeFrameIdPair(frameId, addedShape));
            iterations.Iterations.Sort();

            ShapeData.Add(addedShape.Id, iterations);
        }

        public BaseShape RetrieveShapeForFrame(uint shapeId, uint frameId)
        {
            BaseShape shapeToReturn = null;

            ShapeIterations iterations = null;

            if (ShapeData.TryGetValue(shapeId, out iterations))
            {
                // find the most recent shape that was used for this frame id
                for (int i = 0; i < iterations.Iterations.Count; ++i)
                {
                    if (iterations.Iterations[i].FrameId == frameId)
                    {
                        shapeToReturn = iterations.Iterations[i].Shape;

                        break;
                    }
                    else if (iterations.Iterations[i].FrameId < frameId)
                    {
                        shapeToReturn = iterations.Iterations[i].Shape;
                    }
                    else
                    {
                        // the frame is now higher than the target frame id so we should've found it by now as the list is sorted!
                        break;
                    }
                }
            }

            return shapeToReturn;
        }
    }
}
