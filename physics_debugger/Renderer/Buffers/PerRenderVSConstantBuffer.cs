using SharpDX;

namespace Renderer.Buffers
{
    public struct PerRenderVSConstantBuffer
    {
        public Matrix viewProject;

        public uint selectedId;
        public uint padding1;
        public uint padding2;
        public uint padding3;
    }
}
