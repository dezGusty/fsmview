using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FSMControl.DomainModel.Model.Interfaces;

namespace FSMControl.DomainModel.SecondVersion
{
  public class FSMVConfig : IConfigInterface<AllowedTrigger, FSMVConfig, FSMVTrigger, FSMVState, FSMStep>
  {
    /// <summary>
    /// Gets or sets the array of FSMV trigger.
    /// </summary>
    /// <value>
    /// The array of FSMV trigger.
    /// </value>
    [XmlArray("ArrayOfFSMVTrigger")]
    public Collection<FSMVTrigger> ArrayOfFSMVTrigger
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the state of the array of FSMV.
    /// </summary>
    /// <value>
    /// The state of the array of FSMV.
    /// </value>
    [XmlArray("ArrayOfFSMVState")]
    public Collection<FSMVState> ArrayOfFSMVState
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the default trigger on error.
    /// </summary>
    /// <value>
    /// The default trigger on error.
    /// </value>
    public string DefaultTriggerOnError
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the default trigger on reset.
    /// </summary>
    /// <value>
    /// The default trigger on reset.
    /// </value>
    public string DefaultTriggerOnReset
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
    /// Loads from XML.
    /// </summary>
    /// <param name="xmlContent">Content of the XML.</param>
    /// <returns></returns>
    public FSMVConfig LoadFromXML(string xmlContent)
    {
      FSMVConfig fsmvConfigs = Utilities.FromXmlString<FSMVConfig>(xmlContent);
      return fsmvConfigs;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FSMVConfig"/> class.
    /// </summary>
    public FSMVConfig()
    {
    }

    /// <summary>
    /// Founds the AllowdTrigger from a State configuration in list of triggers.
    /// </summary>
    /// <param name="trig">The trig.</param>
    /// <returns></returns>
    public FSMVTrigger FoundTriggerInList(AllowedTrigger trig)
    {
      return this.ArrayOfFSMVTrigger.Where(t => t.Name == trig.TriggerName || t.Name == trig.StateAndTriggerName || t.CommonID == trig.StateAndTriggerName).FirstOrDefault();
    }

    public FSMVState FoundNextState(string stateName)
    {
      return this.ArrayOfFSMVState.Where(state => string.Compare(state.Name.Trim(), stateName.Trim()) == 0).FirstOrDefault();
    }

    public FSMVTrigger FoundTriggerInList(FSMStep stepp)
    {
      foreach (FSMVTrigger trigger in this.ArrayOfFSMVTrigger)
      {
        if (string.IsNullOrEmpty(trigger.CommonID))
        {
          if (string.Compare(trigger.SequenceID.Trim(), stepp.Name.Trim()) == 0)
          {
            return trigger;
          }
        }
        else
        {
          if (string.Compare(trigger.CommonID.Trim(), stepp.Name.Trim()) == 0)
          {
            return trigger;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Adds a new trigger to trigger list.
    /// </summary>
    /// <param name="triggerToAdd">The trigger to add.</param>
    /// <returns></returns>
    public bool AddNewTrigger(FSMVTrigger triggerToAdd)
    {
      bool exists = false;
      foreach (FSMVTrigger item in this.ArrayOfFSMVTrigger)
      {
        if (item.CompareTo(triggerToAdd) == true)
        {
          exists = true;
          break;
        }
      }

      if (exists == false)
      {
        this.ArrayOfFSMVTrigger.Add(triggerToAdd);
        return true;
      }

      return false;
    }

    public FSMVTrigger FoundSteppInTriggerList(FSMStep step)
    {
      foreach (FSMVTrigger item in this.ArrayOfFSMVTrigger)
      {
        if (string.IsNullOrEmpty(item.CommonID))
        {
          if (string.Compare(item.SequenceID, step.Name) == 0)
          {
            return item;
          }
        }
        else
        {
          if (string.Compare(item.CommonID, step.Name) == 0)
          {
            return item;
          }
        }
      }

      return null;
    }

    public FSMVTrigger FoundStringTriggerList(string triggerName)
    {
      foreach (FSMVTrigger item in this.ArrayOfFSMVTrigger)
      {
        if (string.IsNullOrEmpty(item.CommonID))
        {
          if (string.Compare(item.Name, triggerName) == 0)
          {
            return item;
          }
        }
        else
        {
          if (string.Compare(item.CommonID, triggerName) == 0)
          {
            return item;
          }
        }
      }

      return null;
    }
  }
}