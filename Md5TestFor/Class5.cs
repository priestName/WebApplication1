using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md5TestFor
{
    class Class5
    {
        private static char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToArray();
        private static int isbit = keys.Length;
        public void Md5For()
        {
        aaaa:
            Console.WriteLine(strTo10(Console.ReadLine().ToString()));
            goto aaaa;
        }

        string strTo64(int num, decimal text)
        {
            string result = string.Empty;
            do
            {
                decimal index = text % isbit;
                result = keys[(int)index] + result;
                text = (text - index) / isbit;
            } while (text > 0);

            int resultLength = result.Length;
            if (resultLength < num)
            {
                for (int s = 0; s < num - resultLength; s++)
                {
                    result = keys[0] + result;
                }
            }

            return result;
        }
        decimal strTo10(string text)
        {
            decimal result = 0;
            for (int i = 0; i < text.Length; i++)
            {
                result += Array.IndexOf(keys, text[i]) * (decimal)Math.Pow(isbit, text.Length - 1 - i);
            }
            return result;
        }
    }
}
