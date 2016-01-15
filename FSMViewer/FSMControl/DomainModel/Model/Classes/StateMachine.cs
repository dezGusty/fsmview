using System.Windows.Media;

namespace FSMControl
{
  public class StateMachine
  {
    public CustomGraph MyGraph
    {
      get;
      set;
    }

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
      return string.Empty;
    }

    public virtual void AddNewEdge(CustomVertex vertexFrom, string trigger, CustomVertex vertexTo)
    {
    }

    public virtual string DeleteVertex(string text)
    {
      return string.Empty;
    }

    public virtual string DeleteEdge(string sourceText, string targetText)
    {
      return string.Empty;
    }
  }
}