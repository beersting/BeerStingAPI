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
    public static class DBManager
    {
        //internal static IDB DB = null;
        public static IDB DB = null;
        public static IDB InitDB(Config config)
        {
            IDB idb = null; 
            switch (config.dbtype)
            {
                case BeerSting.Api.Enums.Database.MYSQL:
                    {
                        idb = new MySQL();
                    } break;
                case BeerSting.Api.Enums.Database.SQLSERVER:
                    {
                        idb = new SQLServer();
                    } break;
                case BeerSting.Api.Enums.Database.ORACLE:
                    {
                        //idb = new Oracle();
                    } break;
                case BeerSting.Api.Enums.Database.SQLLITE:
                    {
                        //idb = new SQLite();
                    } break;
                case BeerSting.Api.Enums.Database.ACCESS:
                    {
                        idb = new Access();
                    } break;
                case BeerSting.Api.Enums.Database.Excel:
                    {
                        idb = new Excel();
                    } break;
            }
            idb.Connect(config);
            DB = idb;
            return idb;
        }
    }
}
