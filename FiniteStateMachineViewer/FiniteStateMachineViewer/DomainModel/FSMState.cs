using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiniteStateMachineViewer.DomainModel
{
    [Serializable]
    public class FSMState
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reentry using trigger.
        /// </summary>
        /// <value>
        /// The reentry using trigger.
        /// </value>
        public string ReentryUsingTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the array of allowed trigger.
        /// </summary>
        /// <value>
        /// The array of allowed trigger.
        /// </value>
        public Collection<AllowedTrigger> ArrayOfAllowedTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default handler.
        /// </summary>
        /// <value>
        /// The default handler.
        /// </value>
        public string DefaultHandler
        {
            get;
            set;
        }

        //returneaza numele starii in care duce trigerul cu numele trigName si stare curenta\
        public AllowedTrigger FoundTriggerInCureentState(FSMTrigger trig)
        {
            AllowedTrigger tr = new AllowedTrigger();
            tr = this.ArrayOfAllowedTrigger.Where(t => (t.TriggerName == trig.Name || t.StateAndTriggerName == trig.CommonID)).FirstOrDefault();
            return tr;

        }
    }
}
