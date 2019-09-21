using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Md5TestFor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Class5 class5 = new Class5();
            //class5.Md5For();

            Console.Write("方式(1.枚举，2.循环，3.62进制转换)：");
            int a = Convert.ToInt32(Console.ReadLine());
            switch (a)
            {
                case 1:
                    Class1 class1 = new Class1();
                    //Thread thread1 = new Thread(new ThreadStart(class1.Md5For));
                    class1.Md5For();
                    break;
                case 2:
                    Class2 class2 = new Class2();
                    //Thread thread2 = new Thread(new ThreadStart(class2.Md5For));
                    class2.Md5For();
                    break;
                case 3:
                    Class4 class4 = new Class4();
                    //Thread thread4 = new Thread(new ThreadStart(class4.Md5For));
                    class4.Md5For();
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
