using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FSMControl.DomainModel.SecondVersion
{
    public class SecondStateMachine:StateMachine
    {
        //stores the XML configuration, the states and the triggers
        public FSMVConfig config;

        //stores the sequences
        public FSMSequenceConfig seq;

        public SecondStateMachine(string xmlConfig, string xmlSequenceConfig, Version v)
        {
            this.MyGraph = new CustomGraph();
            this.Xml = xmlConfig;
            this.XmlSeq = xmlSequenceConfig;
            this.CurrentVersion = v;
        }

        public override void GetDates()
        {
            seq = new FSMSequenceConfig();
            config = new FSMVConfig();
            seq = seq.LoadFromXML(this.XmlSeq);  //load sequences from XML
            config = config.LoadFromXML(this.Xml); //load XML configuration
            SetGraphNodes();
        }

        /// <summary>
        /// Gets the nodes and represents them.
        /// </summary>
        public override void SetGraphNodes()
        {
            foreach (FSMVState item in config.ArrayOfFSMVState)
            {
                CustomVertex vertex = new CustomVertex(item.Name, Colors.Wheat);
                this.MyGraph.AddVertex(vertex);
            }
            FSMVTrigger t = new FSMVTrigger();
            try
            {
                foreach (FSMVState state in config.ArrayOfFSMVState)
                {
                    foreach (AllowedTrigger trigger in state.ArrayOfAllowedTrigger)
                    {
                        t = config.FoundTriggerInList(trigger);
                        if (String.IsNullOrEmpty(trigger.TriggerName))
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
        /// Represents the sequence by changing vertices and edges color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="sequence">The sequence.</param>
        /// /// <param name="oneSequence">if is true the Property Represented will be setted true.</param>
        public OperationResult RepresentSequence(Color color, FSMSequence sequence, bool oneSequence)
        {
            //current step
            FSMStep stepp = new FSMStep();

            AllowedTrigger aTrigger = new AllowedTrigger();

            //the states to be represented
            FSMVState initialState = new FSMVState();
            FSMVState otherState = new FSMVState();

            //verifies if there are steps to be represented
            if (sequence.ArrayOfStep.Count != 0)
            {
                try
                {
                    //get first step
                    stepp = sequence.ArrayOfStep.First();

                    FSMVTrigger tr = new FSMVTrigger();
                    //found the trigger in the list of triggers
                    tr = config.FoundTriggerInList(stepp);

                    //initial state
                    initialState = config.ArrayOfFSMVState.FirstOrDefault();
                    if (oneSequence)
                    {
                        this.MyGraph.GetVertexByName(initialState.Name).Represented = true;
                    }
                    aTrigger = new AllowedTrigger();
                    aTrigger = initialState.FoundTriggerInCureentState(tr); //founds the trigger in current state
                    string newState = "";
                    if (String.IsNullOrEmpty(aTrigger.StateName)) //verifies if the trigger has a state name, if it doesn't, then it has a StateandTriggerName
                    {
                        newState = aTrigger.StateAndTriggerName;
                        CustomEdge edge = new CustomEdge(aTrigger.StateAndTriggerName, this.MyGraph.GetVertexByName(initialState.Name), this.MyGraph.GetVertexByName(newState), color);
                        this.MyGraph.ChangeOneVertexColor(initialState.Name, color);
                        this.MyGraph.ChangeOneEdgeColor(edge, color);
                    }
                    else
                    {
                        newState = aTrigger.StateName;
                        CustomEdge edge = new CustomEdge(aTrigger.TriggerName, this.MyGraph.GetVertexByName(initialState.Name), this.MyGraph.GetVertexByName(newState), color);
                        this.MyGraph.ChangeOneVertexColor(initialState.Name, color);
                        this.MyGraph.ChangeOneEdgeColor(edge, color);
                    }
                    this.MyGraph.ChangeOneVertexColor(newState, color);
                    if (oneSequence)
                    {
                        this.MyGraph.GetVertexByName(initialState.Name).Represented = true;
                        this.MyGraph.GetVertexByName(newState).Represented = true;
                    }

                    //switch to new state
                    otherState = config.FoundNextState(newState);

                    for (int i = 1; i < sequence.ArrayOfStep.Count; i++)
                    {
                        stepp = sequence.ArrayOfStep.ElementAt(i);
                        tr = config.FoundTriggerInList(stepp);
                        newState = "";

                        aTrigger = otherState.FoundTriggerInCureentState(tr);
                        if(aTrigger!=null)
                        {
                            if (String.IsNullOrEmpty(aTrigger.StateName))
                            {
                                newState = aTrigger.StateAndTriggerName;
                                CustomEdge edge = new CustomEdge(aTrigger.StateAndTriggerName, this.MyGraph.GetVertexByName(initialState.Name), base.MyGraph.GetVertexByName(newState), color);
                                base.MyGraph.ChangeOneEdgeColor(edge, color);
                            }
                            else
                            {
                                newState = aTrigger.StateName;
                                CustomEdge edge = new CustomEdge(aTrigger.TriggerName, this.MyGraph.GetVertexByName(initialState.Name), base.MyGraph.GetVertexByName(newState), color);
                                this.MyGraph.ChangeOneEdgeColor(edge, color);
                            }
                            this.MyGraph.ChangeOneVertexColor(newState, color);
                            if (oneSequence)
                            {
                                this.MyGraph.GetVertexByName(initialState.Name).Represented = true;
                                this.MyGraph.GetVertexByName(newState).Represented = true;
                            }
                            initialState = otherState;
                            otherState = config.FoundNextState(newState);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new OperationResult(false, "Incorrect sequence!\r\n" + ex.Message);
                }
               
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
            if (seq.ArrayOfSequence.Count > 0)
            {
                foreach (FSMSequence s in seq.ArrayOfSequence)
                {
                    if (this.RepresentSequence(color, s, true).Succes)
                    {
                        this.RepresentSequence(color, s, true);
                    }
                }
            }
        }
    }
}
