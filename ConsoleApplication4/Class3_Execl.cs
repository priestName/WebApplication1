using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.Extractor;
using System.Data;

namespace ConsoleApplication4
{
    class Class3_Execl
    {
        public void saa()
        {
            //DataTable dt = duquToTable();
            //foreach (DataRow item in dt.Rows)
            //{
            //    var a = item["测试四"];
            //    Console.WriteLine(item["测试四"]);
            //}


        }
        public DataTable duquToTable()
        {
            HSSFWorkbook wk = new HSSFWorkbook();
            using (FileStream fs = File.Open(@"C:\Users\admin\Desktop\News.xls", FileMode.Open, FileAccess.Read))
            {
                wk = new HSSFWorkbook(fs);
                fs.Close();
            }
            ISheet sheet = wk.GetSheetAt(0);
            DataTable dt = new DataTable();

            IRow irt = sheet.GetRow(0);
            for (var j = 0; j < irt.LastCellNum; j++)
            {
                DataColumn col = new DataColumn(irt.GetCell(j).StringCellValue);
                dt.Columns.Add(col);
            }

            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                for (var j = 0; j < sheet.GetRow(i).LastCellNum; j++)
                {
                    dr[j] = sheet.GetRow(i).GetCell(j);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public string[,] duqu()
        {
            HSSFWorkbook wk = new HSSFWorkbook();
            using (FileStream fs = File.Open(@"C:\Users\admin\Desktop\News.xls", FileMode.Open, FileAccess.Read))
            {
                wk = new HSSFWorkbook(fs);
                fs.Close();
            }
            ISheet sheet = wk.GetSheetAt(0);
            string[,] aa = new string[sheet.LastRowNum, sheet.GetRow(0).LastCellNum];
            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                for (var j = 0; j < sheet.GetRow(i).LastCellNum; j++)
                {
                    aa[i, j] = sheet.GetRow(i).GetCell(j).ToString();
                    Console.Write(aa[i, j]);
                }
            }
            return aa;
        }
        public void xiugai()
        {
            HSSFWorkbook wk = new HSSFWorkbook();
            using (FileStream fs = File.Open(@"C:\Users\admin\Desktop\News.xls", FileMode.Open,FileAccess.Read))
            {
                wk = new HSSFWorkbook(fs);
                fs.Close();
            }
            ISheet sheet = wk.GetSheetAt(0);
            string[,] aa = new string[sheet.LastRowNum, sheet.GetRow(0).LastCellNum];
            sheet.GetRow(1).GetCell(0).SetCellValue("测试2");
            using (FileStream fls = File.Open(@"C:\Users\admin\Desktop\News.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wk.Write(fls);
            }
            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                for (var j=0;j< sheet.GetRow(i).LastCellNum; j++)
                {
                    aa[i, j] = sheet.GetRow(i).GetCell(j).ToString();
                    Console.Write(aa[i, j]);
                }
                Console.WriteLine();
            }
            foreach (var item in aa) { Console.Write(item); }
           
        }
        public void cuangjian()
        {
            HSSFWorkbook wk = new HSSFWorkbook();
            ISheet sheet = wk.CreateSheet("例子");
            IRow row = sheet.CreateRow(1);
            //在第一行的第一列创建单元格  
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("测试1");
            using (FileStream fs = File.OpenWrite(@"C:\Users\admin\Desktop\News.xls"))
            {
                wk.Write(fs);//向打开的这个xls文件中写入并保存。  
            }
        }


        private static string GetTextFromExcel2007Format(string filePath)
        {
            XSSFExcelExtractor excelExtractor = null;

            try
            {
                excelExtractor = new XSSFExcelExtractor(filePath);
                excelExtractor.IncludeCellComments = false; // optional
                excelExtractor.IncludeHeaderFooter = false; // optional
                excelExtractor.IncludeSheetNames = false; // optional

                return excelExtractor.Text;
            }
            catch (Exception e)
            {
                // handle the exception
            }
            finally
            {
                if (excelExtractor != null)
                {
                    excelExtractor.Close();
                    excelExtractor = null;
                }
            }

            return string.Empty;
        }
        public List<string> red(string txt)
        {
            StreamReader str = new StreamReader(txt, Encoding.Default);
            List<string> aa = new List<string> { };
            int bol = 1;
            while (bol!=0)
            {
                string st = str.ReadLine();
                if (st != null)
                {
                    aa.Add(st);
                    bol += 1;
                }
                bol -= 1;
            }
            str.Close(); str.Close();
            return aa;
        }

        public void text()
        {
            //StreamReader str = new StreamReader(@"C:\Users\admin\Desktop\News.txt", Encoding.Default);
            //string a = str.ReadLine();
            //foreach (var item in red(@"C:\Users\admin\Desktop\News.xls"))
            //{
            //    Console.WriteLine(item);
            //}
            //FileStream fs = new FileStream(@"C:\Users\admin\Desktop\News.xls", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //GetTextFromExcel2007Format(@"C:\Users\admin\Desktop\News.xls");

            //HSSFWorkbook wk = null;
            //FileStream fileS = new FileStream(@"C:\Users\admin\Desktop\News.xls", FileMode.Open, FileAccess.Read);
            //wk = new HSSFWorkbook(fileS);
            //ISheet sheet = wk.GetSheetAt(0);
            //string a = sheet.GetRow(1).GetCell(0).ToString();
        }
    }
}
