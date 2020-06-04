using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms.VisualStyles;
using Chad.Client.WinForms;
using Chad.Client.WinForms.Models;

namespace ChaD.client
{
    public class Client
    {
        public Client(string address, int port, string name)
        {
           Interlocutors = new List<Interlocutor>();
           var serverSocket = new Socket(IPAddress.Parse(address).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
           serverSocket.Connect(new IPEndPoint(IPAddress.Parse(address), port));
           ServerConnection = new ServerConnection(serverSocket);
           MainForm._appendText("test");
           ServerConnection.Send($"/auth࣐{name}");
        }

        public static List<Interlocutor> Interlocutors = new List<Interlocutor>();

        public ServerConnection ServerConnection;


        
    }
}