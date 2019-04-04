using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication4
{
    class Class_路径
    {
        public void saa() {
            string a= Directory.GetCurrentDirectory();
            string b = AppDomain.CurrentDomain.BaseDirectory;
            string dbstring = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "WebApplication1\\Images";
        }
    }
}
