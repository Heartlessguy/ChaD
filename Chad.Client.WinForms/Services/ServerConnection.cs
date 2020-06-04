using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Chad.Client.WinForms;
using Chad.Client.WinForms.Models;
using ChaD.shared.Services;

namespace ChaD.client
{
    public class ServerConnection : Connection
    {
        public ServerConnection(Socket socket) : base(socket)
        {
        }

        public override void MessageProcessor(string message)
        {
            if (message == "") return;
            if (message[0] == '/')
            {
                var commandArray = message.Split("࣐");
                switch (commandArray[0])
                {
                    case "/error":
                        MainForm._appendText($"ERROR: {commandArray[1]}");
                        return;

                    // any user added
                    case "/rn":
                        if (Client.Interlocutors.FindAll(x => x.Id == int.Parse(commandArray[1].Split(":")[0])).Count != 0) return;
                        Client.Interlocutors.Add(new Interlocutor()
                        {
                            Id = int.Parse(commandArray[1].Split(":")[0]),
                            Name = commandArray[1].Split(":")[1]
                        });
                        MainForm._DrawUserList();
                        // MainForm._appendText($"USER: {commandArray[1]} added/renamed");
                        return;

                    // user list response
                    case "/ul":
                        var users = commandArray[1]
                            .Split(";")
                            .Select(x => x.Split(":"))
                            .Select(x => new KeyValuePair<int, string>(int.Parse(x[0]), x[1]))
                            .ToDictionary(x => x.Key, x => x.Value);
                        foreach (var user in users.TakeWhile(user => Client.Interlocutors.FindAll(x => x.Id == user.Key).Count == 0))
                        {
                            Client.Interlocutors.Add(new Interlocutor()
                            {
                                Id = user.Key, 
                                Name = user.Value
                            });
                        }
                        MainForm._DrawUserList();
                        return;

                    // any user deleted
                    case "/ud":
                        Client.Interlocutors.RemoveAll(x => x.Id == int.Parse(commandArray[1].Split(":")[0]));
                        MainForm._DrawUserList();
                        // MainForm._appendText($"USER: {commandArray[1]} Disconnected");
                        return;

                    // private message
                    case "/pm":
                        Client.Interlocutors
                            .First(x => x.Id == int.Parse(commandArray[1])).MessageHistory.Add(new MessageRecord()
                            {
                                IsIncoming = true,
                                Message = commandArray[2]
                            });
                        MainForm._appendText($" {Client.Interlocutors.First(x => x.Id == int.Parse(commandArray[1])).Name}: {commandArray[2]}");
                        return;
                    default:
                        Send("/error࣐Command not found");
                        return;
                }
            }
        }


        public void SendTo(int recipientId, string message)
        {
            Send($"/sendto࣐{recipientId}࣐{message}");
        }
        public void Disconnect()
        {
            Send("/exit");
            _userSocket.Disconnect(false);
        }
    }
}
