using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Main();
           

            Console.ReadKey();
        }
    }
}
