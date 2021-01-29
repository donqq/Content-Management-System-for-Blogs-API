/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

using System.Xml;
using Newtonsoft.Json;

namespace contentMngApp.api.Data
{
    public class XMLtoJSON : IXMLtoJSON
    {
        public string getJson(string xmlcontent) {
            XmlDocument doc = new XmlDocument(); // create a XML Document
            doc.LoadXml(xmlcontent); // load the XML data as string.

            return JsonConvert.SerializeXmlNode(doc); 
            /* SerializeXmlNode takes XML Node as a paramter. Since XMLNode is the parent class of the XmlDocument.
                an instance of XMlDocument can be assigned to XMlNode variable/paramter as per polymorphism 
            */
        }
    }
}