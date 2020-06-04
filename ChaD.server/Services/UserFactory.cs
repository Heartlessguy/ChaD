using System.Net.Sockets;
using ChaD.server.Models;

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