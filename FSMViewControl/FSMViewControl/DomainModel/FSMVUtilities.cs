using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FSMViewControl.DomainModel
{
    public class FSMVUtilities
    {
        /// <summary>
        /// Froms the XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData">The XML data.</param>
        /// <returns></returns>
        public static T FromXmlString<T>(string xmlData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamReader streamReader = new StreamReader(xmlData);
            return (T)xmlSerializer.Deserialize(streamReader);
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData">The XML data.</param>
        public static void ToXmlString<T>(string xmlSource, object O)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamWriter streamWriter = new StreamWriter(xmlSource);
            xmlSerializer.Serialize(streamWriter, O);
        }


        public static void SerializeObject(string filename)
        {
            Console.WriteLine("Writing With XmlTextWriter");

            XmlSerializer serializer =
            new XmlSerializer(typeof(FSMConfig));
            FSMConfig i = new FSMConfig();
            i.LoadFromXML("../../WinCCOADeployment.xml");
            // Create an XmlTextWriter using a FileStream.
            Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriter writer =
            new XmlTextWriter(fs, Encoding.Unicode);
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, i);
            writer.Close();
        }
    }
}
