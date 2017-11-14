using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

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
    public class XMLSerialize
    {
        private static String UTF8ByteArrayToString(Byte[] characters) // แปลงไบต์เป็นสติง
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
        public static String Serialize(Object pObject)
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                if (XmlizedString[0] > 255)
                {
                    XmlizedString = XmlizedString.Substring(1);
                }
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XmlizedString);
                XmlizedString = xml.CreateNavigator().InnerXml; // จัดระเบียบ XML ให้เว้นบรรทัดตาม  Element

                return XmlizedString;
            }
            catch (Exception e) { System.Console.WriteLine(e); return null; }
        }

        public static object DeSerialize(Type classType, string str)
        {
            if (str[0] > 255)
                str = str.Substring(1);

            Type pObjectType = classType;
            MemoryStream memoryStream = new MemoryStream();
            byte[] b = UTF8Encoding.UTF8.GetBytes(str); // แปลงสติงเป็๋นไบต์
            memoryStream.Write(b, 0, b.Length);
            memoryStream.Position = 0;
            XmlTextReader xmlTextRead = new XmlTextReader(memoryStream);
            xmlTextRead.XmlResolver = null; ;

            XmlSerializer xs = new XmlSerializer(pObjectType);

            object obj = xs.Deserialize(xmlTextRead);
            xmlTextRead.Close();

            return obj;

        }
    }
}
