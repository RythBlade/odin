using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.FrameData.Shapes;

namespace physics_debugger
{
    public class ConvexHullRenderable : ConvexHullShape
    {
        public ConvexHullShape Shape { get; }
        public int RenderHandle { get; }

        public ConvexHullRenderable(ConvexHullShape shapeToContain, int renderHandle)
        {
            Id = shapeToContain.Id;
            ShapeType = shapeToContain.ShapeType;

            Shape = shapeToContain;
            RenderHandle = renderHandle;
        }
    }
}
