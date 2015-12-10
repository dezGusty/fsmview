using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace FSMControl.DomainModel.FirstVersion
{
  public class FirstStateMachine : StateMachine
  {
    ////stores the XML configuration, the states and the triggers
    public FSMConfig Configuration = new FSMConfig();

    ////stores the sequences
    public FSMSequenceConfig Sequences = new FSMSequenceConfig();

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstStateMachine"/> class.
    /// </summary>
    /// <param name="v">The version.</param>
    public FirstStateMachine(Version v)
    {
      this.MyGraph = new CustomGraph();
      this.Xml = v.Xml;
      this.XmlSeq = v.XmlSeq;
      this.CurrentVersion = v;
    }

    /// <summary>
    /// Gets the machine configuration.
    /// </summary>
    public override void GetDates()
    {
      this.Sequences = new FSMSequenceConfig();
      this.Configuration = new FSMConfig();
      this.Sequences = this.Sequences.LoadFromXML(this.XmlSeq);
      this.Configuration = this.Configuration.LoadFromXML(this.Xml);
      this.SetGraphNodes();
    }

    /// <summary>
    /// Gets the nodes and represents them.
    /// </summary>
    public override void SetGraphNodes()
    {
      foreach (FSMState item in this.Configuration.ArrayOfFSMState)
      {
        CustomVertex vertex = new CustomVertex(item.Name, Colors.Wheat);
        this.MyGraph.AddVertex(vertex);
      }

      FSMTrigger t = new FSMTrigger();
      try
      {
        foreach (FSMState state in this.Configuration.ArrayOfFSMState)
        {
          foreach (AllowedTrigger trigger in state.ArrayOfAllowedTrigger)
          {
            t = this.Configuration.FoundTriggerInList(trigger);
            if (string.IsNullOrEmpty(trigger.TriggerName))
            {
              this.MyGraph.AddNewEdge(trigger.StateAndTriggerName, this.MyGraph.GetVertexByName(state.Name), this.MyGraph.GetVertexByName(trigger.StateAndTriggerName));
            }
            else
            {
              this.MyGraph.AddNewEdge(trigger.TriggerName, this.MyGraph.GetVertexByName(state.Name), this.MyGraph.GetVertexByName(trigger.StateName));
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Gets the machine to next state with given step.
    /// </summary>
    /// <param name="curredntStep">The currednt step.</param>
    /// <param name="currentState">State of the current.</param>
    /// <returns></returns>
    public FSMState OneStep(FSMStep currentStep, FSMState currentState, Color color)
    {
      this.MyGraph.ChangeOneVertexColor(currentState.Name, color);
      FSMState newState = new FSMState();
      FSMTrigger triggerFounded = this.Configuration.FoundSteppInTriggerList(currentStep);
      if (triggerFounded != null)
      {
        AllowedTrigger allowedTrigger = new AllowedTrigger();
        allowedTrigger = currentState.FoundTriggerInCureentState(triggerFounded);
        if (allowedTrigger != null)
        {
          newState = new FSMState();
          if (string.IsNullOrEmpty(allowedTrigger.StateName))
          {
            newState = this.Configuration.FoundNextState(allowedTrigger.StateAndTriggerName);
          }
          else
          {
            newState = this.Configuration.FoundNextState(allowedTrigger.StateName);
          }

          if (newState != null)
          {
            this.MyGraph.ChangeOneVertexColor(newState.Name, color);
            CustomEdge edge = new CustomEdge(null, null);

            if (string.Compare(currentState.Name, newState.Name) == 0)
            {
              this.MyGraph.Vertices.Where(v => (string.Compare(v.Text, currentState.Name) == 0)).FirstOrDefault().Represented = true;
            }

            edge = this.MyGraph.GetEdgeBetween(currentState.Name, newState.Name);
            this.MyGraph.ChangeOneEdgeColor(edge, Colors.Yellow);
          }
        }
      }

      return newState;
    }

    /// <summary>
    /// Represents the sequence by changing vertices and edges color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="sequence">The sequence.</param>
    /// /// <param name="oneSequence">if is true the Property Represented will be setted true.</param>
    public OperationResult RepresentSequence(Color color, FSMSequence sequence, bool oneSequence)
    {
      if (oneSequence)
      {
        this.MyGraph.ResetToDefault();
      }

      FSMStep stepp = new FSMStep();
      FSMState initialState = new FSMState();
      FSMState otherState = new FSMState();

      if (sequence.ArrayOfStep.Count != 0)
      {
        initialState = this.Configuration.ArrayOfFSMState.FirstOrDefault();
        stepp = sequence.ArrayOfStep.First();
        otherState = this.OneStep(stepp, initialState, Colors.Yellow);

        for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
        {
          FSMStep step = new FSMStep();
          step = sequence.ArrayOfStep.ElementAt(i);
          if (!string.IsNullOrEmpty(otherState.Name))
          {
            FSMState auxState = new FSMState();
            auxState = otherState;
            otherState = new FSMState();
            otherState = this.OneStep(step, auxState, Colors.Yellow);
          }
        }
      }
      else
      {
        return new OperationResult(false, "Sequence with no steps!");
      }

      return new OperationResult(true, "Sequence represented succesfully!");
    }

    /// <summary>
    /// Represents this machine by representing all sequences.
    /// </summary>
    /// <returns></returns>
    public override void RepresentThisMachine(Color color)
    {
      this.MyGraph.ResetToDefault();
      if (this.Sequences.ArrayOfSequence.Count > 0)
      {
        foreach (FSMSequence s in this.Sequences.ArrayOfSequence)
        {
          this.RepresentSequence(color, s, false);
        }
      }
    }

    /// <summary>
    /// Adds a new state.
    /// </summary>
    /// <param name="stateName">Name of the state.</param>
    /// <param name="stateDefaultHandler">The state default handler.</param>
    /// <param name="stateReentryTrigger">The state reentry trigger.</param>
    /// <returns></returns>
    public override string AddNewState(string stateName, string stateDefaultHandler, string stateReentryTrigger)
    {
      if (!string.IsNullOrEmpty(stateName) && !string.IsNullOrEmpty(stateDefaultHandler) && !string.IsNullOrEmpty(stateReentryTrigger))
      {
        if (this.MyGraph.Vertices.Where(v => string.Compare(v.Text.ToLower(), stateName.ToLower()) == 0).FirstOrDefault() != null)
        {
          return string.Format("A vertex with the name {0} already exists in the graph!", stateName);
        }
        else
        {
          CustomVertex vertex = new CustomVertex(stateName, Colors.Wheat);
          FSMState state = new FSMState();
          state.Name = stateName;
          state.DefaultHandler = stateDefaultHandler;
          state.ReentryUsingTrigger = stateReentryTrigger;
          this.Configuration.ArrayOfFSMState.Add(state);
          this.MyGraph.AddVertex(vertex);
          return string.Format("Vertex {0} successfully added!", stateName);
        }
      }

      return "Please complete the fields first!";
    }

    /// <summary>
    /// Adds a new edge, a new allowed trigger to source vertex, a new trigger if it doesn't exist.
    /// </summary>
    /// <param name="vertexFrom">The vertex from.</param>
    /// <param name="trigger">The trigger.</param>
    /// <param name="vertexTo">The vertex to.</param>
    public override void AddNewEdge(CustomVertex vertexFrom, string trigger, CustomVertex vertexTo)
    {
      FSMState state = new FSMState();
      FSMControl.DomainModel.FirstVersion.AllowedTrigger aw = new FSMControl.DomainModel.FirstVersion.AllowedTrigger();
      FSMTrigger trig = new FSMTrigger();
      state.Name = vertexFrom.Text;
      if (string.IsNullOrEmpty(trigger))
      {
        CustomEdge edge = new CustomEdge(vertexTo.Text, vertexFrom, vertexTo);
        this.MyGraph.AddEdge(edge);
        aw.StateAndTriggerName = trig.CommonID = vertexTo.Text;
      }
      else
      {
        CustomEdge edge = new CustomEdge(trigger, vertexFrom, vertexTo);
        this.MyGraph.AddEdge(edge);
        aw.StateName = vertexTo.Text;
        aw.TriggerName = trigger;
        trig.Name = vertexTo.Text;
        trig.SequenceID = trigger;
      }

      foreach (var item in this.Configuration.ArrayOfFSMState)
      {
        if (item.Name == state.Name)
        {
          if (item.ArrayOfAllowedTrigger == null)
          {
            item.ArrayOfAllowedTrigger = new Collection<FSMControl.DomainModel.FirstVersion.AllowedTrigger>();
          }

          item.ArrayOfAllowedTrigger.Add(aw);
          break;
        }
      }

      this.Configuration.AddNewTrigger(trig);
      this.MyGraph.Message += string.Format("Edge from {0} to {1} added successfully!", vertexFrom, vertexTo);
    }
  }
}