using System;
using System.Xml;
using System.Xml.Schema;

namespace XMLDataValidation
{
    
    // XML Data Validation
    // https://www.c-sharpcorner.com/article/xml-data-validation/

    // Supporting Validation Types
    // [DTD
    // It is a text file whose syntax stems are directly from standard generalized markup language.It follows a non-XML syntax to define a set of valid tags.It allows us to specify the children for each tag, and few other properties.
    // [XDR]
    // It is a schema language and it is flexible and overcome the limitation of DTD.All data content is character data, XDR language allows to specify the data type of an element.
    // [XSD]
    // It defines the elements and attributes that form an XML document and each element is strongly typed. It is composed of primitive and derived types. 

    // Validation Event Handler Event
    //public delegate void ValidationEventHandler(
    // object sender,
    // validationEventArgs e
    //);

    //public class ValidationEventArgs : EventArgs
    //{
    //    public XmlSchemaException exception;
    //    public string Message;
    //    public XmlSeverityType severity;
    //}

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static bool ValidateDocument(string fileName)
        {
            XmlTextReader _coreReader = new XmlTextReader(fileName);

            XmlValidatingReader reader = new XmlValidatingReader(_coreReader);

            //Auto      - Determine the appropriate type of validation at the content of the document.
            //DTD       - Validate according to DTD
            //Schema    - validate according to the specified XSD scheme including line schemas.
            //XDR       - Validate to XDR scheme including in-line schemas.
            //None      - Create a nonvalidating reader and ignore any error invalidation

            reader.ValidationType = ValidationType.Auto;
            reader.ValidationEventHandler += new ValidationEventHandler(MyHandler);

            while (reader.Read())
            {
                
            }
            reader.Close();

            // Creates XML DOM from a variety of sources with stream, text, reader, file name.
            // The below target is shown when loading the document through an XML validating reader.
            //XmlTextReader _coreReader = new XmlTextReader(fileName);
            //XmlValidatingReader reader = new XmlValidatingReader(_ coreReader);
            //XmlDocument doc = new XmlDocument();
            //doc.Load(reader);


            return true;
        }

        static void MyHandler(object sender, ValidationEventArgs e)
        {

        }
    }
}
