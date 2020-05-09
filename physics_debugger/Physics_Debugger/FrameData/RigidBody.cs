using physics_debugger.FrameData.Properties;
using System.Collections.Generic;
using System.Numerics;

namespace physics_debugger.FrameData
{
    public class RigidBody
    {
        public uint Id = 0;

        public Matrix4x4 WorldMatrix = new Matrix4x4();

        public Dictionary<string, IBaseProperty> Properties = new Dictionary<string, IBaseProperty>();
    }
}
