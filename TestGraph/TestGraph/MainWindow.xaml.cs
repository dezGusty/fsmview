using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TestGraph.DomainModel;
using TestGraph.ViewModel;

namespace TestGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string xml = "../../statemachinecfg.xml";
        private string xmlSeq = "../../statemachinecfgsequences.xml"; 

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
        /// 

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as SampleVertex;
            vertex.Change();
        }

        //private string xmlAbloute = "C:/Users/Mircea.Solovastru/Desktop/TestGraph/TestGraph/statemachinecfg.xml";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          FSMSequenceConfig seq = new FSMSequenceConfig();
          seq=seq.LoadFromXML(xmlSeq);

          if (seq.ArrayOfSequence.Count == 0)
              MessageBox.Show("Nothing to display!");
          else
          {
              foreach (var p in seq.ArrayOfSequence)
              {
                  xmlParse.Items.Add("Name: "+p.Name + "Final description:  " + p.FinalDescription+" Description: "+p.Description);
                  xmlParse.Items.Add("Steps:");
                  foreach (var item in p.ArrayOfStep)
                  {
                      xmlParse.Items.Add("Name: " + item.Name);
                  }
                  xmlParse.Items.Add(" ");
              }
          }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FSMVConfig seq = new FSMVConfig();
            seq = seq.LoadFromXML(xml);
            foreach (var item in seq.ArrayOfFSMVState)
            {
                xmlParse.Items.Add("Name of state: " + item.Name + " Default handler: " + item.DefaultHandler);
                xmlParse.Items.Add("Triggers:");
                foreach (var it in item.ArrayOfAllowedTrigger)
                {
                     xmlParse.Items.Add("Name of trigger: " + it.TriggerName + " State name: " + it.StateName);
                }
            }

            foreach (var item in seq.ArrayOfFSMVTrigger)
            {
                StringBuilder s = new StringBuilder();
                if (item.Name.Trim().Length != 0)
                    s.Append("Trigger name: " +item.Name);
                if(item.SequenceID.Trim().Length!=0)
                    s.Append("Trigger SequenceID: " + item.SequenceID);
                if (item.CommonID.Trim().Length != 0)
                    s.Append("Trigger CommonID: " + item.CommonID);
                xmlParse.Items.Add(s);
            }
        }
    }
}

