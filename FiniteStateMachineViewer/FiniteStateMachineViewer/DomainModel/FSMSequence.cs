using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    public class FSMSequence
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string FinalDescription
        {
            get;
            set;
        }

        public Collection<FSMStep> ArrayOfStep
        {
            get;
            set;
        }
    }
}
