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
        NetworkStream stream = null;

        public DataStream()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 27015;
                client = new TcpClient("localhost", port);

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
            readBytes = new Byte[256];
            readBytesLength = 0;

            try
            {
                if (stream.DataAvailable && stream.CanRead)
                {
                    // Read the first batch of the TcpServer response bytes.
                    readBytesLength = stream.Read(readBytes, 0, readBytes.Length);
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

        public void Dispose()
        {
            stream.Close();
            client.Close();
        }
    }
}
