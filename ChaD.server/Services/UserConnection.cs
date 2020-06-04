using System;
using System.Linq;
using System.Net.Sockets;
using System.Timers;
using ChaD.server.Services;
using ChaD.shared.Services;
using Serilog;

namespace ChaD.server.Models
{
    public class UserConnection : Connection, IDisposable
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        private bool _isAuthorized;

        private Timer Timeout { get; set; }

        public UserConnection(Socket socket) : base(socket)
        {
            // Таймаут на 20 сек
            Timeout = new Timer(10000)
            {
                AutoReset = false,
                Enabled = true
            };
            Timeout.Elapsed += OnTimeoutEvent;
            _isAuthorized = false;
        }

        private void OnTimeoutEvent(Object source, ElapsedEventArgs e)
        {
            Timeout.Stop();
            Timeout.Close();
            Exit();
        }

        private readonly char[] _restrictedCharset = {';', ':', '/'};

        public override void MessageProcessor(string message)
        {
            // 5min timeout
            Timeout.Interval = 300000;
            if (message == "") return;
            // Command section
            if (message[0] == '/')
            {
                var commandArray = message.Split("࣐");
                var commandLength = commandArray.Length;
                switch (commandArray[0])
                {
                    case "/auth":
                        if (commandLength < 2)
                        {
                            Send("/error࣐Not enough arguments");
                            return;
                        }
                        Authorize(commandArray[1]);
                        return;

                    case "/sendto":
                        if (commandLength < 3)
                        {
                            Send("/error࣐Not enough arguments");
                            return;
                        }
                        SendTo(Int32.Parse(commandArray[1]), commandArray[2]);
                        return;

                    case "/setname":
                        if (commandLength < 2)
                        {
                            Send("/error࣐Not enough arguments");
                            return;
                        }
                        SetName(commandArray[1]);
                        return;

                    case "/bcast":
                        if (commandLength < 2)
                        {
                            Send("/error࣐Not enough arguments");
                            return;
                        }
                        Broadcast($"/bc࣐{commandArray[1]}");
                        return;

                    case "/ls":
                        ListUsers();
                        return;

                    case "/exit":
                        Exit();
                        return;
                    default:
                        Send("/error࣐Command not found");
                        return;
                }
            }
        }

        private void Authorize(string name)
        {
            if (!_isAuthorized)
            {
                try
                {
                    SetName(name);
                    _isAuthorized = true;
                    ListUsers();
                    Server.Log.Information($"User {UserName} now authorized");
                }
                catch (Exception e)
                {
                    Server.Log.Error($"User ID={Id} unauthorized. {e.Message}");
                }
            }
            else
            {
                Send("/error࣐Already authorized");
            }
        }
        private void SendTo(int recipientId, string message)
        {
            if (!_isAuthorized)
            {
                Server.Log.Error($"Send from ID={Id} is not allowed due user unauthorized");
                Send("/error࣐Unauthorized");
                return;
            }
            Server.Log.Information($@"{UserName} => {Server.Users
                .Where(x => x.Id == recipientId)
                .Select(x => x.UserName).First()}: {message}");
            Server.SendTo(this.Id, recipientId, message);
        }
        private void SetName(string newName)
        {
            // Проверка имени на запрещенные сиволы и уникальность.
            if (_restrictedCharset.Any(newName.Contains))
            {
                Send("/error࣐Some characters not allowed");
                throw new Exception("Some characters not allowed");
            }

            if (Server.Users.Select(x => x.UserName).Contains(newName))
            {
                Send($"/error࣐User {newName} already exists");
                throw new Exception($"User {newName} already exists");
            }

            var oldName = UserName ?? "Unidentified";
            UserName = newName;
            Server.Broadcast($"/rn࣐{Id}:{UserName}");
            Server.Log.Information($"Username {oldName} changed to {UserName}");

        }
        private void Broadcast(string message)
        {
            if (!_isAuthorized)
            {
                Server.Log.Error($"Bcast from ID={Id} is not allowed due user unauthorized");
                Send("/error࣐Unauthorized");
                return;
            }
            Server.Log.Information($@"{UserName} => BROADCAST: {message}");
            Server.Broadcast(message);
        }
        private void Exit()
        {
            _userSocket.Disconnect(false);
            Server.Log.Information($"User {UserName} disconnected");
            Server.DeleteUser(this);
        }
        private void ListUsers()
        {
            if (!_isAuthorized)
            {
                Server.Log.Error($"ListUsers from ID={Id} is not allowed due user unauthorized");
                Send("/error࣐Unauthorized");
                return;
            }
            Server.Log.Debug($"User listing request from {UserName}");
            Send(@$"/ul࣐{Server.Users
                .Select(x => $"{x.Id}:{x.UserName}")
                .Aggregate((x, y) => $"{x};{y}")}");
        }

        public void Dispose()
        {
            Timeout?.Dispose();
        }
    }
}