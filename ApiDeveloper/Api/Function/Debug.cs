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

namespace Developer.Api.Function
{
    class GenerateTempName
    {
        public static uint m_Cnt = 0;
        public static string GetTempName()
        {
            m_Cnt++;
            return DateTime.Now.ToString("yyyy-MM-dd-") + "-" + m_Cnt + Guid.NewGuid().ToString();
        }
    }

    public static class Debug
    {
        private static StreamWriter m_Stream = null;


        public static void Init() // เป็นคำสั่งในการสร้างไฟล์ไว้รอ
        {
            try
            {
                if (m_Stream != null)
                { m_Stream.Close(); m_Stream.Dispose(); }
                m_Stream = null;

                string fname = System.Windows.Forms.Application.StartupPath + "\\log\\error.log";

                if (File.Exists(fname)) // ถ้าไฟล์มีอยู่จริง
                {
                    FileInfo f = new FileInfo(fname); // อ่านไฟล์เข้ามา
                    if (f.Length > 1024000)
                        m_Stream = new StreamWriter(fname, false, UTF8Encoding.UTF8);
                    else
                        m_Stream = new StreamWriter(fname, true, UTF8Encoding.UTF8);
                }
                else
                    m_Stream = new StreamWriter(fname, false, UTF8Encoding.UTF8); // สร้างไฟล์ว่างเปล่า ต้องมีการสร้าง Folder ไว้รอแล้วไม่งั้น Error
                m_Stream.AutoFlush = true;
            }
            catch { m_Stream = null; }
        }


        public static void Write(Exception e)
        {
            if (m_Stream == null)
            {
                Init();
            }
            string str = "";
            if (e is System.Web.Services.Protocols.SoapException)
            {
                str = ((System.Web.Services.Protocols.SoapException)e).Detail.InnerText;
            }
            else
                str = e.ToString();

            str = DateTime.Now.ToString("[dd/MM/yy hh:mm:ss] ") + str;
            if (m_Stream.BaseStream.Length > 1024000)
            {
                string fname = System.Windows.Forms.Application.StartupPath + "\\log\\error.log";
                m_Stream.Close();
                m_Stream.Dispose();
                FileStream fs = new FileStream(fname, FileMode.Create);
                m_Stream = new StreamWriter(fs);
            }
            m_Stream.WriteLine(str);

        }
        public static void Write(string str, params object[] param)
        {
            if (m_Stream == null)
            {
                Init(); // คำสั่งในการเขียน File
            }
            str = string.Format(str, param);

            str = DateTime.Now.ToString("[dd/MM/yy hh:mm:ss] ") + str;
            if (m_Stream.BaseStream.Length > 1024000)
            {
                string fname = System.Windows.Forms.Application.StartupPath + "\\log\\error.log";
                m_Stream.Close();
                m_Stream.Dispose();
                FileStream fs = new FileStream(fname, FileMode.Create);
                m_Stream = new StreamWriter(fs);
            }
            m_Stream.WriteLine(str); // คำสั้งให้เขียน มันจะเขียนต่อท้ายลงไปเรื่อยๆ

        }
        public static void Alert(Exception e)
        {

            string str = "";
            if (e is System.Web.Services.Protocols.SoapException)
            {
                str = ((System.Web.Services.Protocols.SoapException)e).Detail.InnerText;
            }
            else
                str = e.ToString();
            Write(str);
            System.Windows.Forms.MessageBox.Show(str, "Error", System.Windows.Forms.MessageBoxButtons.OK);
        }
        public static void Alert(string str, params object[] param)
        {
            str = string.Format(str, param);
            Write(str);
            System.Windows.Forms.MessageBox.Show(str, "Error", System.Windows.Forms.MessageBoxButtons.OK);
        }

        internal static void Throw(Exception e)
        {
            string str = "";
            if (e is System.Web.Services.Protocols.SoapException)
            {
                str = ((System.Web.Services.Protocols.SoapException)e).Detail.InnerText;
            }
            else
                str = e.ToString();
            Write(str);

            throw e;
        }

        internal static void Throw(string str, params object[] param)
        {
            str = string.Format(str, param);
            Write(str);

            throw new Exception(str);

        }
    }
}
