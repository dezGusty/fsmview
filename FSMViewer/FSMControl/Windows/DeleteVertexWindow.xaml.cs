using System.Windows;
using System.Windows.Input;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
  /// <summary>
  /// Interaction logic for DeleteVertexWindow.xaml
  /// </summary>
  public partial class DeleteVertexWindow : Window
  {
    private StateMachine machine;

    public DeleteVertexWindow(StateMachine fsm)
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
      this.autocomplete.ItemsSource = this.machine.MyGraph.Vertices;
    }

    public DeleteVertexWindow()
    {
      this.InitializeComponent();
    }

    private void BtnDeleteVertex(object sender, RoutedEventArgs e)
    {
      if (this.machine != null)
      {
        if (this.machine is FirstStateMachine)
        {
          MessageBox.Show(((FirstStateMachine)this.machine).DeleteVertex(autocomplete.Text));
        }
        else
        {
          MessageBox.Show(((SecondStateMachine)this.machine).DeleteVertex(autocomplete.Text));
        }

        this.DataContext = this.machine.MyGraph;
      }
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void Autocomplete_MouseEnter(object sender, MouseEventArgs e)
    {
      autocomplete.Text = string.Empty;
    }
  }
}