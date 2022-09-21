using Gtk;
using System;
using SimpleChat.interfaces;
using System.Net;
using System.Net.Sockets;
using SimpleChat.communications;
using System.Threading;



namespace SimpleChat
{
    internal class Program
    {


        static Simple_Chat appli;
        public static void Main()
        {
         

            Connection.udp.BeginReceive(Connection.listenConnection, new object());

            Application.Init();
            appli = new Simple_Chat();
            Application.Run();
      


        }

        public static void AddMessage(Message message)
        {
            appli.AddMessage(message);
        }
    }
}

