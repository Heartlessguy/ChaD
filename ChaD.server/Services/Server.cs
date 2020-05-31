using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ChaD.server.Models;

namespace ChaD.server.Services
{
    public class Server
    {
        public Server(string host, int port)
        {
            Users = new List<UserConnection>();
            _host = host;
            _port = port;
            serverSocket = new Socket(IPAddress.Parse(host).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(host), port));
            serverSocket.Listen();
            IsWorking = true;
            Console.WriteLine($@"Server init at {host}:{port}.");
            }

        private int _port;
        private string _host;
        private Socket serverSocket;

        public bool IsWorking;
        public static List<UserConnection> Users { get; set; }

        public void Run()
        {
            int counter = 0;
            while (IsWorking)
            {
                Socket socketHandler = serverSocket.Accept();
                Console.WriteLine($@"Got connection from {socketHandler.RemoteEndPoint}");
                UserConnection userConnection = new UserConnection(socketHandler);
                userConnection.UserName = $@"User-{counter++}";
                userConnection.Id = counter;
                Users.Add(userConnection);
                Broadcast(Encoding.UTF8.GetBytes($@"{userConnection.UserName} connected"));
            }
        }

        public void AddUser(Socket socket)
        {
            Users.Add(new UserConnection(socket));
        }

        public void DeleteUser(UserConnection userConnection)
        {

        }

        public static void SendTo(int userId, string message)
        {
            Users.FindAll(x => x.Id == userId).FirstOrDefault()?.Send(message);
        }
        public static void Broadcast(byte[] message)
        {
            Users.AsParallel().ForAll(x => x.Send(message));
        }
        public static void Broadcast(string message)
        {
            foreach (var user in Users)
            {
                user.Send(message);
            }
            // Users.AsParallel().ForAll(x => x.Send(message));
        }
    }
}