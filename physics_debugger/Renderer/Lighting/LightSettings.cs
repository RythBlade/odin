using SharpDX;

namespace Renderer.Lighting
{
    public class LightSettings
    {
        private Vector4 lightDirection;
        public Vector4 LightDirection 
        {
            get { return lightDirection; }
            set
            {
                lightDirection = value;
                lightDirection.Normalize();
            }
        }

        public Vector4 LightColour { get; set; }
        public float AmbientLightStrength { get; set; }
        public float SpecularLightStrength { get; set; }

        public LightSettings()
        {
            AmbientLightStrength = 0.1f;
            SpecularLightStrength = 0.5f;
            LightColour = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
            LightDirection = new Vector4(1.0f, -1.0f, 1.0f, 0.0f);
        }
    }
}
