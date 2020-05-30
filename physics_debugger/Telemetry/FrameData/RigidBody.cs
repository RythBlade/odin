using Physics.Telemetry.Serialised;
using System.Collections.Generic;
using System.Numerics;
using Telemetry.FrameData.Properties;

namespace Telemetry.FrameData
{
    public class RigidBody
    {
        public uint Id = 0;

        public Matrix4x4 WorldMatrix = new Matrix4x4();
        public Vector4 Velocity = new Vector4();

        public Dictionary<string, IBaseProperty> Properties = new Dictionary<string, IBaseProperty>();
        public List<uint> CollisionShapeIds = new List<uint>();

        public void CopyFromPacket(RigidBodyPacket packetBody)
        {
            if(packetBody != null)
            {
                Id = packetBody.Id;
                WorldMatrix.M11 = packetBody.Position.M11;
                WorldMatrix.M12 = packetBody.Position.M12;
                WorldMatrix.M13 = packetBody.Position.M13;
                WorldMatrix.M14 = packetBody.Position.M14;

                WorldMatrix.M21 = packetBody.Position.M21;
                WorldMatrix.M22 = packetBody.Position.M22;
                WorldMatrix.M23 = packetBody.Position.M23;
                WorldMatrix.M24 = packetBody.Position.M24;

                WorldMatrix.M31 = packetBody.Position.M31;
                WorldMatrix.M32 = packetBody.Position.M32;
                WorldMatrix.M33 = packetBody.Position.M33;
                WorldMatrix.M34 = packetBody.Position.M34;

                WorldMatrix.M41 = packetBody.Position.M41;
                WorldMatrix.M42 = packetBody.Position.M42;
                WorldMatrix.M43 = packetBody.Position.M43;
                WorldMatrix.M44 = packetBody.Position.M44;

                Velocity.X = packetBody.Velocity.X;
                Velocity.Y = packetBody.Velocity.Y;
                Velocity.Z = packetBody.Velocity.Z;
                Velocity.W = packetBody.Velocity.W;

                foreach(uint id in packetBody.CollisionShapes)
                {
                    CollisionShapeIds.Add(id);
                }
            }
        }

        public RigidBodyPacket ExportToPacket()
        {
            RigidBodyPacket packet = new RigidBodyPacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(RigidBodyPacket packet)
        {
            if(packet != null)
            {
                packet.Id = Id;
                packet.Position = new Matrix4x4Packet();
                packet.Position.M11 = WorldMatrix.M11;
                packet.Position.M12 = WorldMatrix.M12;
                packet.Position.M13 = WorldMatrix.M13;
                packet.Position.M14 = WorldMatrix.M14;

                packet.Position.M21 = WorldMatrix.M21;
                packet.Position.M22 = WorldMatrix.M22;
                packet.Position.M23 = WorldMatrix.M23;
                packet.Position.M24 = WorldMatrix.M24;

                packet.Position.M31 = WorldMatrix.M31;
                packet.Position.M32 = WorldMatrix.M32;
                packet.Position.M33 = WorldMatrix.M33;
                packet.Position.M34 = WorldMatrix.M34;

                packet.Position.M41 = WorldMatrix.M41;
                packet.Position.M42 = WorldMatrix.M42;
                packet.Position.M43 = WorldMatrix.M43;
                packet.Position.M44 = WorldMatrix.M44;

                packet.Velocity = new Vector4Packet();
                packet.Velocity.X = Velocity.X;
                packet.Velocity.Y = Velocity.Y;
                packet.Velocity.Z = Velocity.Z;
                packet.Velocity.W = Velocity.W;

                foreach (uint id in CollisionShapeIds)
                {
                    packet.CollisionShapes.Add(id);
                }
            }
        }
    }
}
