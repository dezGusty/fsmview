using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraph.DomainModel
{
  public class FSMSequence
  {
    public string Name
    {
      get;
      set;
    }

    public string Description
    {
      get;
      set;
    }

    public string FinalDescription
    {
      get;
      set;
    }

    public Collection<FSMStep> ArrayOfStep
    {
      get;
      set;
    }
  }
}
