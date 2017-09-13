using System.Collections;

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
   public interface IDB
    {
        string DatabaseType { get; }
        void Connect(Config config);
        void Connect(object dbConnection);
        void Connect(string server, string port, string dbname, string username, string password);
        System.Data.DataTable Query(string query, params object[] param);
        int ExecuteNonQuery(string query, params object[] param);
        object ExecuteScalar(string query, params object[] param);
        int insertQuery(string table, string[] fields, object[] values);
        int updateQuery(string table, string[] fields, object[] values, string conditionField, object conditionValue);
        int Execute(string query, IDictionary parameters);
        void Close();
   }
}
