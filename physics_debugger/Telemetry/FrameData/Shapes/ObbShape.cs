using Physics.Telemetry.Serialised;
using System.Numerics;

namespace Telemetry.FrameData.Shapes
{
    public class ObbShape : BaseShape
    {
        public Vector4 HalfExtents = new Vector4(0.0f);

        public ObbShape()
        {
            ShapeType = ShapeType.eObb;
        }

        public void CopyFromPacket(ObbShapePacket packetObbShape)
        {
            if (packetObbShape != null)
            {
                base.CopyFromPacket(packetObbShape.Base);

                HalfExtents.X = packetObbShape.HalfExtents.X;
                HalfExtents.Y = packetObbShape.HalfExtents.Y;
                HalfExtents.Z = packetObbShape.HalfExtents.Z;
            }
        }

        public ObbShapePacket ExportToPacket()
        {
            ObbShapePacket packet = new ObbShapePacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ObbShapePacket packet)
        {
            if (packet != null)
            {
                packet.Base = base.ExportToPacket();

                packet.HalfExtents = new Vector3Packet();
                packet.HalfExtents.X = HalfExtents.X;
                packet.HalfExtents.Y = HalfExtents.Y;
                packet.HalfExtents.Z = HalfExtents.Z;
            }
        }
    }
}
