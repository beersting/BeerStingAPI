using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    public class HttpConnect
    {
        public static String getData(String url, String method, string data)
        {
            Uri uri = new Uri(url);
            return getData(uri, method, data);
        }

        public static String getData(Uri uri, String method, string data)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = method; //Or GET, PUT, DELETE, POST
            request.ContentType = "text/plain;charset=utf-8";
            //httpWebRequest.ContentType = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";

            if (method.ToLower().Equals("post"))
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] bytes = encoding.GetBytes(data);

                request.ContentLength = bytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the data.
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }

            string ReturnedData = "";
            using (HttpWebResponse Response = request.GetResponse() as HttpWebResponse)
            {
                if (Response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("The request did not complete successfully and returned status code " + Response.StatusCode);
                using (StreamReader Reader = new StreamReader(Response.GetResponseStream()))
                {
                    ReturnedData = Reader.ReadToEnd();
                }
            }
            return ReturnedData;
        }

    }
}
