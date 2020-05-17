using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Network
{
    enum PacketType : uint
    {
        eRigidBodyFrameUpdate
        , eShapeAdded
    }

    public class BasePacketHeader
    {
        public uint FrameID;
        public uint PacketType;
        public uint DataSize;

        public const int MaxPacketLength = 1024;
        public const int StartOfPacketData = 12; // 3 uints into the buffer

        public byte[] PacketBytes = new byte[MaxPacketLength];
    }
}
