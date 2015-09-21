using FiniteStateMachineViewer.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FiniteStateMachineViewer.ViewModel
{
    public class StateMachine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine"/> class.
        /// </summary>
        public StateMachine()
        {
            seq = seq.LoadFromXML(xmlSeq);  //load sequences from XML
            config = config.LoadFromXML(xml); //load XML configuration
            Graph = new GraphViewModel();  //initialize a new instance of Graph
            Reset();
        }

        //realtive paths to XMLs
        private string xml = "../../statemachinecfg.xml";
        private string xmlSeq = "../../statemachinecfgsequences.xml";

        //stores the XML configuration, the states and the triggers
        private FSMVConfig config = new FSMVConfig();

        //stores the sequences
        private FSMSequenceConfig seq = new FSMSequenceConfig();

        //the graph
        public GraphViewModel Graph
        {
            get;
            set;
            
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            Graph = new GraphViewModel();
            Graph.Sequences = seq.ArrayOfSequence.ToList();
            GetTheNodes();
        }

        /// <summary>
        /// Represents the one sequence by creating a new graph that contains only this sequence.
        /// </summary>
        /// <param name="sequence">The sequence to be represented.</param>
        /// <returns></returns>
        public OperationResult RepresentOneSequence(FSMSequence sequence,Color color)
        {
            this.Graph.ChangeEdgeColor(Colors.Black);
            this.Graph.ChangeVertexColor(Colors.Beige);
            return RepresentSequence(color,sequence,true);
        }

        /// <summary>
        /// Gets the nodes and represents them.
        /// </summary>
        public void GetTheNodes()
        {
            foreach (FSMVState item in config.ArrayOfFSMVState)
            {
                CustomVertex vertex = new CustomVertex(item.Name,Colors.Wheat);
                GraphViewModel.Message=Graph.AddNewVertex(vertex);
            }
        }

        /// <summary>
        /// Shows the machine configuration.
        /// </summary>
        public void ViewMachineConfiguration()
        {
            GraphViewModel.Message += "Getting machine configuration...\r\n";
            FSMVTrigger t = new FSMVTrigger();
            Graph.LayoutAlgorithmType = "Circular";
            try
            {
                foreach (FSMVState state in config.ArrayOfFSMVState)
                {
                    foreach (AllowedTrigger trigger in state.ArrayOfAllowedTrigger)
                    {
                        t = FoundTriggerInList(trigger);
                        if (String.IsNullOrEmpty(trigger.TriggerName))
                        {
                            GraphViewModel.Message += "Trigger name: " + trigger.StateAndTriggerName + "\r\n";
                            GraphViewModel.Message += "Added edge: " + Graph.AddNewEdge(trigger.StateAndTriggerName, Graph.GetVertexByName(state.Name), Graph.GetVertexByName(trigger.StateAndTriggerName),Colors.Black) + "\r\n";
                        }
                        else
                        {
                            GraphViewModel.Message += "Trigger name: " + trigger.TriggerName + "\r\n";
                            GraphViewModel.Message += "Added edge: " + Graph.AddNewEdge(trigger.TriggerName, Graph.GetVertexByName(state.Name), Graph.GetVertexByName(trigger.StateName),Colors.Black) + "\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GraphViewModel.Message += "An error ocurred! Done representing\r\n"+ex.Message;
            }
        }
   
        /// <summary>
        /// Represents this machine by representing all sequences.
        /// </summary>
        /// <returns></returns>
        public void RepresentThisMachine(Color color)
        {
            if (seq.ArrayOfSequence.Count > 0)
            {
                foreach (FSMSequence s in seq.ArrayOfSequence)
                {
                    this.RepresentSequence(color,s,true);
                }
            }
        }

        /// <summary>
        /// Founds the trigger in list of triggers.
        /// </summary>
        /// <param name="stepp">The stepp that contains trigger SequenceID or CommonID.</param>
        /// <returns></returns>
        public FSMVTrigger FoundTriggerInList(FSMStep stepp)
        {
             return config.ArrayOfFSMVTrigger.Where(trig => (string.Compare(trig.SequenceID.Trim(),stepp.Name.Trim())==0)  || string.Compare(trig.CommonID.Trim(),stepp.Name.Trim())==0).FirstOrDefault();
        }

        /// <summary>
        /// Founds the next state of this machine.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <returns></returns>
        public FSMVState FoundNextState(string stateName)
        {
            return config.ArrayOfFSMVState.Where(state => state.Name.Trim() == stateName.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Founds the AllowdTrigger from a State configuration in list of triggers.
        /// </summary>
        /// <param name="trig">The trig.</param>
        /// <returns></returns>
        public FSMVTrigger FoundTriggerInList(AllowedTrigger trig)
        {
           return  config.ArrayOfFSMVTrigger.Where(t => t.Name == trig.TriggerName || t.Name == trig.StateAndTriggerName || t.CommonID==trig.StateAndTriggerName).FirstOrDefault();
        }

        /// <summary>
        /// Represents the sequence by changing vertices and edges color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="sequence">The sequence.</param>
        /// /// <param name="oneSequence">if is true the Property Represented will be setted true.</param>
        public OperationResult RepresentSequence(Color color,FSMSequence sequence,bool oneSequence)
        {
            //the result of operation
            OperationResult result = new OperationResult(true,"Succesfully represented!");

             //current step
            FSMStep stepp = new FSMStep();

            AllowedTrigger aTrigger = new AllowedTrigger();

            //the states to be represented
            FSMVState initialState = new FSMVState();
            FSMVState otherState = new FSMVState();

            //verifies if there are steps to be represented
            if (sequence.ArrayOfStep.Count != 0)
            {
                //get first step
                stepp = sequence.ArrayOfStep.First();
                GraphViewModel.Message += "First step: " + stepp.Name + "\r\n";

                FSMVTrigger tr = new FSMVTrigger();

                try
                {
                    //found the trigger in the list of triggers
                    tr = FoundTriggerInList(stepp);

                    GraphViewModel.Message += "Found trigger: " + tr.Name + "\r\n";

                    //initial state
                    initialState = config.ArrayOfFSMVState.FirstOrDefault();
                    if(oneSequence)
                    {
                        this.Graph.GetVertexByName(initialState.Name).Represented = true;
                    }
                    GraphViewModel.Message += "Initial state is always: " + initialState.Name + "\r\n";

                    try
                    {
                        aTrigger = new AllowedTrigger();
                        aTrigger = initialState.FoundTriggerInCureentState(tr); //founds the trigger in current state
                        string newState = "";
                        if (String.IsNullOrEmpty(aTrigger.StateName))
                        {
                            newState = aTrigger.StateAndTriggerName;
                            CustomEdge edge=new CustomEdge(aTrigger.StateAndTriggerName, Graph.GetVertexByName(initialState.Name), Graph.GetVertexByName(newState), color);
                            Graph.ChangeOneVertexColor(Graph.GetVertexByName(initialState.Name), color);
                            Graph.ChangeOneEdgeColor(Graph.Graph.Edges.Where(e => e.CompareTo(edge)).FirstOrDefault(), color);
                        }
                        else
                        {
                            newState = aTrigger.StateName;
                            CustomEdge edge = new CustomEdge(aTrigger.TriggerName, Graph.GetVertexByName(initialState.Name), Graph.GetVertexByName(newState), color);
                            Graph.ChangeOneVertexColor(Graph.GetVertexByName(initialState.Name), color);                      
                            Graph.ChangeOneEdgeColor(Graph.Graph.Edges.Where(e => e.CompareTo(edge)).FirstOrDefault(), color);
                        }
                        Graph.ChangeOneVertexColor(Graph.GetVertexByName(newState), color);  
                        if (oneSequence)
                        {
                           this.Graph.GetVertexByName(initialState.Name).Represented = true;
                           this.Graph.GetVertexByName(newState).Represented = true;
                        }

                        //switch to new state
                        otherState = FoundNextState(newState);
                        GraphViewModel.Message += "Next state is:" + otherState.Name + " Allowed triggers: " + otherState.ArrayOfAllowedTrigger.Count.ToString() + "\r\n";

                        for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
                        {
                            stepp = sequence.ArrayOfStep.ElementAt(i);
                            GraphViewModel.Message += "\nNext step: " + stepp.Name + "\r\n";
                            try
                            {
                                tr = FoundTriggerInList(stepp);
                                if(String.IsNullOrEmpty(tr.Name) && (String.IsNullOrEmpty(tr.SequenceID) && String.IsNullOrEmpty(tr.CommonID)))
                                {
                                    GraphViewModel.Message += "Incorrect trigger!Done representing \r\n ";
                                }
                                GraphViewModel.Message += "Found trigger: " + tr.Name + "\r\n";
                                newState = "";
                                try
                                {
                                    aTrigger = otherState.FoundTriggerInCureentState(tr);
                                    if (String.IsNullOrEmpty(aTrigger.StateName))
                                    {
                                        newState = aTrigger.StateAndTriggerName;
                                        CustomEdge edge = new CustomEdge(aTrigger.StateAndTriggerName, Graph.GetVertexByName(initialState.Name), Graph.GetVertexByName(newState), color);
                                        Graph.ChangeOneEdgeColor(Graph.Graph.Edges.Where(e => e.CompareTo(edge)).FirstOrDefault(), color);
                                    }
                                    else
                                    {
                                        newState = aTrigger.StateName;
                                        CustomEdge edge = new CustomEdge(aTrigger.TriggerName, Graph.GetVertexByName(initialState.Name), Graph.GetVertexByName(newState), color);
                                        Graph.ChangeOneEdgeColor(Graph.Graph.Edges.Where(e => e.CompareTo(edge)).FirstOrDefault(), color);        
                                    }
                                    Graph.ChangeOneVertexColor(Graph.GetVertexByName(newState), color);
                                    if (oneSequence)
                                    {
                                        this.Graph.GetVertexByName(initialState.Name).Represented = true;
                                        this.Graph.GetVertexByName(newState).Represented = true;
                                    }
                                    initialState = otherState;
                                    otherState = FoundNextState(newState);
                                    GraphViewModel.Message += "Takes FSM to state: " + otherState.Name + "\r\n";
                                }
                                catch (Exception ex)
                                {
                                    GraphViewModel.Message += ex.Message + "\r\nA problem ocurred! Invalid state name!"+newState+"\nDone representing!" + "\r\n";
                                    result.Succes = false;
                                    result.Message= "\r\nA problem ocurred! Invalid state!"+newState+"\nDone representing!"; 
                                }
                            }
                            catch (Exception ex)
                            {
                                GraphViewModel.Message += ex.Message + "\r\nIncorrect trigger!\r\nDone representing!";
                                result.Message= "\r\nIncorrect trigger!\r\nDone representing!";
                                result.Succes = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        GraphViewModel.Message += ex.Message + "\r\nTrigger does not exist in current state!\r\nDone representing!";
                        result.Message="\r\nTrigger does not exist in current state!\r\nDone representing!";
                        result.Succes = false;
                    }
                }
                catch (Exception ex)
                {
                    GraphViewModel.Message += ex.Message + "\r\n Nothing to draw!";
                    result.Message= "\r\n Nothing to draw!";
                    result.Succes = false;
                }
            }
            else
            {
                GraphViewModel.Message += "\r\n Sequence with no steps!";
                result.Message = "\r\n Workflow with no steps!";
                result.Succes = false;
            }
            CustomVertex v = new CustomVertex();
            //TREBUIE SA IAU ULTIMUL NOD GALBEN
            //v = this.Graph.existingVertices.Last();

            //CustomEdge ed = new CustomEdge(config.DefaultTriggerOnError, Graph.GetVertexByName(v.Text), Graph.GetVertexByName(config.DefaultTriggerOnError), color);
          //  Graph.ChangeOneEdgeColor(Graph.Graph.Edges.Where(e => e.CompareTo(ed)).FirstOrDefault(), color); 
            //CustomEdge ed = new CustomEdge(aTrigger.TriggerName, Graph.GetVertexByName(v.Text), Graph.GetVertexByName(config.DefaultTriggerOnError), color);
            return result;
        }
    }
}
