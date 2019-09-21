using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Threading;

namespace Md5TestFor
{
    class Class1
    {
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
        }

        string Quzimu(int num, string text1, string text2)
        {
            string[] zimu = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "0","1","2","3","4","5","6","7","8","9"
            };
            try
            {
                string NumberStr = string.Empty;
                int[] Text;
                string str = text1 + text2;
                for (int i = 0; i < num; i++)
                {
                    int t;
                    if (str.Length > i)
                    {
                        t = Array.IndexOf(zimu, str[i].ToString());
                        if (t < 0)
                        {
                            return "输入字符不存在";
                        }
                        NumberStr += t > 0 ? t.ToString() + "," : "0,";
                    }
                    else {
                        NumberStr += "0,";
                    }
                
                }
                Text = Array.ConvertAll(NumberStr.Trim(',').Split(','), int.Parse);
                if (!string.IsNullOrEmpty(text2))
                {
                    Text[num-1] += 1;
                }
                for (int i = Text.Last(); i <= zimu.Length; i++)
                {
                jinzh:
                    Text[Text.Length - 1] = i;
                    int Text62 = Array.IndexOf(Text, 62);
                    if (Text62 == text1.Length)
                    {
                        //Console.WriteLine("结束");
                        return "结束";
                    }
                    if (Text62 == num - 2)
                    {
                        Console.WriteLine(string.Join(",",Text));
                    }
                    if (Text62 > 0)
                    {
                        i = 0;
                        Text[Text62] = 0;
                        Text[Text62 - 1] += +1;
                        goto jinzh;
                    }

                    string MinText = string.Empty;
                    foreach (var item in Text)
                    {
                        MinText += zimu[Convert.ToInt32(item)];
                    }
                    addMd5(MinText, MD5(MinText));
                    //Console.WriteLine(MinText);
                }
                return "结束";
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
            //KeyValueContext.Md5TestUser.Add(new Md5TestUser() { Key = "Test", Value = Value });
            //KeyValueContext.SaveChanges();
            KeyValueContext.Md5Test.Add(new Md5Test() { Key = Key, Value = Value });
            KeyValueContext.SaveChanges();

        }
    }
}
