using System;
using System.Collections.ObjectModel;
using FSMControl.DomainModel.Model.Interfaces;

namespace FSMControl.DomainModel.SecondVersion
{
  [Serializable]
  public class FSMVState : IStateInterface<AllowedTrigger, FSMVTrigger>
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the reentry using trigger.
    /// </summary>
    /// <value>
    /// The reentry using trigger.
    /// </value>
    public string ReentryUsingTrigger
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the array of allowed trigger.
    /// </summary>
    /// <value>
    /// The array of allowed trigger.
    /// </value>
    public Collection<AllowedTrigger> ArrayOfAllowedTrigger
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the default handler.
    /// </summary>
    /// <value>
    /// The default handler.
    /// </value>
    public string DefaultHandler
    {
      get;
      set;
    }

    ////returneaza numele starii in care duce trigerul cu numele trigName si stare curenta\
    public AllowedTrigger FoundTriggerInCureentState(FSMVTrigger trig)
    {
      if (trig != null)
      {
        foreach (AllowedTrigger item in this.ArrayOfAllowedTrigger)
        {
          if (string.IsNullOrEmpty(item.TriggerName))
          {
            if (string.Compare(item.StateAndTriggerName, trig.CommonID) == 0)
            {
              return item;
            }
          }
          else
          {
            if (string.Compare(item.TriggerName, trig.Name) == 0)
            {
              return item;
            }
          }
        }
      }

      return null;
    }
  }
}