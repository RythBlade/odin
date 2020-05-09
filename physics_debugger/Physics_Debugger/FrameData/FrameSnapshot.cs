using System.Collections.Generic;

namespace physics_debugger.FrameData
{
    public class FrameSnapshot
    {
        public int FrameId { get; set; }

        public Dictionary<uint, RigidBody> RigidBodies = new Dictionary<uint, RigidBody>();

        public FrameSnapshot()
        {
        }
    }
}
