using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for GenerateSequenceWindow.xaml
    /// </summary>
    public partial class GenerateSequenceWindow : Window
    {
        private StateMachine machine;
        FSMControl.DomainModel.SecondVersion.FSMSequence sequenceSecondVersion;
        FSMControl.DomainModel.FirstVersion.FSMSequence sequenceFirstVersion;
        Stack<CustomVertex> listOfVertices;

        public GenerateSequenceWindow()
        {
            InitializeComponent();
        }

        public GenerateSequenceWindow(StateMachine fsm)
            : this()
        {
            if (fsm != null)
            {
                fsm.MyGraph.ResetToDefault();
                this.DataContext = fsm;
                listOfVertices = new Stack<CustomVertex>();
            }
            if (fsm is FirstStateMachine)
            {
                this.machine = new FirstStateMachine(fsm.CurrentVersion);
                this.machine = fsm;
                firstStep.Text = "First state is " + ((FirstStateMachine)machine).Configuration.ArrayOfFSMState.FirstOrDefault().Name + "! Select next sequence!";
                sequenceFirstVersion = new FSMControl.DomainModel.FirstVersion.FSMSequence();
                sequenceFirstVersion.ArrayOfStep = new System.Collections.ObjectModel.Collection<DomainModel.FirstVersion.FSMStep>();
            }
            else
            {
                this.machine = new SecondStateMachine(fsm.CurrentVersion);
                this.machine = fsm;
                firstStep.Text = "First state is " + ((SecondStateMachine)machine).Configuration.ArrayOfFSMVState.FirstOrDefault().Name + "! Select next sequence!";
                sequenceSecondVersion = new FSMControl.DomainModel.SecondVersion.FSMSequence();
                sequenceSecondVersion.ArrayOfStep = new System.Collections.ObjectModel.Collection<DomainModel.SecondVersion.FSMStep>();
            }
            listOfVertices.Push(machine.MyGraph.Vertices.FirstOrDefault());
            this.DataContext = machine.MyGraph;
        }

        /// <summary>
        /// Handles the Click event of the done control.
        /// </summary>
        private void done_Click(object sender, RoutedEventArgs e)
        {
            if (machine is FirstStateMachine)
            {
                if (string.IsNullOrEmpty(seqName.Text) || string.IsNullOrEmpty(seqDesc.Text))
                {
                    MessageBox.Show("You must enter a name and a description for sequence!");
                }
                else
                {
                    sequenceFirstVersion.Name = seqName.Text;
                    sequenceFirstVersion.Description = seqDesc.Text;
                    sequenceFirstVersion.FinalDescription = seqFinalDesc.Text;
                    ((FirstStateMachine)machine).Sequences.ArrayOfSequence.Add(this.sequenceFirstVersion);
                    MessageBox.Show("Sequence added!");
                    this.sequenceFirstVersion = new DomainModel.FirstVersion.FSMSequence();
                    this.Close();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(seqName.Text) || string.IsNullOrEmpty(seqDesc.Text))
                {
                    MessageBox.Show("You must enter a name and a description for sequence!");
                }
                else
                {
                    sequenceSecondVersion.Name = seqName.Text;
                    sequenceSecondVersion.Description = seqDesc.Text;
                    sequenceSecondVersion.FinalDescription = seqFinalDesc.Text;
                    ((SecondStateMachine)machine).Sequences.ArrayOfSequence.Add(this.sequenceSecondVersion);
                    MessageBox.Show("Sequence added!");
                    this.sequenceSecondVersion = new DomainModel.SecondVersion.FSMSequence();
                    this.DataContext = machine.MyGraph;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the undo control.
        /// </summary>
        private void undo_Click(object sender, RoutedEventArgs e)
        {
            if (machine is FirstStateMachine)
            {
                if (this.sequenceFirstVersion.ArrayOfStep.Count > 0)
                {
                    this.sequenceFirstVersion.ArrayOfStep.RemoveAt(sequenceFirstVersion.ArrayOfStep.Count - 1);
                }
            }
            else
            {
                if (this.sequenceSecondVersion.ArrayOfStep.Count > 0)
                {
                    this.sequenceSecondVersion.ArrayOfStep.RemoveAt(sequenceSecondVersion.ArrayOfStep.Count - 1);
                }
            }
            if (listOfVertices.Count > 1)
            {
                machine.MyGraph.ChangeOneVertexColor(listOfVertices.Peek().Text, Colors.Wheat);
                CustomEdge edge = new CustomEdge(listOfVertices.ElementAt(1), listOfVertices.Peek());
                edge = machine.MyGraph.GetEdgeBetween(listOfVertices.ElementAt(1), listOfVertices.Pop());
                if (listOfVertices.Count == 1)
                {
                    machine.MyGraph.ChangeOneVertexColor(listOfVertices.Peek().Text, Colors.Wheat);
                }
                machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Wheat);
            }
        }

        /// <summary>
        /// Handles the Click event of the reset control.
        /// </summary>
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            if (machine is FirstStateMachine)
            {
                this.sequenceFirstVersion.ArrayOfStep.Clear();
            }
            else
            {
                this.sequenceSecondVersion.ArrayOfStep.Clear();
            }
            listOfVertices.Clear();
            listOfVertices.Push(machine.MyGraph.Vertices.FirstOrDefault());
            machine.MyGraph.ResetToDefault();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the veritces control.
        /// </summary>
        private void veritces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listOfVertices.Push((CustomVertex)veritces.SelectedItem);
            if (listOfVertices.Count > 1)
            {
                CustomEdge edge = new CustomEdge(listOfVertices.ElementAt(1), listOfVertices.Peek());
                edge = machine.MyGraph.GetEdgeBetween(listOfVertices.ElementAt(1), listOfVertices.Peek());
                if (edge != null)
                {
                    machine.MyGraph.ChangeOneVertexColor(listOfVertices.ElementAt(listOfVertices.Count - 1).Text, Colors.Red);
                    machine.MyGraph.ChangeOneVertexColor(listOfVertices.Peek().Text, Colors.Red);
                    machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Red);
                    if (machine is FirstStateMachine)
                    {
                        ((FirstStateMachine)machine).AddNewStep(this.sequenceFirstVersion, edge.Trigger);
                    }
                    else
                    {
                        ((SecondStateMachine)machine).AddNewStep(this.sequenceSecondVersion, edge.Trigger);
                    }
                }
                else
                {
                    MessageBox.Show("There is no edge between these vertices! \r\n Please choose another one!");
                    listOfVertices.Pop();
                }
                this.DataContext = machine.MyGraph;
            }
        }
    }
}
