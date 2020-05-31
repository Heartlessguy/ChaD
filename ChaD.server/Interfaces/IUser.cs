using System.Threading;

namespace ChaD.server.Interfaces
{
    public interface IUser
    {
        public void Listen();


        public void Send(byte[] buffer);


    }
}