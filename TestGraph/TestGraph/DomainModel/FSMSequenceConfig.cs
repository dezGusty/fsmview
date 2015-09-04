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

    public void LoadFromXML(string xmlContent)
    {
      FSMSequenceConfig fsmsSequence = FSMVUtilities.FromXmlString<FSMSequenceConfig>(xmlContent);

      //MessageBox.Show("What a success");
      // do your work here
    }

    public FSMSequenceConfig() { }
  }
}
