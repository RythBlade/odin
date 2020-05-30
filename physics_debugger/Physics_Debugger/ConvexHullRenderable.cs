using Physics.Telemetry.Serialised;
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
            // todo - refactor these mesh based renderables into a proper render binding system - e.g. a shape is bound to a renderable thing
            // bit of a hack at the minute - this really needs to be returning the contained types, rather than copying them internally.
            Id = shapeToContain.Id;
            ShapeType = shapeToContain.ShapeType;
            HasLocalMatrix = shapeToContain.HasLocalMatrix;
            LocalMatrix = shapeToContain.LocalMatrix;

            Shape = shapeToContain;
            RenderHandle = renderHandle;

            Faces = shapeToContain.Faces;
            Vertices = shapeToContain.Vertices;
        }

        public ConvexHullShapePacket ExportToPacket()
        {
            return Shape.ExportToPacket();
        }

        public void ExportToPacket(ConvexHullShapePacket packet)
        {
            Shape.ExportToPacket(packet);
        }
    }
}
