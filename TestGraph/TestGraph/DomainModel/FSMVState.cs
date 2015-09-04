using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestGraph.DomainModel
{
  public class FSMVState
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

    public string ReentryUsingTrigger
    {
      get;
      set;
    }

    public Collection<AllowedTrigger> ArrayOfAllowedTrigger
    {
      get;
      set;
    }

    public string DefaultHandler
    {
      get;
      set;
    }
  }
}
