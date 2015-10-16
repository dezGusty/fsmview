using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FiniteStateMachineViewer.DomainModel
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
    }
}
