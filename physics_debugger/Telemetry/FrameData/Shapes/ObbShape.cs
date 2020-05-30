using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.FrameData.Shapes
{
    public class ObbShape : BaseShape
    {
        public Vector4 HalfExtents = new Vector4(0.0f);

        public ObbShape()
        {
            ShapeType = ShapeType.eObb;
        }

        public void CopyFromPacket(Physics.Telemetry.Serialised.ObbShapePacket packetObbShape)
        {
            if (packetObbShape != null)
            {
                base.CopyFromPacket(packetObbShape.Base);

                HalfExtents.X = packetObbShape.HalfExtents.X;
                HalfExtents.Y = packetObbShape.HalfExtents.Y;
                HalfExtents.Z = packetObbShape.HalfExtents.Z;
            }
        }
    }
}
