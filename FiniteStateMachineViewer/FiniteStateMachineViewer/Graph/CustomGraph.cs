using GraphSharp.Controls;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineViewer
{

    /// <summary>
    /// Used for graph layout
    /// </summary>
    public class CustomGraphLayout : GraphLayout<CustomVertex, CustomEdge, CustomGraph> { }

    /// <summary>
    /// Class that extends BidirectionalGraph
    /// </summary>
    public class CustomGraph : BidirectionalGraph<CustomVertex, CustomEdge>
    {
        public CustomGraph() { }

        public CustomGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public CustomGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }

       
    }
}
