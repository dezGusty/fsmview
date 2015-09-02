using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TestGraph.ViewModel;

namespace TestGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CreateGraph();
        }
        /// <summary>
        /// The _graph
        /// </summary>
        private readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();
        /// <summary>
        /// Gets the graph.
        /// </summary>
        /// <value>
        /// The graph.
        /// </value>
        public IBidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
        }

        private string _layoutAlgorithm = "Circular";
        public string LayoutAlgorithm
        {
            get { return _layoutAlgorithm; }
            set
            {
                if (value != _layoutAlgorithm)
                {
                    _layoutAlgorithm = value;
                }
            }
        }
        /// <summary>
        /// Creates the graph.
        /// </summary>
        private void CreateGraph()
        {
            _graph.Clear();
            SampleVertex obj1 = new SampleVertex("1");
            _graph.AddVertex(obj1);
            SampleVertex obj2 = new SampleVertex("2");
            _graph.AddVertex(obj2);
            SampleVertex obj3 = new SampleVertex("3");
            _graph.AddVertex(obj3);
            SampleVertex obj4 = new SampleVertex("4");
            _graph.AddVertex(obj4);
            _graph.AddEdge(new Edge<object>(obj1, obj2));
            _graph.AddEdge(new Edge<object>(obj1, obj4));
            _graph.AddEdge(new Edge<object>(obj1, obj3));
            _graph.AddEdge(new Edge<object>(obj2, obj3));
            _graph.AddEdge(new Edge<object>(obj2, obj4));
            _graph.AddEdge(new Edge<object>(obj3, obj4));
        }
        /// <summary>
        /// Handles the Click event of the MenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as SampleVertex;
            vertex.Change();
        }
    }
}

