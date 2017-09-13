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
    public class SQLServerDialect
    {
	public bool supportsLimit() {
		return true;
	}

	public bool supportsLimitOffset() {
		return true;
	}

	/**
	 * Add a LIMIT clause to the given SQL SELECT
	 *
	 * The LIMIT SQL will look like:
	 *
	 * WITH query AS
	 *      (SELECT TOP 100 percent ROW_NUMBER() OVER (ORDER BY CURRENT_TIMESTAMP) as __row_number__, * from table_name)
	 * SELECT *
	 * FROM query
	 * WHERE __row_number__ BETWEEN :offset and :lastRows
	 * ORDER BY __row_number__
	 * 
	 * @param querySqlString The SQL statement to base the limit query off of.
	 * @param offset         Offset of the first row to be returned by the query (zero-based)
	 * @param last           Maximum number of rows to be returned by the query
	 * @return A new SQL statement with the LIMIT clause applied.
	 */

	public String getLimitString(String querySqlString, int offset, int limit, bool dynamicSql) {
        StringBuilder pagingBuilder = new StringBuilder();
		String orderby = getOrderByPart(querySqlString);
		String distinctStr = "";

		String loweredString = querySqlString.ToLower();
		String sqlPartString = querySqlString;
		if (loweredString.Trim().StartsWith("select")) {
			int index = 6;
			if (loweredString.StartsWith("select distinct")) {
				distinctStr = "DISTINCT ";
				index = 15;
			}
			sqlPartString = sqlPartString.Substring(index);
		}
		pagingBuilder.Append(sqlPartString);

		// if no ORDER BY is specified use fake ORDER BY field to avoid errors
		if (orderby == null || orderby.Length == 0) {
			orderby = "ORDER BY CURRENT_TIMESTAMP";
		}

        StringBuilder result = new StringBuilder();
        if(dynamicSql == false){
          result.Append("WITH query AS (SELECT ")
                .Append(distinctStr)
                .Append("TOP 100 PERCENT ")
                .Append(" ROW_NUMBER() OVER (")
                .Append(orderby)
                .Append(") as __row_number__, ")
                .Append(pagingBuilder)
                .Append(") SELECT * FROM query WHERE __row_number__ BETWEEN ")
                .Append(offset).Append(" AND ").Append(limit)
                .Append(" ORDER BY __row_number__");
        }
        else
        {
          //*** Rewrite by BeerSting
          result.Append("WITH query AS (SELECT ")
                .Append(distinctStr)
                .Append("TOP 100 PERCENT ")
                .Append(" ROW_NUMBER() OVER (")
                .Append(orderby)
                .Append(") as __row_number__, queryView.* FROM (SELECT ")
                .Append(((pagingBuilder.ToString().ToLower().IndexOf("order by") != -1) ? pagingBuilder.ToString().Substring(0, pagingBuilder.ToString().ToLower().IndexOf("order by")) : "") + ") AS queryView")
                .Append(") SELECT * FROM query WHERE __row_number__ BETWEEN ")
                .Append(offset).Append(" AND ").Append(limit)
                .Append(" ORDER BY __row_number__");
        }
 
		return result.ToString();
	}

	static String getOrderByPart(String sql) {
		String loweredString = sql.ToLower();
		int orderByIndex = loweredString.IndexOf("order by");
		if (orderByIndex != -1) {
			// if we find a new "order by" then we need to ignore
			// the previous one since it was probably used for a subquery
			return sql.Substring(orderByIndex);
		} else {
			return "";
		}
	}
    }
}
