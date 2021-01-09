using Physics.Telemetry.Serialised;
using System.Numerics;

namespace Telemetry.FrameData.Shapes
{
    public enum ShapeType : uint
    {
        eObb
        , eSphere
        , eCone
        , eConvexHull
        , eTetrahedron
    };

    public class BaseShape
    {
        public uint Id = 0;
        public bool HasLocalMatrix = false;
        public Matrix4x4 LocalMatrix = new Matrix4x4();
        public ShapeType ShapeType = ShapeType.eObb;

        public void ImportFromPacket(ShapeBasePacket packetBaseShape)
        {
            if (packetBaseShape != null)
            {
                Id = packetBaseShape.Id;
                HasLocalMatrix = packetBaseShape.HasLocalMatrix;

                switch (packetBaseShape.ShapeType)
                {
                    case ShapeTypePacket.Obb:
                        ShapeType = ShapeType.eObb;
                        break;
                    case ShapeTypePacket.Sphere:
                        ShapeType = ShapeType.eSphere;
                        break;
                    case ShapeTypePacket.Cone:
                        ShapeType = ShapeType.eCone;
                        break;
                    case ShapeTypePacket.ConvexHull:
                        ShapeType = ShapeType.eConvexHull;
                        break;
                    case ShapeTypePacket.Tetrahedron:
                        ShapeType = ShapeType.eTetrahedron;
                        break;
                    default:
                        // to do error handling
                        break;
                }

                LocalMatrix.M11 = packetBaseShape.LocalMatrix.M11;
                LocalMatrix.M12 = packetBaseShape.LocalMatrix.M12;
                LocalMatrix.M13 = packetBaseShape.LocalMatrix.M13;
                LocalMatrix.M14 = packetBaseShape.LocalMatrix.M14;

                LocalMatrix.M21 = packetBaseShape.LocalMatrix.M21;
                LocalMatrix.M22 = packetBaseShape.LocalMatrix.M22;
                LocalMatrix.M23 = packetBaseShape.LocalMatrix.M23;
                LocalMatrix.M24 = packetBaseShape.LocalMatrix.M24;

                LocalMatrix.M31 = packetBaseShape.LocalMatrix.M31;
                LocalMatrix.M32 = packetBaseShape.LocalMatrix.M32;
                LocalMatrix.M33 = packetBaseShape.LocalMatrix.M33;
                LocalMatrix.M34 = packetBaseShape.LocalMatrix.M34;

                LocalMatrix.M41 = packetBaseShape.LocalMatrix.M41;
                LocalMatrix.M42 = packetBaseShape.LocalMatrix.M42;
                LocalMatrix.M43 = packetBaseShape.LocalMatrix.M43;
                LocalMatrix.M44 = packetBaseShape.LocalMatrix.M44;
            }
        }

        public ShapeBasePacket ExportToPacket()
        {
            ShapeBasePacket packet = new ShapeBasePacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ShapeBasePacket packet)
        {
            if (packet != null)
            {
                packet.Id = Id;
                packet.HasLocalMatrix = HasLocalMatrix;

                switch (ShapeType)
                {
                    case ShapeType.eObb:
                        packet.ShapeType = ShapeTypePacket.Obb;
                        break;
                    case ShapeType.eSphere:
                        packet.ShapeType = ShapeTypePacket.Sphere;
                        break;
                    case ShapeType.eCone:
                        packet.ShapeType = ShapeTypePacket.Cone;
                        break;
                    case ShapeType.eConvexHull:
                        packet.ShapeType = ShapeTypePacket.ConvexHull;
                        break;
                    case ShapeType.eTetrahedron:
                        packet.ShapeType = ShapeTypePacket.Tetrahedron;
                        break;
                    default:
                        // to do error handling
                        break;
                }

                packet.LocalMatrix = new Matrix4x4Packet();
                packet.LocalMatrix.M11 = LocalMatrix.M11;
                packet.LocalMatrix.M12 = LocalMatrix.M12;
                packet.LocalMatrix.M13 = LocalMatrix.M13;
                packet.LocalMatrix.M14 = LocalMatrix.M14;

                packet.LocalMatrix.M21 = LocalMatrix.M21;
                packet.LocalMatrix.M22 = LocalMatrix.M22;
                packet.LocalMatrix.M23 = LocalMatrix.M23;
                packet.LocalMatrix.M24 = LocalMatrix.M24;

                packet.LocalMatrix.M31 = LocalMatrix.M31;
                packet.LocalMatrix.M32 = LocalMatrix.M32;
                packet.LocalMatrix.M33 = LocalMatrix.M33;
                packet.LocalMatrix.M34 = LocalMatrix.M34;

                packet.LocalMatrix.M41 = LocalMatrix.M41;
                packet.LocalMatrix.M42 = LocalMatrix.M42;
                packet.LocalMatrix.M43 = LocalMatrix.M43;
                packet.LocalMatrix.M44 = LocalMatrix.M44;
            }
        }
    }
}
