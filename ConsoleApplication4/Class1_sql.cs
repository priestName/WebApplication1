using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication4
{
    class Class1_sql
    {
        //sql参数化
        public void saa()
        {
            string a = "@a1+@a2=@a3";
            SqlParameter[] spr ={
            new SqlParameter("@a1",SqlDbType.VarChar),
            new SqlParameter("@a2",SqlDbType.VarChar),
            new SqlParameter("@a3",SqlDbType.VarChar)
            };
            spr[0].Value = "1";
            spr[1].Value = "2";
            spr[2].Value = "3";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = a;
            foreach (SqlParameter parm in spr)
                if (!cmd.Parameters.Contains(parm.ParameterName))
                    cmd.Parameters.Add(parm);
        }
    }
}
