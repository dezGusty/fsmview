using System;
using System.Collections.ObjectModel;
using FSMControl.DomainModel.Model.Interfaces;

namespace FSMControl.DomainModel.FirstVersion
{
  [Serializable]
  public class FSMState : IStateInterface<AllowedTrigger, FSMTrigger>
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
    /// Shoulds the serialize reentry using trigger.
    /// </summary>
    /// <returns></returns>
    public bool ShouldSerializeReentryUsingTrigger()
    {
      if (string.IsNullOrEmpty(this.ReentryUsingTrigger))
      {
        return false;
      }

      return true;
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

    /// <summary>
    /// Shoulds the serialize default handler.
    /// </summary>
    /// <returns></returns>
    public bool ShouldSerializeDefaultHandler()
    {
      if (string.IsNullOrEmpty(this.DefaultHandler))
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// Founds the state of the trigger in cureent.
    /// </summary>
    /// <param name="trig">The trig.</param>
    /// <returns></returns>
    public AllowedTrigger FoundTriggerInCureentState(FSMTrigger trig)
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