using System;
using System.Xml;
using System.Xml.Schema;

namespace ValidatingXMLDocumentInDOM
{
    // Validating an XML Document in the DOM
    // https://docs.microsoft.com/en-us/dotnet/standard/data/xml/validating-an-xml-document-in-the-dom

    // TheXmlDocument class does not validate the XML in the Document Object Model (DOM) against an XML Schema definition language (XSD) schema
    // or document type definition (DTD) by default;

    // To validate the XML in the DOM, you can validate the XML as it is loaded into the DOM by passing a schema-validating XmlReader to the Load method of the XmlDocument class,
    // or validate a previously unvalidated XML document in the DOM using the Validate method of the XmlDocument class.


    // After successful validation, schema defaults are applied, text values are converted to atomic values as necessary, and type information is associated with validated information items.
    // As a result, typed XML data replaces previously untyped XML data.
    class Program
    {
        static void Main(string[] args)
        {
            CreatingXMLSchemaValidatingXmlReader();
            ValidatingXMLDocumentInDOM();
        }
        static void ValidatingXMLDocumentInDOM()
        {
            try 
            {
                // Create a new XmlDocument instance and load
                // The XML document into the DOM.
                XmlDocument document = new XmlDocument();
                document.Load("contosoBooks.xml");

                // Add the XML schema for the XML document to the 
                // Schema property of the XmlDocument.
                document.Schemas.Add("http://www.contoso.com/books", "contosoBooks.xsd");

                // Validate the XML Document loaded into the DOM.
                document.Validate(ValidationEventHandler);

                // Make an invalid change to the first and last
                // price elements in the XML document, and write
                // the XmlSchemaInfo values assigned to the price
                // element during validation to the console.
                XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("bk", "http://www.contoso.com/books");

                XmlNode priceNode = document.SelectSingleNode(@"/bk:bookstore/bk:book/bk:price", manager);

                Console.WriteLine("SchemaInfo.IsDefault: {0}", priceNode.SchemaInfo.IsDefault);
                Console.WriteLine("SchemaInfo.IsNil: {0}", priceNode.SchemaInfo.IsNil);
                Console.WriteLine("SchemaInfo.SchemaElement: {0}", priceNode.SchemaInfo.SchemaElement);
                Console.WriteLine("SchemaInfo.SchemaType: {0}", priceNode.SchemaInfo.SchemaType);
                Console.WriteLine("SchemaInfo.Validity: {0}", priceNode.SchemaInfo.Validity);

                priceNode.InnerXml = "A";

                XmlNodeList priceNodes = document.SelectNodes(@"/bk:bookstore/bk:book/bk:price", manager);
                XmlNode lastprice = priceNodes[priceNodes.Count - 1];

                lastprice.InnerXml = "B";

                // Validate the XML document with the invalid changes.
                // The invalid changes cause schema validation errors.
                document.Validate(ValidationEventHandler);

                // Correct the invalid change to the first price element.
                priceNode.InnerXml = "8.99";

                // Validate only the first book element. The last book
                // element is invalid, but not included in validation.
                XmlNode bookNode = document.SelectSingleNode(@"/bk:bookstore/bk:book", manager);
                document.Validate(ValidationEventHandler, bookNode);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.XmlException: {0}", ex.Message);
            }
            catch (XmlSchemaValidationException ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.XmlSchemaValidationException: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.Exception: {0}", ex.Message);
            }
        }
        static void CreatingXMLSchemaValidatingXmlReader()
        {
            // Console.WriteLine("Hello World!");
            // [Creating an XML Schema-Validating XmlReader]
            // To create an XML schema-validating XmlReader, follow steps.
            // 1. Construct a new XmlReaderSettins instance.
            // 2. Add an XML schema to the Schemas property of the XmlReaderSettings instance.
            // 3. Specify Schema as the ValidationType.
            // 4. Optionally specify ValidationFlags and a ValidationEventHandler to handle schema validation errors and warnings encountered during validation.
            // 5. Finally, pass the XmlReaderSettings object to the Create method of the XmlReader class along with the XML document, creating a schema-validating XmlReader.
            try
            {
                // Create a schema validating XmlReader.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://www.contoso.com/books", "contosoBooks.xsd");
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
                settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create("contosoBooks.xml", settings);

                // The XmlDocument validates the XML document contained
                // in the XmlReader as it is loaded into the DOM.
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                // Make an invalid change to the first and last
                // price elements in the XML document, and write
                // the XmlSchemaInfo values assigned to the price
                // element during load validation to the console.

                XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("bk", "http://www.contoso.com/books");

                XmlNode priceNode = document.SelectSingleNode(@"/bk:bookstore/bk:book/bk:price", manager);

                Console.WriteLine("SchemaInfo.IsDefault: {0}", priceNode.SchemaInfo.IsDefault);
                Console.WriteLine("SchemaInfo.IsNil: {0}", priceNode.SchemaInfo.IsNil);
                Console.WriteLine("SchemaInfo.SchemaElement: {0}", priceNode.SchemaInfo.SchemaElement);
                Console.WriteLine("SchemaInfo.SchemaType: {0}", priceNode.SchemaInfo.SchemaType);
                Console.WriteLine("SchemaInfo.Validity: {0}", priceNode.SchemaInfo.Validity);

                priceNode.InnerXml = "A";

                XmlNodeList priceNodes = document.SelectNodes(@"/bk:bookstore/bk:book/bk:price", manager);
                XmlNode lastprice = priceNodes[priceNodes.Count - 1];

                lastprice.InnerXml = "B";

                // Validate the XML document with the invalid changes.
                // The invaild changes cause schema validation errors.
                document.Validate(ValidationEventHandler);

                // Correct the invalid change to the first price element.
                priceNode.InnerXml = "8.99";

                // Validate only the first book element. The last book
                // element is invalid, but not included in validation.
                XmlNode bookNode = document.SelectSingleNode(@"/bk:bookstore/bk:book", manager);
                document.Validate(ValidationEventHandler, bookNode);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.XmlException: {0}", ex.Message);
            }
            catch (XmlSchemaValidationException ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.XmlSchemaValidationException: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("XmlDocumentValidationExample.Exception: {0}", ex.Message);
            }
        }
        static void ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("\nWARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("\nERROR: ");

            Console.WriteLine(args.Message);
        }
    }
}
