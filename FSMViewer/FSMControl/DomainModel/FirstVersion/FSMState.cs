using FSMControl.DomainModel.Model.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace FSMControl.DomainModel.FirstVersion
{
    [Serializable]
    public class FSMState : IStateInterface<AllowedTrigger, FSMTrigger>
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

        public AllowedTrigger FoundTriggerInCureentState(FSMTrigger trig)
        {
            if (trig != null)
            {
                foreach (AllowedTrigger item in this.ArrayOfAllowedTrigger)
                {
                    if (string.IsNullOrEmpty(item.TriggerName))
                    {
                        if (string.Compare(item.StateAndTriggerName, trig.CommonID) == 0)
                        {
                            return item;
                        }
                    }
                    else
                    {
                        if (string.Compare(item.TriggerName, trig.Name) == 0)
                        {
                            return item;
                        }
                    }
                }
            }

            return null;
        }
    }
}