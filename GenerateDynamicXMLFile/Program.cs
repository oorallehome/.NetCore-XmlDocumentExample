using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GenerateDynamicXMLFile
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateEmployeeXML();
        }

        // Generate Dynamic XML File In .NET Core
        // https://www.thecodehubs.com/generate-dynamic-xml-file-in-net-core/
        static bool CreateEmployeeXML()
        {
            try
            {
                //Xml Do
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlElement employeeDataNode = doc.CreateElement("EmployeeData");
                employeeDataNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                employeeDataNode.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.testwebsite.org/data/schema/rr/2021 xy-abc-1-1.xsd");
                employeeDataNode.SetAttribute("xmlns", "http://www.testwebsite.org/data/schema/rr/2021");
                
                doc.AppendChild(employeeDataNode);

                XmlElement headerNode = doc.CreateElement("Header");
                employeeDataNode.AppendChild(headerNode);


                XmlNode contentDateNode = doc.CreateElement("ContentDate");
                contentDateNode.AppendChild(doc.CreateTextNode("2017-02-01T12:00:00Z"));
                headerNode.AppendChild(contentDateNode);

                //RelationshipRecords
                XmlNode employeeRecoredsNode = doc.CreateElement("EmployeeRecords");
                doc.DocumentElement.AppendChild(employeeRecoredsNode);

                XmlNode employeeRecordNode = doc.CreateElement("EmployRecord");
                employeeRecoredsNode.AppendChild(employeeRecordNode);

                //EmployeeName
                XmlNode employeeNameNode = doc.CreateElement("EmployeeName");
                employeeNameNode.AppendChild(doc.CreateTextNode("TABISH RANGREJ"));
                employeeRecordNode.AppendChild(employeeNameNode);

                //EmployeeType
                XmlNode employeeTypeNode = doc.CreateElement("EmployeeType");
                employeeTypeNode.AppendChild(doc.CreateTextNode("USER"));
                employeeRecordNode.AppendChild(employeeTypeNode);

                //StartNode
                XmlNode addressNode = doc.CreateElement("Address");
                employeeRecordNode.AppendChild(addressNode);
                XmlNode addressLineNode = doc.CreateElement("AddressLine");
                addressLineNode.AppendChild(doc.CreateTextNode("1/234 xyz building, xyz park, 395003"));
                addressNode.AppendChild(addressLineNode);
                XmlNode countryNode = doc.CreateElement("Country");
                countryNode.AppendChild(doc.CreateTextNode("UAE"));
                addressNode.AppendChild(countryNode);

               //EmployeeSubscriptions
                XmlNode employeeSubscriptionsNode = doc.CreateElement("EmployeeSubscriptions");
                employeeRecordNode.AppendChild(employeeSubscriptionsNode);

                //EmployeeSubscription
                XmlNode employeeSubscriptionNode = doc.CreateElement("EmployeeSubscription");
                employeeSubscriptionsNode.AppendChild(employeeSubscriptionNode);
                XmlNode startDateNode = doc.CreateElement("StartDate");
                startDateNode.AppendChild(doc.CreateTextNode("2018-02-01T00:00:00Z"));
                employeeSubscriptionNode.AppendChild(startDateNode);
                XmlNode endDateNode = doc.CreateElement("EndDate");
                endDateNode.AppendChild(doc.CreateTextNode("2020-02-01T00:00:00Z"));
                employeeSubscriptionNode.AppendChild(endDateNode);
                XmlNode periodTypeNode = doc.CreateElement("SubscriptionType");
                periodTypeNode.AppendChild(doc.CreateTextNode("VIP"));
                employeeSubscriptionNode.AppendChild(periodTypeNode);

                //RelationshipStatus
                XmlNode employeeStatusNode = doc.CreateElement("EmployeeStatus");
                employeeStatusNode.AppendChild(doc.CreateTextNode("ACTIVE"));
                employeeRecordNode.AppendChild(employeeStatusNode);

                //for (int i = 0; i < 5; i++)
                //{
                //}
                Console.WriteLine(doc.OuterXml);


                //var basePath = Path.Combine(Environment.CurrentDirectory, @"XmlFiles\");
                //if (!Directory.Exists(basePath))
                //{
                //    Directory.CreateDirectory(basePath);
                //}
                //var newFileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ".xml");
                //doc.Save(basePath + newFileName);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // C# - Dictionary<TKey, TValue>
        //https://www.tutorialsteacher.com/csharp/csharp-dictionary
        static XmlNode CreateXMLElement(XmlDocument document, string name, string text, IDictionary<string, string> attributes)
        {
            XmlElement element = document.CreateElement(name);
            if (!string.IsNullOrEmpty(text)) element.AppendChild(document.CreateTextNode(text));
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                element.SetAttribute(attribute.Key, attribute.Value);
                //How to add attributes to xml using XmlDocument in c# .net CF 3.5
                //https://stackoverflow.com/questions/3405284/how-to-add-attributes-to-xml-using-xmldocument-in-c-sharp-net-cf-3-5
                //node.Attributes.Append(document.CreateAttribute(attribute.Key, attribute.Value));
                
            }

            return element;
        }
    }
}
