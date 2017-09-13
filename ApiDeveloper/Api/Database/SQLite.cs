using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.IO;

#region Creator
/// 
/// <summary>
/// 	  @author Programer Beer <br>
/// <b>Copyright: </b><br>
/// 2010, Yoottapong<br>
/// 
/// <b>Create by: </b><br>
/// Yoottapong Wongwiwut<br>  
/// 
/// <b>Create Date: </b><br>
///  July 07 2010<br>
/// 
/// <b>Email: </b><br>
/// <A href="mailto:beer.sting@hotmail.com">beer.sting@hotmail.com</A><br> 
///	  @version 1.0
/// 
/// </summary>
#endregion

namespace BeerSting.Api.Database
{
    class SQLite : IDB
    {
        SQLiteConnection m_Connection = null;

        #region IDB Members

        public string DatabaseType { get { return BeerSting.Api.Enums.Database.SQLLITE.ToString(); ; } }

        public void Connect(object dbConnection)
        {
            m_Connection = (SQLiteConnection)dbConnection;
        }

        public void Connect(Config config)
        {
            //string dbFile = (new DirectoryInfo(Directory.GetCurrentDirectory())).FullName + "\\" + dbname;
            string dbFile = config.path;
            string connStr = String.Format("Version=3;New=False;Compress=False;Data Source={0}", dbFile);
            m_Connection = new SQLiteConnection(connStr);
            m_Connection.Open();
        }

        public void Connect(string server,string port, string dbname, string username, string password)
        {
         
        }

        public System.Data.DataTable Query(string query, params object[] param)
        {
            query = string.Format(query, param);
            SQLiteDataAdapter adapt = new SQLiteDataAdapter(query, m_Connection);

            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            //if (dt.Rows.Count == 0)
            //{ dt.Dispose(); dt = null; }
            return dt;

        }

        public int ExecuteNonQuery(string query, params object[] param)
        {
            if (param.Length > 0) query = string.Format(query, param);
            SQLiteCommand command = new SQLiteCommand(query, m_Connection);
            int ret = command.ExecuteNonQuery();
            return ret;
        }

        public object ExecuteScalar(string query, params object[] param)
        {
            query = string.Format(query, param);
            SQLiteCommand command = new SQLiteCommand(query, m_Connection);
            object ret = command.ExecuteScalar();
            return ret;
        }

        public int insertQuery(string table, string[] fields, object[] values)
        {
            try
            {
                StringBuilder cmd = new StringBuilder("INSERT INTO " + table + "(");
                int i = 1;
                foreach (string field in fields)
                {
                    if (i == fields.Length)
                    {
                        cmd.Append(field + ")");
                        //cmd.Append(field + " = @" + field + " ");
                    }
                    else
                    {
                        cmd.Append(field + ", ");
                    }
                    i++;
                }

                cmd = cmd.Append(" VALUES (");

                i = 1;
                foreach (string field in fields)
                {
                    if (i == fields.Length)
                    {
                        cmd.Append("@" + field + ");");
                    }
                    else
                    {
                        cmd.Append("@" + field + ", ");
                    }
                    i++;
                }

                SQLiteCommand command = new SQLiteCommand();
                command.Connection = m_Connection;
                command.CommandText = cmd.ToString();
                int j = 0;
                foreach (object value in values)
                {
                    command.Parameters.AddWithValue("@" + fields[j], value);
                    j++;
                }
                int ret = command.ExecuteNonQuery();
                return ret;
                //m_Connection.Close(); 
                // return command.CommandText;        //"INSERT INTO picture(Name, Type, Format, Path, explanation, Picture, Template, Create_date, Add_id, Chk_id) VALUES (:Name, @Type, @Format, @Path, @explanation, @Picture, @Template, @Create_date, @Add_id, @Chk_id)";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateQuery(string table, string[] fields, object[] values, string conditionField, object conditionValue)
        {
            try
            {
                StringBuilder cmd = new StringBuilder("UPDATE " + table + " SET ");
                int i = 1;
                foreach (string field in fields)
                {
                    if (i == fields.Length)
                    {
                        cmd.Append(field + " = @" + field + " ");
                    }
                    else
                    {
                        cmd.Append(field + " = @" + field + ", ");
                    }
                    i++;
                }

                cmd.Append("WHERE (" + conditionField + " = @" + "search_" + conditionField + ");");
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = m_Connection;
                command.CommandText = cmd.ToString();
                int j = 0;
                foreach (object value in values)
                {
                    command.Parameters.AddWithValue("@" + fields[j], value);
                    j++;
                }
                command.Parameters.AddWithValue("@" + "search_" + conditionField, conditionValue);
                int ret = command.ExecuteNonQuery();
                return ret;
                //return command.CommandText;  // ผลลัพธ์ เช่น UPDATE cf_user SET strLastName = @strLastName, strFirstName = @strFirstName WHERE (nID = @nID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Execute(string query, IDictionary parameters)
        {
            SQLiteCommand command = new SQLiteCommand(query, m_Connection);
            foreach (DictionaryEntry parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
            }
            int ret = command.ExecuteNonQuery();
            return ret;
        }

        public void Close()
        {
            m_Connection.Close();
            m_Connection.Dispose();
        }

        #endregion

        string IDB.DatabaseType
        {
            get { throw new NotImplementedException(); }
        }

        void IDB.Connect(Config config)
        {
            throw new NotImplementedException();
        }

        void IDB.Connect(string server, string port, string dbname, string username, string password)
        {
            throw new NotImplementedException();
        }

        DataTable IDB.Query(string query, params object[] param)
        {
            throw new NotImplementedException();
        }

        int IDB.ExecuteNonQuery(string query, params object[] param)
        {
            throw new NotImplementedException();
        }

        object IDB.ExecuteScalar(string query, params object[] param)
        {
            throw new NotImplementedException();
        }

        int IDB.insertQuery(string table, string[] fields, object[] values)
        {
            throw new NotImplementedException();
        }

        int IDB.updateQuery(string table, string[] fields, object[] values, string conditionField, object conditionValue)
        {
            throw new NotImplementedException();
        }

        int IDB.Execute(string query, IDictionary parameters)
        {
            throw new NotImplementedException();
        }

        void IDB.Close()
        {
            throw new NotImplementedException();
        }
    }
}
