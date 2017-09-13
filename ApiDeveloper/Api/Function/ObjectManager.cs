using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace BeerSting.Api.Function
{
    public static class ObjectManager
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();
            foreach (KeyValuePair<string, object> item in source)
            {
                var propertyFieldList = someObjectType.GetProperties().Where(c => c.Name.ToLower() == item.Key.ToLower());
                if (propertyFieldList.ToList().Count > 0)
                {
                    var propertyField = propertyFieldList.ToList()[0];
                    if (propertyField.CanWrite && item.Value != null && item.Value.ToString() != "")
                    {
                        propertyField.SetValue(someObject, item.Value, null);
                    }
                }
            }

            return someObject;
        }
    }
}
