using FiniteStateMachineViewer;
using GraphSharp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachineViewer.ViewModel;
using FiniteStateMachineViewer.DomainModel;
using System.Windows.Media;

namespace FiniteStateMachineViewer
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        private string layoutAlgorithmType;
        private CustomGraph graph;
        private List<String> layoutAlgorithmTypes = new List<string>();
        public List<CustomVertex> existingVertices = new List<CustomVertex>();

        public static String Message
        {
            get;
            set;
        }

        public GraphViewModel()
        {
            Graph = new CustomGraph(true);
            existingVertices = new List<CustomVertex>();

            layoutAlgorithmTypes.Add("BoundedFR");
            layoutAlgorithmTypes.Add("Circular");
            layoutAlgorithmTypes.Add("CompoundFDP");
            layoutAlgorithmTypes.Add("EfficientSugiyama");
            layoutAlgorithmTypes.Add("FR");
            layoutAlgorithmTypes.Add("ISOM");
            layoutAlgorithmTypes.Add("KK");
            layoutAlgorithmTypes.Add("LinLog");
            layoutAlgorithmTypes.Add("Tree");

            LayoutAlgorithmType = "LinLog";
            Message ="";
        }

        public string AddNewVertex(CustomVertex vertex)
        {
            existingVertices.Add(vertex);
            Graph.AddVertex(vertex);
            NotifyPropertyChanged("Graph");
            return "Added vertex: " + vertex.Text;
        }

        public CustomEdge AddNewEdge(CustomVertex from, CustomVertex to)
        {
            string edgeString = string.Format("{0}-{1} Connected", from.ID, to.ID);

            CustomEdge newEdge = new CustomEdge( from, to);
            Graph.AddEdge(newEdge);
             NotifyPropertyChanged("Graph");
            return newEdge;
        }

        public CustomEdge AddNewEdge(string id,CustomVertex from, CustomVertex to,Color color)
        {
            string edgeString = string.Format("{0}-{1} Connected", from.ID, to.ID);

            CustomEdge newEdge = new CustomEdge(id,from, to,color);
            Graph.AddEdge(newEdge);
            NotifyPropertyChanged("Graph");
            return newEdge;
        }

        public List<String> LayoutAlgorithmTypes
        {
            get { return layoutAlgorithmTypes; }
        }

        public string LayoutAlgorithmType
        {
            get { return layoutAlgorithmType; }
            set
            {
                layoutAlgorithmType = value;
                NotifyPropertyChanged("LayoutAlgorithmType");
            }
        }

        public List<FSMSequence> Sequences
        {
            get;
            set;
        }

        public CustomGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged("Graph");
            }
        }

        public CustomVertex GetVertexByName(string vertexName)
        {
            return existingVertices.Where(v => v.Text == vertexName).FirstOrDefault();
        }

        public void ChangeEdgeColor(Color c)
        {
            
            for (int i = 0; i < this.graph.EdgeCount;i++ )
            {
                this.graph.Edges.ElementAt(i).EdgeColor = c;
            }
            NotifyPropertyChanged("Graph");
        }

        public void ChangeOneEdgeColor(CustomEdge edge,Color c)
        {

            for (int i = 0; i < this.graph.EdgeCount; i++)
            {
                this.graph.Edges.Where(e=>e==edge).FirstOrDefault().EdgeColor = c;
            }
            NotifyPropertyChanged("Graph");
        }

        public CustomEdge FindEdgeInGraph(CustomEdge edge)
        {
           return graph.Edges.Where(e => e.CompareTo(edge)).FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
