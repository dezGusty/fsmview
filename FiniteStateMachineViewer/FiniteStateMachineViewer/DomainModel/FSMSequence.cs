using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    [Serializable]
    public class FSMSequence
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the final description.
        /// </summary>
        /// <value>
        /// The final description.
        /// </value>
        public string FinalDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the array of step.
        /// </summary>
        /// <value>
        /// The array of step.
        /// </value>
        public Collection<FSMStep> ArrayOfStep
        {
            get;
            set;
        }
    }
}
