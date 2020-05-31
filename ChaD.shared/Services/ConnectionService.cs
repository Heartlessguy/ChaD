using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChaD.shared.Services
{
    public class ConnectionService
    {
        private Thread _listenerThread;
        private Socket _userSocket;

        /// <summary>
        /// Инициализация класса с заданым сокетом. Инициализация и запуск потока прослушивания
        /// </summary>
        /// <param name="socket"></param>
        public ConnectionService(Socket socket)
        {
            _userSocket = socket;
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
                byte[] buffer = new byte[1024];
                string message = "";
                try
                {
                    var bytesReceived = _userSocket.Receive(buffer);
                    message = Encoding.UTF8.GetString(buffer)?.Replace("\0", string.Empty);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                MessageProcessor(message);
            }
        }

        public virtual void MessageProcessor(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message}");
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