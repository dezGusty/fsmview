using FSMViewControl.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace FSMViewControl
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        private string layoutAlgorithmType;
        private CustomGraph graph;
        private List<String> layoutAlgorithmTypes = new List<string>();

        [XmlIgnoreAttribute]
        public List<CustomVertex> existingVertices = new List<CustomVertex>();

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [XmlIgnoreAttribute]
        public static String Message
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphViewModel"/> class.
        /// </summary>
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

            LayoutAlgorithmType = "Tree";
        }

        /// <summary>
        /// Adds the new vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns></returns>
        public string AddNewVertex(CustomVertex vertex)
        {
            existingVertices.Add(vertex);
            Graph.AddVertex(vertex);
            NotifyPropertyChanged("Graph");
            return "Added vertex: " + vertex.Text;

        }

        /// <summary>
        /// Adds the new edge.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public CustomEdge AddNewEdge(string trigger, CustomVertex from, CustomVertex to, Color color)
        {
            string edgeString = string.Format("{0}-{1} Connected", from.Text, to.Text);
            CustomEdge newEdge = new CustomEdge(trigger, from, to, color);
            if (!this.graph.Edges.Contains(newEdge))
            {
                if (from.CompareTo(to))
                {
                    existingVertices.Where(v => v.CompareTo(from)).FirstOrDefault().Highlight = true;
                    NotifyPropertyChanged("CustomVertex");
                }
                else
                {
                    Graph.AddEdge(newEdge);
                }
                NotifyPropertyChanged("Graph");

            }
            return newEdge;
        }

        /// <summary>
        /// Gets the layout algorithm types.
        /// </summary>
        /// <value>
        /// The layout algorithm types.
        /// </value>
        [XmlIgnoreAttribute]
        public List<String> LayoutAlgorithmTypes
        {
            get { return layoutAlgorithmTypes; }
        }

        /// <summary>
        /// Gets or sets the type of the layout algorithm.
        /// </summary>
        /// <value>
        /// The type of the layout algorithm.
        /// </value>
        [XmlIgnoreAttribute]
        public string LayoutAlgorithmType
        {
            get { return layoutAlgorithmType; }
            set
            {
                layoutAlgorithmType = value;
                NotifyPropertyChanged("LayoutAlgorithmType");
            }
        }

        /// <summary>
        /// Gets or sets the sequences.
        /// </summary>
        /// <value>
        /// The sequences.
        /// </value>
        public List<FSMSequence> Sequences
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        /// <value>
        /// The graph.
        /// </value>
        [XmlIgnoreAttribute]
        public CustomGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged("Graph");
            }
        }

        /// <summary>
        /// Gets the name of the vertex by.
        /// </summary>
        /// <param name="vertexName">Name of the vertex.</param>
        /// <returns></returns>
        public CustomVertex GetVertexByName(string vertexName)
        {
            return existingVertices.Where(v => v.Text.ToLower() == vertexName.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Changes the color of the edges.
        /// </summary>
        /// <param name="c">The c.</param>
        public void ChangeEdgeColor(Color c)
        {
            for (int i = 0; i < this.graph.EdgeCount; i++)
            {
                this.graph.Edges.ElementAt(i).EdgeColor = c;
            }
            NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Changes the color of the vertices.
        /// </summary>
        /// <param name="color">The color.</param>
        public void ChangeVertexColor(Color color)
        {
            for (int i = 0; i < this.graph.VertexCount; i++)
            {
                this.graph.Vertices.ElementAt(i).BackgroundColor = color;
            }
            NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Resets the highlight to default.
        /// </summary>
        public void ResetHighlightToDefault()
        {
            for (int i = 0; i < this.graph.VertexCount; i++)
            {
                this.graph.Vertices.ElementAt(i).Highlight = false;
            }
            NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Resets the highlight to default.
        /// </summary>
        public void ResetRepresentedToDefault()
        {
            for (int i = 0; i < this.graph.VertexCount; i++)
            {
                this.graph.Vertices.ElementAt(i).Represented = false;
            }
            NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Changes the color of the one edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="c">The color.</param>
        public void ChangeOneEdgeColor(CustomEdge edge, Color color)
        {
            for (int i = 0; i < this.graph.EdgeCount; i++)
            {
                this.graph.Edges.Where(e => e == edge).FirstOrDefault().EdgeColor = color;
            }
            NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Changes the color of the one vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <param name="color">The color.</param>
        public void ChangeOneVertexColor(CustomVertex vertex, Color color)
        {
            for (int i = 0; i < this.graph.VertexCount; i++)
            {
                this.graph.Vertices.Where(e => e == vertex).FirstOrDefault().BackgroundColor = color;
            }
            NotifyPropertyChanged("Graph");
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
