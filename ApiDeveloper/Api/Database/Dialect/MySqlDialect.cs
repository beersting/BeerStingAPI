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
    public class MySQLDialect
    {
        public String getLimitString(String sql, int offset, int limit)
        {
            return new StringBuilder(sql.Length + 40)
                   .Append(sql)
                   .Append(offset > 0 ? " limit " + offset + "," + limit : " limit " + offset)
                   .ToString();
        }   
  
    }
}
