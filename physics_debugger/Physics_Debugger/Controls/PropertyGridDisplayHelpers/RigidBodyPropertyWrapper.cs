using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using Telemetry.FrameData;
using Telemetry.FrameData.Properties;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class RigidBodyPropertyWrapper
    {
        [BrowsableAttribute(false)]
        public RigidBody BodyToWrap { get; }

        [BrowsableAttribute(false)]
        public Matrix4x4PropertyWrapper Matrix4x4Wrapper { get; }

        [BrowsableAttribute(false)]
        public ShapeListWrapper ShapeListWrapper { get; }

        [Category("Rigid Body")]
        public uint Id { get { return BodyToWrap.Id; } }

        [Category("Rigid Body")]
        public Matrix4x4PropertyWrapper WorldMatrix { get { return Matrix4x4Wrapper; } }

        [Category("Rigid Body")]
        public Vector4 Velocity { get { return BodyToWrap.Velocity; } }

        [Category("Rigid Body")]
        public Dictionary<string, IBaseProperty> Properties { get { return BodyToWrap.Properties; } }

        [Category("Rigid Body")]
        public ShapeListWrapper CollisionShapeIds { get { return ShapeListWrapper; } }

        public RigidBodyPropertyWrapper(RigidBody bodyToWrap)
        {
            BodyToWrap = bodyToWrap;
            Matrix4x4Wrapper = new Matrix4x4PropertyWrapper(BodyToWrap.WorldMatrix);
            ShapeListWrapper = new ShapeListWrapper(bodyToWrap.CollisionShapeIds);
        }
    }
}
