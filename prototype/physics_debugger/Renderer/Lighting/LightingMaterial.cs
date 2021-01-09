using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer.Lighting
{
    public class LightingMaterial
    {
        public float AmbientLightStrength { get; set; }
        public float DiffuseLightStrength { get; set; }
        public float SpecularLightStrength { get; set; }
        public float SpecularShininess { get; set; }
        public Vector4 ColourTint { get; set; }

        public LightingMaterial()
        {
            AmbientLightStrength = 0.1f;
            DiffuseLightStrength = 0.5f;
            SpecularLightStrength = 0.4f;
            SpecularShininess = 1.0f;

            ColourTint = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
