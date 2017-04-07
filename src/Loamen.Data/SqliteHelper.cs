using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Data.SQLite;

namespace Loamen.Data
{
    public abstract class SqliteHelper
    {
        private static string connectionString = @"Data Source=" + DbFileName + @";password=" + Password + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
        /// <summary>
        /// ConnectionString样例：Data Source=Test.db3;Pooling=true;FailIfMissing=false
        /// </summary>
        public static string ConnectionString
        {
            get
            {
               VerifyDatabaseExists();
               return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        private static string dbFileName;
        /// <summary>
        /// 数据库文件名，如：c:\My Docuements\db.db3
        /// </summary>
        public static string DbFileName
        {
            get { return dbFileName; }
            set
            {
                dbFileName = value;
                connectionString = @"Data Source=" + DbFileName + @";password=" + Password + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
            }
        }

        private static string password;
        /// <summary>
        /// 设置访问密码
        /// </summary>
        public static string Password
        {
            get { return SqliteHelper.password; }
            set 
            {
                SqliteHelper.password = value;
                connectionString = @"Data Source=" + DbFileName + @";password=" + Password + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";

            }
        }


        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sqlite_master where name = '" + TableName + "' and type='table'";
 
            object obj = GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

       
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
               SQLiteParameter myParameter = new SQLiteParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
               SQLiteParameter myParameter = new SQLiteParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
               SQLiteParameter myParameter = new SQLiteParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public static object GetSingle(string SQLString, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader ( 注意：调用该方法后，一定要对SQLiteDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader ExecuteReader(string strSQL)
        {
            
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
            try
            {
                connection.Open();
                SQLiteDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
       
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                            foreach (SQLiteParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SQLiteParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader ( 注意：调用该方法后，一定要对SQLiteDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SQLiteDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                throw e;
            }
            //			finally
            //			{
            //				cmd.Dispose();
            //				connection.Close();
            //			}	

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SQLite.SQLiteException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        public static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SQLiteParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 创建Sqlite数据库
        /// <summary>
        /// 判断数据库是否存在，如果不存在则创建
        /// </summary>
        public static void VerifyDatabaseExists()
        {
            if (!File.Exists(DbFileName))
            {
                SQLiteConnection.CreateFile(DbFileName);
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.SetPassword(Password);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static bool CopyTable(DataSet ds, string queryString)
        {
            int i = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                adapter.SelectCommand = new SQLiteCommand(queryString, connection);
                SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);

                try
                {
                    connection.Open();
                    i = adapter.Update(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    adapter.Dispose();
                    connection.Close();
                }
            }

            return i > 0;
        }

        public static int CopyTableBySql(DataSet ds, string TableName, string pkColumnName)
        {
            int res = 0;
            List<string> listSql = new List<string>();
            StringBuilder sbWhere = new StringBuilder();

            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        #region insertSql
                        ArrayList sqlColumn = new ArrayList();
                        ArrayList sqlValue = new ArrayList();
                        #endregion

                        #region updateSql
                        ArrayList sqlUpdate = new ArrayList();
                        #endregion

                        #region whereSql
                        ArrayList sqlUpdateWhere = new ArrayList();
                        #endregion

                        #region
                        int count = 0;
                        if (!pkColumnName.Equals(""))
                        {
                            StringBuilder sql = new StringBuilder("select count(0) from ");
                            sql.Append(TableName);
                            sql.Append(" where ");
                            sql.Append(pkColumnName);
                            sql.Append("='");
                            sql.Append(dr[pkColumnName]);
                            sql.Append("'");
                            count = int.Parse(GetSingle(sql.ToString()).ToString());
                        }

                        if (count > 0)
                        {
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (dc.ColumnName == pkColumnName)
                                {
                                    if (!sqlUpdateWhere.Contains(dc.ColumnName + " = '" + dr[dc.ColumnName] + "'"))
                                    {
                                        sbWhere = new StringBuilder(pkColumnName);
                                        sbWhere.Append(" = '");
                                        sbWhere.Append(dr[pkColumnName]);
                                        sbWhere.Append("'");
                                        sqlUpdateWhere.Add(sbWhere.ToString());
                                    }
                                }
                                else
                                {
                                    if (!sqlUpdate.Contains(dc.ColumnName + " = '" + dr[dc.ColumnName] + "'"))
                                    {
                                        sbWhere = new StringBuilder(dc.ColumnName);
                                        sbWhere.Append(" = '");
                                        sbWhere.Append(dr[dc.ColumnName]);
                                        sbWhere.Append("'");
                                        sqlUpdate.Add(sbWhere.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                sqlColumn.Add(dc.ColumnName);
                                sbWhere = new StringBuilder("'");
                                sbWhere.Append(dr[dc.ColumnName]);
                                sbWhere.Append("'");
                                sqlValue.Add(sbWhere.ToString());
                            }
                        }
                        #endregion

                        StringBuilder strSql = new StringBuilder();

                        #region insert
                        if (sqlColumn.Count > 0)
                        {
                            strSql.Append("insert into ");
                            strSql.Append(TableName);
                            strSql.Append(" ( ");
                            strSql.Append(string.Join(",", ConvertArrayListToStringArray(sqlColumn)));
                            strSql.Append(" ) values ( ");
                            strSql.Append(string.Join(",", ConvertArrayListToStringArray(sqlValue)));
                            strSql.Append(" ) ");

                            listSql.Add(strSql.ToString());

                        }
                        #endregion

                        #region update
                        if (sqlUpdate.Count > 0)
                        {
                            strSql = new StringBuilder();
                            strSql.Append("update ");
                            strSql.Append(TableName);
                            strSql.Append(" set ");
                            strSql.Append(string.Join(",", ConvertArrayListToStringArray(sqlUpdate)));
                            strSql.Append(" where 1=1 ");

                            string[] alWhere = ConvertArrayListToStringArray(sqlUpdateWhere);
                            if (alWhere.Length > 1)
                            {
                                strSql.Append(string.Join(" and ", alWhere));
                            }
                            else if (alWhere.Length == 1)
                            {
                                strSql.Append(string.Concat(" and ", alWhere[0]));
                            }

                            listSql.Add(strSql.ToString());
                        }
                        #endregion

                        if (listSql.Count == 50)
                        {
                            int r = ExecuteSqlTran(listSql) == 50 ? 0 : 1;
                            if (r == 0)
                            {
                                listSql.Clear();
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                    res += ExecuteSqlTran(listSql) > 0 ? 0 : 1;
                }

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static bool CopyTable(DataSet ds, string queryString,string tableName)
        {
            int i = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                adapter.SelectCommand = new SQLiteCommand(queryString, connection);
                SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
                //adapter.InsertCommand = builder.GetInsertCommand();
                //adapter.UpdateCommand = builder.GetUpdateCommand();
                //adapter.DeleteCommand = builder.GetDeleteCommand(); 

                try
                {
                    connection.Open();
                    DataSet dsOld = new DataSet();
                    adapter.Fill(dsOld, tableName);
                    //dsOld.AcceptChanges();
                    //dsOld.Tables[0].Merge(ds.Tables[0]);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow drNew = dsOld.Tables[0].NewRow();
                        drNew.ItemArray = dr.ItemArray;
                        dsOld.Tables[0].Rows.Add(drNew);
                    } 
                   // DataSet d = dsOld.GetChanges();
                    i = adapter.Update(dsOld, tableName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    adapter.Dispose();
                    connection.Close();
                }
            }

            return i > 0;
        }

        public static string[] ConvertArrayListToStringArray(ArrayList al)
        {
            string[] strs = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                strs[i] = al[i].ToString();
            }
            return strs;
        }
        #endregion
    }
}
