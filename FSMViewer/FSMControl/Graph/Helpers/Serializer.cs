using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMControl
{
    public class Serializer<MACHINECONFIG,SEQCONFIG>
    {
        /// <summary>
        /// Serializes the configuration.
        /// </summary>
        /// <param name="fsmConfig">The FSM configuration.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void SerializeConfig(MACHINECONFIG fsmConfig, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MACHINECONFIG));
            //XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
           // xmlNamespace.Add("", "");
            TextWriter writer = new StreamWriter(fileName);
            xmlSerializer.Serialize(writer, fsmConfig);
            writer.Close();
        }

        /// <summary>
        /// Serializes the sequence.
        /// </summary>
        /// <param name="fsmSequenceConfig">The FSM sequence configuration.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void SerializeSequence(SEQCONFIG fsmSequenceConfig, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SEQCONFIG));
            //XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
           // xmlNamespace.Add("", "");
            TextWriter writer = new StreamWriter(fileName);
            xmlSerializer.Serialize(writer, fsmSequenceConfig);
            writer.Close();
        }
    }
}
