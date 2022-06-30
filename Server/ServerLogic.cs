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
            NetworkStream stream = null;
            try
            {
                stream = _tcpClient.GetStream();
                byte[] data = new byte[64]; // буфер для получаемых данных //buffer for accepet data 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Успешное соединение с клиентом!");
                Console.ForegroundColor = ConsoleColor.White;
                while (true)
                {
                    

                    // получаем сообщение //get message

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);

                    //стринг в инт для меню //string to int for menu
                    string[] command = message.Split(":");
                    int commandInput = int.Parse(command[0]);

                    switch (commandInput)
                    {
                        case 1:
                            bucket3 = 3;
                            steps += 1;
                            message = string.Format("Количество воды в трёхлитровом ведре: {0}" +
                                "\nКоличество воды в пятилитровом ведре: {1}" + "\nКоличество шагов: {2}", bucket3, bucket5, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;
                        case 2:
                            bucket5 = 5;
                            steps += 1;
                            message = string.Format("Количество воды в трёхлитровом ведре: {0}" +
                                "\nКоличество воды в пятилитровом ведре: {1}" + "\nКоличество шагов: {2}", bucket3, bucket5, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;
                        case 3:
                            capacity = 5 - bucket5;
                            for (int i = 0; i < capacity; i++)
                            {
                                for (int j = 0; j < bucket3; j++)
                                {
                                    bucket3 -= 1;
                                    bucket5 += 1;
                                    capacity -= 1;
                                    if (capacity == 0) break;
                                }
                            }
                            steps += 1;
                            message = string.Format("Количество воды в трёхлитровом ведре: {0}" +
                                "\nКоличество воды в пятилитровом ведре: {1}" + "\nКоличество шагов: {2}", bucket3, bucket5, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;

                        case 4:
                            capacity = 3 - bucket3;
                            for (int i = 0; i < capacity; i++)
                            {
                                for (int j = 0; j <= bucket5; j++)
                                {
                                    bucket5 -= 1;
                                    bucket3 += 1;
                                    capacity -= 1;

                                }
                                if (bucket5 == 0 || capacity == 0) break;
                            }
                            steps += 1;
                            message = string.Format("Количество воды в трёхлитровом ведре: {0}" +
                                "\nКоличество воды в пятилитровом ведре: {1}" + "\nКоличество шагов: {2}", bucket3, bucket5, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;
                        case 5:
                            bucket3 = 0;
                            steps += 1;
                            message = string.Format("Трехлитровое ведро опусташено, количество воды: {0}\n" + "Количество воды в пятилитровом: {1}\n" + "Количество шагов: {2}", bucket3,bucket5, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;
                        case 6:
                            bucket5 = 0;
                            steps += 1;
                            message = string.Format("Пятилитровое ведро опусташено, количество воды: {0}\n" +"Количество воды в трёхлитровом: {1}\n"+ "Количество шагов: {2}", bucket5,bucket3, steps);
                            data = Encoding.Unicode.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            break;
                        //тестирование победы так сказать.
                        case 8:
                            bucket5 = 4;
                            steps += 1;
                            break;
                        case 7:
                            stream.Close();
                            _tcpClient.Close();
                            break;
                    }

                    if(bucket5 == 4)
                    {
                        bucket3 = 0;
                        bucket5 = 0;
                        capacity = 0;
                        message = string.Format("\nWINNER = Ты сделал это за: {0} " + "шагов", steps);
                        data = Encoding.Unicode.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        steps = 0;
                        _tcpClient.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                if(stream != null)
                {
                    stream.Close();
                }
                if(_tcpClient != null)
                {
                    _tcpClient.Close();
                }
            }
        }
    }
}
