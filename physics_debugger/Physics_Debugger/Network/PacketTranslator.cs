using physics_debugger.FrameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace physics_debugger.Network
{
    public class PacketTranslator
    {
        public Dictionary<uint, Tuple<bool, FrameSnapshot>> ConstructedSnapehots = new Dictionary<uint, Tuple<bool, FrameSnapshot>>();

        public PacketTranslator()
        {
        }

        public FrameSnapshot FindOrCreateSnapshotForFrame(uint frameId)
        {
            FrameSnapshot snapshot = FindSnapshotForFrame(frameId);

            if (snapshot == null)
            {
                snapshot = new FrameSnapshot();
                ConstructedSnapehots.Add(frameId, new Tuple<bool, FrameSnapshot>(false, snapshot));
            }

            return snapshot;
        }

        public FrameSnapshot FindSnapshotForFrame(uint frameId)
        {
            FrameSnapshot snapshot = null;

            // todo: Error handling - if you do packets broken up into bits, they you'll need to keep a list of the bits you've recieved, so you know if you recieve a piece twice!
            Tuple<bool, FrameSnapshot> tuple = null;

            if (ConstructedSnapehots.TryGetValue( frameId, out tuple))
            {
                snapshot = tuple.Item2;
            }
            
            return snapshot;
        }

        public bool TranslatePacket(BasePacketHeader packet)
        {
            bool toReturn = false;

            if (packet != null)
            {
                FrameSnapshot snapshot = FindOrCreateSnapshotForFrame(packet.FrameID);

                switch(packet.PacketType)
                {
                    case (uint)PacketType.eRigidBodies:

                        // -- rigid body packet layout --
                        //      uint        number of rigid bodies
                        ///////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////      -- generic property description packet --
                        ///////////////////////////////////////////////////////////////////      uint                        number of properties
                        ///////////////////////////////////////////////////////////////////      array<uint, char, uint>     string length, property name, property id
                        ///////////////////////////////////////////////////////////////////      -- end generic property description packet --
                        ///////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////       --generic property packet --
                        ///////////////////////////////////////////////////////////////////      array<uint, uint, object>     property id, property type, property data
                        ///////////////////////////////////////////////////////////////////      -- end generic property packet --
                        //
                        //      -- rigid body specific property packet --
                        //      array<uint matrix4>         rigid bodoy Id, rigid body matrix
                        //      -- rigid body specific property packet --
                        // -- end rigid body packet layout --
                        int byteIndex = BasePacketHeader.StartOfPacketData;

                        uint numberOfRigidBodies = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;

                        for( uint i = 0; i < numberOfRigidBodies; ++i)
                        {
                            RigidBody body = new RigidBody();
                            body.Id = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;

                            body.WorldMatrix.M11 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M12 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M13 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M14 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M21 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M22 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M23 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M24 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M31 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M32 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M33 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M34 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M41 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M42 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M43 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                            body.WorldMatrix.M44 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;

                            // todo: error handle - what if the rigid body already exists?
                            snapshot.RigidBodies.Add(body.Id, body);
                        }

                        toReturn = true;
                        break;

                    default:
                        break;
                }
            }

            return toReturn;
        }
    }
}
