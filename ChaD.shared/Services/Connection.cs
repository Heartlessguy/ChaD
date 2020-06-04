using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChaD.shared.Services
{
    public class Connection
    {
        protected Thread _listenerThread;
        protected Socket _userSocket;

        /// <summary>
        /// Инициализация класса с заданым сокетом. Инициализация и запуск потока прослушивания
        /// </summary>
        /// <param name="socket"></param>
        public Connection(Socket socket)
        {
            _userSocket = socket;
            _userSocket.ReceiveBufferSize = 2048;
            _userSocket.SendBufferSize = 2048;
            _listenerThread = new Thread(Listen) { IsBackground = true };
            _listenerThread.Start();
        }

        /// <summary>
        /// Метод для прослушивания соединения 
        /// </summary>
        public void Listen()
        {
            while (_userSocket.Connected)
            {
                var buffer = new byte[2048];
                try
                {
                    var bytesReceived = _userSocket.Receive(buffer);
                    var message = Encoding.UTF8.GetString(buffer)?.Replace("\0", string.Empty);
                    MessageProcessor(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }



        public virtual void MessageProcessor(string message)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Метод для отправки сообщения по соединению
        /// </summary>
        /// <param name="buffer"></param>
        public void Send(byte[] buffer)
        {
            _userSocket.Send(buffer);
        }

        public void Send(string message)
        {
            _userSocket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}