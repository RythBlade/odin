using SharpDX;

namespace Renderer.Buffers
{
    public struct PerObjectConstantbuffer
    {
        public Matrix worldMatrix;

        public uint userDataValue;
        public float padding1;
        public float padding2;
        public float padding3;

        public Vector4 ColourTint;
    }
}
