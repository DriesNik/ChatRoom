﻿using System;
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
            TCPclients duh = new TCPclients();
            duh.Main();
            Console.ReadKey();
        }
    }
}
