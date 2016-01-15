using System.IO;
using System.Xml.Serialization;

namespace FSMControl
{
  public class Serializer<MACHINECONFIG, SEQCONFIG>
  {
    /// <summary>
    /// Serializes the configuration.
    /// </summary>
    /// <param name="fsmConfig">The FSM configuration.</param>
    /// <param name="fileName">Name of the file.</param>
    public static void SerializeConfig(MACHINECONFIG fsmConfig, string fileName)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MACHINECONFIG));
      XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
      xmlNamespace.Add(string.Empty, string.Empty);
      TextWriter writer = new StreamWriter(fileName);
      xmlSerializer.Serialize(writer, fsmConfig, xmlNamespace);
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
      XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
      xmlNamespace.Add(string.Empty, string.Empty);
      TextWriter writer = new StreamWriter(fileName);
      xmlSerializer.Serialize(writer, fsmSequenceConfig, xmlNamespace);
      writer.Close();
    }
  }
}