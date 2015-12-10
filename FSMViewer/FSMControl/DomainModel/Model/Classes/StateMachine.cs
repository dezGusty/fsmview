using System.Windows.Media;

namespace FSMControl
{
  public class StateMachine
  {
    public CustomGraph MyGraph;

    public Version CurrentVersion
    {
      get;
      set;
    }

    public string Xml
    {
      get;
      set;
    }

    public string XmlSeq
    {
      get;
      set;
    }

    public virtual void GetDates()
    {
    }

    public virtual void SetGraphNodes()
    {
    }

    public virtual void RepresentThisMachine(Color color)
    {
    }

    public virtual string AddNewState(string stateName, string stateDefaultHandler, string stateReentryTrigger)
    {
      return null;
    }

    public virtual void AddNewEdge(CustomVertex vertexFrom, string trigger, CustomVertex vertexTo)
    {
    }
  }
}