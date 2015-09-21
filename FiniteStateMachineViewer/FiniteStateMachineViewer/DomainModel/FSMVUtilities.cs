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
    }
}
