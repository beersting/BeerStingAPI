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

namespace BeerSting.Api.Action.Web
{
    public class AjaxResult
    {
        public String id { get; set; }
        public String status { get; set; }
        public String name { get; set; }
        public String text { get; set; }
        public Object value { get; set; }
        public String comment { get; set; }
        //public DateTime create_Date { get; set; }
    }
}
