using System.Collections.Generic;
using System.Numerics;

namespace Telemetry.FrameData.Shapes
{
    public class ConvexHullShape : BaseShape
    {
        public class Face
        {
            public int[] Index = new int[] { 0, 0, 0 };
        }

        public class Vertex
        {
            public Vector4 Point = new Vector4();
            public Vector4 Normal = new Vector4();
        }

        public List<Vertex> Vertices = new List<Vertex>();
        public List<Face> Faces = new List<Face>();
    }
}
