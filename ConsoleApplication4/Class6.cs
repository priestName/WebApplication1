using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Class6
    {
        public void saa()
        {
            var a = 0;
            aaa(out a);
            Console.WriteLine(a);
        }
        public void aaa(out int c)
        {
            c = 1;
        }
    }
}
