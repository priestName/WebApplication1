using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Class1
    {
        public void Main()
        {
            Console.Write("初始数：");
            var NumList = Array.ConvertAll(Console.ReadLine().ToString().Replace("，",",").Split(','), int.Parse);
            int Num2 = 0;
            foreach (var Num in NumList)
            {
                Num2 = gys(Num2, Num);
            }
            Console.WriteLine(Num2);
            Console.ReadLine();
        }
        public int gys(int Num1,int Num2)
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
}
