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

	public class ArrayManager
	{

        public static String toString(Object[] array)
        {
            return ArrayManager.toString(array, "", 0, array.Length - 1);
        }

        public static String toString(Object[] array, int index)
        {
            return ArrayManager.toString(array, "", index, array.Length - 1);
        }

        public static String toString(Object[] array, int index, int last)
        {
            return ArrayManager.toString(array, "", index, last);
        }

        public static String toString(Object[] array, String separator)
        {
            return ArrayManager.toString(array, separator, 0, array.Length - 1);
        }

        public static String toString(Object[] array, String separator, int index)
        {
            return ArrayManager.toString(array, separator, index, array.Length - 1);
        }

        public static String toString(Object[] array, String separator, int index, int last)
        {
            String result = "";
            if (array == null || array.Length == 0) return "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i < index)
                {
                    continue;
                }
                if (i > last) break;
                // result += (String) ((i==index)? array[i]:separator + array[i]);
                result += (((i == index) ? array[i] : separator + array[i])).ToString();
            }
            return result;
        }
	}

}