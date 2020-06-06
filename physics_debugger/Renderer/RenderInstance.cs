using SharpDX;

namespace Renderer
{
    public class RenderInstance
    {
        public enum FillMode
        {
            eFill
            , eWireFrame
        }

        public Matrix WorldMatrix { get; set; }

        public int MeshId { get; set; }

        public FillMode Fill { get; set; }

        public uint UserDataValue { get; set; }

        public Vector4 ColourTint { get; set; }

        public RenderInstance(Matrix worldMatrix, int meshId)
        {
            WorldMatrix = worldMatrix;
            MeshId = meshId;

            ColourTint = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
