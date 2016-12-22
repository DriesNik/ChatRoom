using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatRoomClient
{
    public class TCPclient
    {
        NetworkStream stream;
        public void Connect()
        {
            string name;
            try
            {
                Console.WriteLine("What is your name?");
                name = Console.ReadLine();
                
                Console.WriteLine("Enter your message");
                
                    string server;                    
                    server = "127.0.0.1";
                    string message;
                    Int32 port = 8002;
                    TcpClient client = new TcpClient(server, port);
                
                    stream = client.GetStream();
                var t = Task.Run(() => GetMessage());
                while (true)
                {
                    message = Console.ReadLine();
                    string output = (name + ": " + message);
                    Byte[] data = Encoding.ASCII.GetBytes(output);
                    stream.Write(data, 0, data.Length);

                    //string responseData = String.Empty;
                    //Int32 bytes = stream.Read(data, 0, data.Length);
                    //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    //Console.WriteLine("{0}", responseData);
                }
                                      
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Disconnected");
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
        public void GetMessage()
        {
            Byte[] bytes = new Byte[256];
            string data = null;
            int i;
            while ((i = stream.Read(bytes, 0, 256)) != 1)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("{0}", data);                
           }
        }
    }
}