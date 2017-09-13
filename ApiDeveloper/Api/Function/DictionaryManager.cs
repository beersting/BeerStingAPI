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

    public class DictionaryManager
    {
        public static IDictionary<string, object> ToDictionary(Object o)
        {
            return ToDictionary(o, false);
        }
        public static IDictionary<string, object> ToDictionary(Object o, bool emptyData)
        {
            IDictionary<string, object> res = new Dictionary<string, object>();
            PropertyInfo[] props = o.GetType().GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].CanRead)
                {
                    if (emptyData)
                    {
                        res.Add(props[i].Name, props[i].GetValue(o, null));
                    }
                    else if (props[i].GetValue(o, null) != null)
                    {
                        res.Add(props[i].Name, props[i].GetValue(o, null));
                    }
                }
            }
            return res;
        }

    }

}