using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
  /// <summary>
  /// Interaction logic for GenerateSequenceWindow.xaml
  /// </summary>
  public partial class GenerateSequenceWindow : Window
  {
    private StateMachine machine;
    private FSMControl.DomainModel.FirstVersion.FSMSequence sequenceFirstVersion;
    private FSMControl.DomainModel.SecondVersion.FSMSequence sequenceSecondVersion;
    private CustomVertex firstVertex;
    private CustomVertex secondVertex;

    public GenerateSequenceWindow()
    {
      this.InitializeComponent();
    }

    public GenerateSequenceWindow(StateMachine fsm)
      : this()
    {
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

      this.firstVertex = new CustomVertex();
      this.firstVertex = this.machine.MyGraph.Vertices.FirstOrDefault();
      this.DataContext = this.machine.MyGraph;
    }

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
          this.Close();
        }
      }

      this.DataContext = this.machine.MyGraph;
    }

    private void Undo_Click(object sender, RoutedEventArgs e)
    {
      if (this.machine is FirstStateMachine)
      {
        this.sequenceFirstVersion.ArrayOfStep.RemoveAt(this.sequenceFirstVersion.ArrayOfStep.Count - 1);
      }
      else
      {
        this.sequenceSecondVersion.ArrayOfStep.RemoveAt(this.sequenceSecondVersion.ArrayOfStep.Count - 1);
      }

      MessageBox.Show("Last selected vertex is " + this.firstVertex.Text + this.secondVertex.Text);
      this.machine.MyGraph.ChangeOneVertexColor(this.firstVertex.Text, Colors.Wheat);
      CustomEdge edge = new CustomEdge(this.firstVertex, this.secondVertex);
      edge = this.machine.MyGraph.GetEdgeBetween(this.secondVertex, this.firstVertex);
      this.machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Wheat);
      this.firstVertex = this.secondVertex;
      veritces.SelectedIndex = -1;
    }

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

      this.machine.MyGraph.ResetToDefault();
      this.firstVertex = this.machine.MyGraph.Vertices.FirstOrDefault();
    }

    private void Vertices_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.secondVertex = new CustomVertex();
      this.secondVertex = (CustomVertex)veritces.SelectedItem;
      CustomEdge edge = new CustomEdge(this.firstVertex, this.secondVertex);
      edge = this.machine.MyGraph.GetEdgeBetween(this.firstVertex, this.secondVertex);
      if (edge != null)
      {
        this.machine.MyGraph.ChangeOneVertexColor(this.firstVertex.Text, Colors.Red);
        this.machine.MyGraph.ChangeOneVertexColor(this.secondVertex.Text, Colors.Red);
        this.machine.MyGraph.ChangeOneEdgeColor(edge, Colors.Red);
        if (this.machine is FirstStateMachine)
        {
          ((FirstStateMachine)this.machine).AddNewStep(this.sequenceFirstVersion, edge.Trigger);
        }
        else
        {
          ((SecondStateMachine)this.machine).AddNewStep(this.sequenceSecondVersion, edge.Trigger);
        }

        CustomVertex aux = new CustomVertex();
        aux = this.firstVertex;
        this.firstVertex = this.secondVertex;
        this.secondVertex = aux;
      }
      else
      {
        MessageBox.Show("There is no edge between these vertices! \r\n Please choose another one!");
      }

      this.DataContext = this.machine.MyGraph;
    }
  }
}
