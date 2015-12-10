using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
      this.DataContext = machine.MyGraph;
    }

    private void BtnAddEdge(object sender, RoutedEventArgs e)
    {
      CustomVertex vertexFrom;
      CustomVertex vertexTo;
      if (sourceVeritces.SelectedIndex == -1 || targetVeritces.SelectedIndex == -1)
      {
        MessageBox.Show("You must select a source and a target vertex!");
      }
      else
      {
        vertexFrom = new CustomVertex();
        vertexFrom = (CustomVertex)sourceVeritces.SelectedItem;
        vertexTo = new CustomVertex();
        vertexTo = (CustomVertex)targetVeritces.SelectedItem;

        string trigger = txtTrigger.Text.Trim();

        if (machine is FirstStateMachine)
        {
          ((FirstStateMachine)machine).AddNewEdge(vertexFrom, trigger, vertexTo);
          this.DataContext = machine.MyGraph;
        }
        else
        {
          ((SecondStateMachine)machine).AddNewEdge(vertexFrom, trigger, vertexTo);
          this.DataContext = machine.MyGraph;
        }
      }
    }
  }
}