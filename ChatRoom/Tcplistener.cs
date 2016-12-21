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
        static TcpListener server;
        //int indexNumber;
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
                Thread help = new Thread (ListenThread);
                help.Start();
                //Thread messaging = new Thread(Messages);
                //messaging.Start();

                for (;;)
                {
                    try
                    {
                        
                        Byte[] bytes = new Byte[256];
                        string data = null;
                        NetworkStream stream = server.AcceptTcpClient().GetStream();
                        Console.WriteLine("A user has joined");
                        int i;
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
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
                    }
                    catch (System.IO.IOException)
                    {
                        Console.WriteLine("user has disconnected");
                    }

                    //Messager();
                    Console.WriteLine("client close bit");
                    server.AcceptTcpClient().Close();
                }

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
           int  indexNumber = 0;
            while (true)
            {
                server.AcceptTcpClient();
                //TcpClient client = server.AcceptTcpClient();
                
                //string MyDictKey = client.Client.RemoteEndPoint.ToString();
                //clientDictionary.Add(MyDictKey, indexNumber);
                Console.WriteLine("A User has joined your server!");
                TcpClient client = server.AcceptTcpClient();

                string MyDictKey = client.Client.RemoteEndPoint.ToString();
                clientDictionary.Add(MyDictKey, indexNumber);
                indexNumber++;
            }
        }
           
        
        public static void Messages()
        {
            for (;;)
            {
                server.Start();
                try
                {
                    Byte[] bytes = new Byte[256];
                    string data = null;
                    NetworkStream stream = server.AcceptTcpClient().GetStream();
                    Console.WriteLine("A user has joined");
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
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
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("user has disconnected");
                }

                //Messager();
                Console.WriteLine("client close bit");
                server.AcceptTcpClient().Close();
            }
        }
        //public static void Messager()
        //{
        //    Byte[] bytes = new Byte[255];
        //    string data = null;
        //    try
        //    {
        //        NetworkStream stream = server.AcceptTcpClient().GetStream();
        //        Console.WriteLine("writing bit");
        //        int i;
        //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        //        {                   
        //            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
        //            Console.WriteLine("Received: {0}", data);

                    
        //            data = data.ToUpper();

        //            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    
        //            stream.Write(msg, 0, msg.Length);
        //            Console.WriteLine("Sent: {0}", data);
        //        }
        //    }
        //    catch (System.IO.IOException)
        //    {
        //        Console.WriteLine("user has disconnected");
        //    }
        //}
        
    }
}