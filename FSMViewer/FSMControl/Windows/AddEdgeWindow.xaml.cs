using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FSMControl.Windows
{
    /// <summary>
    /// Interaction logic for AddEdgeWindow.xaml
    /// </summary>
    public partial class AddEdgeWindow : Window
    {
        private StateMachine machine;

        public AddEdgeWindow()
        {
            InitializeComponent();
        }

        public AddEdgeWindow(StateMachine fsm)
            : this()
        {
            if (fsm is FirstStateMachine)
            {
                this.machine = new FirstStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
            }
            else
            {
                if (fsm is SecondStateMachine)
                {
                    this.machine = new SecondStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
                }
            }
            this.machine = fsm;
        }

        private void btnAddEdge(object sender, RoutedEventArgs e)
        {
            string vertexFrom = txtFromVertex.Text.Trim();
            string vertexTo = txtToVertex.Text.Trim();
            string trigger = txtTrigger.Text.Trim();

            if (String.IsNullOrEmpty(vertexFrom) || String.IsNullOrEmpty(trigger))
            {
                MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
                return;
            }
            else
            {
                if (machine != null)
                {
                    if ((machine.MyGraph.Vertices.Where(v => String.Compare(v.Text.ToLower(), vertexFrom.ToLower()) == 0).FirstOrDefault() == null))
                    {

                        MessageBox.Show(string.Format("{0} doesn't exist!", vertexFrom));
                        return;
                    }
                    else
                    {
                        if (machine is FirstStateMachine)
                        {
                            CustomVertex vtxFrom = machine.MyGraph.GetVertexByName(vertexFrom);
                            CustomVertex vtxTo = null; //s-ar putea sa nu mearga, ma mai uit eu maine (nu ma refer ca nu merge = null;
                            //, ci la restul, pentru ca am modificat ceva
                            FSMState state = new FSMState();
                            FSMControl.DomainModel.FirstVersion.AllowedTrigger aw = new FSMControl.DomainModel.FirstVersion.AllowedTrigger();
                            FSMTrigger trig = new FSMTrigger();
                            state.Name = vtxFrom.Text;
                            if (checkSATN.IsChecked == true && checkSN.IsChecked == false)
                            {
                                vtxTo = machine.MyGraph.GetVertexByName(trigger);
                                CustomEdge edge = new CustomEdge(trigger, vtxFrom, vtxTo);
                                machine.MyGraph.AddEdge(edge);
                                aw.StateAndTriggerName = trigger; //StateAndTriggerName adica
                                trig.CommonID = trigger;
                            }
                            else
                            {
                                vtxTo = machine.MyGraph.GetVertexByName(vertexTo);
                                CustomEdge edge = new CustomEdge(trigger, vtxFrom, vtxTo);
                                machine.MyGraph.AddEdge(edge);
                                aw.StateName = vtxTo.Text;
                                aw.TriggerName = trigger;
                                trig.Name = vtxTo.Text;
                                trig.SequenceID = trigger;
                            }
                            foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMState)
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

                            bool triggerExists = false;

                            foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMTrigger)
                            {
                                if (item.Name == aw.StateName || item.Name == aw.StateAndTriggerName ||
                                  item.CommonID == aw.StateAndTriggerName || item.CommonID == aw.StateName)
                                {
                                    triggerExists = true;
                                    break;
                                }
                            }

                            if (triggerExists == false)
                            {
                                ((FirstStateMachine)machine).config.ArrayOfFSMTrigger.Add(trig);
                            }
                            MessageBox.Show(string.Format("Edge from <<{0} to {1}>> added successfully!", vtxFrom, vtxTo));
                            this.DataContext = machine.MyGraph;
                        }
                        else
                        {
                            CustomVertex vtxFrom = machine.MyGraph.GetVertexByName(vertexFrom);
                            CustomVertex vtxTo = null; //s-ar putea sa nu mearga, ma mai uit eu maine (nu ma refer ca nu merge = null;
                            //, ci la restul, pentru ca am modificat ceva
                            FSMVState state = new FSMVState();
                            FSMControl.DomainModel.SecondVersion.AllowedTrigger aw = new FSMControl.DomainModel.SecondVersion.AllowedTrigger();
                            FSMVTrigger trig = new FSMVTrigger();
                            state.Name = vtxFrom.Text;
                            if (checkSATN.IsChecked == true && checkSN.IsChecked == false)
                            {
                                vtxTo = machine.MyGraph.GetVertexByName(trigger);
                                CustomEdge edge = new CustomEdge(trigger, vtxFrom, vtxTo);
                                machine.MyGraph.AddEdge(edge);
                                aw.StateAndTriggerName = trigger; //StateAndTriggerName adica
                                trig.CommonID = trigger;
                            }
                            else
                            {
                                vtxTo = machine.MyGraph.GetVertexByName(vertexTo);
                                CustomEdge edge = new CustomEdge(trigger, vtxFrom, vtxTo);
                                machine.MyGraph.AddEdge(edge);
                                aw.StateName = vtxTo.Text;
                                aw.TriggerName = trigger;
                                trig.Name = vtxTo.Text;
                                trig.SequenceID = trigger;
                            }
                            foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVState)
                            {
                                if (item.Name == state.Name)
                                {
                                    if (item.ArrayOfAllowedTrigger == null)
                                    {
                                        item.ArrayOfAllowedTrigger = new Collection<FSMControl.DomainModel.SecondVersion.AllowedTrigger>();
                                    }
                                    item.ArrayOfAllowedTrigger.Add(aw);
                                    break;
                                }
                            }

                            bool triggerExists = false;

                            foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVTrigger)
                            {
                                if (item.Name == aw.StateName || item.Name == aw.StateAndTriggerName ||
                                  item.CommonID == aw.StateAndTriggerName || item.CommonID == aw.StateName)
                                {
                                    triggerExists = true;
                                    break;
                                }
                            }

                            if (triggerExists == false)
                            {
                                ((SecondStateMachine)machine).config.ArrayOfFSMVTrigger.Add(trig);
                            }
                            MessageBox.Show(string.Format("Edge from <<{0} to {1}>> added successfully!", vtxFrom, vtxTo));
                            this.DataContext = machine.MyGraph;
                        }
                    }
                }
            }
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
