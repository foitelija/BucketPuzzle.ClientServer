using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        const int port = 8080;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while(true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Успешное подключение к серверу.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1. Набрать воду в трёхлитровое ведро");
                    Console.WriteLine("2. Набрать воду в пятилитровое ведро");
                    Console.WriteLine("3. Перелить из 3-х литровое в 5-ти литровое");
                    Console.WriteLine("4. Перелить из 5-ти литрового в 3-х литровое");
                    Console.WriteLine("5. Вылить всю воду из 3-х литрового");
                    Console.WriteLine("6. Вылить всю воду из 5-ти литрового");
                    Console.WriteLine("8. Тестирование победы");
                    Console.WriteLine("7. Выход\n");

                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}