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
            var a = job.SelectToken("$.a.b2[?(@.c1 == 'a>b2>c1')]").Select(s => (string)s).ToList();
            Console.WriteLine(a);//'a>b2>c1','a>b2>c2'
            JToken a1 = job.SelectToken("$.a.b1[?(@.c1 == 'a>b2>c1')]");
            Console.WriteLine(a1);//null
            JToken a2 = job.SelectToken("$.a.b1");
            Console.WriteLine(a2);//{['a>b11','a>b12']}
            var t1 = JToken.Parse("['b>b1','a>b1']");
            job["b"] = JObject.Parse("{b1:'b1'}");//{b{b1:'b1'}}
            job["b"]["b1"] = JToken.Parse("['b>b11','b>b12']");
            JToken b1 = job.SelectToken("$.b.b1");
            Console.WriteLine(b1);//{['b>b11','b>b12']}
            job["a"]["b1"] = JToken.Parse("['a>b11s', 'a>b12s']");
            JToken a12 = job.SelectToken("$.a.b1");
            Console.WriteLine(a12);//{['a>b11s', 'a>b12s']}
            job["a"]["b3"] = JToken.Parse("['a>b31','a>b32']");
            JToken a13 = job.SelectToken("$.a.b3");
            Console.WriteLine(a13);//{['a>b31','a>b32']}

            //JArray jar = JArray.Parse(aa);
            //var va = jar.Select(s => s["c1"].ToString() == "a>b2>c1");
            //a1.ToList
        }
    }
}
