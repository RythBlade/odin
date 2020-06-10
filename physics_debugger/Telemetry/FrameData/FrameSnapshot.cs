using Physics.Telemetry.Serialised;
using System.Collections.Generic;

namespace Telemetry.FrameData
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

        public void AddRigidBody(RigidBody body)
        {
            // todo: error handle - what if the rigid body already exists?
            RigidBodies.Add(body.Id, body);
        }

        public void AddRigidBody(RigidBodyPacket packet)
        {
            RigidBody body = new RigidBody();

            body.ImportFromPacket(packet);

            AddRigidBody(body);
        }

        public void ImportFromPacket(FrameSnapshotPacket packet)
        {
            if (packet != null)
            {
                FrameId = packet.FrameId;

                foreach(RigidBodyPacket rigidBodyPacket in packet.RigidBodies)
                {
                    AddRigidBody(rigidBodyPacket);
                }
            }
        }

        public FrameSnapshotPacket ExportToPacket()
        {
            FrameSnapshotPacket snapshot = new FrameSnapshotPacket(); ;

            ExportToPacket(snapshot);

            return snapshot;
        }

        public void ExportToPacket(FrameSnapshotPacket exportTarget)
        {
            if (exportTarget != null)
            {
                exportTarget.FrameId = FrameId;

                foreach (RigidBody rigidBody in RigidBodies.Values)
                {
                    exportTarget.RigidBodies.Add(rigidBody.ExportToPacket());
                }
            }
        }
    }
}
