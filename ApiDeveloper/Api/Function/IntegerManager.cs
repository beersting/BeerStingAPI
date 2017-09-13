using System.Collections.Generic;
using System.IO;
using BeerSting.Api.Enums;
using System;

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

    public class IntegerManager
    {
        public static bool isNumber(String text)
        {
            int n;
            bool isNumeric = int.TryParse(text, out n);
            return isNumeric;
        }
    }
}