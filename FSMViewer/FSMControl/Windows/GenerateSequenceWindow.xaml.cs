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
    FSMControl.DomainModel.SecondVersion.FSMSequence sequence;
    CustomVertex firstVertex;
    CustomVertex secondVertex;

    public GenerateSequenceWindow()
    {
      InitializeComponent();
    }

    public GenerateSequenceWindow(StateMachine fsm)
      : this()
    {
      if (fsm is FirstStateMachine)
      {
        this.machine = new FirstStateMachine(fsm.CurrentVersion);
        this.machine = fsm;
        firstStep.Text = "First state is " + ((FirstStateMachine)machine).Configuration.ArrayOfFSMState.FirstOrDefault().Name + "\r\nSelect next sequence.";
      }
      else
      {
        if (fsm is SecondStateMachine)
        {
          this.machine = new SecondStateMachine(fsm.CurrentVersion);
          this.machine = fsm;
          firstStep.Text = "First state is " + ((SecondStateMachine)machine).Configuration.ArrayOfFSMVState.FirstOrDefault().Name + "\r\nSelect next sequence.";
        }
      }
      sequence = new FSMControl.DomainModel.SecondVersion.FSMSequence();
      sequence.Name = "MySequence";
      sequence.Description = "Description";
      sequence.FinalDescription = "FinalDescription";
      sequence.ArrayOfStep = new System.Collections.ObjectModel.Collection<DomainModel.SecondVersion.FSMStep>();
      firstVertex = new CustomVertex();
      firstVertex = machine.MyGraph.Vertices.FirstOrDefault();
      this.DataContext = machine.MyGraph;
    }

    private void done_Click(object sender, RoutedEventArgs e)
    {
      ((SecondStateMachine)machine).Sequences.ArrayOfSequence.Add(sequence);
      this.DataContext = machine.MyGraph;
    }

    private void undo_Click(object sender, RoutedEventArgs e)
    {
      machine.MyGraph.ChangeOneVertexColor(secondVertex.Text, Colors.Wheat);
      CustomEdge edge = new CustomEdge(firstVertex, secondVertex);
      edge = machine.MyGraph.GetEdgeBetween(firstVertex, secondVertex);
      if (edge != null)
      {
        machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Wheat);
        secondVertex = firstVertex;
      }
    }

    private void veritces_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      secondVertex = new CustomVertex();
      secondVertex = (CustomVertex)veritces.SelectedItem;
      CustomEdge edge = new CustomEdge(firstVertex, secondVertex);
      edge = machine.MyGraph.GetEdgeBetween(firstVertex, secondVertex);
      if (edge != null)
      {
        machine.MyGraph.ChangeOneVertexColor(firstVertex.Text, Colors.Red);
        machine.MyGraph.ChangeOneVertexColor(secondVertex.Text, Colors.Red);
        machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Red);
        FSMControl.DomainModel.SecondVersion.FSMStep step = new FSMControl.DomainModel.SecondVersion.FSMStep();
        step.Name = edge.Trigger;
        step.TimeoutInSeconds = " 10";
        step.Weight = "1";
        sequence.ArrayOfStep.Add(step);
        firstVertex = secondVertex;
      }
      else
      {
        MessageBox.Show("There is no edge between these vertices! \r\n Please choose another one!");
      }

      this.DataContext = machine.MyGraph;
    }

  }
}
