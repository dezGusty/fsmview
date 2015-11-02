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

        public static void SerializeConfig(FSMConfig o)
        {
            XmlSerializer s = new XmlSerializer(typeof(FSMConfig));
            TextWriter writer = new StreamWriter("FSMConfigSerialized.xml");
            s.Serialize(writer, o);
            writer.Close();
        }

        public static void SerializeSequence(FSMSequenceConfig o)
        {
            XmlSerializer s = new XmlSerializer(typeof(FSMSequenceConfig));
            TextWriter writer = new StreamWriter("FSMSequenceSerialized.xml");
            s.Serialize(writer, o);
            writer.Close();
        }
    }
}
