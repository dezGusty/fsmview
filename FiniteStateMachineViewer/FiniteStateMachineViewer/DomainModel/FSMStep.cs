using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    public class FSMStep
    {
        public string Name
        {
            get;
            set;
        }

        public string Weight
        {
            get;
            set;
        }

        public string TimeoutInSeconds
        {
            get;
            set;
        }
    }
}
