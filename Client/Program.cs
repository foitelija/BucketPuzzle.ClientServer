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


                    //ввод сообщения
                    //message input

                    string message = Console.ReadLine();
                    if (message != null && message != "7")
                    {
                        message = String.Format("{0}", message);
                        //преобразуем сообщение в массив байтов //message to byte
                        byte[] data = Encoding.Unicode.GetBytes(message);

                        //отправка сообщения  //message send
                        stream.Write(data, 0, data.Length);

                        //получаем ответ //message get
                        data = new byte[data.Length];
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = stream.Read(data,0,data.Length);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);

                        message = builder.ToString();
                        Console.WriteLine("Ответ от сервера:\n{0}", message);

                    }
                    else
                    {
                        client.Close();
                        System.Environment.Exit(1);
                    }

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