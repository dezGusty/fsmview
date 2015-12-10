using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMControl.DomainModel.FirstVersion
{
  [Serializable]
  public class FSMStep
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
    /// Gets or sets the weight.
    /// </summary>
    /// <value>
    /// The weight.
    /// </value>
    public string Weight
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the timeout in seconds.
    /// </summary>
    /// <value>
    /// The timeout in seconds.
    /// </value>
    public string TimeoutInSeconds
    {
      get;
      set;
    }
  }
}
