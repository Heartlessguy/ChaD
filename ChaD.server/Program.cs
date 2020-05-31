using System;
using System.Collections;
using System.Resources;
using System.Text;
using ChaD.server.Services;

namespace ChaD.server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 20000);
            server.Run();

        }
    }
}
