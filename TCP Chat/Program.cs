using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_Chat
{
    internal class Program
    {
        static void Main(string[] args)
        {
	    TcpClient client = new TcpClient();
            client.Connect(Console.ReadLine(), Int32.Parse(Console.ReadLine()));
            Task.Run(() =>
            {
                StreamReader sr = new StreamReader(client.GetStream());
                while (true)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            });
            StreamWriter sw = new StreamWriter(client.GetStream());
            while (true)
            {
                sw.WriteLine(Console.ReadLine());
                sw.Flush();
            }
        }
    }
}
