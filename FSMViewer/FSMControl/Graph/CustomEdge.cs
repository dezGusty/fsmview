using System;
using System.ComponentModel;
using System.Windows.Media;
using GraphSharp;

namespace FSMControl
{
  /// <summary>
  /// Public CustomEdge class that extens the base class TypedEdge and implements interface INotifyPropertyChanged
  /// </summary>
  [Serializable]
  public class CustomEdge : TypedEdge<CustomVertex>, INotifyPropertyChanged
  {
    /// <summary>
    /// The trigger that gets the machine to a new State
    /// </summary>
    private string trigger;

    /// <summary>
    /// The color of edge
    /// </summary>
    private Color color;

    /// <summary>
    /// Gets or sets the color of the edge.
    /// </summary>
    /// <value>
    /// The color of the edge.
    /// </value>
    public Color EdgeColor
    {
      get
      {
        return this.color;
      }

      set
      {
        this.color = value;
        this.NotifyPropertyChanged("EdgeColor");
      }
    }

    /// <summary>
    /// Gets or sets the trigger.
    /// </summary>
    /// <value>
    /// The trigger.
    /// </value>
    public string Trigger
    {
      get
      {
        return this.trigger;
      }

      set
      {
        this.trigger = value;
        this.NotifyPropertyChanged("Trigger");
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomEdge"/> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public CustomEdge(CustomVertex source, CustomVertex target)
      : base(source, target, EdgeTypes.General)
    {
      this.color = Colors.Black;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomEdge"/> class.
    /// </summary>
    /// <param name="trig">The trig.</param>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="color">The color.</param>
    public CustomEdge(string trig, CustomVertex source, CustomVertex target)
      : base(source, target, EdgeTypes.General)
    {
      this.trigger = trig;
      this.color = Colors.Black;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomEdge"/> class.
    /// </summary>
    /// <param name="trig">The trig.</param>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="color">The color.</param>
    public CustomEdge(string trig, CustomVertex source, CustomVertex target, Color color)
      : base(source, target, EdgeTypes.General)
    {
      this.trigger = trig;
      this.color = color;
    }

    /// <summary>
    /// Compares to.
    /// </summary>
    /// <param name="edge">The edge.</param>
    /// <returns></returns>
    public bool CompareTo(CustomEdge edge)
    {
      if (edge != null)
      {
        if (string.Compare(this.Trigger.Trim(), edge.Trigger.Trim()) == 0)
        {
          if (string.Compare(this.Source.Text, edge.Source.Text) == 0 && string.Compare(this.Target.Text, edge.Target.Text) == 0)
          {
            return true;
          }
        }
      }

      return false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string info)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(info));
      }
    }

    internal bool IsSelfEdge()
    {
      if (string.Compare(this.Source.Text, this.Target.Text) == 0)
      {
        return true;
      }

      return false;
    }

    public override string ToString()
    {
      return this.Source.Text + " " + this.Target.Text + " " + this.Trigger;
    }
  }
}