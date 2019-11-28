using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Class1
    {
        public void Main()
        {
            GenerateSequence();
        }
        public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }
}
