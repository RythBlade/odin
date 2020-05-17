using System.Collections.Generic;

namespace physics_debugger.FrameData
{
    public class FrameItem
    {
        public int ObjectId = -1;
        public int FrameIdToUse = -1;
    }

    public class FrameSnapshot
    {
        public int FrameId { get; set; }

        public Dictionary<uint, RigidBody> RigidBodies = new Dictionary<uint, RigidBody>();

        public FrameSnapshot()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<FrameItem> Shapes = new List<FrameItem>();
    }
}
