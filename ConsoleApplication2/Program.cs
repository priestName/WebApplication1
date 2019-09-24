using System;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class1 c1 = new Class1();
            //c1.Main();

            TCP_Server c1 = new TCP_Server();
            c1.Main();
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
            

            

            Console.ReadLine();
        }
    }
}
