﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Data.Entity;

namespace Md5TestFor
{
    class Class1
    {
        public void Md5For()
        {
        gonum:
            Console.Clear();
            Console.Write("位数:");
            string num = Console.ReadLine();
            if (string.IsNullOrEmpty(num)) goto gonum;
            Console.Write("起始项(可空):");
            string text = Console.ReadLine().ToString();
            quzimu(Convert.ToInt32(num), text);

            Console.ReadLine();
        }

        void quzimu(int num, string stretext)
        {
            string[] zimu = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "0","1","2","3","4","5","6","7","8","9"
            };

            string NumberStr = string.Empty;
            int[] Text;
            if (!string.IsNullOrEmpty(stretext))
            {
                foreach (var item in stretext)
                {
                    int t = Array.IndexOf(zimu, item.ToString());
                    NumberStr += t > 0 ? t.ToString() + "," : "0,";
                }
                Text = Array.ConvertAll(NumberStr.Trim(',').Split(','), int.Parse);
                Text[5] += 1;
            }
            else
            {
                for (int j = 0; j < num; j++)
                {
                    NumberStr += "0,";
                }
                Text = Array.ConvertAll(NumberStr.Trim(',').Split(','), int.Parse);
            }
            for (int i = Text.Last(); i <= zimu.Length; i++)
            {
            jinzh:
                Text[Text.Length - 1] = i;
                int Text62 = Array.IndexOf(Text, 62);
                if (Text62 > 0)
                {
                    i = 0;
                    Text[Text62] = 0;
                    Text[Text62 - 1] += +1;
                    goto jinzh;
                }
                if (Text62 == 0)
                {
                    Console.WriteLine("结束");
                    return;
                }

                string MinText = string.Empty;
                foreach (var item in Text)
                {
                    MinText += zimu[Convert.ToInt32(item)];
                }
                addMd5(MinText, MD5(MinText));
                Console.WriteLine(MinText);
                
            }
        }
        string MD5(string input)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

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
            
        }
    }
}
