﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Class3
    {
        //System.Timers.Timer tim = new System.Timers.Timer(100);
        public void saa()
        {
            Thread thread = new Thread(new ThreadStart(aa));
            thread.Start();
            //tim.Elapsed += new System.Timers.ElapsedEventHandler(bb);
            //tim.AutoReset = true;
            //tim.Enabled = true;
            bb();
        }
        public void bb()//object source, System.Timers.ElapsedEventArgs e
        {
            for (int i = 0; i < 99000; i++)
            {
                Console.WriteLine("timmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
            }
        }
        public void aa()
        {
            for (int i = 0; i < 99999; i++)
            {
                Console.WriteLine("for");
            }
        }
    }
}
