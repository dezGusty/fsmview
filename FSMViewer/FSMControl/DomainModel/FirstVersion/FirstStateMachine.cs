using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace FSMControl.DomainModel.FirstVersion
{
    public class FirstStateMachine : StateMachine
    {
        ////stores the XML configuration, the states and the triggers
        public FSMConfig Configuration
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstStateMachine"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public FirstStateMachine(Version version)
        {
            this.MyGraph = new CustomGraph();
            this.Configuration = new FSMConfig();
            this.Sequences = new FSMSequenceConfig();
            this.Xml = version.Xml;
            this.XmlSeq = version.XmlSeq;
            this.CurrentVersion = version;
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
            foreach (FSMState state in this.Configuration.ArrayOfFSMState)
            {
                CustomVertex vertex = new CustomVertex(state.Name, Colors.Wheat, true);
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
                foreach (FSMSequence sequence in this.Sequences.ArrayOfSequence)
                {
                    this.RepresentSequence(color, sequence, false);
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
            if (!string.IsNullOrEmpty(stateName))
            {
                if (this.MyGraph.Vertices.Where(v => string.Compare(v.Text.ToLower(), stateName.ToLower()) == 0).FirstOrDefault() != null)
                {
                    return string.Format("A vertex with the name {0} already exists in the graph!", stateName);
                }
                else
                {
                    CustomVertex vertex = new CustomVertex(stateName, Colors.Wheat, true);
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
                this.MyGraph.AddNewEdge(vertexTo.Text, vertexFrom, vertexTo);
                aw.StateAndTriggerName = trig.CommonID = vertexTo.Text;
            }
            else
            {
                this.MyGraph.AddNewEdge(vertexTo.Text, vertexFrom, vertexTo);
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

            if (source.CompareTo(target))
            {
                if (this.MyGraph.RemoveItselfEdge(source))
                {
                    return "Edge to " + sourceText + " succesfully deleted!";
                }
            }
            else
            {
                foreach (var item in this.Configuration.ArrayOfFSMState)
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

                this.MyGraph.RemoveEdgeIf(v => v.Source.Text.Equals(source.Text) && v.Target.Text.Equals(target.Text));
                return string.Format("Successfully deleted an edge between {0} and {1}", source.Text, target.Text);
            }

            return string.Format("This edge doesn't exist!");
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

            foreach (var item in this.Configuration.ArrayOfFSMState)
            {
                if (string.Compare(item.Name, text) == 0)
                {
                    this.Configuration.ArrayOfFSMState.Remove(item);
                    break;
                }
            }

            this.MyGraph.RemoveVertex(vertex);
            foreach (var item in this.Configuration.ArrayOfFSMState)
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
            FSMStep step = new FSMStep();
            FSMTrigger triig = new FSMTrigger();
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

        public override void HideEdges(CustomVertex vertex, List<CustomEdge> edgesIn, List<CustomEdge> edgesOut)
        {
            if (vertex.BackgroundColor == Colors.Wheat || vertex.Display == true)
            {
                this.MyGraph.GetVertexByName(vertex.Text).BackgroundColor = Colors.Red;
                this.MyGraph.GetVertexByName(vertex.Text).Display = false;
                IEnumerable<CustomEdge> collectionIn = this.MyGraph.InEdges(vertex);
                IEnumerable<CustomEdge> collectionOut = this.MyGraph.OutEdges(vertex);
                foreach (var item in collectionIn)
                {
                    CustomEdge edge = new CustomEdge(item.Trigger, item.Source, item.Target);
                    edgesIn.Add(edge);
                }

                foreach (var item in collectionOut)
                {
                    CustomEdge edge = new CustomEdge(item.Trigger, item.Source, item.Target);
                    edgesOut.Add(edge);
                }

                this.MyGraph.ClearEdges(vertex);
            }
            else
            {
                MessageBox.Show("Already hidden!");
            }

        }

        public override void UnhideEdges(CustomVertex vertex, List<CustomEdge> edgesIn, List<CustomEdge> edgesOut)
        {
            if (vertex.BackgroundColor == Colors.Red || vertex.Display == false)
            {
                this.MyGraph.GetVertexByName(vertex.Text).BackgroundColor = Colors.Wheat;
                this.MyGraph.GetVertexByName(vertex.Text).Display = true;
                for (int i = 0; i < edgesIn.Count; i++)
                {
                    if (string.Compare(edgesIn[i].Target.Text, vertex.Text) == 0 || string.Compare(edgesIn[i].Trigger, vertex.Text) == 0)
                    {
                        CustomEdge edge = new CustomEdge(edgesIn[i].Trigger, edgesIn[i].Source, edgesIn[i].Target);
                        this.MyGraph.AddEdge(edge);
                        edgesIn.Remove(edgesIn[i]);
                        i--;
                    }
                }

                int lenghtOut = edgesOut.Count;
                for (int i = 0; i < edgesOut.Count; i++)
                {
                    if (string.Compare(edgesOut[i].Source.Text, vertex.Text) == 0)
                    {
                        CustomEdge edge = new CustomEdge(edgesOut[i].Trigger, edgesOut[i].Source, edgesOut[i].Target);
                        this.MyGraph.AddEdge(edge);
                        edgesOut.Remove(edgesOut[i]);
                        i--;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nothing to unhide!");
            }
        }
    }
}