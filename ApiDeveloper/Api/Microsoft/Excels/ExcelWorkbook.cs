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
using BeerSting.Api.Enums;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;

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
    public class ExcelWorkbook
    {
        public Excel.Workbook workBook { get; set; }
        public BackgroundWorker backgroundWorker { get; set; }
        private int progress;
        public ExcelWorkbook(Excel.Workbook workBook)
        {
            this.workBook = workBook;
        }

        public ExcelWorkbook(Excel.Workbook workBook, BackgroundWorker backgroundWorker)
        {
            this.workBook = workBook;
            this.backgroundWorker = backgroundWorker;
        }

        public class Data
        {
            public string Sheet { get; set; }
            public string Cell { get; set; }
            public string ExcelFormula { get; set; }
            public string DataFormula { get; set; }
            public string Value { get; set; }
            public DatabaseAction Action { get; set; }
        }

        public List<Data> getAllValues(String sheetName)
        {
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)workBook.Sheets[sheetName];
            //Debug.Print(" ------------- Sheet name = " + xlWorksheet.Name);
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //Excel.Range formulaCell = xlWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeFormulas,
            //    Type.Missing);

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //Debug.Print("rowCount = " + rowCount);
            //Debug.Print("colCount = " + colCount);

            List<Data> listData = new List<Data>();
            for (int i = 1; i <= rowCount; i++)
            {

                for (int j = 1; j <= colCount; j++)
                {
                    //MessageBox.Show(xlRange.Cells[i, j].Value2.ToString());
                    Excel.Range value = (Excel.Range)xlRange.Cells[i, j];
                    //Debug.Print("------------------ Rang -----------------");
                    ////Debug.Print("Range = " + value.Range.Text.ToString());
                    //Debug.Print("Address = " + value.Address);
                    //Debug.Print("AddressLocal = " + value.AddressLocal);
                    //Debug.Print("Text = " + value.Text);
                    //Debug.Print("ListObject = " + value.ListObject);
                    //Debug.Print("CountLarge = " + value.Cells.Columns.CountLarge);
                    //Debug.Print("Count = " + value.Cells.Columns.Count);
                    //Debug.Print("Value = " + ((value.Value != null) ? value.Value.ToString() : ""));
                    //Debug.Print("Value2 = " + ((value.Value2 != null) ? value.Value2.ToString() : ""));
                    //Debug.Print("Value2 = " + value.get_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault));
                    //Debug.Print("Formula = " + value.Formula.ToString());
                    //Debug.Print("FormulaArray = " + value.FormulaArray);
                    //Debug.Print("FormulaHidden = " + value.FormulaHidden);
                    //Debug.Print("FormulaLocal = " + value.FormulaLocal);
                    //Debug.Print("FormulaR1C1 = " + value.FormulaR1C1);
                    //Debug.Print("FormulaR1C1Local = " + value.FormulaR1C1Local);
                    //Debug.Print("Type = " + value.GetType());

                    Excels.ExcelWorkbook.Data data = new ExcelWorkbook.Data();
                    data.Sheet = xlWorksheet.Name;
                    data.Cell = value.Address;
                    data.ExcelFormula = value.Formula.ToString();
                    data.DataFormula = "";
                    data.Value = ((value.Value2 != null) ? value.Value2.ToString() : "");
                    listData.Add(data);
                }
            }
            return listData;
        }

        public int getCountFormula()
        {
            int count = 0;
            foreach (var item in workBook.Sheets)
            {
                Excel.Worksheet xlWorksheet = ((Excel.Worksheet)item);
                Excel.Range xlRange = xlWorksheet.UsedRange;
                if (xlWorksheet.Visible == Excel.XlSheetVisibility.xlSheetHidden || xlRange.Count == 1) continue;
                Excel.Range CelFormula = xlRange.SpecialCells(Excel.XlCellType.xlCellTypeFormulas, Missing.Value);
                count += CelFormula.Count;
            }
            return count;
        }

        public List<Data> getValuesFormula(String sheetName, Boolean flagDataToOffline, params string[] func)
        {
            //Debug.Print(" ------------- Sheet name = " + sheetName);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)workBook.Sheets[sheetName];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            List<Data> listData = new List<Data>();
            if (xlWorksheet.Visible == Excel.XlSheetVisibility.xlSheetHidden || xlRange.Count == 1) return listData;
            Excel.Range CelFormula = xlRange.SpecialCells(Excel.XlCellType.xlCellTypeFormulas, Missing.Value);
            foreach (Excel.Range Cel in CelFormula)
            {
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        break;
                    }
                    this.progress += 1;
                    backgroundWorker.ReportProgress(this.progress);
                    System.Threading.Thread.Sleep(1);
                }

                Excels.ExcelWorkbook.Data data = new ExcelWorkbook.Data();
                data.Sheet = xlWorksheet.Name;
                data.Cell = Cel.Address;
                //Debug.WriteLine("---" + data.Cell);
                data.ExcelFormula = Cel.Formula.ToString();
                data.DataFormula = "";
                data.Value = ((Cel.Value2 != null) ? Cel.Value2.ToString() : "");
                if (flagDataToOffline)
                {
                    if (func == null)
                    {
                        Cel.Value = data.Value;
                        listData.Add(data);
                    }
                    else
                    {
                        foreach (var item in func)
                        {
                            if (data.ExcelFormula.IndexOf(item) != -1)
                            {
                                Cel.Value = data.Value;
                                listData.Add(data);

                                //*** Format PickList
                                //bool isPickList = false;
                                //string Formula1 = "";
                                //try
                                //{
                                //    Cel.Activate();
                                //    Formula1 = Cel.Validation.Formula1;
                                    
                                //    isPickList = true;
                                //}
                                //catch (Exception)
                                //{

                                //    isPickList = false;
                                //}

                                //if (isPickList)
                                //{
                                //    string address = "";
                                //    Excel.Name r_name = workBook.Names.Item(Formula1.Substring(1));
                                //    address = r_name.RefersToRange.Cells.get_Address(true, true, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
                                //    string r_sheetName = r_name.RefersToRange.Cells.Worksheet.Name;
                                //    object[,] rangeResults = (object[,])((Excel.Worksheet)workBook.Sheets[r_sheetName]).get_Range(address).Value2;
                                //    var list = new List<string>();
                                //    for (int i = 1; i <= rangeResults.Length; i++)
                                //    {
                                //        list.Add(rangeResults[i, 1].ToString());
                                //    }
                                //    var flatList = string.Join(",", list.ToArray());

                                //    Cel.Validation.Delete();
                                //    Cel.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation,
                                //        XlFormatConditionOperator.xlBetween, flatList, Type.Missing);
                                //    Cel.Validation.IgnoreBlank = true;
                                //    Cel.Validation.InCellDropdown = true;
                                //}


                                break;
                            }
                        }
                    }
                }
                else
                {
                    listData.Add(data);
                }
            }
            return listData;
        }

        public IDictionary<string, List<Data>> getAllValues()
        {
            IDictionary<string, List<Data>> data = new Dictionary<string, List<Data>>();
            foreach (var item in workBook.Sheets)
            {
                string sheetName = ((Excel.Worksheet)item).Name;
                data.Add(sheetName, getAllValues(sheetName));
            }
            return data;
        }

        public IDictionary<string, List<Data>> getValuesFormula(Boolean flagDataToOffline, string[] ignoreSheetsName, params string[] func)
        {
            this.progress = 0;
            IDictionary<string, List<Data>> data = new Dictionary<string, List<Data>>();
            foreach (var item in workBook.Sheets)
            {
                bool check = false;
                string sheetName = ((Excel.Worksheet)item).Name;
                foreach (var ignoreSheet in ignoreSheetsName)
                {
                    if (sheetName.Equals(ignoreSheet, StringComparison.CurrentCultureIgnoreCase))
                    {
                        check = true;
                        break;
                    }
                }
                if (check == false) data.Add(sheetName, getValuesFormula(sheetName, flagDataToOffline, func));
            }
            return data;
        }
    }
}
