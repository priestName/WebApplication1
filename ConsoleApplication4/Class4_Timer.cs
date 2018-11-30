using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication4
{
    class Class4_Timer
    {
        public void saa()
        {
            Timer tm = new Timer(60*1000);
            tm.AutoReset = true;
            tm.Enabled = true;
            tm.Elapsed += new System.Timers.ElapsedEventHandler(test);
        }
        public void test(object sender, ElapsedEventArgs e) {
            DateTime dtm = Convert.ToDateTime(DateTime.Now.ToString("yyyy-M-d") + " " + "11:37");
            if (DateTime.Now>=dtm)
            {
                Console.WriteLine(dtm+"||"+DateTime.Now.ToString());
            }
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}
