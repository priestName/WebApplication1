using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class4
    {
        public void main()
        {
            Console.Write("初始数：");
            List<shuju> sjList = Console.ReadLine().ToString().Replace("：",":").Replace("，", ",").Split(',').Select(s=> new shuju {Num= Convert.ToInt32(s.Split(':')[0]),em = Convert.ToInt32(s.Split(':')[1]) }).ToList();
            int Num2 = 0;
            foreach (var Numsj in sjList)
            {
                Num2 = gys(Num2, Numsj.Num);
            }
            Console.WriteLine(string.Join(",",sjList.Select(s => s.Num / Num2).ToList()));

            Console.Write("肉：");
            int rou = Convert.ToInt32(Console.ReadLine());
            var sjList2 = sjList.Select(s => new shuju{Num = (s.Num / Num2),em = s.em}).ToList();
            for (int i = 1; i < sjList2.First().Num; i++)
            {
                var he = 0;
                foreach (var item in sjList2.Select(s => s.Num/i * s.em).ToList()) { he += item; }
                if (he <= rou)
                {
                    Console.WriteLine(string.Join(",", sjList2.Select(s => s.Num/i).ToList().ToList()));
                    return;
                }
            }
            
            
            Console.ReadLine();
        }
        public int gys(int Num1, int Num2)
        {
            int Rem;
            if (Num1 == 0)
                return Num2;

            while (Num2 > 0)
            {
                Rem = Num1 % Num2;
                Num1 = Num2;
                Num2 = Rem;
            }

            return Num1;
        }
    }
    public class shuju
    {
        public int Num { get; set; }
        public int em { get; set; }
    }
}
