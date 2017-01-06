using System;
using System.Net.Sockets;
using System.Threading;


namespace ChatRoom
{
    public class UserData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string ID;

        
    }
}
