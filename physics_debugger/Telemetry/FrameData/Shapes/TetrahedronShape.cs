using Physics.Telemetry.Serialised;

namespace Telemetry.FrameData.Shapes
{
    public class TetrahedronShape : BaseShape
    {
        public TetrahedronShape()
        {
            ShapeType = ShapeType.eTetrahedron;
        }

        public void CopyFromPacket(TetrahedronShapePacket packetTetrahedronShape)
        {
            if (packetTetrahedronShape != null)
            {
                base.CopyFromPacket(packetTetrahedronShape.Base);
            }
        }

        public TetrahedronShapePacket ExportToPacket()
        {
            TetrahedronShapePacket packet = new TetrahedronShapePacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(TetrahedronShapePacket packet)
        {
            if (packet != null)
            {
                packet.Base = base.ExportToPacket();
            }
        }
    }
}
