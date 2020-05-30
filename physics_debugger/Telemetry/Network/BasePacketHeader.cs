using Physics.Telemetry.Serialised;
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
        public const int MaxPacketLength = 2048;

        public byte[] PacketBytes = new byte[MaxPacketLength];
        public int startOfPacketData = 0;

        public MessageHeader messageHeader;
    }
}
