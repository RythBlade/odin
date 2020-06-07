using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Telemetry.FrameData;
using Telemetry.FrameData.Properties;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class RigidBodyPropertyWrapper
    {
        [BrowsableAttribute(false)]
        public RigidBody BodyToWrap { get; }

        [Category("Rigid Body")]
        public uint Id { get { return BodyToWrap.Id; } }

        [Category("Rigid Body")]
        public Matrix4x4 WorldMatrix { get { return BodyToWrap.WorldMatrix; } }

        [Category("Rigid Body")]
        public Vector4 Velocity { get { return BodyToWrap.Velocity; } }

        [Category("Rigid Body")]
        public Dictionary<string, IBaseProperty> Properties { get { return BodyToWrap.Properties; } }

        [Category("Rigid Body")]
        public List<uint> CollisionShapeIds { get { return BodyToWrap.CollisionShapeIds; } }

        public RigidBodyPropertyWrapper(RigidBody bodyToWrap)
        {
            BodyToWrap = bodyToWrap;
        }
    }
}
