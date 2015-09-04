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

        public void CreateRandomGraph()
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
