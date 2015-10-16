using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    [Serializable]
    public class AllowedTrigger
    {
        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        /// <value>
        /// The name of the trigger.
        /// </value>
        public string TriggerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string StateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the state and trigger.
        /// </summary>
        /// <value>
        /// The name of the state and trigger.
        /// </value>
        public string StateAndTriggerName
        {
            get;
            set;
        }

    }
}
