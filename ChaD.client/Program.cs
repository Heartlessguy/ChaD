using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ChaD.shared.Services;

namespace ChaD.client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverSocket = new Socket(IPAddress.Parse("127.0.0.1").AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000 ));
            ConnectionService connService = new ConnectionService(serverSocket);

            var msg = "";
            while (msg != "/exit")
            {
                msg = Console.ReadLine();
                connService.Send(msg);
            }
        }


    }
}
