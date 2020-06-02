using SharpDX;

namespace Renderer.Buffers
{
    public struct PerObjectConstantbuffer
    {
        public Matrix worldMatrix;

        public uint objectId;
        public float padding1;
        public float padding2;
        public float padding3;
    }
}
