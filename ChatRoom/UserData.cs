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

        //public UserData()
        //{
        //    ID = Guid.NewGuid().ToString();
        //    clientThread = new Thread(Server.Data_IN);
        //    clientThread.Start(clientSocket);
        //    SendRegistrationPacket();
        //}

     
        //public void SendRegistrationPacket()
        //{
        //    Packet packet = new Packet(PacketType.Registration, "server");
        //    packet.GeneralData.Add(ID);
        //    clientSocket.Send(packet.ToBytes());
        //}
    }
}
