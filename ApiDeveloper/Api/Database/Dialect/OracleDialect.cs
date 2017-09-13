using System;
using System.Collections.Generic;
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

namespace BeerSting.Api.Database.Dialect
{
    public class OracleDialect
    {
        public String getLimitString(String sql, int offset, int limit)
        {
            sql = sql.Trim();
            bool isForUpdate = false;
            if (sql.ToLower().EndsWith(" for update"))
            {
                sql = sql.Substring(0, sql.Length - 11);
                isForUpdate = true;
            }

            StringBuilder pagingSelect = new StringBuilder(sql.Length + 100);
            if (offset > 0)
            {
                pagingSelect.Append("select * from ( select row_.*, rownum rownum_ from ( ");
            }
            else
            {
                pagingSelect.Append("select * from ( ");
            }
            pagingSelect.Append(sql);
            if (offset > 0)
            {
                //			int end = offset+limit;
                String endString = offset + "+" + limit;
                pagingSelect.Append(" ) row_ ) where rownum_ <= " + endString + " and rownum_ > " + offset);
            }
            else
            {
                pagingSelect.Append(" ) where rownum <= " + limit);
            }

            if (isForUpdate)
            {
                pagingSelect.Append(" for update");
            }

            return pagingSelect.ToString();
        }
  
    }
}
