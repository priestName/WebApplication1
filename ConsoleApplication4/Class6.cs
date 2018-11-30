using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.Extractor;
using System.IO;

namespace ConsoleApplication4
{
    class Class6
    {
        public void saa()
        {

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
    }
}
