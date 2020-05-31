using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using ChaD.server.Interfaces;
using ChaD.server.Services;
using ChaD.shared.Services;

namespace ChaD.server.Models
{
    public class UserConnection : ConnectionService
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        private bool _isAuthenticated = false;

        public UserConnection(Socket socket) : base(socket)
        {

        }

        public override void MessageProcessor(string message)
        {
            if (message == "") return;
            // Command section
            if (message[0] == '/')
            {
                var commandArray = message.Split(" ");
                switch (commandArray[0])
                {
                    case "/sendto":
                        if (commandArray.Length < 3)
                            return;
                        Server.SendTo(Int32.Parse(commandArray[1]), message.Remove(0, "/sendto  ".Count() + commandArray[1].Length));
                        return;
                    case "/setname":
                        var oldName = UserName ?? "Unidentified";
                        UserName = commandArray[1];
                        Console.WriteLine($@"{DateTime.Now.ToLongTimeString()}: Username {oldName} changed to {UserName}");
                        return;
                    case "/bcast":
                        Server.Broadcast(message.Remove(0, "/bcast ".Length));
                        return;
                    case "/ls":
                        ListUsers();
                        return;
                    case "/exit":
                        Server.Users.Remove(this);
                        Console.WriteLine($@"{DateTime.Now.ToLongTimeString()}: User {UserName} disconnected by itself");
                        return;
                }
            }
        }

        private void ListUsers()
        {
            var userlist = Server.Users
                .Select(x => $"{x.Id}:{x.UserName}")
                .Aggregate((x,y) => $"{x};{y}");

            Send("USERS=" + userlist);
        }
    }
}