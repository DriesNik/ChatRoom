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
            BasicUsers Bill = new BasicUsers("Bill 89", "Online");
            Bill.Attatch(new Users("Tom"));
            Bill.Attatch(new Users("Keith"));
            Bill.StatusNow = "offline";
            Bill.StatusNow = "offline";
            Bill.StatusNow = "online";

            Console.ReadKey();
        }
    }
}
