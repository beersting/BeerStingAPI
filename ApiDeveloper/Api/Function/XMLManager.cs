using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

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
    public class XMLManager
    {
        public XmlDocument m_XML = new XmlDocument();
        string m_FileName;
        public void LoadXML(string xml, string fileName) 
        {
            m_FileName = fileName;
            m_XML = new XmlDocument();
            m_XML.XmlResolver = null;
            try
            {
                m_XML.LoadXml(xml);
            }
            catch (Exception e) { throw e; }
        }

        //*** Beer ใช้
        public void Load(string file)
        {
            m_FileName = file;
            m_XML = new XmlDocument();
            m_XML.XmlResolver = null;
            try
            {
                m_XML.Load(file);
            }
            catch (Exception e) { throw e; }
        }
        public void Save()
        {
            m_XML.Save(m_FileName);
            //m_XML.Save("c:\\test.xml");
        }


        public bool FindTag(string path)
        {
            XmlNodeList node = FindNode(null, path);
            if (node == null || node.Count == 0) return false;
            return true;
        }
        public bool FindTag(XmlNode node, string path)
        {
            XmlNodeList nodes = FindNode(node, path);
            if (nodes == null || nodes.Count == 0) return false;
            return true;
        }
        public string SetValueAttribute(string path, string valuename,string value)
        {
            XmlNodeList node = FindNode(null, path);
            if (node == null) return "";
            return SetValueAttribute(node[0], valuename, value);
        }
        public string SetValueAttribute(XmlNode node, string path, string valuename, string value)
        {
            XmlNodeList nodes = FindNode(node, path);
            if (nodes == null) return "";
            return SetValueAttribute(nodes[0], valuename, value);
        }
        public string FindValueAttribute(string path, string valuename)
        {
            XmlNodeList node = FindNode(null, path);
            if (node == null) return "";
            return GetValueAttribute(node[0], valuename);
        }
        public string FindValueAttribute(XmlNode node, string path, string valuename)
        {
            XmlNodeList nodes = FindNode(node, path);
            if (nodes == null) return "";
            return GetValueAttribute(nodes[0], valuename);
        }
        public string SetValueInTag(string path, string value) // เมทอดในการค้นหา Element
        {
            XmlNodeList node = FindNode(null, path);
            if (node == null) return "";
            return SetInTag(node[0], value);
        }
        public string SetValueInTag(XmlNode node, string path, string value)
        {
            XmlNodeList nodes = FindNode(node, path);
            if (nodes == null) return "";
            return SetInTag(nodes[0], value);
        }
        public string FindValueInTag(string path) // เมทอดในการค้นหา Element เพี่อเอาค่าใน  Element นั้นออกมา
        {
            XmlNodeList node = FindNode(null, path);
            if (node == null) return "";
            return GetInTag(node[0]);
        }
        public string FindValueInTag(XmlNode node, string path)
        {
            XmlNodeList nodes = FindNode(node, path);
            if (nodes == null) return "";
            return GetInTag(nodes[0]);
        }
        public XmlNodeList FindNode(XmlNode xml, string elementPath)
        {
            XmlNodeList xNode = null;
            string[] paths = elementPath.Split(new char[] { '.' });
            if (paths == null || paths.Length == 0) return null;
            string path = paths[0];
            if (paths.Length > 1)
                elementPath = elementPath.Substring(path.Length + 1); // ตัดคำเพื่อไป Key ตัวอักษรถัดไป +1 เพื่อต้องการให้ตัด . ออกไปด้วย
            if (xml == null)
            {
                xNode = m_XML.GetElementsByTagName(path); // .GetElementsByTagName("ชื่อ Tage") แสดงค่าของ element ในnode นั้นๆ เหมื่อนเข้าไปค้นหา Elements ที่ต้องการ
            }
            else
            {
                xNode = ((XmlElement)xml).GetElementsByTagName(path);
            }
            if (paths.Length == 1)
                return xNode;

            if (xNode != null && elementPath != null && elementPath != "")
            {
                for (int i = 0; i < xNode.Count; i++)
                {
                    XmlNodeList node = FindNode(xNode[i], elementPath);
                    if (node != null && node.Count > 0)
                        return node;
                }
            }
            return null;
        }
        public string SetValueAttribute(XmlNode xml, string valuename, string value)
        {
            if (xml == null) return null;
            for (int i = 0; i < xml.Attributes.Count; i++)
            {
                if (xml.Attributes[i].Name.Equals(valuename, StringComparison.CurrentCultureIgnoreCase))
                {
                    return xml.Attributes[i].Value = value;
                }
            }
            return null;
        }
        public string GetValueAttribute(XmlNode xml, string valuename)
        {
            if (xml == null) return null;
            for (int i = 0; i < xml.Attributes.Count; i++)
            {
                if (xml.Attributes[i].Name.Equals(valuename, StringComparison.CurrentCultureIgnoreCase))
                {
                    return xml.Attributes[i].Value;
                }
            }
            return null;
        }
        public string SetInTag(XmlNode xml, string value) // เมทอดในการ Set ค่าลงใน Element
        {
            if (xml == null) return null;
            return xml.InnerText = value; // ใส่ค่าลง XML
        }
        public string GetInTag(XmlNode xml) // Return ค่าใน Element นั้นออกมา
        {
            if (xml == null) return null;
            return xml.InnerText;
        }
        public string SetInTag(XmlNode xml, string element, string value)
        {
            if (xml == null) return null;
            XmlNodeList node = ((XmlElement)xml).GetElementsByTagName(element);
            if (node == null || node.Count == 0) return null;

            for (int i = 0; i < node.Count; i++)
            {
                if (node[i].Name.Equals(element, StringComparison.CurrentCultureIgnoreCase))
                {
                    return node[i].InnerText = value;
                }
            }
            return null;
        }
        public string GetInTag(XmlNode xml, string element)
        {
            if (xml == null) return null;
            XmlNodeList node = ((XmlElement)xml).GetElementsByTagName(element);
            if (node == null || node.Count == 0) return null;

            for (int i = 0; i < node.Count; i++)
            {
                if (node[i].Name.Equals(element, StringComparison.CurrentCultureIgnoreCase))
                {
                    return node[i].InnerText;
                }
            }
            return null;
        }
    }
}
