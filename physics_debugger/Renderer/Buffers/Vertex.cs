namespace Renderer.Buffers
{
    public struct Vertex
    {
        SharpDX.Vector4 m_position;
        SharpDX.Vector4 m_normal;

        public Vertex(SharpDX.Vector4 position, SharpDX.Vector4 normal )
        {
            m_position = position;
            m_normal = normal;
        }

        public Vertex(System.Numerics.Vector4 position, System.Numerics.Vector4 normal)
        {
            m_position = new SharpDX.Vector4(position.X, position.Y, position.Z, position.W);
            m_normal = new SharpDX.Vector4(normal.X, normal.Y, normal.Z, normal.W);
        }
    }
}
