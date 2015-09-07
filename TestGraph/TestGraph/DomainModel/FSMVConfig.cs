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

    public FSMVConfig LoadFromXML(string xmlContent)
    {
      FSMVConfig fsmvConfigs = FSMVUtilities.FromXmlString<FSMVConfig>(xmlContent);
      return fsmvConfigs;
    }

    public FSMVConfig() 
    {
    
    }
  }
}
