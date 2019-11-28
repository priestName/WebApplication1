using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class6
    {
        public void Main()
        {
            var GetHtmlText = new HtmlWeb();
            HtmlDocument cnblogs = GetHtmlText.Load("http://www.cnblogs.com/");
            HtmlNodeCollection titleNodes = cnblogs.DocumentNode.SelectNodes("//a[@class='titlelnk']");
            foreach (var item in titleNodes)
            {
                Console.WriteLine(item.InnerText);
                Console.WriteLine(item.Attributes.FirstOrDefault(x=>x.Name == "href")?.Value);
            }
        }
    }
}
