using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string a = "06:00";
            //DateTime result = DateTime.Parse(DateTime.Now.ToString("yyyy-M-d hh:mm:ss"));
            //DateTime.TryParse("2018-1-1 " + a, out result);
            //Console.WriteLine(result.ToString("yyyy-MM-dd hh:mm:ss"));
            //Console.WriteLine(result.ToString("hh:mm"));
            //Console.ReadLine();
            Console.WriteLine(writtxt("11111111111"));
            using (StreamReader sr = new StreamReader(@"C:\Users\admin\Desktop\aa.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }
            Console.ReadLine();
        }
        public static string writtxt(string html)
        {
            FileStream fileStream = new FileStream(@"C:\Users\admin\Desktop\aa.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.Write(html + "\r\n");
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            return "true";
        }
    }
}
