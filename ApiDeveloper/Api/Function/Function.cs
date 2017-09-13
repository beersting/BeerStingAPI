using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
    class Function
    {
         // การใช้ฟังก์ชั่น DateDiff นี้ ให้ระวังเรื่องของเวลาด้วย เพราะมันจะเอาค่าเวลามาทำการคำนวณด้วย
         public double DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate)
         {
             double diff = 0;
             System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);

             switch (howtocompare.ToLower())
             {
                 case "year":
                     diff = Convert.ToDouble(TS.TotalDays / 365); // เดาว่า TS.TotalDays น่าจะคือจำนวนวันผลลัพธ์ที่ลบกันแล้ว มาหารด้วย 365 คือจำนวนวันในแต่ละปี ก็จะได้จำนวนปี
                     break;
                 case "month":
                     diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                     break;
                 case "day":
                     diff = Convert.ToDouble(TS.TotalDays);
                     break;
                 case "hour":
                     diff = Convert.ToDouble(TS.TotalHours);
                     break;
                 case "minute":
                     diff = Convert.ToDouble(TS.TotalMinutes);
                     break;
                 case "second":
                     diff = Convert.ToDouble(TS.TotalSeconds);
                     break;
             }

             return diff;
         }

         public static string CalSize(long size)
         {

             if (size < 1024)
             {
                 return ((Math.Round((Double)size, 2)) + " B");
             }
             else if (size < 1024L * 1024)
             {
                 return ((Math.Round((Double)(size / 1024), 2)) + " KB");
             }
             else if (size < (1024L * 1024 * 1024))
             {
                 return ((Math.Round((Double)size / (1024 * 1024), 2)) + " MB");
             }
             else if (size < (1024L * 1024 * 1024 * 1024))
             {
                 return ((Math.Round((Double)size / (1024 * 1024 * 1024), 2)) + " GB");
             }
             else if (size < (1024L * 1024 * 1024 * 1024 * 1024))
             {
                 return ((Math.Round((Double)size / (1024L * 1024 * 1024 * 1024), 2)) + " TB");
             }
             return "" + size;
         }
    }
}
