using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    {
        public void main()
        {
            #region 元组
            //var unnamed = ("one", 200);
            //aaa(unnamed, out int a);
            //Console.WriteLine(a);
            //(string a, string b, string c) ab = ("aa", "bb", "cc");
            //var ab1 = (a: "aa", b: "bb");
            //Console.WriteLine($"{ab.a},{ab.b},{ab.c}");
            //(string aa, string bb) = ab1;
            //Console.WriteLine($"{aa},{bb}");
            //(double x, double y) = new Point(3.14, 2.71);
            //Console.WriteLine($"{x},{y}");
            //var (a1, _, c1) = ab;
            //if (a1 is string)
            //    Console.WriteLine(a1);
            //var points = (x, y);
            //Console.WriteLine($"{points.x},{points.y}");
            //var left = (a: 5, b: 10);
            //(int? a, int? b) nullableMembers = (5, 10);
            //Console.WriteLine(left == nullableMembers);//true
            //Console.WriteLine(left != nullableMembers);//false

            //(long a, long b) longTuple = (5, 10);
            //Console.WriteLine(left == longTuple);//true
            //Console.WriteLine(left != longTuple);//false

            //(long a, int b) longFirst = (5, 10);
            //(int a, long b) longSecond = (5, 10);
            //Console.WriteLine(longFirst == longSecond);//true
            //Console.WriteLine(longFirst != longSecond);//false

            #endregion
            #region 模式匹配
            //List<object> Test = new List<object> {"100",100, "1,2,3,4", new List<int> { 1,2,3,4} };
            //foreach (var item in Test)
            //{
            //    switch (item)
            //    {
            //        case int n when n > 5:
            //            Console.WriteLine(n);
            //            break;
            //        case List<int> hahaha:
            //            {
            //                Console.WriteLine(hahaha.Last());
            //                break;
            //            }
            //        default:
            //            break;
            //    }
            //}
            //case 0: 是常见的常量模式。
            //case IEnumerable<int> childSequence: 是一种类型模式。
            //case int n when n > 0: 是具有附加 when 条件的类型模式。
            //case null: 是 null 模式。
            //default: 是常见的默认事例。
            #endregion
            #region ref局部变量与返回结果
            //int[,] matrix = new int[,] { { 1, 2, 3 }, { 2, 3, 4 } };
            //ref var item = ref Find(matrix, (val) => val == 3);
            //Console.WriteLine(item);//3
            //item = 24;
            //Console.WriteLine(matrix[0, 2]);//24
            //ref var r = ref (matrix[0, 2] != 24 ? ref matrix[0,2] : ref matrix[1,2]);
            //ref var r1 = ref matrix[0, 2];
            //r1 = ref matrix[1, 2];
            #endregion
            #region 本地函数
            //IEnumerable<char> AlphabetSubsets = AlphabetSubset3('a', 'z');
            //Console.WriteLine(string.Join(",", AlphabetSubsets.ToList()));
            //int alphabetSubsetImplementation(int s1,int s2)
            //{
            //    if (s1 > s2)
            //        return s1;
            //    else
            //        return s2;
            //}
            //int alphabetSubsetImplementation1(int s1, int s2) => s1 > s2?s1:s2;
            //Console.WriteLine(alphabetSubsetImplementation(1,10));
            //Console.WriteLine(alphabetSubsetImplementation1(100, 10));
            #endregion

            //int* pArr = stackalloc int[3] { 1, 2, 3 };
            //int* pArr2 = stackalloc int[] { 1, 2, 3 };
            //Span<int> arr = stackalloc[] { 1, 2, 3 };
        }

        //public async ValueTask<int> Func()
        //{
        //    await Task.Delay(100);
        //    return 5;
        //}
        public static IEnumerable<char> AlphabetSubset3(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImplementation();

            IEnumerable<char> alphabetSubsetImplementation()
            {
                for (var c = start; c < end; c++)
                    yield return c;
            }
        }
        public static ref int Find(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found");
        }
        public void aaa((string,int) unnamed, out int a) => a = unnamed.Item2;
        public class Point
        {
            public Point(double x, double y)
                => (X, Y) = (x, y);

            public double X { get; }
            public double Y { get; }

            public void Deconstruct(out double x, out double y) =>
                (x, y) = (X, Y);
        }
    }
}
