using FiniteStateMachine.DomainModel;
using FiniteStateMachine.Machine;
using FiniteStateMachineTest.Graph;
using FiniteStateMachineTest.Machine;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineTest.DomainModel
{
    public class StateMachine
    {
        public StateMachine()
        {
            seq = seq.LoadFromXML(xmlSeq);
            config = config.LoadFromXML(xml);
            Graph = new MyGraph();
            this.GetSequences();
        }

        //realtive path to xml
        private string xml = "../../statemachinecfg.xml";
        private string xmlSeq = "../../statemachinecfgsequences.xml";

        //stores the XML configuration, the states and the triggers
        private FSMVConfig config = new FSMVConfig();

        //stores the sequences
        private FSMSequenceConfig seq = new FSMSequenceConfig();

        //the graph
        public MyGraph Graph
        {
            get;
            set;
        }

        public void GetSequences()
        {
            Graph.Sequences = this.seq.ArrayOfSequence.ToList();
        }

        public String RepresentOneSequence(FSMSequence sequence)
        {
            Graph.Graph.Clear();
            //for console displaying
            StringBuilder str = new StringBuilder();

            //current step
            FSMStep stepp = new FSMStep();

            //first step
            stepp = sequence.ArrayOfStep.First();
            str.Append("First step: " + stepp.Name + "\n");

            //founds the trigger in the config file
            FSMVTrigger tr = new FSMVTrigger();

            //the states to be represented
            FSMVState initialState = new FSMVState();
            FSMVState otherState = new FSMVState();

            //list of vertices
            List<SampleVertex> listOfVertices = new List<SampleVertex>();

            try
            {
                //found the trigger in the list of triggers
                tr = config.ArrayOfFSMVTrigger.Where(trig => (trig.SequenceID == stepp.Name || trig.CommonID == stepp.Name)).FirstOrDefault();
                str.Append("Found trigger: " + tr.Name + "\n");

                //initial state
                initialState = config.ArrayOfFSMVState.FirstOrDefault();
                str.Append("Initial state is always: " + initialState.Name + "\n");

                try
                {
                    //founds the trigger in current state
                    AllowedTrigger aTrigger = new AllowedTrigger();
                    aTrigger = initialState.FoundTriggerInCureentState(tr);
                    string newState = "";
                    if (String.IsNullOrEmpty(aTrigger.StateName))
                        newState = aTrigger.StateAndTriggerName;
                    else
                        newState = aTrigger.StateName;
                    listOfVertices.Add(new SampleVertex(newState));//adds  the node to the list of vertices
                    Graph.Graph.AddVertex(listOfVertices.ElementAt(0)); //adds a new node to graph

                    //switch to new state
                    otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
                    str.Append("Next state is:" + otherState.Name + " Allowed triggers: " + otherState.ArrayOfAllowedTrigger.Count.ToString() + "\n");

                    for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
                    {
                        stepp = sequence.ArrayOfStep.ElementAt(i);
                        str.Append("\nNext step: " + stepp.Name + "\n");
                        try
                        {
                            tr = config.ArrayOfFSMVTrigger.Where(trig => (trig.SequenceID == stepp.Name || trig.CommonID == stepp.Name)).FirstOrDefault();
                            str.Append("Found trigger: " + tr.Name + "\n");
                            newState = "";
                            try
                            {
                                aTrigger = otherState.FoundTriggerInCureentState(tr);
                                if (String.IsNullOrEmpty(aTrigger.StateName))
                                    newState = aTrigger.StateAndTriggerName;
                                else
                                    newState = aTrigger.StateName;

                                listOfVertices.Add(new SampleVertex(newState));
                                Graph.Graph.AddVertex(listOfVertices.Last());
                                Graph.Graph.AddEdge(new Edge<object>(listOfVertices.ElementAt(listOfVertices.Count - 2), listOfVertices.Last()));
                                initialState = otherState;
                                otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
                                str.Append("Takes FSM to state: " + otherState.Name + "\n");
                            }
                            catch (Exception ex)
                            {
                                str.Append(ex.Message + "\nA problem ocurred! The trigger does not exist in current state!\nDone representing!" + "\n");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            str.Append(ex.Message + "\nIncorrect trigger!\nDone representing!");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    str.Append(ex.Message + "\nThe trigger does not exist in current state!\nDone representing!");
                }
            }
            catch (Exception ex)
            {
                str.Append(ex.Message + "\n Nothing to draw!");
            }
            return str.ToString();
        }

        public String RepresentSequence(FSMSequence sequence)
        {
            //for console displaying
            StringBuilder str = new StringBuilder();

            //current step
            FSMStep stepp = new FSMStep();

            //first step
            stepp = sequence.ArrayOfStep.First();
            str.Append("First step: " + stepp.Name + "\n");

            //founds the trigger in the config file
            FSMVTrigger tr = new FSMVTrigger();

            //the states to be represented
            FSMVState initialState = new FSMVState();
            FSMVState otherState = new FSMVState();

            //list of vertices
            List<SampleVertex> listOfVertices = new List<SampleVertex>();

            try
            {
                //found the trigger in the list of triggers
                tr = config.ArrayOfFSMVTrigger.Where(trig => (trig.SequenceID == stepp.Name || trig.CommonID == stepp.Name)).FirstOrDefault();
                str.Append("Found trigger: " + tr.Name + "\n");

                //initial state
                initialState = config.ArrayOfFSMVState.FirstOrDefault();
                str.Append("Initial state is always: " + initialState.Name + "\n");

                try
                {
                    //founds the trigger in current state
                    AllowedTrigger aTrigger = new AllowedTrigger();
                    aTrigger = initialState.FoundTriggerInCureentState(tr);
                    string newState = "";
                    if (String.IsNullOrEmpty(aTrigger.StateName))
                        newState = aTrigger.StateAndTriggerName;
                    else
                        newState = aTrigger.StateName;
                    listOfVertices.Add(new SampleVertex(newState));//adds  the node to the list of vertices
                    Graph.Graph.AddVertex(listOfVertices.ElementAt(0)); //adds a new node to graph

                    //switch to new state
                    otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
                    str.Append("Next state is:" + otherState.Name + " Allowed triggers: " + otherState.ArrayOfAllowedTrigger.Count.ToString() + "\n");

                    for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
                    {
                        stepp = sequence.ArrayOfStep.ElementAt(i);
                        str.Append("\nNext step: " + stepp.Name + "\n");
                        try
                        {
                            tr = config.ArrayOfFSMVTrigger.Where(trig => (trig.SequenceID == stepp.Name || trig.CommonID == stepp.Name)).FirstOrDefault();
                            str.Append("Found trigger: " + tr.Name + "\n");
                            newState = "";
                            try
                            {
                                aTrigger = otherState.FoundTriggerInCureentState(tr);
                                if (String.IsNullOrEmpty(aTrigger.StateName))
                                    newState = aTrigger.StateAndTriggerName;
                                else
                                    newState = aTrigger.StateName;

                                listOfVertices.Add(new SampleVertex(newState));
                                Graph.Graph.AddVertex(listOfVertices.Last());
                                Graph.Graph.AddEdge(new Edge<object>(listOfVertices.ElementAt(listOfVertices.Count - 2), listOfVertices.Last()));
                                initialState = otherState;
                                otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
                                str.Append("Takes FSM to state: " + otherState.Name + "\n");
                            }
                            catch (Exception ex)
                            {
                                str.Append(ex.Message + "\nA problem ocurred! The trigger does not exist in current state!\nDone representing!" + "\n");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            str.Append(ex.Message + "\nIncorrect trigger!\nDone representing!");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    str.Append(ex.Message + "\nThe trigger does not exist in current state!\nDone representing!");
                }
            }
            catch (Exception ex)
            {
                str.Append(ex.Message + "\n Nothing to draw!");
            }
            return str.ToString();
        }

        public String RepresentThisMachine()
        {
            //string containing the result
            StringBuilder str = new StringBuilder();

            //first sequence to be represented
            FSMSequence sequence = new FSMSequence();
            if (seq.ArrayOfSequence.Count > 0)
            {
                foreach (FSMSequence s in seq.ArrayOfSequence)
                {
                    str.Append(this.RepresentSequence(s));
                }
            }
            
            return str.ToString();
        }

    }
}
