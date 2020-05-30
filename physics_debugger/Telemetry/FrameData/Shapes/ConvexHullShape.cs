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

            public Face(Physics.Telemetry.Serialised.ConvexHullShape.Types.Face face)
            {
                Index[0] = face.Vert0;
                Index[1] = face.Vert1;
                Index[2] = face.Vert2;
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

            public Vertex(Physics.Telemetry.Serialised.ConvexHullShape.Types.Vertex vertex)
            {
                Point.X = vertex.Position.X;
                Point.Y = vertex.Position.Y;
                Point.Z = vertex.Position.Z;

                Normal.X = vertex.Normal.X;
                Normal.Y = vertex.Normal.Y;
                Normal.Z = vertex.Normal.Z;
            }
        }

        public List<Vertex> Vertices = new List<Vertex>();
        public List<Face> Faces = new List<Face>();

        public ConvexHullShape()
        {
            ShapeType = ShapeType.eConvexHull;
        }

        public void CopyFromPacket(Physics.Telemetry.Serialised.ConvexHullShape packetConvexHullShape)
        {
            if (packetConvexHullShape != null)
            {
                base.CopyFromPacket(packetConvexHullShape.Base);

                foreach( Physics.Telemetry.Serialised.ConvexHullShape.Types.Face face in packetConvexHullShape.Faces)
                {
                    Faces.Add(new Face(face));
                }

                foreach(Physics.Telemetry.Serialised.ConvexHullShape.Types.Vertex vertex in packetConvexHullShape.Vertices)
                {
                    Vertices.Add(new Vertex(vertex));
                }
            }
        }
    }
}
