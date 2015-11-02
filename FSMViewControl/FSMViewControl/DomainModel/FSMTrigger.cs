using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMViewControl.DomainModel
{
    [Serializable]
    public class FSMTrigger
    {
        /// <summary>
        /// The name by which the trigger is identified in the state machine.
        /// Should be unique.
        /// </summary>
        
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The name or ID by which the trigger is identified in the sequences definition.
        /// Should also be unique.
        /// </summary>

        public string SequenceID
        {
            get;
            set;
        }

        /// <summary>
        /// If the same content should be used for the Name and Sequence ID, consider using the CommonID property.
        /// </summary>
        public string CommonID
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
                this.SequenceID = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMTrigger"/> class.
        /// </summary>
        public FSMTrigger()
        {
            this.Name = string.Empty;
            this.SequenceID = string.Empty;
        }

        /// <summary>
        /// Compares current trigger to another trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public bool compareTo(FSMTrigger trigger)
        {
            if (string.Compare(this.Name.Trim(), trigger.Name.Trim()) == 0 && string.Compare(this.CommonID.Trim(), trigger.CommonID.Trim()) == 0 && string.Compare(this.SequenceID, trigger.SequenceID) == 0)
                return true;
            return false;
        }
    }
}
