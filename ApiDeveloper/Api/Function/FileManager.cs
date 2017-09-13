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
    public class FileManager
    {
        public static System.Text.Encoding getFileEncoding(string pathFile)
        {
            System.Text.Encoding enc = System.Text.Encoding.Default;

            byte[] buffer = new byte[5];
            FileStream file = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            file.Read(buffer, 0, 5);
            file.Close();

            string extension = System.IO.Path.GetExtension(pathFile).ToLower();
            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = System.Text.Encoding.UTF8;
            else if (buffer[0] == 0xff && buffer[1] == 0xfe)
                enc = System.Text.Encoding.Unicode;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = System.Text.Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = System.Text.Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = System.Text.Encoding.UTF7;
            else if (extension == ".xml" || extension == ".txt")
                enc = System.Text.Encoding.UTF8;
            else
                enc = System.Text.Encoding.ASCII;

            return enc;
        }
    }
}
