using Physics.Telemetry.Serialised;
using System.Collections.Generic;
using System.Numerics;

namespace Telemetry.FrameData.Shapes
{
    public class ConvexHullShape : BaseShape
    {
        public class Face
        {
            public int[] Index = new int[] { 0, 0, 0 };

            public Face()
            {
            }

            public Face(int vert0, int vert1, int vert2)
            {
                Index[0] = vert0;
                Index[1] = vert1;
                Index[2] = vert2;
            }

            public Face(ConvexHullShapePacket.Types.Face face)
            {
                Index[0] = face.Vert0;
                Index[1] = face.Vert1;
                Index[2] = face.Vert2;
            }

            public ConvexHullShapePacket.Types.Face ExportToPacket()
            {
                ConvexHullShapePacket.Types.Face packet = new ConvexHullShapePacket.Types.Face();

                ExportToPacket(packet);

                return packet;
            }

            public void ExportToPacket(ConvexHullShapePacket.Types.Face packet)
            {
                if (packet != null)
                {
                    packet.Vert0 = Index[0];
                    packet.Vert1 = Index[1];
                    packet.Vert2 = Index[2];
                }
            }
        }

        public class Vertex
        {
            public Vector4 Point = new Vector4();
            public Vector4 Normal = new Vector4();

            public Vertex(Vector4 point, Vector4 normal)
            {
                Point = point;
                Normal = normal;
            }

            public Vertex(ConvexHullShapePacket.Types.Vertex vertex)
            {
                Point.X = vertex.Position.X;
                Point.Y = vertex.Position.Y;
                Point.Z = vertex.Position.Z;
                Point.W = 1.0f;

                Normal.X = vertex.Normal.X;
                Normal.Y = vertex.Normal.Y;
                Normal.Z = vertex.Normal.Z;
                Normal.W = 1.0f;
            }

            public ConvexHullShapePacket.Types.Vertex ExportToPacket()
            {
                ConvexHullShapePacket.Types.Vertex packet = new ConvexHullShapePacket.Types.Vertex();

                ExportToPacket(packet);

                return packet;
            }

            public void ExportToPacket(ConvexHullShapePacket.Types.Vertex packet)
            {
                if (packet != null)
                {
                    packet.Position = new Vector3Packet();
                    packet.Position.X = Point.X;
                    packet.Position.Y = Point.Y;
                    packet.Position.Z = Point.Z;

                    packet.Normal = new Vector3Packet();
                    packet.Normal.X = Normal.X;
                    packet.Normal.Y = Normal.Y;
                    packet.Normal.Z = Normal.Z;
                }
            }
        }

        public List<Vertex> Vertices = new List<Vertex>();
        public List<Face> Faces = new List<Face>();

        public ConvexHullShape()
        {
            ShapeType = ShapeType.eConvexHull;
        }

        public void ImportFromPacket(ConvexHullShapePacket packetConvexHullShape)
        {
            if (packetConvexHullShape != null)
            {
                base.ImportFromPacket(packetConvexHullShape.Base);

                foreach(ConvexHullShapePacket.Types.Face face in packetConvexHullShape.Faces)
                {
                    Faces.Add(new Face(face));
                }

                foreach(ConvexHullShapePacket.Types.Vertex vertex in packetConvexHullShape.Vertices)
                {
                    Vertices.Add(new Vertex(vertex));
                }
            }
        }

        public ConvexHullShapePacket ExportToPacket()
        {
            ConvexHullShapePacket packet = new ConvexHullShapePacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ConvexHullShapePacket packet)
        {
            if (packet != null)
            {
                packet.Base = base.ExportToPacket();

                foreach(Face face in Faces)
                {
                    packet.Faces.Add(face.ExportToPacket());
                }

                foreach(Vertex vertex in Vertices)
                {
                    packet.Vertices.Add(vertex.ExportToPacket());
                }
            }
        }
    }
}
