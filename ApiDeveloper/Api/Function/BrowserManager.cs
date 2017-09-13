using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

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
    public class BrowserManager
    {
        public static string GetDefaultBrowserFullPath()
        {
            string urlAssociation = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http";
            string browserPathKey = @"$BROWSER$\shell\open\command";

            RegistryKey userChoiceKey = null;
            string browserPath = "";

            try
            {
                //Read default browser path from userChoiceLKey
                userChoiceKey = Registry.CurrentUser.OpenSubKey(urlAssociation + @"\UserChoice", false);

                //If user choice was not found, try machine default
                if (userChoiceKey == null)
                {
                    //Read default browser path from Win XP registry key
                    var browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                    //If browser path wasn’t found, try Win Vista (and newer) registry key
                    if (browserKey == null)
                    {
                        browserKey =
                        Registry.CurrentUser.OpenSubKey(
                        urlAssociation, false);
                    }
                    var path = browserKey.GetValue(null) as string;
                    browserKey.Close();
                    return path;
                }
                else
                {
                    // user defined browser choice was found
                    string progId = (userChoiceKey.GetValue("ProgId").ToString());
                    userChoiceKey.Close();

                    // now look up the path of the executable
                    string concreteBrowserKey = browserPathKey.Replace("$BROWSER$", progId);
                    var kp = Registry.ClassesRoot.OpenSubKey(concreteBrowserKey, false);
                    browserPath = kp.GetValue(null) as string;
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                        if (browserPath.IndexOf("\"")>-1)
                        {
                            browserPath = browserPath.Substring(1);
                        }
                    }
 
                    kp.Close();
                    return browserPath;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetIEFullPath()
        {
            RegistryKey registry = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\IE Setup\setup");
            if (registry == null)
            {
                registry = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\IEXPLORE.EXE");
            }
            string[] subkeys = registry.GetValueNames();
            string fullPath = registry.GetValue(null).ToString();
            if (String.IsNullOrEmpty(fullPath))
            {
                fullPath = registry.GetValue("path").ToString().Split(';')[0] + "\\IEXPLORE.EXE";
            }
            return fullPath;
        }

    }
}
