using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace XmlDocumentExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();

            //https://www.surinderbhomra.com/Blog/2011/04/15/XmlDocumentLoad-Error-Handling

            //Check if language XML file exists
            if (File.Exists("DI-C01_DU Spec v1.8.2_Fixed Primary Attchd.xml"))
            {
                try
                {
                }
                catch (XmlException ex)
                {
                }
            }
            doc.Load("DI-C01_DU Spec v1.8.2_Fixed Primary Attchd.xml");
            doc.PreserveWhitespace = true;

            //XmlDocument doc = new XmlDocument();
            //using (StreamReader streamReader = new StreamReader(path_name, Encoding.UTF8))
            //{
            //    contents = streamReader.ReadToEnd();
            //}
            //doc.LoadXml(contents);

            //Display the document element.
            //Console.WriteLine(doc.DocumentElement.OuterXml);


            //XmlDocument doc = new XmlDocument();
            //doc.Load("d:\\product.xml");
            //XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Store/Product");
            //string product_id = "", product_name = "", product_price = "";
            //foreach (XmlNode node in nodes)
            //{
            //    product_id = node.SelectSingleNode("Product_id").InnerText;
            //    product_name = node.SelectSingleNode("Product_name").InnerText;
            //    product_price = node.SelectSingleNode("Product_price").InnerText;
            //    MessageBox.Show(product_id + " " + product_name + " " + product_price);
            //}

            XmlNode root = doc.DocumentElement;
            if (false)
            {
                Console.WriteLine(root.HasChildNodes);
                Console.WriteLine(root.OuterXml);

                //Display the contents of the child nodes.
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        Console.WriteLine(root.ChildNodes[i].InnerText);
                    }
                }
            }

            //XmlNode currNode = doc.DocumentElement.FirstChild;  
            //Console.WriteLine(currNode.OuterXml);

            //XmlNode nextNode = currNode.NextSibling;
            //Console.WriteLine(nextNode.OuterXml);

            //XmlNode lastNode = doc.DocumentElement.LastChild;
            //Console.WriteLine(lastNode.OuterXml);

            //XmlNode prevNode = lastNode.PreviousSibling;
            //Console.WriteLine(prevNode.OuterXml);

            Console.WriteLine(doc.NameTable);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("mismo", "http://www.mismo.org/residential/2009/schemas");
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsmgr.AddNamespace("ULAD", "http://www.datamodelextension.org/Schema/ULAD");
            nsmgr.AddNamespace("DU", "http://www.datamodelextension.org/Schema/DU");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");


            //Enumerable.Contains

            //Console.WriteLine(XPathWithNamesapce("", "ns:MESSAGE/ns:ABOUT_VERSIONS/ns:ABOUT_VERSION/ns:CreatedDatetime", null));
            string xPath = XPathWithNamesapce("", "/MESSAGE/ABOUT_VERSIONS/ABOUT_VERSION/CreatedDatetime", "mismo");
            Console.WriteLine(xPath);
            Console.WriteLine(doc.SelectSingleNode(xPath, nsmgr).InnerText);
            
        }
        static string XPathWithNamesapce(string? xParentPath, string xPath, string nameSpace)
        {
            if (!string.IsNullOrWhiteSpace(xPath) && !string.IsNullOrWhiteSpace(nameSpace))
            {
                string pathHeader = "";
                string pathFooter = "";

                //stackoverflow.com/questions/3917589/string-array-contains
                if (Enumerable.Contains("/|.|@".Split('|'), xPath.Left(1)))
                {
                    if (Enumerable.Contains("//|..".Split('|'), xPath.Left(2)))
                    {
                        pathHeader = xPath.Left(2) + nameSpace + ":";
                        pathFooter = xPath.Right(xPath.Length-2);
                    }
                    else
                    {
                        pathHeader = "";
                        pathFooter = xPath;
                    }
                }
                else
                {
                    pathHeader = "";
                    pathFooter = nameSpace + ":" + xPath;
                }
                
                return pathHeader + pathFooter.Replace("/", "/" + nameSpace + ":");
            }
            else
            {
                return xPath;
            }
        }
    }

    //How to get the left, right, and mid part of a C# string?
    //https://kodify.net/csharp/strings/left-right-mid/

    public static class StringExtensions
    {
        /// <summary>
        /// Returns the left part of this string instance.
        /// </summary>
        /// <param name="count">Number of characters to return.</param>
        /// 

        public static string Left(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }

        /// <summary>
        /// Returns the right part of the string instance.
        /// </summary>
        /// <param name="count">Number of characters to return.</param>
        public static string Right(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
        }


        /// <summary>
        /// Returns the mid part of this string instance.
        /// </summary>
        /// <param name="start">Character index to start return the midstring from.</param>
        /// <returns>Substring or empty string when start is outside range.</returns>
        public static string Mid(this string input, int start)
        {
            return input.Substring(Math.Min(start, input.Length));
        }

        /// <summary>
        /// Returns the mid part of this string instance.
        /// </summary>
        /// <param name="start">Starting character index number.</param>
        /// <param name="count">Number of characters to return.</param>
        /// <returns>Substring or empty string when out of range.</returns>
        public static string Mid(this string input, int start, int count)
        {
            return input.Substring(Math.Min(start, input.Length), Math.Min(count, Math.Max(input.Length - start, 0)));
        }
    }
}