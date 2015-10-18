using FiniteStateMachineViewer.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace FiniteStateMachineViewer
{
    /// <summary>
    /// Interaction logic for AddEdgeWindows.xaml
    /// </summary>
    public partial class AddEdgeWindow : Window
  {
    StateMachine machine = new StateMachine();

    public AddEdgeWindow()
    {
      InitializeComponent();
    }

    public AddEdgeWindow(StateMachine machine)
      : this()
    {
      this.machine = machine;
    }

    private void add_edge_(object sender, RoutedEventArgs e)
    {
      string textFrom = from.Text;
      string textTo = to.Text;
      string trig = trigger.Text;
      if (textFrom.Length.Equals(0) || textTo.Length.Equals(0) || trig.Length.Equals(0))
      {
        MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
        Close();
      }
      else
      {
        if (!(machine.Graph.existingVertices.Exists(v => v.Text.ToLower() == textFrom.ToLower())))
        {
          MessageBox.Show(textFrom + " doesn't exist!");
          Close();
        }
        else
        {
          if (!(machine.Graph.existingVertices.Exists(v => v.Text.ToLower() == textTo.ToLower())))
          {
            MessageBox.Show(textTo + " doesn't exist!");
            Close();
          }
          else
          {
            CustomVertex vtxFrom = machine.Graph.GetVertexByName(textFrom);
            CustomVertex vtxTo = machine.Graph.GetVertexByName(textTo);
            machine.Graph.AddNewEdge(trig, vtxFrom, vtxTo, Colors.Yellow);
            this.DataContext = machine.Graph;
          }
        }
      }
    }
  }
}
