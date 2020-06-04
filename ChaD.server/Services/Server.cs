using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using ChaD.server.Models;
using Serilog;
using Serilog.Core;

namespace ChaD.server.Services
{
    public class Server
    {
        public Server(string host, int port)
        {
            Log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Users = new List<UserConnection>();
            _host = host;
            _port = port;
            serverSocket = new Socket(IPAddress.Parse(host).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(host), port));
            serverSocket.Listen();
            IsWorking = true;
            Log.Information($"Server started at {host}:{port}.");
        }
        private int _port;
        private string _host;
        private Socket serverSocket;
        public static Logger Log;

        public bool IsWorking;
        public static List<UserConnection> Users { get; set; }

        public void Run()
        {
            while (IsWorking)
            {
                Socket socketHandler = serverSocket.Accept();
                Log.Information($@"Accepted connection from {socketHandler.RemoteEndPoint}");
                AddUser(socketHandler);
            }
        }

        public void AddUser(Socket socket)
        {
            int id = new Random().Next(0,999);
            var user = UserFactory.Next(socket);
            Log.Debug($"User {user.Id}:{user.UserName} connected");
            Users.Add(user);
        }

        public static void DeleteUser(UserConnection userConnection)
        {
            Users.Remove(userConnection);
            Broadcast($"/ud࣐{userConnection.Id}:{userConnection.UserName}");
        }

        public static void SendTo(int authorId, int userId, string message)
        {
            Users.FindAll(x => x.Id == userId).FirstOrDefault()?.Send($"/pm࣐{authorId}࣐{message}");
        }
        public static void Broadcast(string message)
        {
            Users.AsParallel().ForAll(x => x.Send(message));
        }
    }
}