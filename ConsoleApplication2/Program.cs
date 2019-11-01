using System;
using System.Timers;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class1 c1 = new Class1();
            //c1.Main();

            //TCP_Server c1 = new TCP_Server();
            //c1.Main();
            //SocketServiceWuZi socketWuZi = new SocketServiceWuZi();
            //socketWuZi.Start();
            //SocketService socket = new SocketService();
            //socket.Start();
            Class3 socket = new Class3();
            socket.Start();

            //Timer timer = new Timer(600000-30);
            //timer.AutoReset = true;
            //timer.Enabled = true;
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(aaaa);
            Console.ReadLine();
            //Console.WriteLine("1：聊天，2：五子棋");
            //switch (Console.ReadLine())
            //{
            //    case "1":
            //        SocketService socket = new SocketService();
            //        socket.Start();
            //        break;
            //    case "2":
            //        SocketServiceWuZi socketWuZi = new SocketServiceWuZi();
            //        socketWuZi.Start();
            //        break;
            //    default:
            //        break;
            //}

        }
        public static void aaaa(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now>= Convert.ToDateTime("23:50:00"))
            {
                Environment.Exit(0);
            }
        }
    }
}
