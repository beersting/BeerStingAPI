using System.Collections.Generic;
using System.IO;
using BeerSting.Api.Enums;
using System;
using System.ComponentModel;
using System.Reflection;

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

namespace BeerSting.Api.Function
{

    public class DateTimeManager
    {
        public static DateTime toDate(String dateTime, String format)
        {
            DateTime date = DateTime.ParseExact(dateTime, format,
                             System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }

        public static String toString(DateTime date, String return_formate)
        {
            return date.ToString(return_formate);
        }

        public static String toString(String dateTime, String format, String return_format)
        {
            DateTime date = toDate(dateTime, format);
            return toString(date, return_format);
        }

    }

}