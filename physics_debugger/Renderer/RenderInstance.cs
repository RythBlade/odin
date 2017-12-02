using SharpDX;

namespace Renderer
{
    public class RenderInstance
    {
        public Matrix WorldMatrix { get; set; }
        public int MeshId { get; set; }

        public RenderInstance(Matrix worldMatrix, int meshId)
        {
            WorldMatrix = worldMatrix;
            MeshId = meshId;
        }
    }
}
