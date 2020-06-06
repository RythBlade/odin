using SharpDX;

namespace Renderer.Buffers
{
    public struct PerRenderConstantBuffer
    {
        public Matrix ViewProject;
        public Vector4 ViewPosition;
        public Vector4 LightDirection;
        public Vector4 LightColour;
        public float AmbientLightStrength;
        public float SpecularLightStrength;
        public float padding2;
        public float padding3;
    }
}
