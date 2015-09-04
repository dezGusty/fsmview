using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace TestGraph.DomainModel
{
  public class FSMVConfig
  {
    public Collection<FSMVTrigger> ArrayOfFSMVTrigger
    {
      get;
      set;
    }

    public Collection<FSMVState> ArrayOfFSMVState
    {
      get;
      set;
    }

    public string DefaultTriggerOnReset
    {
      get;
      set;
    }

    public string DefaultTriggerOnError
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
      FSMVConfig fsmvConfigs = FSMVUtilities.FromXmlString<FSMVConfig>(xmlContent);

      //MessageBox.Show("What a success");
      // do your work here
    }


    public FSMVConfig() {}
  }
}
