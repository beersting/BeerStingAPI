using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.IO;

#region Creator
/**
 * 
 	  @author BeerSting <br>
 	     * <b>The MIT License (MIT) Copyright: </b><br>
		 * Copyright (c) 2017, BeerSting<br>
		 * 
		 * <b>Create by: </b><br>
		 * Yoottapong Wongwiwut<br>  
		 * 
		 * <b>Create Date: </b><br>
		 *  July 07 2017<br>
		 * 
		 * <b>Email: </b><br>
		 * <A href="mailto:beer.sting@gmail.com">beer.sting@gmail.com</A><br> 
	  @version 1.0
 * 
 */
#endregion

namespace BeerSting.Api.Database
{
    class Access : IDB
    {
        OleDbConnection m_Connection = null;

        #region IDB Members

        public string DatabaseType { get { return BeerSting.Api.Enums.Database.ACCESS.ToString(); } }

        public void Connect(object dbConnection)
        {
            m_Connection = (OleDbConnection)dbConnection;
        }

        public void Connect(Config config)
        {
            Connect(config.server, config.port, config.dbname, config.username, config.password);
        }

        public void Connect(string server, string port, string dbname, string username, string password)
        {
            //dbname =  dbname;
            string connStr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={4};",
                         server, port, username, password, dbname); 
            m_Connection = new OleDbConnection(connStr);
            m_Connection.Open();
        }

        public System.Data.DataTable Query(string query, params object[] param)
        {
            query = string.Format(query, param);
            OleDbDataAdapter adapt = new OleDbDataAdapter(query, m_Connection);

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
            OleDbCommand command = new OleDbCommand(query, m_Connection);
            int ret = command.ExecuteNonQuery();
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

                OleDbCommand command = new OleDbCommand();
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
                OleDbCommand command = new OleDbCommand();
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
            OleDbCommand command = new OleDbCommand(query, m_Connection);
            foreach (DictionaryEntry parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
            }
            int ret = command.ExecuteNonQuery();
            return ret;
        }

        public object ExecuteScalar(string query, params object[] param)
        {
            query = string.Format(query, param);
            OleDbCommand command = new OleDbCommand(query, m_Connection);
            object ret = command.ExecuteScalar();
            return ret;
        }

        public void Close()
        {
            m_Connection.Close();
            m_Connection.Dispose();
        }

        #endregion
    }
}






