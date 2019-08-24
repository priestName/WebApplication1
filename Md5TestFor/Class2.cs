using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Md5TestFor
{
    class Class2
    {
        public void Md5For()
        {
            Console.Clear();
            Console.Write("起始项(可空):");
            string text = Console.ReadLine().ToString();
            quzimu(text);
            Console.ReadLine();
        }
        void quzimu(string StartText)
        {
            string[] zimu = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "0","1","2","3","4","5","6","7","8","9"
            };
            int MaxList = zimu.Length;
            string z0 ,z1 ,z2 ,z3 ,z4 ,z5;
            if (!string.IsNullOrEmpty(StartText))
            {
                for (int i0 = zimu.ToList().IndexOf(StartText[0].ToString()); i0 < MaxList; i0++)
                {
                    z0 = zimu[i0];
                    for (int i1 = zimu.ToList().IndexOf(StartText[1].ToString()); i1 < MaxList; i1++)
                    {
                        z1 = zimu[i1];
                        for (int i2 = zimu.ToList().IndexOf(StartText[2].ToString()); i2 < MaxList; i2++)
                        {
                            z2 = zimu[i2];
                            for (int i3 = zimu.ToList().IndexOf(StartText[3].ToString()); i3 < MaxList; i3++)
                            {
                                z3 = zimu[i3];
                                for (int i4 = zimu.ToList().IndexOf(StartText[4].ToString()); i4 < MaxList; i4++)
                                {
                                    z4 = zimu[i4];
                                    for (int i5 = zimu.ToList().IndexOf(StartText[5].ToString())+1; i5 < MaxList; i5++)
                                    {
                                        z5 = zimu[i5];
                                        string MinText = z0 + z1 + z2 + z3 + z4 + z5;
                                        string IsMinText = GetMD5(MinText);
                                        KeyValue KeyValueContext = new KeyValue();
                                        KeyValueContext.Md5Test.Add(new Md5Test() { Key = MinText, Value = IsMinText });
                                        KeyValueContext.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else {
                for (int i0 = 0; i0 < MaxList; i0++)
                {
                    z0 = zimu[i0];
                    for (int i1 = 0; i1 < MaxList; i1++)
                    {
                        z1 = zimu[i1];
                        for (int i2 = 0; i2 < MaxList; i2++)
                        {
                            z2 = zimu[i2];
                            for (int i3 = 0; i3 < MaxList; i3++)
                            {
                                z3 = zimu[i3];
                                for (int i4 = 0; i4 < MaxList; i4++)
                                {
                                    z4 = zimu[i4];
                                    for (int i5 = 0; i5 < MaxList; i5++)
                                    {
                                        z5 = zimu[i5];
                                        string MinText = z0 + z1 + z2 + z3 + z4 + z5;
                                        string IsMinText = GetMD5(MinText);
                                        KeyValue KeyValueContext = new KeyValue();
                                        KeyValueContext.Md5Test.Add(new Md5Test() { Key = MinText, Value = IsMinText });
                                        KeyValueContext.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            

        }
        string GetMD5(string input)
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
    }
}
