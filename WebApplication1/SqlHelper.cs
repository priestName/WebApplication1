using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    /// <summary>
    /// Sql 连接基类
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public static readonly string defaultConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static readonly string databaseName = new SqlConnection(defaultConnectionString).Database;

        #region ExecuteSuccess
        /// <summary>
        /// 返回执行结果：True 成功；False 失败 使用默认连接字符串
        /// </summary>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>执行是否成功</returns>
        public static bool ExecuteSuccess(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(defaultConnectionString, cmdType, cmdText, cmdParms) > 0 ? true : false; ;
        }


        /// <summary>
        /// 返回执行结果：True 成功；False 失败 使用默认连接字符串
        /// </summary>
        /// <param name="connStr">连接字符串</param>+
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>执行是否成功</returns>
        public static bool ExecuteSuccess(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(connStr, cmdType, cmdText, cmdParms) > 0 ? true : false;
        }


        /// <summary>
        /// 返回执行结果：True 成功；False 失败 使用默认连接字符串
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>执行是否成功</returns>
        public static bool ExecuteSuccess(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(conn, cmdType, cmdText, cmdParms) > 0 ? true : false;
        }
        #endregion


        #region ExcuteNonQuery
        /// <summary>
        /// 返回影响的行数：使用默认连接字符串
        /// </summary>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回影响的行数：使用指定连接字符串
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);
            return ExecuteNonQuery(conn, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回影响的行数：使用连接对象 SqlConnection
        /// </summary> 
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            catch (SqlException e)
            {
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());
                return -1;
            }

            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = PrepareCommand(conn, tran, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();

                tran.Commit();

                tran.Dispose();
                DisposeResource(conn, cmd);

                return val;
            }
            catch (SqlException e)
            {
                tran.Rollback();

                tran.Dispose();
                conn.Close();

                // 写入日志
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());;

                return -1;
            }
        }

        #endregion


        #region ExecuteReader
        /// <summary>
        /// 返回 SqlDataReader：使用默认连接字符串
        /// </summary>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回 SqlDataReader：使用指定连接字符串
        /// </summary>
        /// <param name="connStr">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExecuteReader(conn, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回 SqlDataReader：使用连接对象 SqlConnection
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = PrepareCommand(conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                cmd.Parameters.Clear();

                return rdr;
            }
            catch (SqlException e)
            {
                conn.Close();

                //写入日志
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());;
                return null;
            }
        }
        #endregion


        #region ExcuteScalar : return object
        /// <summary>
        /// 返回查询结果：使用默认连接字符串
        /// </summary>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteScalar(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回查询结果：使用指定连接字符串
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExecuteScalar(conn, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回查询结果：使用连接对象 SqlConnection
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = PrepareCommand(conn, null, cmdType, cmdText, cmdParms);

                object obj = cmd.ExecuteScalar();

                DisposeResource(conn, cmd);

                return obj;
            }
            catch (SqlException e)
            {
                conn.Close();

                //写入日志
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());
                return null;
            }
        }

        #endregion


        #region ExcuteScalar : return T
        /// <summary>
        /// 返回查询结果：使用默认连接字符串
        /// </summary>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>T</returns>
        public static T ExecuteScalar<T>(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return (T)ExecuteScalar(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回查询结果：使用指定连接字符串
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>T</returns>
        public static T ExecuteScalar<T>(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return (T)ExecuteScalar(connStr, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 返回查询结果：使用连接对象 SqlConnection
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>T</returns>
        public static T ExecuteScalar<T>(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return (T)ExecuteScalar(conn, cmdType, cmdText, cmdParms);
        }
        #endregion


        #region ExcuteDataTable

        /// <summary>
        /// 返回查询结果集：使用指定连接字符串
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataTable ExcuteDataTable(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExcuteDataTable(defaultConnectionString, cmdType, cmdText, cmdParms);
        }
        /// <summary>
        /// 返回查询结果集：使用指定连接字符串
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataTable ExcuteDataTable(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExcuteDataTable(conn, cmdType, cmdText, cmdParms);
        }



        /// <summary>
        /// 返回查询结果：使用连接对象 SqlConnection
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns></returns>
        public static DataTable ExcuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = PrepareCommand(conn, null, cmdType, cmdText, cmdParms);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);

                DisposeResource(conn, cmd, adapter);

                return table;
            }
            catch (SqlException e)
            {
                conn.Close();
                // 写入日志
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());;
                return null;
            }
        }
        #endregion


        #region ExcuteDataSet

        /// <summary>
        /// 返回查询结果集：使用指定连接字符串
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExcuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExcuteDataSet(defaultConnectionString, cmdType, cmdText, cmdParms);
        }
        /// <summary>
        /// 返回查询结果集：使用指定连接字符串
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExcuteDataSet(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExcuteDataSet(conn, cmdType, cmdText, cmdParms);
        }



        /// <summary>
        /// 返回查询结果：使用连接对象 SqlConnection
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns></returns>
        public static DataSet ExcuteDataSet(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = PrepareCommand(conn, null, cmdType, cmdText, cmdParms);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                DisposeResource(conn, cmd, adapter);

                return ds;
            }
            catch (SqlException e)
            {
                conn.Close();
                // 写入日志
                //Common.Logs.WriteLog(e, cmdText, cmdType.ToString());;
                return null;
            }
        }
        #endregion


        #region DataReaderToList


        /// <summary>  
        /// DataReader 转换为 List<T>  
        /// </summary>         
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>List<T></returns>
        public static IList<T> ExcuteList<T>(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExcuteList<T>(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>  
        /// DataReader 转换为 List<T>  
        /// </summary>  
        /// <param name="connStr">数据库连接字符串</param>        
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>List<T></returns>
        public static IList<T> ExcuteList<T>(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExcuteList<T>(conn, cmdType, cmdText, cmdParms);
        }



        /// <summary>  
        /// DataReader 转换为 List<T>  
        /// </summary>  
        /// <param name="conn">数据库连接对象</param>        
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>List<T></returns>  
        public static IList<T> ExcuteList<T>(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlDataReader reader = ExecuteReader(conn, cmdType, cmdText, cmdParms);

            IList<T> list = new List<T>();

            if (reader == null)
                return list;

            try
            {
                while (reader.Read())
                {
                    T t = System.Activator.CreateInstance<T>();
                    Type type = t.GetType();

                    // 循环字段  
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object tempValue = null;

                        if (reader.IsDBNull(i))
                        {
                            string typeName = type.GetProperty(reader.GetName(i)).PropertyType.FullName;
                            tempValue = GetDBNullValue(typeName);
                        }
                        else
                        {
                            tempValue = reader.GetValue(i);
                        }

                        try
                        {
                            type.GetProperty(reader.GetName(i)).SetValue(t, tempValue, null);
                        }

                        catch
                        {
                            //Common.Logs.WriteLog("colName: " + reader.GetName(i) + " \r\n colValue" + tempValue, e.Source);
                        }
                    }

                    list.Add(t);

                }

                reader.Close();

                return list;
            }
            catch
            {
                return null;
            }
        }

        #endregion


        #region DataReaderToModel

        /// <summary>  
        /// DataReader 转换为 Model 
        /// </summary>  
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Model</returns>  
        public static T ExcuteObject<T>(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExcuteObject<T>(defaultConnectionString, cmdType, cmdText, cmdParms);
        }


        /// <summary>  
        /// DataReader 转换为 Model  
        /// </summary>  
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Model</returns>  
        public static T ExcuteObject<T>(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(connStr);

            return ExcuteObject<T>(conn, cmdType, cmdText, cmdParms);
        }


        /// <summary>  
        /// DataReader 转换为 Model  
        /// </summary>  
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>Model</returns>  
        public static T ExcuteObject<T>(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlDataReader reader = ExecuteReader(conn, cmdType, cmdText, cmdParms);

            if (reader == null)
                return default(T);

            T t = System.Activator.CreateInstance<T>();
            Type type = t.GetType();

            if (reader.Read())
            {
                object tempValue;

                // 循环字段  
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tempValue = null;

                    if (reader.IsDBNull(i))
                    {
                        string typeName = type.GetProperty(reader.GetName(i)).PropertyType.FullName;
                        tempValue = GetDBNullValue(typeName);
                    }
                    else
                    {
                        tempValue = reader.GetValue(i);
                    }

                    try
                    {
                        type.GetProperty(reader.GetName(i)).SetValue(t, tempValue, null);
                    }
                    catch
                    {
                        //
                    }
                }

                reader.Close();

                return t;
            }
            else
                return default(T);

        }

        #endregion


        #region Command
        /// <summary>
        /// 返回 SqlCommand
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="trans">数据库事物</param>
        /// <param name="cmdType">Command 类型：Stored Procedure, Text, TableDirect.)</param>
        /// <param name="cmdText">存储过程名称 / T-SQL语句</param>
        /// <param name="cmdParms">Command 参数数组</param>
        /// <returns>SqlCommand</returns>
        static SqlCommand PrepareCommand(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 600;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {

                foreach (SqlParameter parm in cmdParms)
                    if (!cmd.Parameters.Contains(parm.ParameterName))
                        cmd.Parameters.Add((SqlParameter)parm);

            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                //Common.Logs.WriteLog("打开数据库：" + DateTime.Now.ToString(), cmdText);
            }

            return cmd;

        }
        #endregion


        #region Release resource
        /// <summary>
        /// 资源释放
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        /// <param name="adapter"></param>
        static void DisposeResource(SqlConnection conn, SqlCommand cmd, SqlDataAdapter adapter)
        {
            adapter.Dispose();

            DisposeResource(conn, cmd);
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        /// <param name="tran"></param>
        static void DisposeResource(SqlConnection conn, SqlCommand cmd, SqlTransaction tran)
        {
            tran.Dispose();

            DisposeResource(conn, cmd);
        }


        /// <summary>
        /// 资源释放
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        static void DisposeResource(SqlConnection conn, SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Dispose();

            conn.Close();

            //Common.Logs.WriteLog("关闭数据库：" + DateTime.Now.ToString(), cmd.CommandText);
        }
        #endregion


        #region DBNull
        /// <summary>  
        /// 返回值为DBNull的默认值  
        /// </summary>  
        /// <param name="typeFullName">数据类型的全称，类如：system.int32</param>  
        /// <returns>返回的默认值</returns>  
        static object GetDBNullValue(string typeName)
        {
            object obj = null;
            switch (typeName)
            {
                case "System.String":
                    obj = string.Empty;
                    break;
                case "System.Date":
                case "System.Datetime":
                    obj = DateTime.MinValue;
                    break;
                case "System.Decimal":
                case "System.Double":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    obj = 0;
                    break;
                case "System.Boolean":
                    obj = false;
                    break;
                default:
                    obj = null;
                    break;
            }

            return obj;
        }
        #endregion


        /// <summary>
        /// 使用事物执行带GO的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ExecuteSqlWithGoUseTran(String sql)
        {
            bool isSuccess = false;
            using (SqlConnection conn = new SqlConnection(defaultConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    //注： 此处以 换行_后面带0到多个空格_再后面是go 来分割字符串
                    String[] sqlArr = System.Text.RegularExpressions.Regex.Split(sql.Trim(), "\r\n\\s*go", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    foreach (string strsql in sqlArr)
                    {
                        if (strsql.Trim().Length > 1 && strsql.Trim() != "\r\n")
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    isSuccess = true;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    //Common.Logs.WriteLog(E, sql.ToString(), CommandType.Text.ToString());
                    isSuccess = false;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();

                }

            }
            return isSuccess;
        } 
    }
}
