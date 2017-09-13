using System;
using System.Collections.Generic;
using System.Linq;
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
    class WinRar
    {
        public static void RarFiles(string PathRAR, string PathoutPutRar, string PathInputFileFolder, bool deleteRarFile)
        {
            // Process.Start(@"C:\Program Files\WinRar\rar.exe",@"a c:\test d:\photos\*.gif");
            //string exeFolder = @"C:\Program Files\WinRAR";
            string exeFolder = @PathRAR;
           // string a = @"""" + PathoutPutRar + "\" ";
            string exePath = string.Format("{0}\\{1}", PathRAR, "Rar.exe");
            System.Diagnostics.ProcessStartInfo sdp = new System.Diagnostics.ProcessStartInfo();
            // sdp.Arguments = " a " + " -ep" + @" C:\BackupQdoc\MyFolder.rar " + @"C:\BackupQdoc\TestRar"; // ตัวนี้จะเป็นคำสั่งเขียนไฟล์ rar แต่จะไม่มีการสร้าง Folder ซ้อนจะเป็นมีแต่ไฟล์กระจายกันอยู่
            // d คือ ลบ
            // a คือ จะเอาชื่อ Folder แม่ที่มีไฟล์และโฟร์เดอร์นั้นอยู่ และมาสร้างไฟล์ rar ตามโครงสร้างโฟร์เดอร์อีกที
            // a -ep คือ รวมเอาแต่ไฟล์ไม่มีโฟร์เดอร์
            // a -ep1 คือ จะสร้างไฟล์ rar ตามโครงสร้างไฟล์จริง และเป็นไปตามโครงสร้างที่เรากำหนด คือไม่เอา base ไดเร็คทอรีไปใส่ชื่อ
            //string a = @"""C:\BackupQdoc\TestRar\BackupQDOC 14-05-2009 11-07-45.rar""";
            //string b = "\"C:\\BackupQdoc\\TestRar\\BackupQDOC 14-05-2009 11-07-45.rar\"";
            //sdp.Arguments = " a -ep1 " + @"""C:\BackupQdoc\TestRar\BackupQDOC 14-05-2009 11-07-45.rar"" " + @"""C:\BackupQdoc\BackupQDOC 14-05-2009 11-07-45""";
            sdp.Arguments = " a -ep1 " + @"""" + PathoutPutRar + "\" " + @"""" + PathInputFileFolder + "\"";
            //sdp.Arguments = " a -ep1 " + "\"C:\\BackupQdoc\\TestRar\\BackupQDOC 14-05-2009 11-07-45.rar\" " + "\"C:\\BackupQdoc\\BackupQDOC 14-05-2009 11-07-45\"";
            sdp.CreateNoWindow = false; // น่าจะเป็นคำสั่งในการบอกว่าให้ ไปเปิดการทำงานรันที่วินโดร์ตัวใหม่หรือเปล่า
            sdp.FileName = PathRAR + "\\" + "WinRAR.exe";
          //  sdp.FileName = @"C:\Program Files\WinRAR\winrar.exe";
            // sdp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // ให้การทำงานมันซ่อน ไม่ต้องแสดง Interface ออกมาทางหน้าจอ
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(sdp);
            process.WaitForExit(); // เป็นคำสั่งที่จะรอ ให้ process หรือ โปรแกรมอื่นทำงานเสร็จก่อนถึงไปทำงานบรรทัดอื่นได้
            process.Close();
            if (deleteRarFile) Directory.Delete(PathInputFileFolder, true); // Argement true จะทำให้ลบ Folder รวมทั้งไฟล์ใน Folder 
        }

        public static void UnRarFiles(string PathRAR, string PathInputRar, string PathOutputFileFolder, bool deleteRarFile)
        {
            string exeFolder = @PathRAR;
            string exePath = string.Format("{0}\\{1}", exeFolder, "UnRAR.exe");
            System.Diagnostics.ProcessStartInfo sdp = new System.Diagnostics.ProcessStartInfo();
            // sdp.Arguments = " a " + " -ep" + @" C:\BackupQdoc\MyFolder.rar " + @"C:\BackupQdoc\TestRar"; // ตัวนี้จะเป็นคำสั่งเขียนไฟล์ rar แต่จะไม่มีการสร้าง Folder ซ้อนจะเป็นมีแต่ไฟล์กระจายกันอยู่
            // d คือ ลบ
            sdp.Arguments = " x " + @"" + "\""+ PathInputRar + "\""+" " + @"" + "\""+PathOutputFileFolder + "\""+"";
            sdp.CreateNoWindow = false; // น่าจะเป็นคำสั่งในการบอกว่าให้ ไปเปิดการทำงานรันที่วินโดร์ตัวใหม่หรือเปล่า
            sdp.FileName = @PathRAR+"\\winrar.exe";
            // sdp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // ให้การทำงานมันซ่อน ไม่ต้องแสดง Interface ออกมาทางหน้าจอ
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(sdp);
            process.WaitForExit(); // เป็นคำสั่งที่จะรอ ให้ process หรือ โปรแกรมอื่นทำงานเสร็จก่อนถึงไปทำงานบรรทัดอื่นได้
            process.Close();
            if (deleteRarFile) Directory.Delete(PathInputRar, true); // Argement true จะทำให้ลบ Folder รวมทั้งไฟล์ใน Folder 
        }

    }
}
