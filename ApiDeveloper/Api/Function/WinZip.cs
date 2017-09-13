using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

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

namespace Developer.Api.Function
{
    class WinZip
    {
        public static void ZipFiles(string inputFolderPath, string outputPathAndFile, string password, bool deleteZipFile)
        {
            FileStream ostream = null;
            ZipOutputStream oZipStream = null;
            ZipEntry oZipEntry = null; // ตัวแปรเก็บชื่อในการอ้างถึงข้อมูลใน zip
            byte[] obuffer;
            //If the Zip stream already exists then 
            //My Input folder is c:\Test which contains 7 .txt files.
            string path = inputFolderPath.Substring(0, inputFolderPath.LastIndexOf("\\"));
            string outPath = path + @"\" + outputPathAndFile;

            string[] chkPath = outputPathAndFile.Split(':');
            if (chkPath.Length > 1) outPath = outputPathAndFile;

            try
            {
                if (File.Exists(outPath)) // ถ้ามีไฟล์ zip อยู่ก่อนหน้านั้นจริงให้ลบ
                {
                    File.Delete(outPath);
                }
                ArrayList ar = GenerateFileList(inputFolderPath); // generate file list
                //remove the log file from the list.
                int TrimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
                // find number of chars to remove // from orginal file path
                TrimLength += 1;
                oZipStream = new ZipOutputStream(File.Create(outPath)); // create zip stream
                if (password != null && password != String.Empty) // String.Empty =""
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression
                // ze.CompressionMethod = CompressionMethod.Deflated;
                foreach (string Fil in ar) // for each file, generate a zipentry
                {
                    oZipEntry = new ZipEntry(Fil.Remove(0, TrimLength)); // .Remove(ตำแหน่งเริ่มต้น,ตำแหน่งสิ้นสุด ); ตัดข้อความส่วนที่ไม่ต้องการออก ให้เหลือแต่ส่วนที่ต้องการ
                    oZipEntry.IsUnicodeText = true;
                    oZipStream.PutNextEntry(oZipEntry);  // เก็บชื่ออ้าง Path ตำแหน่ง ไฟล์ใน Zip ไม่ใช่ตำแหน่งใน Windown การใส่ชื่อต้องอ้างตำแหน่งpathไฟล์ใน zip และนามสกุลไฟล์ พอแตกไฟล์ zip ออกมาจะได้ไฟล์นามสกุลตามนั้น
                    if (!Fil.EndsWith(@"/")) // if a file ends with '/' its a directory // .EndsWith(@"/") คือคำสั้งต้องไม่ลงท้ายด้วย ตัว String ที่กำหนดในที่นี้คือต้องไม่ลงท้ายด้วย "/"
                    {   // ถ้าไม่ใช้ Folder จริง
                        ostream = File.OpenRead(Fil); // อ่านข้อมูลโดยผ่านท่อส่งแบบ FileStream
                        obuffer = new byte[ostream.Length]; // สร้าง Byte ขนาดของ Array เท่ากับ ขนาดของ Byte FileStream
                        ostream.Read(obuffer, 0, obuffer.Length); // อ่านข้อมูล และส่งข้อมูล Byte ให้ตัวแปร obuffer //.Read(ตัวแปร,ตำแหน่งเริ่มต้นอ่านไบต์,ตำแหน่งสิ้นสุด); 
                        oZipStream.Write(obuffer, 0, obuffer.Length); // Zip ไฟล์ .Write(ตัวแปร,ตำแหน่งเริ่มต้นอ่านไบต์,ตำแหน่งสิ้นสุด); 
                    }
                }
            }

            catch (Exception ex)
            {
                string strmg = ex.Message.ToString();
            }

            finally
            {
                oZipStream.CloseEntry();
                ostream.Flush();
                oZipStream.Finish(); // การทำงาน Zip การทำงานสิ้นสุด
                ostream.Close();
                ostream.Dispose();
                ostream = null;
                oZipStream.Flush();
                oZipStream.Close();  // คืนค่าทรัพยากรให้ระบบ และหยุดการทำงาน ขั้นตอนนี้ต้องมีทุกครั้ง
                oZipStream.Dispose();
                oZipStream = null;
                oZipEntry = null;
                obuffer = null;
                GC.Collect(); // คำสั่งเรียก GC ให้จัดการคืนหน่วยความจำ โดยไม่ต้องรอให้ memory ไม่พอ
                GC.WaitForPendingFinalizers(); // เพื่อหยุดการทำของ thread ไว้จนกว่า thread ที่กำลังดำเนินการกับคิว finalizer จะล้างคิวหมด 
                //Delete the directory and all the files inside it.
               if (deleteZipFile) Directory.Delete(inputFolderPath, true); // Argement true จะทำให้ลบ Folder รวมทั้งไฟล์ใน Folder 
            }
        }

        public static void ZipFiles2(string inputFolderPath, string outputPathAndFile, bool deleteZipFile)
        {
            // เมทอดนี้จะติดปัญหาด้านการ Zip ภาษาไทย อ่าน ฟอนต์ ไม่ได้
            FastZip fZip = new FastZip();
            //fZip.CreateEmptyDirectories = false;
            //fZip.NameTransform = new ZipNameTransform(false);
            fZip.CreateZip(outputPathAndFile, inputFolderPath, true, "");// Still need to figure out how
            if (deleteZipFile) Directory.Delete(inputFolderPath, true);
        }

        private static ArrayList GenerateFileList(string Dir)
        {
            ArrayList fils = new ArrayList();
            bool Empty = true;
            foreach (string file in Directory.GetFiles(Dir)) // add each file in directory // จะได้ชื่อไฟล์ที่อยู่ข้างใน Folde ออกมาเป็น อารเรย์แบบสติง
            {
                fils.Add(file);
                Empty = false;
            }

            if (Empty) // ถ้าโฟล์เดอร์นั้นไม่มีไฟล์ และ โฟร์เดอร์ย่อยลงไปอีก
            {
                if (Directory.GetDirectories(Dir).Length == 0)
                // if directory is completely empty, add it
                {
                    fils.Add(Dir + @"/"); // @"/" มีค่าเท่ากัย "//" เป็นการช่วยให้เราอ้างถึง Path ได้ง่ายขึ้น
                }
            }

            foreach (string dirs in Directory.GetDirectories(Dir)) // recursive  // จะได้ชื่อ Folder ที่อยู่ข้างใน Path ออกมาเป็น อารเรย์แบบสติง
            {
                foreach (object obj in GenerateFileList(dirs)) // จะเห็นว่าจะใช้การวนลูป และรีเทร์น ค่า ArrayList ออกมาเป็น Object
                {
                    fils.Add(obj);
                }
            }
            return fils; // return file list
        }

        public static void expandFolder(string zipFile, string baseFolder)
      {
            if (!Directory.Exists(baseFolder))
         {
            Directory.CreateDirectory(baseFolder);
         }

        FileStream fr = File.OpenRead(zipFile);
        ZipInputStream ins = new ZipInputStream(fr);
            //ZipFile zf = new ZipFile(zipFile);
        ZipEntry ze = ins.GetNextEntry();
        while (ze != null)
        {
        if(ze.IsDirectory)
        {
               Directory.CreateDirectory(baseFolder +"\\"+ ze.Name);
        }
        else if(ze.IsFile)
        {
        if (!Directory.Exists(baseFolder + Path.GetDirectoryName(ze.Name)))
        {
               Directory.CreateDirectory(baseFolder + Path.GetDirectoryName(ze.Name));
        }

            FileStream fs = File.Create(baseFolder + "\\" + ze.Name);
            byte[] writeData = new byte[ze.Size];
            int iteration = 0;
            while (true)
         {
            int size = 2048;
            size = ins.Read(writeData, (int)Math.Min(ze.Size, (iteration * 2048)), (int)Math.Min(ze.Size - (int)Math.Min(ze.Size, (iteration * 2048)), 2048));
        if (size > 0)
          {
            fs.Write(writeData, (int)Math.Min(ze.Size, (iteration * 2048)), size);
          }
            else
          {
            break;
          }
                iteration++;
              }
               fs.Close();
             }
               ze = ins.GetNextEntry();
           }
                ins.Close();
                fr.Close();
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile)); // อ่าน path ไฟล์ Zip
            if (password != null && password != String.Empty)
                s.Password = password;
            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null) // วนลูปอ่านข้อมูลใน zip s.GetNextEntry()
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name); // Path.GetFileName("BackupQdoc 14-05-2009 11-07-45\\BackupQdoc 14-05-2009 11-07-45.xml") อ่านแต่ชื่อไฟล์ใน Path ออกมาในที่นี้ได้ค่า "BackupQdoc 14-05-2009 11-07-45.xml"

                string[] chkPath = outputFolder.Split(':');
                if (chkPath.Length == 1) // ถ้าไม่ได้มีการอ้าง ตำแหน่ง Path output
                {
                    string path = zipPathAndFile.Substring(0, zipPathAndFile.LastIndexOf("\\"));
                    directoryName = path;
                }
                // create directory 
                if (directoryName != "")
                {
                    if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);  // สร้าง Folder
                }

                if (fileName == String.Empty) // ตรวจสอบสร้าง Folder
                {
                    string fullPath = directoryName + "\\" + theEntry.Name;
                    if (fullPath.LastIndexOf("/") != -1)
                    {
                        if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath); // ถ้า path โฟร์เดอร์ไม่มีจริงให้สร้าง
                    }
                }

                if (fileName != String.Empty) //ตรวจสอบสร้าง File ถ้าชื่อไฟล์ ไม่เท่ากับ ""
                {
                    // int a = theEntry.Name.IndexOf(".ini");
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = directoryName + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath); // เอา path Folder ที่ไฟล์บรรจุอยู่ให้ตัวแปร fullDirPath
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath); // ถ้า path โฟร์เดอร์ไม่มีจริงให้สร้าง
                        FileStream streamWriter = File.Create(fullPath); // สร้างไฟล์
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            try
                            {
                                size = s.Read(data, 0, data.Length); // อ่านค่า byte ในตัวแปร  s วนลูปรับข้อมูลครั้งละ 2 Mb และส่งค่า Byte ให้ตัวแปร Data
                            }
                            catch (Exception)
                            {


                            }

                            if (size > 0) // ตัวแปร size จะเก็บ ข้อมูลขนาด Byte ของอาเรย์ ถ้า s ไม่มีข้อมูล size จะเท่ากับ 0 
                            {
                                streamWriter.Write(data, 0, size); //เขียนไฟล์ // .Write(ตัวแปรที่เก็บไบต์,ตำแหน่งของไบต์เริ่มต้น,ตำแหน่งสิ้นสุดของไบต์)
                            }
                            else
                            {
                                break; // ถ้าไม่มีข้อมูลจะออกจาก Loop รอบนั้น
                            }
                        }
                        streamWriter.Close();
                    }
                }
            }
            s.Close();
            if (deleteZipFile) File.Delete(zipPathAndFile); // ลบไฟล์ เนื่องจาก ไฟล์ Zip เป็นข้อมูลชนิดไฟล์
        }


    }
}
