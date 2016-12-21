using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPclient client = new TCPclient();
            
                client.Connect();
            
            Console.ReadKey();

        }
    }
}
