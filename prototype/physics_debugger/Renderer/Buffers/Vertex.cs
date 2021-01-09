namespace Renderer.Buffers
{
    public struct Vertex
    {
        public SharpDX.Vector4 Position;
        public SharpDX.Vector4 Normal;

        public Vertex(SharpDX.Vector4 position, SharpDX.Vector4 normal )
        {
            Position = position;
            Normal = normal;
        }

        public Vertex(System.Numerics.Vector4 position, System.Numerics.Vector4 normal)
        {
            Position = new SharpDX.Vector4(position.X, position.Y, position.Z, position.W);
            Normal = new SharpDX.Vector4(normal.X, normal.Y, normal.Z, normal.W);
        }

        public void SetNormal(SharpDX.Vector3 normal)
        {
            Normal.X = normal.X;
            Normal.Y = normal.Y;
            Normal.Z = normal.Z;
            Normal.W = 0.0f;
        }
    }
}
