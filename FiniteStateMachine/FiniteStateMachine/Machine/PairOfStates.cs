using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.Machine
{
    public class PairOfStates
    {
        public string FirstState
        {
            get;
            set;
        }
        public string SecondState
        {
            get;
            set;
        }
        public PairOfStates(string s1,string s2)
        {
            FirstState = s1;
            SecondState = s2;
        }

        public string  ToString()
        {
            return FirstState+" "+SecondState;
        }
    }
}
