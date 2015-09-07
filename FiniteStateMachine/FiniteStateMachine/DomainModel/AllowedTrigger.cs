using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.DomainModel
{
    public class AllowedTrigger
    {
        public string TriggerName
        {
            get;
            set;
        }

        public string StateName
        {
            get;
            set;
        }
    }
}
