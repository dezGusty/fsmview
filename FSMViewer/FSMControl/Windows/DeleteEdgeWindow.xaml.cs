using System.Windows;
using System.Windows.Input;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
  /// <summary>
  /// Interaction logic for DeleteEdgeWindow.xaml
  /// </summary>
  public partial class DeleteEdgeWindow : Window
  {
    private StateMachine machine;

    public DeleteEdgeWindow(StateMachine fsm)
      : this()
    {
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
      this.autocompleteSource.ItemsSource = this.machine.MyGraph.Vertices;
      this.autocompleteTarget.ItemsSource = this.machine.MyGraph.Vertices;
    }

    public DeleteEdgeWindow()
    {
      this.InitializeComponent();
    }

    private void BtnDeleteEdge(object sender, RoutedEventArgs e)
    {
      if (autocompleteSource.Text.Equals(string.Empty) || autocompleteTarget.Text.Equals(string.Empty))
      {
        MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
        return;
      }
      else
      {
        if (this.machine != null)
        {
          if (this.machine is FirstStateMachine)
          {
            MessageBox.Show(((FirstStateMachine)this.machine).DeleteEdge(autocompleteSource.Text, autocompleteTarget.Text));
          }
          else
          {
            MessageBox.Show(((SecondStateMachine)this.machine).DeleteEdge(autocompleteSource.Text, autocompleteTarget.Text));
          }

          this.DataContext = this.machine.MyGraph;
        }
      }
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void autocompleteSource_MouseEnter(object sender, MouseEventArgs e)
    {
      autocompleteSource.Text = string.Empty;
    }

    private void autocompleteTarget_MouseEnter(object sender, MouseEventArgs e)
    {
      autocompleteTarget.Text = string.Empty;
    }
  }
}