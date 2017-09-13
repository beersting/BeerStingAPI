using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools.Excel.Extensions;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Excels;
using System.Diagnostics;
using BeerSting.Api.Function;

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

namespace Microsoft.Excels
{
   public class ExcelManager
    {
       public static Excel.Worksheet getSheetName(Excel.Workbook wbk, String name)
        {
            Excel.Worksheet sheet = null;
            foreach (Excel.Worksheet nSheet in wbk.Sheets)
            {
                if (nSheet.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    sheet = nSheet;
                    break;
                }
            }
            return sheet;
        }

       public static bool SheetExists(Excel.Workbook wbk, string sheetName)
        {
            for (int i = 1; i <= wbk.Worksheets.Count; i++)
            {
                if (((Excel.Worksheet)wbk.Worksheets[i]).Name == sheetName)
                {
                    return true;
                }
            }

            return false;
        }

       public static void copySheet(String sheetName, Excel.Workbook sourceWorkbook, Excel.Workbook targetWorkbook)
        {
            Excel.Worksheet ws;
            ws = (Excel.Worksheet)sourceWorkbook.Sheets.get_Item(sheetName);
            ws.Copy(targetWorkbook.Worksheets[1]);
        }

       public static object fuction(Excel.Worksheet sheet, string refExcel)
       {
           return fuction(sheet, refExcel, null);
       }

       public static object fuction(Excel.Worksheet sheet, string refExcel, object val)
       {
           object ref_val = null;
           if (val != null) sheet.get_Range(refExcel).Formula = val;
           ref_val = StringManager.toString(sheet.get_Range(refExcel).Value2);            
           return ref_val;
       }

       public static object value(Excel.Worksheet sheet, string refExcel)
       {
           return value(sheet, refExcel, null);
       }

       public static object value(Excel.Worksheet sheet, string refExcel, object val)
       {
           object ref_val = null;
           if (val != null) sheet.get_Range(refExcel).Value = val;
           if (val == null) ref_val = sheet.get_Range(refExcel).Value2;
           return ref_val;
       }

    }
}

