using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

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
    public static class TimeSyncronize
    {
        #region P/Invoke
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetLocalTime([System.Runtime.InteropServices.In] ref SYSTEMTIME st);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            UInt32 BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out LUID lpLuid);
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public LUID Luid;
            public int Attributes;

        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public uint HighPart;
        }

        #endregion

        public static DateTime GetServerDateTime(string endPoint)
        {
            DateTime dt = DateTime.Now;
            System.Net.HttpWebRequest web = System.Net.HttpWebRequest.Create(endPoint) as System.Net.HttpWebRequest;
            web.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();
            web.Method = "HEAD";
            System.Net.HttpWebResponse res = web.GetResponse() as System.Net.HttpWebResponse;
            string s = res.Headers["Date"];
            TimeSpan ts = DateTime.Now - dt;
            res.Close();
          
            return DateTime.Parse(s).Add(ts).AddSeconds(30);
        }

        public static DateTime GetServerDateTime()
        {
            return GetServerDateTime(WebServiceFactory.getEndpointAddress());
        }

        public static bool IsLocalDateTimeValid(string endPoint)
        {
            DateTime remoteDateTime = GetServerDateTime(endPoint);
            DateTime localDate = DateTime.Now;
            TimeSpan ts = remoteDateTime - localDate;
            return (ts.TotalSeconds >= 0 && ts.TotalSeconds < 300);
        }

        public static bool IsLocalDateTimeValid()
        {
            return IsLocalDateTimeValid(WebServiceFactory.getEndpointAddress());
        }

        public static void SyncronizeDateTime(string endPoint)
        {
            DateTime remoteDateTime = GetServerDateTime(endPoint);
            SetTime(remoteDateTime);
        }
        public static void SyncronizeDateTime()
        {
            SyncronizeDateTime(WebServiceFactory.getEndpointAddress());
        }

        #region SetTime
        private static void SetTime(DateTime time)
        {

            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = (short)time.Year;
            st.wMonth = (short)time.Month;
            st.wDay = (short)time.Day;
            st.wHour = (short)time.Hour;
            st.wMinute = (short)time.Minute;
            st.wSecond = (short)time.Second;
            st.wMilliseconds = (short)time.Millisecond;

            if (SetLocalTime(ref st) == false)
            {
                uint TOKEN_QUERY = 0x08;
                uint TOKEN_ADJUST_PRIVILEGES = 0x20;
                string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";

                int SE_PRIVILEGE_ENABLED = 0x02;

                IntPtr hProc = IntPtr.Zero;
                IntPtr hToken = IntPtr.Zero;
                TOKEN_PRIVILEGES tokenPriviliges;

                // get the current current process security token
                hProc = GetCurrentProcess();
                bool ret = OpenProcessToken(hProc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken);
                if (!ret)
                    return;
                // lookup the LUID for the shutdown privilege
                ret = LookupPrivilegeValue(String.Empty, SE_SYSTEMTIME_NAME, out tokenPriviliges.Luid);
                if (!ret)
                    return;

                // adjust the privileges of the current process to include the shutdown privilege
                tokenPriviliges.PrivilegeCount = 1;

                tokenPriviliges.Attributes = SE_PRIVILEGE_ENABLED;

                ret = AdjustTokenPrivileges(hToken, false, ref tokenPriviliges, 0, IntPtr.Zero, IntPtr.Zero);
                if (!ret)
                    return;


                SetLocalTime(ref st);

                tokenPriviliges.Attributes = 0;
                AdjustTokenPrivileges(hToken, false, ref tokenPriviliges, 0, IntPtr.Zero, IntPtr.Zero);
                CloseHandle(hToken);
            }
        }

        private static void SetTime(SYSTEMTIME time)
        {
            if (SetLocalTime(ref time) == false)
            {
                uint TOKEN_QUERY = 0x08;
                uint TOKEN_ADJUST_PRIVILEGES = 0x20;
                string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";

                int SE_PRIVILEGE_ENABLED = 0x02;

                IntPtr hProc = IntPtr.Zero;
                IntPtr hToken = IntPtr.Zero;
                TOKEN_PRIVILEGES tokenPriviliges;

                // get the current current process security token
                hProc = GetCurrentProcess();
                bool ret = OpenProcessToken(hProc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken);
                if (!ret)
                    return;
                // lookup the LUID for the shutdown privilege
                ret = LookupPrivilegeValue(String.Empty, SE_SYSTEMTIME_NAME, out tokenPriviliges.Luid);
                if (!ret)
                    return;

                // adjust the privileges of the current process to include the shutdown privilege
                tokenPriviliges.PrivilegeCount = 1;

                tokenPriviliges.Attributes = SE_PRIVILEGE_ENABLED;

                ret = AdjustTokenPrivileges(hToken, false, ref tokenPriviliges, 0, IntPtr.Zero, IntPtr.Zero);
                if (!ret)
                    return;


                SetLocalTime(ref time);

                tokenPriviliges.Attributes = 0;
                AdjustTokenPrivileges(hToken, false, ref tokenPriviliges, 0, IntPtr.Zero, IntPtr.Zero);
                CloseHandle(hToken);
            }
        }
        #endregion
    }
}
