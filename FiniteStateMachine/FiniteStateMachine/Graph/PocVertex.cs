﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.Graph
{
    /// <summary>
    /// A simple identifiable vertex.
    /// </summary>
    [DebuggerDisplay("{ID}-{IsMale}")]
    public class PocVertex
    {
        public string ID { get; private set; }
        public bool IsMale { get; private set; }

        public PocVertex(string id, bool isMale)
        {
            ID = id;
            IsMale = isMale;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", ID, IsMale);
        }
    }
}
