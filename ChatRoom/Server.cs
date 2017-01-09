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
        public static Dictionary<string, Users> clientDictionary = new Dictionary<string, Users>();
        public static List<Users> users;
        public static TcpListener server;
        public  void Main()
        {
            try
            {
                users = new List<Users>();
                Int32 port = 8002;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");              
                server = new TcpListener(localAddr, port);
                server.Start();
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
            
            Console.WriteLine("\nHit enter to continue.");
            Console.Read();
        }
        static void AddingUserThread( Users info)
        {
            var q = Task.Run(() => clientDictionary.Add(info.GetName(), info));
            q.Wait();          
            info.StartUp();
            UserJoin();
        }
        static void ListenThread()
        {
            while (true)
            {
                while (server.Pending() == true)
                {
                    Console.WriteLine("A User has joined your server!");
                    
                    Users user = new Users(server.AcceptTcpClient().GetStream());
                    var q = Task.Run(() => AddingUserThread(/*user.GetName(),*/ user));
                    //user.StartUp();
                    //clientDictionary.Add(user.GetName(),user);

                    //users.Add(user);
                    //var q = Task.Run(() => user.Reading());

                }
            }
        }
        public static void Writing(string text)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"Logs.txt", true))
            {
                file.WriteLine(text);
            }
        }
        public static void UserJoin()
        {
            foreach (var users in clientDictionary.Values)
            {
                
                users.Update("A User has joined the channel.");
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
                    foreach (var users in clientDictionary.Values)
                    {
                        users.Update(words);
                    }
                }
                else
                {

                }
            }
            
        }
    }
}