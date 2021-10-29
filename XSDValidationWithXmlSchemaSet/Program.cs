using System;
using System.Xml;
using System.Xml.Schema;

namespace XSDValidationWithXmlSchemaSet
{
    // XML schema (XSD) validation with XmlSchemaSet
    // https://docs.microsoft.com/en-us/dotnet/standard/data/xml/xml-schema-xsd-validation-with-xmlschemaset

    // XML documents can be validated against an XML schema definition language (XSD) schema in an XmlSchemaSet.
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //(1)
            //var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            //XmlSchemaSet schema = new XmlSchemaSet();
            //schema.Add("", path + "\\input.xsd");
            //XmlReader rd = XmlReader.Create(path + "\\input.xml");
            //XDocument doc = XDocument.Load(rd);
            //doc.Validate(schema, ValidationEventHandler);

            //(2)
            //XmlTextReader _coreReader = new XmlTextReader(fileName);
            //XmlValidatingReader reader = new XmlValidatingReader(_ coreReader);
            //reader.ValidationType = ValidationType.Auto;
            //reader.ValidationEventHandler += new ValidationEventHandler(MyHandler);
            //XmlDocument doc = new XmlDocument();
            //doc.Load(reader);

            // The schema above is added to the Schemas property of the XmlReaderSettings object. 
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("http://www.contoso.com/books", "books.xsd");

            // The validation Type property denotes the type of validation performed in the document
            //Auto      - Determine the appropriate type of validation at the content of the document.
            //DTD       - Validate according to DTD
            //Schema    - validate according to the specified XSD scheme including line schemas.
            //XDR       - Validate to XDR scheme including in-line schemas.
            //None      - Create a nonvalidating reader and ignore any error invalidation.
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            // The XmlReaderSettings object is passed as a parameter to the Create method of the XmlReader object,
            // which validates the XML document above.
            XmlReader books = XmlReader.Create("books.xml", booksSettings);

            while (books.Read()) { };
            books.Close();
        }

        // ValidationEventArgs e
        //public class ValidationEventArgs : EventArgs
        //{
        //    public XmlSchemaException exception;
        //    public string Message;
        //    public XmlSeverityType severity;
        //}
        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}


