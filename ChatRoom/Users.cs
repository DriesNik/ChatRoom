using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ChatRoom
{
    class Users
    {
        private NetworkStream accept;
        public Users(NetworkStream accept)
        {
            this.accept = accept;
        }
        public void Update(string word)
        {
            try
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(word);
                accept.Write(msg, 0, msg.Length);
                string remove = System.Text.Encoding.ASCII.GetString(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", remove);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error");
            }
        }
        public void Reading()
        {
            Byte[] bytes = new Byte[256];
            string data = null;
            int i;
            try
            {
                while ((i = accept.Read(bytes, 0, bytes.Length)) != 1)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);
                    Server.messageQueue.Enqueue(data);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("A User has left");
            }
        }
    }
}