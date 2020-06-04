using ChaD.server.Models;
using System.Net.Sockets;

namespace ChaD.server.Services
{
    public class UserFactory
    {
        private static int _iterator;

        public static UserConnection Next(Socket socket)
        {
            return new UserConnection(socket)
            {
                Id = _iterator++,
                UserName = ""
            };
        }
    }
}