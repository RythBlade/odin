using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.FrameData.Shapes
{
    public class TetrahedronShape : BaseShape
    {
        public TetrahedronShape()
        {
            ShapeType = ShapeType.eTetrahedron;
        }

        public void CopyFromPacket(Physics.Telemetry.Serialised.TetrahedronShapePacket packetTetrahedronShape)
        {
            if (packetTetrahedronShape != null)
            {
                base.CopyFromPacket(packetTetrahedronShape.Base);
            }
        }
    }
}
