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
        public static List<Users> users;
        public static TcpListener server;
        //public static System.IO.StreamWriter file
        public  void Main()
        {
            try
            {
                users = new List<Users>();
                Int32 port = 8002;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");              
                server = new TcpListener(localAddr, port);
                server.Start();
                //MakeLog();
                Console.WriteLine("Waiting for a connection. ");
                var listen = Task.Run(() => ListenThread());
                var messaging = Task.Run(() => Messages());
                listen.Wait();   
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                Console.WriteLine("The Server has closed.");
                server.Stop();
            }
            //Console.WriteLine("end server");
            Console.WriteLine("\nHit enter to continue.");
            Console.Read();
        }
        static void ListenThread()
        {
            while (true)
            {
                while (server.Pending() == true)
                {
                    Console.WriteLine("A User has joined your server!");
                    UserJoin();
                    Users user = new Users(server.AcceptTcpClient().GetStream());
                    users.Add(user);
                    var q = Task.Run(() => user.Reading());
                    
                }
            }
        }
        public static void Writing(string uno)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"Logs.txt", true))
            {
                file.WriteLine(uno);
            }
            
        }
        public static void UserJoin()
        {
            foreach (Users user in users)
            {
                user.Update("A User has joined the channel.");
            }
        }
        public static void Messages()
        {
            while (true)
            {
                if (messageQueue.Count > 0)
                {
                    string words = messageQueue.Dequeue();
                    Writing(words);
                    foreach (Users user in users)
                    {
                        user.Update(words);
                    }
                }
                else
                {

                }
            }
            
        }
    }
}