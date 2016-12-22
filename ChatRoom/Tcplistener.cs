using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


namespace ChatRoom
{
    class Server
    {        
        public static Queue<string> messageQueue = new Queue<string>();
        public static Dictionary<string, int> clientDictionary = new Dictionary<string, int>();
        public static TcpListener server;
        public static NetworkStream stream;
        public static byte[] message;
        public  void Main()
        {
            try
            {
                Int32 port = 8002;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");              
                server = new TcpListener(localAddr, port);
                server.Start();
                Console.WriteLine("Waiting for a connection... ");
                var t = Task.Run(() =>ListenThread());
                t.Wait();   
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                Console.WriteLine("finnaly bit");
                server.Stop();
            }
            Console.WriteLine("end server");
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
        static void ListenThread()
        {
            while (true)
            {
                while (server.Pending() == true)
                {
                    stream = server.AcceptTcpClient().GetStream();
                    var message = Task.Run(() => Messages());
                    Console.WriteLine("A User has joined your server!");
                }
            }
        }
        public static void Messages()
        {
            for (;;)
            {
                try
                {
                    Byte[] bytes = new Byte[256];
                    string data = null;
                    int i;                    
                    while ((i = stream.Read(bytes, 0, 256)) != 1)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        messageQueue.Enqueue(data);

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        message = msg;
                        stream.Write(message, 0, message.Length);

                        Console.WriteLine("Sent: {0}", data);
                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("User has disconnected");
                } 
                server.AcceptTcpClient().Close();
            }
        }
    }
}