using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class2
    {

        public void main()
        {
            //Point p = new Point() {X=2,Y=3 };
            //string aaa = "1";
            //static int Len(string a) => a.Length;
            //using (var file = new System.IO.StreamWriter("WriteLines2.txt"))
            //{Console.WriteLine(Len("123"));};
            //外门:获得:4800; 用时: 66小时50分钟; 购买2次//8:20
            //内门:获得:4908; 用时: 55小时0分钟; 购买2次//6:40
            //执法:获得:5409; 用时: 51小时40分钟; 购买2次//5:40
            //真传:获得:7401; 用时: 65小时40分钟; 购买2次//5:00
            List<int[]> list = new List<int[]> { new int[] { 0, 12 },new int[]{ 100, 15 }, new int[] { 500, 18 }, new int[] { 2000, 20 } };
            int sjsj = 0, hdgx = 0, dqgx = 0, sd = 0, zhong = 3600;
            int dj = -1;int ts = 0;
            while (dj < list.Count-1)
            {
                hdgx += sd; dqgx += sd; sjsj++;

                if (dqgx>= list[dj+1][0])
                {
                    dj++;
                    dqgx -= list[dj][0];
                    sd = list[dj][1];

                    if (sjsj - (ts * 24 * 6) >= (6 * 20))
                    {
                        if (dqgx < 600)
                        {
                            sjsj += (int)Math.Ceiling((double)(600 - dqgx) / sd);
                            hdgx += (int)Math.Ceiling((double)(600 - dqgx) / sd) * sd;
                            ts++;
                        }
                        else
                        {
                            dqgx -= 600;
                        }
                    }

                    int zhts = 0;
                    int sj = (int)Math.Ceiling((double)(zhong - dqgx) / sd);
                    int gx = (sj * sd) - zhong + dqgx;
                    while ((sj + sjsj) - ((zhts + ts) * 24 * 6) > 22 * 6)
                    {
                        sj += (int)Math.Ceiling((double)(600 - gx) / sd);
                        gx += ((int)Math.Ceiling((double)(600 - gx) / sd) * sd) - 600;
                        zhts++;
                    }
                    Console.WriteLine($"获得:{hdgx + (sj * sd)};用时:{(sjsj + sj)/6}小时{(sjsj + sj) % 6 *10}分钟;购买{zhts + ts}次");
                }
            }
        }
        public static string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };
        public object mosshi12(object item)
        =>item switch
            {
                int n when n >= 0 => n,
                string n when n.Length>10=>n,
                double n => n,
                _ => ""
            };
        public object mosshi1(object item)
        {
            object text = "";
            switch (item)
            {
                case int n when n >= 0:
                    text = "int>=0";
                    break;
                case string n when n.Length > 10:
                    text = $"{n}.Length > 10";
                    break;
                case double n:
                    text = n.ToString("x2");
                    break;
                default:
                    break;
            }
            return text;
        }
        public readonly struct Point
        {
            public readonly double X;
            public readonly double Y;

        }

    }
}