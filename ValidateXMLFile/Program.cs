using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ValidateXMLFile
{
    class Program
    {
        // How To Validate XML Using XSD In C#
        // https://www.c-sharpcorner.com/article/how-to-validate-xml-using-xsd-in-c-sharp/

        // Console.WriteLine Method
        // docs.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-5.0#System_Console_WriteLine_System_String_
        static void Main(string[] args)
        {
            Console.WriteLine($"System.Reflection.Assembly.GetExecutingAssembly().CodeBase : {System.Reflection.Assembly.GetExecutingAssembly().CodeBase}");
            Console.WriteLine($"Path.GetDirectoryName : {Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)}");
            Console.WriteLine($"new Uri().LocalPath : {new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath}");

            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", path + "\\input.xsd");

            XmlReader rd = XmlReader.Create(path + "\\input.xml");
            XDocument doc = XDocument.Load(rd);
            doc.Validate(schema, ValidationEventHandler);

        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error) throw new Exception(e.Message);
            }
        }
    }
}
