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

        }
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
