using System;
using System.Collections.ObjectModel;
using System.IO;
using FSMControl.DomainModel.Model.Classes;
using FSMControl.DomainModel.Model.Interfaces;

namespace FSMControl.DomainModel.FirstVersion
{
  [Serializable]
  public class FSMSequenceConfig : SequenceConfig, ISequenceConfigInterface<FSMSequenceConfig>
  {
    /// <summary>
    /// Gets or sets the array of sequence.
    /// </summary>
    /// <value>
    /// The array of sequence.
    /// </value>
    public Collection<FSMSequence> ArrayOfSequence
    {
      get;
      set;
    }

    /// <summary>
    /// Loads from XML file.
    /// </summary>
    /// <param name="absoluteFilePath">The absolute file path.</param>
    public void LoadFromXMLFileAbsolute(string absoluteFilePath)
    {
      string xmlContent = File.ReadAllText(absoluteFilePath);
      this.LoadFromXML(xmlContent);
    }

    /// <summary>
    /// Loads from XML an returns a FSMSequenceConfig object.
    /// </summary>
    /// <param name="xmlContent">Content of the XML.</param>
    /// <returns></returns>
    public FSMSequenceConfig LoadFromXML(string xmlContent)
    {
      FSMSequenceConfig fsmsSequence = Utilities.FromXmlString<FSMSequenceConfig>(xmlContent);
      return fsmsSequence;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FSMSequenceConfig"/> class.
    /// </summary>
    public FSMSequenceConfig()
    {
    }
  }
}