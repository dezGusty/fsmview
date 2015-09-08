using FiniteStateMachine.DomainModel;
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

        public List<FSMSequence> Sequences
        {
            get;
            set;
        }

    }
}
