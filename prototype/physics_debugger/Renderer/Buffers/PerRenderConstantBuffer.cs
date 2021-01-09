using SharpDX;

namespace Renderer.Buffers
{
    public struct PerRenderConstantBuffer
    {
        public Matrix ViewProject;
        public Vector4 ViewPosition;
        public Vector4 LightDirection;
        public Vector4 LightColour;
    }
}
