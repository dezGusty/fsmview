using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraph.DomainModel
{
  public class FSMSequenceConfig
  {
    public Collection<FSMSequence> ArrayOfSequence
    {
      get;
      set;
    }

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
      FSMSequenceConfig fsmsSequence = FSMVUtilities.FromXmlString<FSMSequenceConfig>(xmlContent);
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
