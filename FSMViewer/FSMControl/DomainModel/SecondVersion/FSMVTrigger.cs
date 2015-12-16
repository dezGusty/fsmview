using FSMControl.DomainModel.Model.Interfaces;
using System;

namespace FSMControl.DomainModel.SecondVersion
{
    [Serializable]
    public class FSMVTrigger : ITriggerInterface<FSMVTrigger>
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
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMVTrigger"/> class.
        /// </summary>
        public FSMVTrigger()
        {
        }

        /// <summary>
        /// Compares current trigger to another trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public bool CompareTo(FSMVTrigger trigger)
        {
            if (this.Name == null && this.SequenceID == null)
            {
                if (!string.IsNullOrEmpty(trigger.CommonID))
                {
                    if (string.Compare(this.CommonID.Trim(), trigger.CommonID.Trim()) == 0)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(trigger.Name) && !string.IsNullOrEmpty(trigger.SequenceID))
                {
                    if (string.Compare(this.Name.Trim(), trigger.Name.Trim()) == 0 && string.Compare(this.SequenceID, trigger.SequenceID) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}