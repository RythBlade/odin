using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace physics_debugger
{
    public class DataStream : IDisposable
    {
        private TcpClient client = null;
        private NetworkStream stream = null;

        public string HostName { get; set; }
        public int Port { get; set; }
        public bool Connected { get { return stream != null; } }

        public DataStream()
        {
            HostName = "192.168.1.8";
            Port = 27015;
        }

        public void Connect()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                client = new TcpClient(HostName, Port);
                stream = client.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void ReadBytes(out Byte[] readBytes, out int readBytesLength)
        {
            if (stream == null)
            {
                readBytes = new Byte[1];
                readBytesLength = 0;
                return;
            }

            // read in packet sized chunks so we don't have to worry about re-constructing the packets ourselves
            readBytes = new Byte[248];
            readBytesLength = 0;

            try
            {
                if (stream.DataAvailable && stream.CanRead)
                {
                    // read to the last, whole, packet 
                    // in the final version we should be cache these packets somewhere as it's data we should save.
                    // For now - throw it away so we don't fall too far behind the simulation
                    while(stream.DataAvailable && client.Available >= readBytes.Length)
                    {
                        Console.WriteLine($"Amount of data: {client.Available}");

                        // Read the first batch of the TcpServer response bytes.
                        readBytesLength = stream.Read(readBytes, 0, readBytes.Length);
                    }
                }
                else
                {
                    Console.WriteLine("Data done");
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Disconnect()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream.Close();
            }

            stream = null;

            if (client != null)
            {
                client.Close();
            }

            client = null;
        }
    }
}
