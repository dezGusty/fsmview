using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FSMControl.Windows
{
    /// <summary>
    /// Interaction logic for GenerateSequenceWindow.xaml
    /// </summary>
    public partial class GenerateSequenceWindow : Window
    {
        private StateMachine machine;
        private FSMControl.DomainModel.SecondVersion.FSMSequence sequenceSecondVersion;
        private FSMControl.DomainModel.FirstVersion.FSMSequence sequenceFirstVersion;
        private Stack<CustomVertex> listOfVertices;

        public GenerateSequenceWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateSequenceWindow"/> class.
        /// </summary>
        /// <param name="fsm">The Finite State Machine.</param>
        public GenerateSequenceWindow(StateMachine fsm)
            : this()
        {
            if (fsm != null)
            {
                fsm.MyGraph.ResetToDefault();
                this.DataContext = fsm;
                this.listOfVertices = new Stack<CustomVertex>();
            }

            if (fsm is FirstStateMachine)
            {
                this.machine = new FirstStateMachine(fsm.CurrentVersion);
                this.machine = fsm;
                firstStep.Text = "First state is " + ((FirstStateMachine)this.machine).Configuration.ArrayOfFSMState.FirstOrDefault().Name + "! Select next sequence!";
                this.sequenceFirstVersion = new FSMControl.DomainModel.FirstVersion.FSMSequence();
                this.sequenceFirstVersion.ArrayOfStep = new System.Collections.ObjectModel.Collection<DomainModel.FirstVersion.FSMStep>();
            }
            else
            {
                this.machine = new SecondStateMachine(fsm.CurrentVersion);
                this.machine = fsm;
                firstStep.Text = "First state is " + ((SecondStateMachine)this.machine).Configuration.ArrayOfFSMVState.FirstOrDefault().Name + "! Select next sequence!";
                this.sequenceSecondVersion = new FSMControl.DomainModel.SecondVersion.FSMSequence();
                this.sequenceSecondVersion.ArrayOfStep = new System.Collections.ObjectModel.Collection<DomainModel.SecondVersion.FSMStep>();
            }

            this.listOfVertices.Push(this.machine.MyGraph.Vertices.FirstOrDefault());
            this.DataContext = this.machine.MyGraph;
        }

        /// <summary>
        /// Handles the Click event of the done control.
        /// Saves generated sequence to current finite state machine
        /// </summary>
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (this.machine is FirstStateMachine)
            {
                if (string.IsNullOrEmpty(seqName.Text) || string.IsNullOrEmpty(seqDesc.Text))
                {
                    MessageBox.Show("You must enter a name and a description for sequence!");
                }
                else
                {
                    this.sequenceFirstVersion.Name = seqName.Text;
                    this.sequenceFirstVersion.Description = seqDesc.Text;
                    this.sequenceFirstVersion.FinalDescription = seqFinalDesc.Text;
                    ((FirstStateMachine)this.machine).Sequences.ArrayOfSequence.Add(this.sequenceFirstVersion);
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
                    this.sequenceSecondVersion.Name = seqName.Text;
                    this.sequenceSecondVersion.Description = seqDesc.Text;
                    this.sequenceSecondVersion.FinalDescription = seqFinalDesc.Text;
                    ((SecondStateMachine)this.machine).Sequences.ArrayOfSequence.Add(this.sequenceSecondVersion);
                    MessageBox.Show("Sequence added!");
                    this.sequenceSecondVersion = new DomainModel.SecondVersion.FSMSequence();
                    this.DataContext = this.machine.MyGraph;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the undo control.
        /// Removes last selected vertex and edge
        /// </summary>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (this.machine is FirstStateMachine)
            {
                if (this.sequenceFirstVersion.ArrayOfStep.Count > 0)
                {
                    this.sequenceFirstVersion.ArrayOfStep.RemoveAt(this.sequenceFirstVersion.ArrayOfStep.Count - 1);
                }
            }
            else
            {
                if (this.sequenceSecondVersion.ArrayOfStep.Count > 0)
                {
                    this.sequenceSecondVersion.ArrayOfStep.RemoveAt(this.sequenceSecondVersion.ArrayOfStep.Count - 1);
                }
            }

            if (this.listOfVertices.Count > 1)
            {
                this.machine.MyGraph.ChangeOneVertexColor(this.listOfVertices.Peek().Text, Colors.Wheat);
                CustomEdge edge = new CustomEdge(this.listOfVertices.ElementAt(1), this.listOfVertices.Peek());
                edge = this.machine.MyGraph.GetEdgeBetween(this.listOfVertices.ElementAt(1), this.listOfVertices.Pop());
                if (this.listOfVertices.Count == 1)
                {
                    this.machine.MyGraph.ChangeOneVertexColor(this.listOfVertices.Peek().Text, Colors.Wheat);
                }

                this.machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Wheat);
            }
        }

        /// <summary>
        /// Handles the Click event of the reset control.
        /// </summary>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (this.machine is FirstStateMachine)
            {
                this.sequenceFirstVersion.ArrayOfStep.Clear();
            }
            else
            {
                this.sequenceSecondVersion.ArrayOfStep.Clear();
            }

            this.listOfVertices.Clear();
            this.listOfVertices.Push(this.machine.MyGraph.Vertices.FirstOrDefault());
            this.machine.MyGraph.ResetToDefault();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the veritces control.
        /// Adds the selected vertex to current sequence
        /// </summary>
        private void Veritces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listOfVertices.Push((CustomVertex)veritces.SelectedItem);
            if (this.listOfVertices.Count > 1)
            {
                CustomEdge edge = new CustomEdge(this.listOfVertices.ElementAt(1), this.listOfVertices.Peek());
                edge = this.machine.MyGraph.GetEdgeBetween(this.listOfVertices.ElementAt(1), this.listOfVertices.Peek());
                if (edge != null)
                {
                    this.machine.MyGraph.ChangeOneVertexColor(this.listOfVertices.ElementAt(this.listOfVertices.Count - 1).Text, Colors.Red);
                    this.machine.MyGraph.ChangeOneVertexColor(this.listOfVertices.Peek().Text, Colors.Red);
                    this.machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Red);
                    if (this.machine is FirstStateMachine)
                    {
                        ((FirstStateMachine)this.machine).AddNewStep(this.sequenceFirstVersion, edge.Trigger);
                    }
                    else
                    {
                        ((SecondStateMachine)this.machine).AddNewStep(this.sequenceSecondVersion, edge.Trigger);
                    }
                }
                else
                {
                    MessageBox.Show("There is no edge between these vertices! \r\n Please choose another one!");
                    this.listOfVertices.Pop();
                }

                this.DataContext = this.machine.MyGraph;
            }
        }
    }
}