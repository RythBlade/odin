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
    }
}
