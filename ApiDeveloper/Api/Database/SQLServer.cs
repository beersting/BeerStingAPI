using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

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
   public class SQLServer : IDB 
    {
        SqlConnection m_Connection = null;

        #region IDB Members

        public string DatabaseType { get { return BeerSting.Api.Enums.Database.SQLSERVER.ToString() ; } }

        public void Connect(object dbConnection)
        {
            m_Connection = (SqlConnection) dbConnection;
        }


        public void Connect(Config config)
        {
            Connect(config.server, config.port, config.dbname, config.username, config.password); 
        }

        public void Connect(string server, string port, string dbname, string username, string password)
        {
            string connStr = String.Format("Data Source={0}; user id={2}; password={3}; database={4};",
                         server, port, username, password, dbname); 

            m_Connection = new SqlConnection(connStr);
            m_Connection.Open();
        }

        public System.Data.DataTable Query(string query, params object[] param)
        {
            query = string.Format(query, param);
            SqlDataAdapter adapt = new SqlDataAdapter(query, m_Connection);

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
            SqlCommand command = new SqlCommand(query, m_Connection);
            int ret = command.ExecuteNonQuery();
            return ret;
        }

//____________ วิธีการใช้งาน
   //  DBManager.DB.ExecuteNonQuery("UPDATE cf_user SET strLastName = '{0}', strFirstName = '{1}' WHERE nID = {2:d}", "Beer_1", "Beer_2", 2);


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
                    string field_name = field.Replace("'", "");
                    field_name = field.Replace(" ", "_");
                    if (i == fields.Length)
                    {
                        cmd.Append("@" + field_name + ");");
                    }
                    else
                    {
                        cmd.Append("@" + field_name + ", ");
                    }
                    i++;
                }

                SqlCommand command = new SqlCommand();
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
 //____________ วิธีการใช้งาน
   //DBManager.DB.insertQuery("cf_attachment", new string[] { "strPath", "nCirculationHistoryId" }, new object[] { "WCW Sting", 5 });
       

        public int updateQuery(string table, string[] fields, object[] values, string conditionField, object conditionValue)
        {
            try
            {
                StringBuilder cmd = new StringBuilder("UPDATE " + table + " SET ");
                int i = 1;

                foreach (string field in fields)
                {
                    string field_name = field.Replace("'", "");
                    field_name = field.Replace(" ", "_");
                    if (i == fields.Length)
                    {
                        cmd.Append(field + " = @" + field_name + " ");
                    }
                    else
                    {
                        cmd.Append(field + " = @" + field_name + ", ");
                    }
                    i++;
                }


                cmd.Append("WHERE (" + conditionField + " = @" + "search_" + conditionField + ");");
                SqlCommand command = new SqlCommand();
                command.Connection = m_Connection;
                command.CommandText = cmd.ToString();
                int j = 0;
                foreach (object value in values)
                {
                    command.Parameters.AddWithValue("@" + fields[j],value);
                    j++;
                }
                command.Parameters.AddWithValue("@" + "search_"+conditionField, conditionValue);
                int ret = command.ExecuteNonQuery();
                return ret;
                //return command.CommandText;  // ผลลัพธ์ เช่น UPDATE cf_user SET strLastName = @strLastName, strFirstName = @strFirstName WHERE (nID = @nID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 //____________ วิธีการใช้งาน
   //DBManager.DB.updateQuery("cf_attachment", new string[] { "strPath", "nCirculationHistoryId" }, new object[] { "TNA Sting", 3 }, "nID", 3);


        public int Execute(string query, IDictionary parameters)
        {
            SqlCommand command = new SqlCommand(query, m_Connection);
            foreach (DictionaryEntry parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
            }
            int ret = command.ExecuteNonQuery();
            return ret;
        }
 //____________ วิธีการใช้งาน

        //   IDictionary<string, object> iDictionary = new Dictionary<string, object>();
        //   iDictionary.Add("@strLastName", "LastBeer");
        //   iDictionary.Add("@strFirstName", "FirstBeer");
        //   iDictionary.Add("@nID", 2);
        //   DBManager.DB.Execute("UPDATE cf_user SET strLastName = @strLastName, strFirstName = @strFirstName WHERE (nID = @nID);", (IDictionary)iDictionary);



        public object ExecuteScalar(string query, params object[] param)
        {
            query = string.Format(query, param);
            SqlCommand command = new SqlCommand(query, m_Connection);
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
