using FSMViewControl.DomainModel;
using FSMViewControl.Windows;
using GraphSharp.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FSMViewControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FSMControl : UserControl
    {
        /// <summary>
        /// The machine
        /// </summary>
        public StateMachine machine;

        public FSMControl()
        {
            FSMVUtilities.SerializeSequence(machine.seq);
            FSMVUtilities.SerializeConfig(machine.config);
            MessageBox.Show("Graf serializat!");
            InitializeComponent();
            machine.ViewMachineConfiguration();
            machine.RepresentThisMachine(Colors.Beige);
            this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Handles the StateMachine event of the Draw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Draw_Machine(object sender, RoutedEventArgs e)
        {
            machine.RepresentThisMachine(Colors.Yellow);
            this.DataContext = machine.Graph;
        }

        /// <summary>
        ///View the entire state machine configuration.
        /// </summary>
        private void View_Configuration(object sender, RoutedEventArgs e)
        {
            machine.ViewMachineConfiguration();
            this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Represents the selected sequence from combobox
        /// </summary>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machine.ViewMachineConfiguration();
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
                    }
                }
                this.DataContext = machine.Graph;
            }
        }

        /// <summary>
        /// Open a new window for adding a vertex.
        /// </summary>
        private void add_vertex(object sender, RoutedEventArgs e)
        {
            AddVertexWindow addVertexWindow = new AddVertexWindow(machine);
            addVertexWindow.Show();
        }

        /// <summary>
        /// Open a new window for adding an edge.
        /// </summary>
        private void add_edge(object sender, RoutedEventArgs e)
        {
            AddEdgeWindow addEdgeWindow = new AddEdgeWindow(machine);
            addEdgeWindow.Show();
        }

        /// <summary>
        /// Saves the current custom graph.
        /// </summary>
        /// <param name="sender">The graph to save.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a Graph";
            saveFileDialog.ShowDialog();
            SetNodesPositionToSave();
            SerializeHelper.SaveGraph(machine.Graph.Graph, saveFileDialog.FileName);
            MessageBox.Show("Graph saved!");
        }

        /// <summary>
        /// Opens the specified custom graph.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? userClickedOK = openFileDialog.ShowDialog();
            if (userClickedOK == true)
            {
                try
                {
                    machine = new StateMachine();
                    machine.Graph.Graph = SerializeHelper.LoadGraph(openFileDialog.FileName);
                    graphLayout.Graph = machine.Graph.Graph;
                    MessageBox.Show("Done!");
                    SetNodesPositionFromCustomGraph(machine.Graph.Graph);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\nIncorrect file type!");
                    machine.ViewMachineConfiguration();
                }
                this.DataContext = machine.Graph;
            }
        }

        /// <summary>
        /// Sets the nodes position from custom graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private void SetNodesPositionFromCustomGraph(CustomGraph graph)
        {
            for (int i = 0; i < graph.Vertices.Count(); i++)
            {
                GraphLayout.SetX(graphLayout.GetVertexControl(graphLayout.Graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).X);
                GraphLayout.SetY(graphLayout.GetVertexControl(graphLayout.Graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).Y);
            }
        }

        /// <summary>
        /// Sets the nodes position to be serialized.
        /// </summary>
        private void SetNodesPositionToSave()
        {
            foreach (CustomVertex vertex in graphLayout.Graph.Vertices)
            {
                double vertexX = GraphLayout.GetX(graphLayout.GetVertexControl(vertex));
                double vertexY = GraphLayout.GetY(graphLayout.GetVertexControl(vertex));
                machine.Graph.Graph.Vertices.Where(var => var.CompareTo(vertex)).FirstOrDefault().X = vertexX;
                machine.Graph.Graph.Vertices.Where(var => var.CompareTo(vertex)).FirstOrDefault().Y = vertexY;
            }

            //StringBuilder s = new StringBuilder();
            //foreach (CustomVertex vertex in machine.Graph.Graph.Vertices)
            //{
            //    s.Append("Name: " + vertex.Text + " X: " + vertex.X + " Y: " + vertex.Y + " Color: " + vertex.BackgroundColor.ToString() + "\r\n");

            //    // machine.Graph.Graph.Vertices.Where(var => var.CompareTo(vertex)).FirstOrDefault().BackgroundColor = vertex.BackgroundColor;
            //}
            //MessageBox.Show(s.ToString());
        }

        
    }
}
