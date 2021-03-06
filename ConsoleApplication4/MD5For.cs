﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class MD5For
    {
        public void saa()
        {
            
        gonum:
            Console.Clear();
            Console.Write("位数:");
            string num = Console.ReadLine();
            if (string.IsNullOrEmpty(num))
            {
                goto gonum;
            }
            Console.Write("起始项(可空):");
            string text = Console.ReadLine().ToString();
            quzimu(Convert.ToInt32(num), text);

            Console.ReadLine();
        }
        public void quzimu(int num,string stretext)
        {
            string[] zimu = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "0","1","2","3","4","5","6","7","8","9"
            };
            
            Double MaxNumber = 0, MinNumber = 0;
            string NumberStr = string.Empty;
            if (!string.IsNullOrEmpty(stretext))
            {
                foreach (var item in stretext)
                {
                    int t = Array.IndexOf(zimu, item.ToString());
                    NumberStr += t > 0 ? t.ToString() + "," : "0,";

                }
            }
            else {
                for (int j = 0; j < num; j++)
                {
                    NumberStr += "0,";
                }
            }
            int[] Text = Array.ConvertAll(NumberStr.Trim(',').Split(','), int.Parse);
            for (int i = Text.Last(); i <= zimu.Length; i++)
            {
                jinzh:
                Text[Text.Length - 1] = i;
                int Text62 = Array.IndexOf(Text, 62);
                if (Text62 > 0)
                {
                    i = 0;
                    Text[Text62] = 0;
                    Text[Text62 - 1] +=  + 1;
                    if (Text62 == Text.Length - 2)
                    {
                        Console.WriteLine(string.Join(",", Text));
                    }
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
                
                insertText(MinText, "Letters_" + num.ToString() + ".txt");
                insertText(MD5(MinText), "Letters_Md5_" + num.ToString() + ".txt");
                if (i == MaxNumber - 1)
                {
                    Console.WriteLine(MinNumber);
                    Console.WriteLine("结束");
                }

            }
        }
        public void qushu(int num)
        {
            int MaxNumber = 1;
            for (int i = 0; i < num; i++)
            {
                MaxNumber *= 10;
            }
            for (int a = 0; a < MaxNumber; a++)
            {
                string MinNumber = string.Empty;
                if (a.ToString().Length < num)
                {
                    for (int b = 0; b < num - a.ToString().Length; b++)
                    {
                        MinNumber += "0";
                    }
                    MinNumber += a.ToString();
                }
                else
                    MinNumber = a.ToString();
                insertText(MinNumber, "Number_" + num.ToString() + ".txt");
                insertText(MD5(MinNumber), "Number_Md5_" + num.ToString() + ".txt");
                if (MinNumber == (MaxNumber - 1).ToString())
                {
                    Console.WriteLine(MinNumber);
                    Console.WriteLine("结束");
                }
                else if(Convert.ToInt32(MinNumber)%1000==0){
                    Console.WriteLine(MinNumber);
                }
            }
        }
        public string MD5(string input)
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
        public void insertText(string text,string name)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(text+ ",");
            Directory.CreateDirectory(@"MD5_Text\");
            using (FileStream fsWrite = new FileStream(@"MD5_Text\" + name, FileMode.Append))
            {
                fsWrite.Position = fsWrite.Length;
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }
    }
}
