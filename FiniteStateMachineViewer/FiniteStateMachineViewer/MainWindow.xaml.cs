using FiniteStateMachineViewer.DomainModel;
using FiniteStateMachineViewer.ViewModel;
using GraphSharp.Controls;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JR.Utils.GUI.Forms;

namespace FiniteStateMachineViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The machine
        /// </summary>
        public StateMachine machine;

        public MainWindow()
        {
            machine = new StateMachine();
            machine.ViewMachineConfiguration();
            this.DataContext = machine.Graph;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the StateMachine event of the Draw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Draw_StateMachine(object sender, RoutedEventArgs e)
        {
            machine.RepresentThisMachine(Colors.Yellow);
            this.DataContext = machine.Graph;
        }

        /// <summary>
        ///View the entire state machine configuration.
        /// </summary>
        private void View_Configuration(object sender, RoutedEventArgs e)
        {
            machine = new StateMachine();
            machine.ViewMachineConfiguration();
            this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Represents the selected sequence from combobox
        /// </summary>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FSMSequence sequence = new FSMSequence();
            OperationResult result = new OperationResult();
            if (cmbBox.SelectedIndex != -1)
            {
                sequence = (FSMSequence)cmbBox.SelectedItem;
                result = machine.RepresentOneSequence(sequence, Colors.Yellow);
                if (!result.Succes)
                {
                    MessageBox.Show(result.Message);
                    if (result.ResultID == Result.IncorrectSequence)
                    {
                        machine.RepresentOneSequence(sequence, Colors.Red);
                        //machine.ToErrorState(Colors.Red);
                    }
                }
                this.DataContext = machine.Graph;
            }
        }

        private void add_vertex(object sender, RoutedEventArgs e)
        {
            AddVertexWindow addVertexWindow = new AddVertexWindow(machine);
            addVertexWindow.Show();
            this.DataContext = machine.Graph;
        }

        private void add_edge(object sender, RoutedEventArgs e)
        {
            AddEdgeWindow addEdgeWindow = new AddEdgeWindow(machine);
            addEdgeWindow.Show();
            this.DataContext = machine.Graph;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            foreach (CustomVertex vertex in graphLayout.Graph.Vertices)
            {
                double vertexX = GraphLayout.GetX(graphLayout.GetVertexControl(vertex));
                double vertexY = GraphLayout.GetY(graphLayout.GetVertexControl(vertex));
            }
            SetNodesPositionToSave();
            SerializeHelper.SaveGraph(machine.Graph.Graph, "customgraph");
            MessageBox.Show("Graph saved!");
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            machine.Graph.Graph = new CustomGraph();
            machine.Graph.Graph = SerializeHelper.LoadGraph("customgraph");
            StringBuilder s = new StringBuilder();
            foreach (CustomVertex vertex in machine.Graph.Graph.Vertices)
            {
                s.Append("X:" + vertex.X + " Y:" + vertex.Y + " Name:" + vertex.Text + " Color: " + vertex.BackgroundColor.ToString() + "\r\n");
            }
            FlexibleMessageBox.Show(s.ToString());
            SetNodesPositionFromCustomGraph(machine.Graph.Graph);
            this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Sets the nodes position from custom graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private void SetNodesPositionFromCustomGraph(CustomGraph graph)
        {
            for (int i = 0; i < graph.Vertices.Count(); i++)
            {
                GraphLayout.SetX(graphLayout.GetVertexControl(graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).X);
                GraphLayout.SetY(graphLayout.GetVertexControl(graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).Y);
                machine.Graph.Graph.Vertices.Where(v => v.Equals(graph.Vertices.ElementAt(i))).FirstOrDefault().BackgroundColor = graph.Vertices.ElementAt(i).BackgroundColor;
            }
            this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Sets the nodes position.
        /// </summary>
        private void SetNodesPositionToSave()
        {
            foreach (CustomVertex vertex in graphLayout.Graph.Vertices)
            {
                double vertexX = GraphLayout.GetX(graphLayout.GetVertexControl(vertex));
                double vertexY = GraphLayout.GetY(graphLayout.GetVertexControl(vertex));
                graphLayout.Graph.Vertices.Where(var => var.Equals(vertex)).FirstOrDefault().X = vertexX;
                graphLayout.Graph.Vertices.Where(var => var.Equals(vertex)).FirstOrDefault().Y = vertexY;
            }

        }
    }
}
