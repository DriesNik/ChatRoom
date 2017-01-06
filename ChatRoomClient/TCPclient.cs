using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace ChatRoomClient
{
    class TCPclients
    {
        TcpClient client;
        NetworkStream stream;
         public void Main()
        {
            TCPclients client = new TCPclients();
            var connect = Task.Run(() => Connect());
            connect.Wait();
        }
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
                     client = new TcpClient(server, port);
                
                    stream = client.GetStream();
                var User = Task.Run(() => GetMessage());
                //Thread tank = new Thread(GetMessage);
                //tank.Start();
                while (true)
                {
                    message = Console.ReadLine();
                    string output = (name + ": " + message);
                    Byte[] data = Encoding.ASCII.GetBytes(output);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("message sent");
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

            
        }
        public void GetMessage()
        {

            if (stream.CanRead)
            {
                try
                {
                    Byte[] bytes = new Byte[256];
                    string data = null;
                    int i;
                    while (stream.DataAvailable != true)
                    {
                        i = stream.Read(bytes, 0, bytes.Length);
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("{0}", data);
                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("Server Connection Lost");
                }
            }
            else
            {

            }
            //stream.Close(5);
            //client.Connect("127.0.0.1",8002);
            //client.GetStream();
        }
    }
}