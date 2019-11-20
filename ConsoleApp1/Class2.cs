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
            string aaa = "1";
            static int Len(string a) => a.Length;
            using (var file = new System.IO.StreamWriter("WriteLines2.txt"))
            {Console.WriteLine(Len("123"));};

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