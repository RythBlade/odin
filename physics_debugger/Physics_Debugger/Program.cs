using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace physics_debugger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

            //ConnectToServer();
        }

//         private static void ConnectToServer()
//         {
//             try
//             {
//                 // Create a TcpClient.
//                 // Note, for this client to work you need to have a TcpServer 
//                 // connected to the same address as specified by the server, port
//                 // combination.
//                 Int32 port = 27015;
//                 TcpClient client = new TcpClient("localhost", port);
// 
//                 // Get a client stream for reading and writing.
//                 //  Stream stream = client.GetStream();
// 
//                 NetworkStream stream = client.GetStream();
// 
//                 // Receive the TcpServer.response.
// 
//                 // Buffer to store the response bytes.
//                 Byte[] data = new Byte[256];
// 
//                 if (stream.CanRead)
//                 {
//                     // Read the first batch of the TcpServer response bytes.
//                     Int32 bytes = stream.Read(data, 0, data.Length);
//                 }
//                 // Close everything.
//                 stream.Close();
//                 client.Close();
//             }
//             catch (ArgumentNullException e)
//             {
//                 Console.WriteLine("ArgumentNullException: {0}", e);
//             }
//             catch (SocketException e)
//             {
//                 Console.WriteLine("SocketException: {0}", e);
//             }
// 
//             Console.WriteLine("\n Press Enter to continue...");
//             Console.Read();
//         }
    }
}