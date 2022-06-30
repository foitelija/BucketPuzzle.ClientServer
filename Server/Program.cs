using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server // NOTE: IN THIS CASE -> I use .NET 6 with .NET 5 template.
{
    class Program
    {
        const int port = 8080;
        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    Console.WriteLine("Соединения установлено");
                    TcpClient client = listener.AcceptTcpClient();
                    ServerLogic serverLogic = new ServerLogic(client);

                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(serverLogic.Process));
                    clientThread.Start();
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}