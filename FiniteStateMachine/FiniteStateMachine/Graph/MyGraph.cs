using FiniteStateMachine.Machine;
using FiniteStateMachineTest.DomainModel;
using FiniteStateMachineTest.Machine;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineTest.Graph
{
    public class MyGraph
    {
        private readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();


        public MyGraph()
        {
           
        }

        public BidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _graph; }
        }

        private string _layoutAlgorithm = "FR";

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
        /// Adds the vertices to current graph.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        public void AddVertex(List<SampleVertex> vertex)
        {
            foreach (var item in vertex)
            {
                 _graph.AddVertex(item);
            }
        }

        public string CreateNodesFromStateMachine(StateMachine machine,string input)
        {
            _graph.Clear();

            List<SampleVertex> aList = new List<SampleVertex>();
            foreach (var item in machine.MachineStates)
            {
                aList.Add(new SampleVertex(item.ToString()));
            }

            foreach (SampleVertex item in aList)
            {
                _graph.AddVertex(item);
            }

            List<PairOfStates> edges = new List<PairOfStates>();
            edges = machine.GetEdges(input);

            foreach (PairOfStates item in edges)
            {
                SampleVertex aux1 = aList.Where(i => i.Text == item.FirstState).FirstOrDefault();
                SampleVertex aux2 = aList.Where(i => i.Text == item.SecondState).FirstOrDefault();

                if(!aux1.Equals(" ") && !aux2.Equals(" "))
                {
                    Edge <object>e = new Edge<object>(aux1, aux2);
                    
                    _graph.AddEdge(new Edge<object>(aux1, aux2));
                }
            }
            return "DCS";
        }
    }
}
