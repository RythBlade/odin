using System.Collections.Generic;
using System.Numerics;
using Telemetry.FrameData.Properties;

namespace Telemetry.FrameData
{
    public class RigidBody
    {
        public uint Id = 0;

        public Matrix4x4 WorldMatrix = new Matrix4x4();

        public Dictionary<string, IBaseProperty> Properties = new Dictionary<string, IBaseProperty>();
    }
}
