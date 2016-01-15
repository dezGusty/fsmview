using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using FSMControl.Windows;
using GraphSharp.Controls;
using System.Windows.Input;

namespace FSMControl
{
  public enum MachineOptions
  {
    Uninitialized,
    Initialized
  }

  public enum GraphOptions
  {
    Uninitialized,
    Initialized
  }

  /// <summary>
  /// Interaction logic for FSMView.xaml
  /// </summary>
  public partial class FSMView : UserControl
  {
    private MachineOptions currentOption;
    private GraphOptions currentOptionGraph;

    private StateMachine machinee = new StateMachine();
    private Version v = new Version();
    private CustomVertex selectedVertex = new CustomVertex();
    private CustomEdge selectedEdge = new CustomEdge(null, null);

    /// <summary>
    /// Initializes a new instance of the <see cref="FSMView"/> class.
    /// </summary>
    public FSMView()
    {
      this.InitializeComponent();

      this.currentOptionGraph = GraphOptions.Uninitialized;
      this.currentOption = MachineOptions.Uninitialized;
      this.EnableButtonStates();
      this.EnableButtonStatesForGraph();
    }

    /// <summary>
    /// Enables the button states for graph.
    /// </summary>
    private void EnableButtonStatesForGraph()
    {
      saveGraph.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      openGraph.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      addVertex.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      addEdge.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      addVertex.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      deleteVertex.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      deleteEdge.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
      generateNewSequence.IsEnabled = this.currentOptionGraph != GraphOptions.Uninitialized;
    }

    /// <summary>
    /// Enables the button states.
    /// </summary>
    private void EnableButtonStates()
    {
      load.IsEnabled = true;
      represent.IsEnabled = this.currentOption != MachineOptions.Uninitialized;
      save.IsEnabled = this.currentOption != MachineOptions.Uninitialized;
      view.IsEnabled = this.currentOption != MachineOptions.Uninitialized;
    }

    /// <summary>
    /// Opens the new version.
    /// </summary>
    private void OpenNewVersion()
    {
      Version auxVersion = new Version(); ////make a new version
      auxVersion.GetVersion();
      if (auxVersion.ID != 0)
      {
        this.v = auxVersion;
        if (this.v.ID == 1)
        {
          this.machinee = new FirstStateMachine(this.v);
          this.machinee.GetDates();
          cmbBox.ItemsSource = ((FirstStateMachine)this.machinee).Sequences.ArrayOfSequence.ToList();
        }
        else
        {
          if (this.v.ID == 2)
          {
            this.machinee = new SecondStateMachine(this.v);
            this.machinee.GetDates();
            cmbBox.ItemsSource = ((SecondStateMachine)this.machinee).Sequences.ArrayOfSequence.ToList();
          }
        }

        this.DataContext = this.machinee.MyGraph;
        console.Text += "A new version opened successfully!\r\n";
      }
      else
      {
        console.Text += "There is no version for this type of xml!\r\n";
        this.currentOption = MachineOptions.Uninitialized;
        this.currentOptionGraph = GraphOptions.Uninitialized;
        this.EnableButtonStates();
        this.EnableButtonStatesForGraph();
      }
    }

    /// <summary>
    /// Shows the current version of application.
    /// </summary>
    private void Show_Version(object sender, RoutedEventArgs e)
    {
      if (this.v.ID == 0)
      {
        MessageBox.Show("There is no version for this type of xml!");
      }
      else
      {
        MessageBox.Show("This is version " + this.v.ID.ToString() + " of application!");
      }
    }

    /// <summary>
    /// Represents all sequences from machine.
    /// </summary>
    private void Represent_machine(object sender, RoutedEventArgs e)
    {
      if (this.v.ID != 0)
      {
        this.machinee.RepresentThisMachine(Colors.Yellow);
        console.Text += "Machine represented successfully!\r\n";
      }
      else
      {
        console.Text += "No machine to represent!\r\n";
      }
    }

    /// <summary>
    /// Represents the selected sequence.
    /// </summary>
    private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
      if (cmbBox.SelectedIndex != -1)
      {
        switch (this.v.ID)
        {
          case 1:
            {
              FSMControl.DomainModel.FirstVersion.FSMSequence seq = new FSMControl.DomainModel.FirstVersion.FSMSequence();
              seq = (FSMControl.DomainModel.FirstVersion.FSMSequence)cmbBox.SelectedItem;
              if (seq.ArrayOfStep.Count == 0)
              {
                MessageBox.Show("Sequence with no steps!");
              }
              else
              {
                ((FirstStateMachine)this.machinee).MyGraph.ResetToDefault();
                ((FirstStateMachine)this.machinee).RepresentSequence(Colors.Yellow, seq, true);
                console.Text += "Sequence represented successfully!\r\n";
                this.DataContext = this.machinee.MyGraph;
              }

              break;
            }

          case 2:
            {
              FSMControl.DomainModel.SecondVersion.FSMSequence seq = new FSMControl.DomainModel.SecondVersion.FSMSequence();
              seq = (FSMControl.DomainModel.SecondVersion.FSMSequence)cmbBox.SelectedItem;
              if (seq.ArrayOfStep.Count == 0)
              {
                MessageBox.Show("Sequence with no steps!");
              }
              else
              {
                ((SecondStateMachine)this.machinee).MyGraph.ResetToDefault();
                if (((SecondStateMachine)this.machinee).RepresentSequence(Colors.Yellow, seq, true).Succes)
                {
                  ((SecondStateMachine)this.machinee).RepresentSequence(Colors.Yellow, seq, true);
                  console.Text += "Sequence represented successfully!\r\n";
                  this.DataContext = this.machinee.MyGraph;
                }
                else
                {
                  MessageBox.Show(((SecondStateMachine)this.machinee).RepresentSequence(Colors.Red, seq, true).Message);
                }
              }

              break;
            }
        }
      }
    }

    /// <summary>
    /// Shows the machine configuration.
    /// </summary>
    private void View_Configuration(object sender, RoutedEventArgs e)
    {
      if (this.machinee != null)
      {
        this.machinee.MyGraph.ResetToDefault();
        this.DataContext = this.machinee.MyGraph;
      }
    }

    /// <summary>
    /// Saves the current custom graph.
    /// </summary>
    /// <param name="sender">The graph to save.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void Save(object sender, RoutedEventArgs e)
    {
      this.EnableButtonStatesForGraph();
      this.SetNodesPositionToSave(this.machinee.MyGraph);
      SerializeHelper.SaveGraph(this.machinee.MyGraph, Utilities.SavePath("Save this graph"));
      console.Text += "Graph succesfully saved!\r\n";
    }

    /// <summary>
    /// Opens the specified custom graph.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void Open(object sender, RoutedEventArgs e)
    {
      string path = Utilities.GetPath("Select the custom graph to open");
      if (path != null)
      {
        try
        {
          CustomGraph graph = new CustomGraph();
          graph = SerializeHelper.LoadGraph(path);
          this.DataContext = graph;
          MessageBox.Show("Graph opened succesfully!");
          this.SetNodesPositionFromCustomGraph(SerializeHelper.LoadGraph(path));
        }
        catch (Exception ex)
        {
          console.Text += ex.Message + "\r\nIncorrect file type!\r\n";
        }
      }
    }

    /// <summary>
    /// Sets the nodes position from custom graph.
    /// </summary>
    /// <param name="graph">The graph.</param>
    private void SetNodesPositionFromCustomGraph(CustomGraph graph)
    {
      StringBuilder s = new StringBuilder();
      for (int i = 0; i < graph.Vertices.Count(); i++)
      {
        GraphLayout.SetX(graphLayout.GetVertexControl(graphLayout.Graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).X);
        GraphLayout.SetY(graphLayout.GetVertexControl(graphLayout.Graph.Vertices.ElementAt(i)), graph.Vertices.ElementAt(i).Y);
      }
    }

    /// <summary>
    /// Sets the nodes position to save.
    /// </summary>
    private void SetNodesPositionToSave(CustomGraph graphToSave)
    {
      foreach (CustomVertex vertex in graphLayout.Graph.Vertices)
      {
        double vertexX = GraphLayout.GetX(graphLayout.GetVertexControl(vertex));
        double vertexY = GraphLayout.GetY(graphLayout.GetVertexControl(vertex));

        graphToSave.Vertices.Where(var => var.CompareTo(vertex)).FirstOrDefault().X = vertexX;
        graphToSave.Vertices.Where(var => var.CompareTo(vertex)).FirstOrDefault().Y = vertexY;
      }
    }

    /// <summary>
    /// Handles the Click event of the SaveConfig control.
    /// </summary>
    private void SaveConfig_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        if (this.v.ID == 1)
        {
          Serializer<FSMConfig, FSMControl.DomainModel.FirstVersion.FSMSequenceConfig>.SerializeConfig(((FirstStateMachine)this.machinee).Configuration, Utilities.SavePath("Configuration"));
          Serializer<FSMConfig, FSMControl.DomainModel.FirstVersion.FSMSequenceConfig>.SerializeSequence(((FirstStateMachine)this.machinee).Sequences, Utilities.SavePath("Sequences"));
        }
        else
        {
          Serializer<FSMVConfig, FSMControl.DomainModel.SecondVersion.FSMSequenceConfig>.SerializeConfig(((SecondStateMachine)this.machinee).Configuration, Utilities.SavePath("Configuration"));
          Serializer<FSMConfig, FSMControl.DomainModel.SecondVersion.FSMSequenceConfig>.SerializeSequence(((SecondStateMachine)this.machinee).Sequences, Utilities.SavePath("Sequences"));
        }

        console.Text += "Configuration succesfully saved!\r\n";
      }
      catch
      {
        console.Text += "\nYou didn't save anything!\r\n";
      }
    }

    /// <summary>
    /// Shows info about this application.
    /// </summary>
    private void About_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Not implemented yet!");
    }

    /// <summary>
    /// Handles the Click event of the AddVertex control.
    /// </summary>
    private void AddVertex_Click(object sender, RoutedEventArgs e)
    {
      AddVertexWindow addVertexWindow = new AddVertexWindow(this.machinee);
      addVertexWindow.Show();
    }

    /// <summary>
    /// Handles the Click event of the AddEdge control.
    /// </summary>
    private void AddEdge_Click(object sender, RoutedEventArgs e)
    {
      AddEdgeWindow addEdgeWindow = new AddEdgeWindow(this.machinee);
      addEdgeWindow.Show();
    }

    /// <summary>
    /// Handles the Click event of the LoadConfig control and loads a new configuration
    /// </summary>
    private void LoadConfig_Click(object sender, RoutedEventArgs e)
    {
      this.currentOption = MachineOptions.Initialized;
      this.EnableButtonStates();
      this.currentOptionGraph = GraphOptions.Initialized;
      this.EnableButtonStatesForGraph();
      this.OpenNewVersion();
    }

    private void DeleteEdge_Click(object sender, RoutedEventArgs e)
    {
      DeleteEdgeWindow deleteEdgeWindow = new DeleteEdgeWindow(this.machinee);
      deleteEdgeWindow.Show();
    }

    private void DeleteVertex_Click(object sender, RoutedEventArgs e)
    {
      DeleteVertexWindow deleteVertexWindow = new DeleteVertexWindow(this.machinee);
      deleteVertexWindow.Show();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      var myWindow = Window.GetWindow(this);
      myWindow.Close();
    }

    private void Generate_Sequence_Click(object sender, RoutedEventArgs e)
    {
      this.currentOptionGraph = GraphOptions.Initialized;
      this.EnableButtonStatesForGraph();
      if (this.v.ID != 0)
      {
        GenerateSequenceWindow window = new GenerateSequenceWindow(this.machinee);
        window.ShowDialog();
        if (this.machinee is FirstStateMachine)
        {
          cmbBox.ItemsSource = ((FirstStateMachine)this.machinee).Sequences.ArrayOfSequence.ToList();
        }
        else
        {
          cmbBox.ItemsSource = ((SecondStateMachine)this.machinee).Sequences.ArrayOfSequence.ToList();
        }
      }
    }

    private void Delete_This_Vertex(object sender, RoutedEventArgs e)
    {
      if (this.machinee != null)
      {
        MessageBox.Show(this.machinee.DeleteVertex(this.selectedVertex.Text));
      }

      this.DataContext = this.machinee.MyGraph;
    }

    private void Add_Edge_To(object sender, RoutedEventArgs e)
    {
      AddEdgeToWindow window = new AddEdgeToWindow(this.machinee, this.selectedVertex);
      window.Show();
    }

    void MyMouseRightButtonDownHandler(object sender, MouseButtonEventArgs e)
    {
        this.selectedVertex = (CustomVertex)(sender as VertexControl).Vertex;
    }

    private void MouseEnter_EventEdge(object sender, RoutedEventArgs e)
    {
      this.selectedEdge = (CustomEdge)(sender as EdgeControl).Edge;
    }

    /// <summary>
    /// Deletes itself edge of the selected vertex, if there is any.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void Delete_My_Edge(object sender, RoutedEventArgs e)
    {
      if (this.selectedVertex != null)
      {
        if (this.machinee != null)
        {
          if (this.machinee is FirstStateMachine)
          {
            MessageBox.Show(((FirstStateMachine)this.machinee).DeleteEdge(this.selectedVertex.Text, this.selectedVertex.Text));
          }
          else
          {
            MessageBox.Show(((SecondStateMachine)this.machinee).DeleteEdge(this.selectedVertex.Text, this.selectedVertex.Text));
          }

          this.DataContext = this.machinee.MyGraph;
        }
      }
    }

    /// <summary>
    /// Delete the selected edge
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void Click_DeleteEdge(object sender, RoutedEventArgs e)
    {
      if (this.selectedEdge != null)
      {
        if (this.machinee != null)
        {
          if (this.machinee is FirstStateMachine)
          {
            MessageBox.Show(((FirstStateMachine)this.machinee).DeleteEdge(this.selectedEdge.Source.Text, this.selectedEdge.Target.Text));
          }
          else
          {
            MessageBox.Show(((SecondStateMachine)this.machinee).DeleteEdge(this.selectedEdge.Source.Text, this.selectedEdge.Target.Text));
          }

          this.DataContext = this.machinee.MyGraph;
        }
      }
    }
  }
}