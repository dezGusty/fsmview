using FiniteStateMachineViewer.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace FiniteStateMachineViewer
{
    /// <summary>
    /// Interaction logic for AddVertexWindow.xaml
    /// </summary>
    public partial class AddVertexWindow : Window
  {
    private StateMachine machine = new StateMachine();

    public AddVertexWindow()
    {
      InitializeComponent();
    }

    public AddVertexWindow(StateMachine machine)
      : this()
    {
      this.machine = machine;
    }

    private void add_vertex_(object sender, RoutedEventArgs e)
    {
      string text = txtaddvertex.Text;
      if (text.Length.Equals(0))
      {
        MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
        Close();
      }
      else
      {
        if (machine.Graph.existingVertices.Exists(v => v.Text.ToLower() == text.ToLower()))
        {
          MessageBox.Show("A vertex with the name << " + text + " >>  already exists in the graph");
          Close();
        }
        else
        {
          CustomVertex vertex = new CustomVertex(text, Colors.Magenta);
          machine.Graph.AddNewVertex(vertex);
        }
      }
    }
  }
}
