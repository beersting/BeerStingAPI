using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BeerSting.Api.Database;

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
    class ExcelTestQuery
    {
        public void query()
        {
            Config config = new Config();
            config.dbtype = BeerSting.Api.Enums.Database.Excel;
            //config.path = @"D:\Test\ExcallToCSV\GENDER_MASTER 2003.xls";
            config.path = @"D:\SimPlus Model\Test\MainMenu.xlsm";
            IDB idb = DBManager.InitDB(config);
            //DataTable dt = idb.Query("select * from [GENDER_MASTER$A:B]");
            //DataTable dt = idb.Query("select * from [Sheet4$A:B]");
            DataTable dt = idb.Query("select * from [SimPlusDB$D2:I]");
            //DataTable dt = idb.Query("select * from [SimPlusDB$]");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine(dt.Rows[i][0] + ". " + dt.Rows[i][1]);
            }

            idb.Close();
        }
    }
}
