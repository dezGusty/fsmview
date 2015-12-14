using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace FSMControl.DomainModel.SecondVersion
{
  public class SecondStateMachine : StateMachine
  {
    ////stores the XML configuration, the states and the triggers
    public FSMVConfig Configuration
    {
      get;
      set;
    }

    ////stores the sequences
    public FSMSequenceConfig Sequences
    {
      get;
      set;
    }

    public SecondStateMachine(Version v)
    {
      this.Configuration = new FSMVConfig();
      this.Sequences = new FSMSequenceConfig();
      this.MyGraph = new CustomGraph();
      this.Xml = v.Xml;
      this.XmlSeq = v.XmlSeq;
      this.CurrentVersion = v;
    }

    public override void GetDates()
    {
      this.Sequences = new FSMSequenceConfig();
      this.Configuration = new FSMVConfig();
      this.Sequences = this.Sequences.LoadFromXML(this.XmlSeq);  ////load sequences from XML
      this.Configuration = this.Configuration.LoadFromXML(this.Xml); ////load XML configuration
      this.SetGraphNodes();
    }

    /// <summary>
    /// Gets the nodes and represents them.
    /// </summary>
    public override void SetGraphNodes()
    {
      foreach (FSMVState item in this.Configuration.ArrayOfFSMVState)
      {
        CustomVertex vertex = new CustomVertex(item.Name, Colors.Wheat);
        this.MyGraph.AddVertex(vertex);
      }

      FSMVTrigger t = new FSMVTrigger();
      try
      {
        foreach (FSMVState state in this.Configuration.ArrayOfFSMVState)
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
    public FSMVState OneStep(FSMStep currentStep, FSMVState currentState, Color color)
    {
      this.MyGraph.ChangeOneVertexColor(currentState.Name, color);
      FSMVState newState = new FSMVState();
      FSMVTrigger triggerFounded = this.Configuration.FoundSteppInTriggerList(currentStep);
      if (triggerFounded != null)
      {
        AllowedTrigger allowedTrigger = new AllowedTrigger();
        allowedTrigger = currentState.FoundTriggerInCureentState(triggerFounded);
        if (allowedTrigger != null)
        {
          newState = new FSMVState();
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

      FSMVState initialState = new FSMVState();
      FSMVState otherState = new FSMVState();

      if (sequence.ArrayOfStep.Count != 0)
      {
        initialState = this.Configuration.ArrayOfFSMVState.FirstOrDefault();
        stepp = sequence.ArrayOfStep.First();
        otherState = this.OneStep(stepp, initialState, Colors.Yellow);

        for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
        {
          FSMStep step = new FSMStep();
          step = sequence.ArrayOfStep.ElementAt(i);
          if (!string.IsNullOrEmpty(otherState.Name))
          {
            FSMVState auxState = new FSMVState();
            auxState = otherState;
            otherState = new FSMVState();
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

    public override string AddNewState(string stateName, string stateDefaultHandler, string stateReentryTrigger)
    {
      if (!string.IsNullOrEmpty(stateName) && !string.IsNullOrEmpty(stateDefaultHandler) && !string.IsNullOrEmpty(stateReentryTrigger))
      {
        if (this.MyGraph.Vertices.Where(v => string.Compare(v.Text.ToLower(), stateName.ToLower()) == 0).FirstOrDefault() != null)
        {
          return "A vertex with the name " + stateName + "   already exists in the graph!";
        }
        else
        {
          CustomVertex vertex = new CustomVertex(stateName, Colors.Wheat);
          FSMVState state = new FSMVState();
          state.Name = stateName;
          state.DefaultHandler = stateDefaultHandler;
          state.ReentryUsingTrigger = stateReentryTrigger;
          this.Configuration.ArrayOfFSMVState.Add(state);
          this.MyGraph.AddVertex(vertex);
          return "Vertex " + stateName + "   successfully added!";
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
      FSMVState state = new FSMVState();
      AllowedTrigger aw = new AllowedTrigger();
      FSMVTrigger trig = new FSMVTrigger();
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

      foreach (var item in this.Configuration.ArrayOfFSMVState)
      {
        if (item.Name == state.Name)
        {
          if (item.ArrayOfAllowedTrigger == null)
          {
            item.ArrayOfAllowedTrigger = new Collection<AllowedTrigger>();
          }

          item.ArrayOfAllowedTrigger.Add(aw);
          break;
        }
      }

      this.Configuration.AddNewTrigger(trig);
      this.MyGraph.Message += string.Format("Edge from {0} to {1} added successfully!", vertexFrom, vertexTo);
    }

    public override string DeleteEdge(string sourceText, string targetText)
    {
      CustomVertex source = this.MyGraph.GetVertexByName(sourceText);
      CustomVertex target = this.MyGraph.GetVertexByName(targetText);

      if (source == null)
      {
        return string.Format("Vertex with name {0} doesn't exist!", sourceText);
      }

      if (target == null)
      {
        return string.Format("Vertex with name {0} doesn't exist!", targetText);
      }

      this.MyGraph.RemoveEdgeIf(v => v.Source.Text.Equals(source.Text) && v.Target.Text.Equals(target.Text));

      foreach (var item in this.Configuration.ArrayOfFSMVState)
      {
        if (string.Compare(item.Name, sourceText) == 0)
        {
          foreach (var it in item.ArrayOfAllowedTrigger.ToList())
          {
            if (string.Compare(it.StateAndTriggerName, targetText) == 0 || string.Compare(it.StateName, targetText) == 0)
            {
              item.ArrayOfAllowedTrigger.Remove(it);
            }
          }
        }
      }

      return string.Format("Successfully deleted an edge between {0} and {1}", source.Text, target.Text);
    }

    public override string DeleteVertex(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return "Invalid name \n It cannot be empty!";
      }

      CustomVertex vertex = this.MyGraph.GetVertexByName(text);
      if (vertex == null)
      {
        return string.Format("A vertex with name {0} doesn't exist in the graph!", text);
      }

      foreach (var item in this.Configuration.ArrayOfFSMVState)
      {
        if (string.Compare(item.Name, text) == 0)
        {
          this.Configuration.ArrayOfFSMVState.Remove(item);
          break;
        }
      }

      this.MyGraph.RemoveVertex(vertex);
      foreach (var item in this.Configuration.ArrayOfFSMVState)
      {
        foreach (var it in item.ArrayOfAllowedTrigger.ToList())
        {
          if (string.Compare(it.StateAndTriggerName, vertex.Text) == 0 || string.Compare(it.StateName, vertex.Text) == 0)
          {
            item.ArrayOfAllowedTrigger.Remove(it);
          }
        }
      }

      return string.Format("Successfully deleted vertex {0}!", text);
    }

    public FSMSequence AddNewStep(FSMSequence sequence, string steppTrigger)
    {
      FSMControl.DomainModel.SecondVersion.FSMStep step = new FSMControl.DomainModel.SecondVersion.FSMStep();
      FSMVTrigger triig = new FSMVTrigger();
      triig = this.Configuration.FoundStringTriggerList(steppTrigger);
      if (string.IsNullOrEmpty(triig.SequenceID))
      {
        step.Name = triig.CommonID;
      }
      else
      {
        step.Name = triig.SequenceID;
      }

      step.Weight = "2";
      step.TimeoutInSeconds = " 2";
      if (!string.IsNullOrEmpty(step.Name))
      {
        sequence.ArrayOfStep.Add(step);
      }

      return sequence;
    }
  }
}