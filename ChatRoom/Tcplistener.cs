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
        //public static NetworkStream stream = server.AcceptTcpClient().GetStream();
        //int indexNumber;
        public static NetworkStream stream;
        public  void Main()
        {
            
            try
            {
                Console.WriteLine("being");
                Int32 port = 8002;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");              
                server = new TcpListener(localAddr, port);
                server.Start();
                Console.WriteLine("Waiting for a connection... ");
                
                Console.WriteLine("Complete?");
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
            
           //int  indexNumber = 0;
            
            while (true)
            {
                //stream = server.AcceptTcpClient().GetStream();
                while (server.Pending() == true)
                {
                    stream = server.AcceptTcpClient().GetStream();
                    
                    //server.AcceptTcpClient();
                    //TcpClient client = server.AcceptTcpClient();

                    //string MyDictKey = client.Client.RemoteEndPoint.ToString();
                    //clientDictionary.Add(MyDictKey, indexNumber);
                    var message = Task.Run(() => Messages());
                    Console.WriteLine("A User has joined your server!");
                    //message.Wait();
                    ////TcpClient client = server.AcceptTcpClient();

                    ////string MyDictKey = client.Client.RemoteEndPoint.ToString();
                    ////clientDictionary.Add(MyDictKey, indexNumber);
                    ////indexNumber++;

                }
            }
            
            //message.Wait();

        }
        public static void Questions()
        {
            Byte[] bytes = new Byte[256];
            string data = null;
            int i;
            i = server.AcceptTcpClient().GetStream().Read(bytes, 0,256);
            while (i != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                foreach (var indexNumber in clientDictionary)
                {
                    server.AcceptTcpClient().GetStream().Write(msg, 0, msg.Length);
                }
                server.AcceptTcpClient().GetStream().Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }
        public static void Messages()
        {
           Console.WriteLine("passed");
            for (;;)
            {
                try
                {
                    Byte[] bytes = new Byte[256];
                    string data = null;
                    Console.WriteLine("A user has joined");
                    int i;
                    while ((i = stream.Read(bytes, 0, 256)) != 1)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        foreach (var indexNumber in clientDictionary)
                        {
                            stream.Write(msg, 0, msg.Length);
                        }
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                    Console.WriteLine("Here?");
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("user has disconnected");
                }                
                Console.WriteLine("client close bit");
                server.AcceptTcpClient().Close();
            }
        }
    }
}