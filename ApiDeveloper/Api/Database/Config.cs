using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerSting.Api.Enums;

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
   public class Config
    {
       public String classname { get; set; }
       public BeerSting.Api.Enums.Database dbtype { get; set; }
       public String server { get; set; }
       public String path { get; set; }
       public String port { get; set; }
       public String service { get; set; }
       public String dbname { get; set; }
       public String username { get; set; }
       public String password { get; set; }
       public Os os { get; set; } 
    }

}
