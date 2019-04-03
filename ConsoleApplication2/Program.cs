using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using WindowsFormsApplication2;
using System.Windows.Forms;

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

            SocketService socket = new SocketService();
            socket.Start();

            //SocketServiceWuZi socket = new SocketServiceWuZi();
            //socket.Start();

            Console.ReadLine();
        }
    }
}
