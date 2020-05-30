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
    }
}
