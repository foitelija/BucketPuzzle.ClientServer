using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ServerLogic
    {
        public int bucket3 = 0;
        public int bucket5 = 0;
        public int capacity = 0;
        public int steps = 0;


        private readonly TcpClient _tcpClient;

        public ServerLogic(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
