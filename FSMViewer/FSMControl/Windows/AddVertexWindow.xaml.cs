using System.Windows;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
  /// <summary>
  /// Interaction logic for AddVertexWindow.xaml
  /// </summary>
  public partial class AddVertexWindow : Window
  {
    private StateMachine machine;

    public AddVertexWindow()
    {
      this.InitializeComponent();
    }

    public AddVertexWindow(StateMachine fsm)
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
    }

    private void BtnAddVertex(object sender, RoutedEventArgs e)
    {
      if (this.machine != null)
      {
        if (this.machine is FirstStateMachine)
        {
          MessageBox.Show(((FirstStateMachine)this.machine).AddNewState(txtAddVertex.Text, txt_DefaultHandler.Text, txt_Reentry.Text));
        }
        else
        {
          MessageBox.Show(((SecondStateMachine)this.machine).AddNewState(txtAddVertex.Text, txt_DefaultHandler.Text, txt_Reentry.Text));
        }

        this.DataContext = this.machine.MyGraph;
      }
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}