using SharpDX;

namespace Renderer.Cameras
{
    public interface ICamera
    {
        Matrix ViewMatrix { get; }
        Matrix ProjectionMatrix { get; }

        Vector3 CameraPosition { get; }

        float NearClipPlane { get; set; }
        float FarClipPlane { get; set; }
        float FieldOfView { get; set; }

        void SetMatrices();
    }
}
