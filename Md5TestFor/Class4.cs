using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Md5TestFor
{
    class Class4
    {
        //private static char[] keys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        private static char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToArray();
        private static int isbit = keys.Length;
        public void Md5For()
        {
        gonum:
            Console.Clear();
        gonum2:
            Console.Write("位数:");
            string num = Console.ReadLine();
            if (string.IsNullOrEmpty(num)) goto gonum;
            Console.Write("分段执行(可空):");
            string text1 = Console.ReadLine();
            Console.Write("起始项(可空):");
            string text = Console.ReadLine().ToString();
            string IsText = Quzimu(Convert.ToInt32(num), text1, text);
            Console.WriteLine(IsText); goto gonum2;
            //Console.ReadLine();
        }
        string Quzimu(int num, string text1, string text2)
        {
            string StartStr = text1 + text2;
            string EndStr = string.Empty;
            for (int i = 0; i < num; i++)
            {
                EndStr += keys.LastOrDefault().ToString();
            }
            if (string.IsNullOrEmpty(text2))
            {
                int StartStrLength = StartStr.Length;
                for (int i = 0; i < num - StartStrLength; i++)
                {
                    StartStr += keys[0].ToString();
                }
            }
            decimal StartText = strTo10(StartStr);
            if (!string.IsNullOrEmpty(text2))
            {
                StartText += 1;
            }
            decimal EndText = strTo10(EndStr);
            
            
            chonglai:
            for (decimal i = StartText; i < EndText; i++)
            {
                try
                {
                    string str = strTo64(num, i);
                    
                    if (!string.IsNullOrEmpty(text1) && str.IndexOf(text1) < 0)
                        return "结束";
                    if (str.LastIndexOf("99") == num - 2)
                        Console.WriteLine(str);
                    addMd5(str, MD5(str));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    StartText = i;
                    goto chonglai;
                }
                
            }
            return "结束";
        }
        string MD5(string input)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            foreach (byte d in data)
            {
                sBuilder.Append(d.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        void addMd5(string Key, string Value)
        {
            KeyValue KeyValueContext = new KeyValue();
            KeyValueContext.Md5Test.Add(new Md5Test() { Key = Key, Value = Value });
            KeyValueContext.SaveChanges();
            

            //KeyValue KeyValueContext = new KeyValue();
            //KeyValueContext.Md5TestUser.Add(new Md5TestUser() { Key = Key, Value = Value });
            //KeyValueContext.SaveChanges();
        }
        string strTo64(int num,decimal text)
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
                result += Array.IndexOf(keys,text[i]) * (decimal)Math.Pow(isbit,text.Length - 1 - i);
            }
            return result;
        }
    }
}
