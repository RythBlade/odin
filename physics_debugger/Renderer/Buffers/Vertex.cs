using SharpDX;

namespace Renderer.Buffers
{
    public struct Vertex
    {
        Vector4 m_position;
        Vector4 m_normal;

        public Vertex(Vector4 position, Vector4 normal )
        {
            m_position = position;
            m_normal = normal;
        }
    }
}
