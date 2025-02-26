using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class7
    {
        public void main()
        {
            Console.WriteLine("开始0,{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            aa3();
            Console.WriteLine("结束3,{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            aa2();
            Console.WriteLine("结束2,{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            aa1();
            Console.WriteLine("结束1,{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
        }

        public void aa1(string aa = "aa1")
        {
            for (int i = 0; i < 10; i++) 
            {
                Thread.Sleep(1000);
                Console.WriteLine("{0},{1},{2}", aa,i, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        public Task aa2(string aa = "aa2")
        {
            return Task.Run(() => aa1(aa));
        }
        public async Task aa3()
        {
            await aa2("aa3").ConfigureAwait(true);
        }
    }
}
