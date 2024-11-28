using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace TCP_Server
{
    internal class Program
    {
        private static List<StreamWriter> clients = new List<StreamWriter>();
        private static TcpListener server;

        static void Main(string[] args)
        {
            server = new TcpListener(IPAddress.Any, 25565);
            server.Start();
            Console.WriteLine("Сервер запущен...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                clients.Add(sw);
                Console.WriteLine("Клиент подключен.");
                Task.Run(() =>
                {
                    while (true)
                    {
                        string msg = sr.ReadLine();
                        foreach (StreamWriter writer in clients)
                        {
                            if (writer != sw)
                            {
                                writer.WriteLine(msg);
                                writer.Flush();
                            }
                        }
                    }
                });
            }
        }
    }
}
