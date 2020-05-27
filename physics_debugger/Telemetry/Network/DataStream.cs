using Physics.Telemetry.Serialised;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Network
{
    public class DataStream
    {
        private Socket clientSocket = null;

        public string HostName { get; set; }
        public int Port { get; set; }

        public bool Connected
        {
            get
            {
                return clientSocket != null && clientSocket.Connected;
            }
        }

        public DataStream()
        {
            HostName = string.Empty;
            Port = 0;
        }

        public DataStream(string hostName, int port)
        {
            HostName = hostName;
            Port = port;
        }

        public bool Connect()
        {
            return Reconnect();
        }

        public bool Reconnect()
        {
            bool toReturn = false;

            if (!string.IsNullOrEmpty(HostName) && Port != 0)
            {
                if (clientSocket == null)
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

                if (clientSocket.Connected)
                {
                    clientSocket.Disconnect(true);
                }

                clientSocket.Connect(HostName, Port);

                toReturn = true;
            }

            return toReturn;
        }

        public bool Disconnect()
        {
            if (clientSocket.Connected)
            {
                clientSocket.Disconnect(false);
                clientSocket = null;
            }

            return true;
        }

        /*public BasePacketHeader1 ReceiveData()
        {
            BasePacketHeader basePacket = null;

            if (clientSocket != null && clientSocket.Connected)
            {
                basePacket = new BasePacketHeader();

                int numberOfReadBytes = clientSocket.Receive(basePacket.PacketBytes);

                if (numberOfReadBytes > 0)
                {
                    Console.WriteLine($"bytes, {basePacket.PacketBytes[0]}, {basePacket.PacketBytes[1]}, {basePacket.PacketBytes[2]}, {basePacket.PacketBytes[3]}, {basePacket.PacketBytes[4]}, {basePacket.PacketBytes[5]}, {basePacket.PacketBytes[6]}, {basePacket.PacketBytes[7]}");

                    int byteIndex = 0;

                    // -- packet layout --
                    // uint32 frame id
                    // uint32 packet type
                    // uint32 data size
                    // byte[] data
                    // -- end packet layout --

                    basePacket.FrameID = BitConverter.ToUInt32(basePacket.PacketBytes, byteIndex); byteIndex += 4;
                    basePacket.PacketType = BitConverter.ToUInt32(basePacket.PacketBytes, byteIndex); byteIndex += 4;
                    basePacket.DataSize = BitConverter.ToUInt32(basePacket.PacketBytes, byteIndex); byteIndex += 4;
                }
            }

            return basePacket;
        }*/

        public BasePacketHeader ReceiveData()
        {
            BasePacketHeader basePacket = null;

            if (clientSocket != null && clientSocket.Connected)
            {
                basePacket = new BasePacketHeader();

                int numberOfReadBytes = clientSocket.Receive(basePacket.PacketBytes);

                if (numberOfReadBytes > 0)
                {
                    Console.WriteLine($"bytes, {basePacket.PacketBytes[0]}, {basePacket.PacketBytes[1]}, {basePacket.PacketBytes[2]}, {basePacket.PacketBytes[3]}, {basePacket.PacketBytes[4]}, {basePacket.PacketBytes[5]}, {basePacket.PacketBytes[6]}, {basePacket.PacketBytes[7]}");

                    int byteIndex = 0;
                    int headerSize = BitConverter.ToInt32(basePacket.PacketBytes, byteIndex); byteIndex += 4;

                    basePacket.messageHeader = MessageHeader.Parser.ParseFrom(basePacket.PacketBytes, byteIndex, headerSize);
                    basePacket.startOfPacketData = headerSize + 4;
                }
            }

            return basePacket;
        }
    }
}
