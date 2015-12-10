using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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
      InitializeComponent();
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
      if (machine != null)
      {
        if (machine is FirstStateMachine)
        {
          MessageBox.Show(((FirstStateMachine)machine).AddNewState(txtAddVertex.Text, txt_DefaultHandler.Text, txt_Reentry.Text));
        }
        else
        {
          MessageBox.Show(((SecondStateMachine)machine).AddNewState(txtAddVertex.Text, txt_DefaultHandler.Text, txt_Reentry.Text));
        }
        this.DataContext = machine.MyGraph;
      }
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}