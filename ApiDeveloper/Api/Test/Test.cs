using BeerSting.Api.Database;
using BeerSting.Api.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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

namespace BeerSting.Api.Test
{
    class Test
    {
        public void Tester()
        {
            Config config = new Config();
            config.dbtype = BeerSting.Api.Enums.Database.SQLSERVER;
            config.server = "localhost";
            config.port = "";
            config.dbname = "ISTAppRestaurant";
            config.username = "sa";
            config.password = "P@ssw0rd";

            IDB idb = DBManager.InitDB(config);
            DataTable dt = idb.Query("select * from Orders");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine(dt.Rows[i]["MenuItemName"] + ". " + dt.Rows[i]["PortionName"]);
            }

            idb.Close();
        }
    }
}
