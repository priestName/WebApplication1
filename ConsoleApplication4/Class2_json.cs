using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ConsoleApplication4
{
    class Class2_json
    {
        //json
        public void saa()
        {
            string aa = @"{
                            a:{
                                b:'a>b',
                                b1:['a>b11','a>b12'],
                                b2:[
                                    {c1:'a>b2>c1',c2:'a>b2>c2'},
                                    {c1:'a>b2>c11',c2:'a>b2>c21'}
                                ]
                            }
                        }";
            JObject job = JObject.Parse(aa);
            var a = job.SelectToken("$.a.b2[?(@.c1 == 'a>b2>c1')]").Select(s => (string)s).ToList() ;

            JToken a1 = job.SelectToken("$.a.b2[?(@.c1 == 'a>b2>c1')]");
            //var va= jar.Select(s=>s["c1"].ToString()== "a>b2>c1");
            Console.WriteLine(a);
            Console.WriteLine(a1);
        }
    }
}
