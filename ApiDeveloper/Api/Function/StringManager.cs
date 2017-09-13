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

    public class StringManager
    {
        public static String toString(Object ob)
        {
            return (ob == null)?"":ob.ToString();
        }

        public static String cut(String text, int length, String text_affix)
        {
            if (text != null)
            {
                if (text.Length > length) text = text.Substring(0, length) + text_affix;
            }
            return (text == null) ? "" : text;
        }

        public static String affix(String text, int length, String text_affix)
        {
            if (text != null)
            {
                if (text.Length < length)
                {
                    int m_length = length - text.Length;
                    for (int i = 0; i < m_length; i++)
                    {
                        text = text + text_affix;
                    }
                }
            }
            return (text == null) ? "" : text;
        }

        public static String getIsNULLtoEmpty(String val)
        {
            return (val == null) ? "" : val;
        }

        public static Object getIsNULLtoEmpty(Object val)
        {
            return (val == null) ? "" : val;
        }

        public static Object getNullOrEmptyToText(Object val, String returnText)
        {
            return (val == null || val.ToString().Trim() == "") ? returnText : val;
        }
    }
}