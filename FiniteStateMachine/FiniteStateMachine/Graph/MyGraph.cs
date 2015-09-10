using FiniteStateMachine.DomainModel;
using FiniteStateMachine.Graph;
using FiniteStateMachine.Machine;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.Graph
{
    public class MyGraph : INotifyPropertyChanged
    {
        
        private string layoutAlgorithmType;
        private CustomGraph graph;
        private List<String> layoutAlgorithmTypes = new List<string>();
        private int count;

        public MyGraph()
        {
            Graph = new CustomGraph(true);


            List<CustomVertex> existingVertices = new List<CustomVertex>();
            existingVertices.Add(new CustomVertex("Sacha Barber")); 
            existingVertices.Add(new CustomVertex("Sarah Barber")); 
            existingVertices.Add(new CustomVertex("Marlon Grech")); 


            foreach (CustomVertex vertex in existingVertices)
                Graph.AddVertex(vertex);


            //add some edges to the graph
            AddNewGraphEdge(existingVertices[0], existingVertices[1]);
            AddNewGraphEdge(existingVertices[0], existingVertices[2]);

            //Add Layout Algorithm Types
            layoutAlgorithmTypes.Add("BoundedFR");
            layoutAlgorithmTypes.Add("Circular");
            layoutAlgorithmTypes.Add("CompoundFDP");
            layoutAlgorithmTypes.Add("EfficientSugiyama");
            layoutAlgorithmTypes.Add("FR");
            layoutAlgorithmTypes.Add("ISOM");
            layoutAlgorithmTypes.Add("KK");
            layoutAlgorithmTypes.Add("LinLog");
            layoutAlgorithmTypes.Add("Tree");

            //Pick a default Layout Algorithm Type
            LayoutAlgorithmType = "LinLog";
        }

        public void ReLayoutGraph()
        {
            graph = new CustomGraph(true);
            count++;

            List<CustomVertex> existingVertices = new List<CustomVertex>();
            existingVertices.Add(new CustomVertex(String.Format("Barn Rubble{0}", count)));
            existingVertices.Add(new CustomVertex(String.Format("Frank Zappa{0}", count)));
            existingVertices.Add(new CustomVertex(String.Format("Gerty CrinckleBottom{0}", count)));


            foreach (CustomVertex vertex in existingVertices)
                Graph.AddVertex(vertex);


            //add some edges to the graph
            AddNewGraphEdge(existingVertices[0], existingVertices[1]);
            AddNewGraphEdge(existingVertices[0], existingVertices[2]);

            Graph.RemoveVertex(existingVertices[0]);

            NotifyPropertyChanged("Graph");
        }



        #region Private Methods
        private CustomEdge AddNewGraphEdge(CustomVertex from, CustomVertex to)
        {
            string edgeString = string.Format("{0}-{1} Connected", from.ID, to.ID);

            CustomEdge newEdge = new CustomEdge(edgeString, from, to);
            Graph.AddEdge(newEdge);
            return newEdge;
        }


        #endregion

        #region Public Properties

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



        public CustomGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged("Graph");
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
