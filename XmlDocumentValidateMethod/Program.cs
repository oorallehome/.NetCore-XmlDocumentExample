using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace XmlDocumentValidateMethod
{
    //[XmlDocument.Validate Method]
    // Validates teh XmlDocument against the XML Schema Definition Language(XSD) schemas contained in the Schemas property.
    // https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument.validate?view=netcore-3.1

    //[Overloads]
    // Validate(ValidationEventHandler) : Validates the XmlDocument against the XML Schema Definition Language(XSD) schemas contained in the Schemas property.
    // Validate(ValidationEventHandler, XmlNode) : Validates the XmlNode object specified against the XML Schema Definition Language(XSD) schemas in the Schemas property.
    class Program
    {
        // The following example illustrates use of the Validate method. The example creates an XmlDocument that contains an associated XSD schema
        // using the XmlReaderSettings and XmlReader objects. The example then uses the XPathNavigator class to incorrectly modify
        // the typed value of an element in the XML document generating a schema validation error.
        static void Main(string[] args)
        {
            Console.WriteLine($"ValidationType.Schema: {ValidationType.Schema}");
            try
            {

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://www.contoso.com/books", "contosoBooks.xsd");
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create("contosoBooks.xml", settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

                // The following call to Validate succeeds.
                document.Validate(eventHandler);

                // add a node so that the document is no longer valid
                XPathNavigator navigator = document.CreateNavigator();
                navigator.MoveToFollowing("price", "http://www.contoso.com/books");
                XmlWriter writer = navigator.InsertAfter();
                writer.WriteStartElement("anotherNode", "http://www.contoso.com/books");
                writer.WriteEndElement();
                writer.Close();

                // the document will now fail to successfully validate
                document.Validate(eventHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
