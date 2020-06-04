using ChaD.server.Services;

namespace ChaD.server
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgsParser(args);
            if (Address == null || Port == 0) return;
            Server server = new Server(Address, Port);
            server.Run();

        }

        private static int Port { get; set; }
        private static string Address { get; set; }
        private static void ArgsParser(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("-address="))
                {
                    Address = arg.Split("=")?[1];
                }

                if (arg.StartsWith("-port="))
                {
                    int.TryParse(arg.Split("=")?[1], out var port);
                    Port = port;
                }
            }
        }
    }
}
