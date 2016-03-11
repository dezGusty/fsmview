using GraphSharp.Controls;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Xml.Serialization;

namespace FSMControl
{
    /// <summary>
    /// Used for graph layout
    /// </summary>
    [Serializable]
    public class CustomGraphLayout : GraphLayout<CustomVertex, CustomEdge, CustomGraph>
    {
    }

    /// <summary>
    /// Class that extends BidirectionalGraph
    /// </summary>
    [Serializable]
    public class CustomGraph : BidirectionalGraph<CustomVertex, CustomEdge>, INotifyPropertyChanged
    {
        public CustomGraph()
        {
            this.ListOfLayoutAlgorithmTypes = new List<string>();
            this.ListOfLayoutAlgorithmTypes.Add("BoundedFR");
            this.ListOfLayoutAlgorithmTypes.Add("Circular");
            this.ListOfLayoutAlgorithmTypes.Add("CompoundFDP");
            this.ListOfLayoutAlgorithmTypes.Add("EfficientSugiyama");
            this.ListOfLayoutAlgorithmTypes.Add("FR");
            this.ListOfLayoutAlgorithmTypes.Add("ISOM");
            this.ListOfLayoutAlgorithmTypes.Add("KK");
            this.ListOfLayoutAlgorithmTypes.Add("LinLog");
            this.ListOfLayoutAlgorithmTypes.Add("Tree");
            this.LayoutAlgorithmType = "Tree";
            this.message = " ";
        }

        private string message;

        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.NotifyPropertyChanged("Message");
            }
        }

        public CustomGraph(bool allowParallelEdges)
          : base(allowParallelEdges)
        {
        }

        public CustomGraph(bool allowParallelEdges, int vertexCapacity)
          : base(allowParallelEdges, vertexCapacity)
        {
        }

        private string layoutAlgorithmType;

        public List<string> ListOfLayoutAlgorithmTypes { get; set; }

        /// <summary>
        /// Gets or sets the type of the layout algorithm.
        /// </summary>
        /// <value>
        /// The type of the layout algorithm.
        /// </value>
        [XmlIgnoreAttribute]
        public string LayoutAlgorithmType
        {
            get
            {
                return this.layoutAlgorithmType;
            }

            set
            {
                this.layoutAlgorithmType = value;
                this.NotifyPropertyChanged("LayoutAlgorithmType");
            }
        }

        /// <summary>
        /// Gets the layout algorithm types.
        /// </summary>
        /// <value>
        /// The layout algorithm types.
        /// </value>
        [XmlIgnoreAttribute]
        public List<string> LayoutAlgorithmTypes
        {
            get { return this.ListOfLayoutAlgorithmTypes; }
        }

        /// <summary>
        /// Adds the new edge.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public void AddNewEdge(string trigger, CustomVertex from, CustomVertex to)
        {
            if (from != null && to != null)
            {
                CustomEdge newEdge = new CustomEdge(trigger, from, to);
                if (!this.Edges.Contains(newEdge))
                {
                    if (from.CompareTo(to))
                    {
                        this.Vertices.Where(v => v.CompareTo(from)).FirstOrDefault().Highlight = true;
                        this.NotifyPropertyChanged("CustomVertex");
                    }
                    this.AddEdge(newEdge);

                    this.NotifyPropertyChanged("Graph");
                    this.Message += "Edge " + newEdge.Source.Text + "--> " + newEdge.Target.Text + " added successfully! \r\n";
                }
                else
                {
                    this.Message += "Edge already exists! \r\n";
                }
            }
        }

        /// <summary>
        /// Removes an edge.
        /// </summary>
        public bool RemoveItselfEdge(CustomVertex vertex)
        {
            if (vertex != null)
            {
                this.GetVertexByName(vertex.Text).Highlight = false;
                CustomEdge edge = new CustomEdge(null, null);
                edge = this.GetEdgeBetween(vertex, vertex);
                if (edge != null)
                {
                    this.RemoveEdgeIf(v => v.Source.Text.Equals(vertex.Text) && v.Target.Text.Equals(vertex.Text));
                    return true;
                }
            }
            return false;
        }

        public CustomVertex GetVertexByName(string vertexName)
        {
            return this.Vertices.Where(v => string.Compare(v.Text.Trim(), vertexName) == 0).FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <summary>
        /// Changes the color of the vertices.
        /// </summary>
        /// <param name="color">The color.</param>
        public void ChangeVertexColor(Color color)
        {
            for (int i = 0; i < this.VertexCount; i++)
            {
                this.Vertices.ElementAt(i).BackgroundColor = color;
            }

            this.NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Changes the color of the one vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <param name="color">The color.</param>
        public void ChangeOneVertexColor(string vertexName, Color color)
        {
            this.Vertices.Where(e => e.CompareTo(this.GetVertexByName(vertexName))).FirstOrDefault().BackgroundColor = color;
            this.NotifyPropertyChanged("Graph");
        }

        /// <summary>
        /// Resets the colors and the highlight to default.
        /// </summary>
        public void ResetToDefault()
        {
            foreach (CustomVertex v in this.Vertices)
            {
                if (v.Display == true)
                {
                    v.BackgroundColor = Colors.Wheat;
                }
                else
                {
                    v.BackgroundColor = Colors.Red;
                }
                v.Represented = false;
                this.NotifyPropertyChanged("Graph");
            }

            foreach (CustomEdge item in this.Edges)
            {
                item.EdgeColor = Colors.Black;
                this.NotifyPropertyChanged("Graph");
            }
        }

        /// <summary>
        /// Changes the color of the one edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="c">The color.</param>
        public void ChangeOneEdgeColor(CustomEdge edge, Color color)
        {
            if (edge != null)
            {
                this.Edges.Where(e => e.CompareTo(edge) == true).FirstOrDefault().EdgeColor = color;
            }
        }

        /// <summary>
        /// Gets the edge between two vertices.
        /// </summary>
        /// <param name="source">The first vertex.</param>
        /// <param name="target">The second vertex.</param>
        public CustomEdge GetEdgeBetween(CustomVertex source, CustomVertex target)
        {
            return this.Edges.Where(e => (e.Source.CompareTo(source) && e.Target.CompareTo(target))).FirstOrDefault();
        }

        /// <summary>
        /// Gets the edge between two vertices.
        /// </summary>
        /// <param name="vOne">The first vertex.</param>
        /// <param name="target">The second vertex.</param>
        public CustomEdge GetEdgeBetween(string source, string target)
        {
            return this.Edges.Where(e => (string.Compare(e.Source.Text, source) == 0 && string.Compare(e.Target.Text, target) == 0)).FirstOrDefault();
        }
    }
}