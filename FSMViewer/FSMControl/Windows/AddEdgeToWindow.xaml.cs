using System.Windows;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
  /// <summary>
  /// Interaction logic for AddEdgeToWindow.xaml
  /// </summary>
  public partial class AddEdgeToWindow : Window
  {
    private StateMachine machine;
    private CustomVertex vertexFrom;

    public AddEdgeToWindow()
    {
      this.InitializeComponent();
    }

    public AddEdgeToWindow(StateMachine fsm, CustomVertex vertex)
      : this()
    {
      this.vertexFrom = new CustomVertex();
      if (fsm is FirstStateMachine)
      {
        this.machine = new FirstStateMachine(fsm.CurrentVersion);
      }
      else
      {
        if (fsm is SecondStateMachine)
        {
          this.machine = new SecondStateMachine(fsm.CurrentVersion);
        }
      }

      this.machine = fsm;
      this.vertexFrom = vertex;
      this.DataContext = this.machine.MyGraph;
    }

    private void BtnAddEdge(object sender, RoutedEventArgs e)
    {
      CustomVertex vertexTo;
      if (targetVeritces.SelectedIndex == -1)
      {
        MessageBox.Show("You must select a source and a target vertex!");
      }
      else
      {
        vertexTo = new CustomVertex();
        vertexTo = (CustomVertex)targetVeritces.SelectedItem;

        string trigger = txtTrigger.Text.Trim();

        if (this.machine is FirstStateMachine)
        {
          ((FirstStateMachine)this.machine).AddNewEdge(this.vertexFrom, trigger, vertexTo);
          this.DataContext = this.machine.MyGraph;
        }
        else
        {
          ((SecondStateMachine)this.machine).AddNewEdge(this.vertexFrom, trigger, vertexTo);
          this.DataContext = this.machine.MyGraph;
        }
      }
    }
  }
}