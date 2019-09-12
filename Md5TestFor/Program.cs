using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Md5TestFor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("方式(1.枚举，2.循环)：");
            int a = Convert.ToInt32(Console.ReadLine());
            switch (a)
            {
                case 1:
                    Class1 class1 = new Class1();
                    class1.Md5For();
                    break;
                case 2:
                    Class2 class2 = new Class2();
                    class2.Md5For();
                    break;
                default:
                    break;
            }
            

            


            //var ctx1 = new KeyValue();
            //var o1 = new Md5Test() { Key="1",Values="2"};
            //ctx1.Md5Test.Add(o1);
            //ctx1.SaveChanges();

            //var query = from order in ctx1.Md5Test
            //            select order;
            //foreach (var q in query)
            //{
            //    Console.WriteLine("OrderId:{0},OrderDate:{1}", q.Key, q.Values);
            //}

            Console.Read();
        }
    }
}
