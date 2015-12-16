using FSMControl.DomainModel.Model.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace FSMControl.DomainModel.FirstVersion
{
    public class FSMConfig : IConfigInterface<AllowedTrigger, FSMConfig, FSMTrigger, FSMState, FSMStep>
    {
        /// <summary>
        /// Gets or sets the array of FSMV trigger.
        /// </summary>
        /// <value>
        /// The array of FSMV trigger.
        /// </value>
        [XmlArray("ArrayOfFSMTrigger")]
        public Collection<FSMTrigger> ArrayOfFSMTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state of the array of FSMV.
        /// </summary>
        /// <value>
        /// The state of the array of FSMV.
        /// </value>
        [XmlArray("ArrayOfFSMState")]
        public Collection<FSMState> ArrayOfFSMState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default trigger on error.
        /// </summary>
        /// <value>
        /// The default trigger on error.
        /// </value>
        [XmlElement("DefaultTriggerOnError")]
        public string DefaultTriggerOnError
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default trigger on reset.
        /// </summary>
        /// <value>
        /// The default trigger on reset.
        /// </value>
        [XmlElement("DefaultTriggerOnReset")]
        public string DefaultTriggerOnReset
        {
            get;
            set;
        }

        /// <summary>
        /// Loads from XML file.
        /// </summary>
        /// <param name="absoluteFilePath">The absolute file path.</param>
        public void LoadFromXMLFileAbsolute(string absoluteFilePath)
        {
            string xmlContent = File.ReadAllText(absoluteFilePath);
            this.LoadFromXML(xmlContent);
        }

        /// <summary>
        /// Loads from XML.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <returns></returns>
        public FSMConfig LoadFromXML(string xmlContent)
        {
            FSMConfig fsmvConfigs = Utilities.FromXmlString<FSMConfig>(xmlContent);
            return fsmvConfigs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMConfig"/> class.
        /// </summary>
        public FSMConfig()
        {
        }

        /// <summary>
        /// Founds the AllowdTrigger from a State configuration in list of triggers.
        /// </summary>
        /// <param name="trig">The trig.</param>
        /// <returns></returns>
        public FSMTrigger FoundTriggerInList(AllowedTrigger trig)
        {
            return this.ArrayOfFSMTrigger.Where(t => t.Name == trig.TriggerName || t.Name == trig.StateAndTriggerName || t.CommonID == trig.StateAndTriggerName).FirstOrDefault();
        }

        /// <summary>
        /// Founds the trigger in list of triggers.
        /// </summary>
        /// <param name="stepp">The stepp that contains trigger SequenceID or CommonID.</param>
        /// <returns></returns>
        public FSMTrigger FoundTriggerInList(FSMStep stepp)
        {
            foreach (FSMTrigger trigger in this.ArrayOfFSMTrigger)
            {
                if (string.IsNullOrEmpty(trigger.CommonID))
                {
                    if (string.Compare(trigger.SequenceID.Trim(), stepp.Name.Trim()) == 0)
                    {
                        return trigger;
                    }
                }
                else
                {
                    if (string.Compare(trigger.CommonID.Trim(), stepp.Name.Trim()) == 0)
                    {
                        return trigger;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Founds the next state of this machine.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <returns></returns>
        public FSMState FoundNextState(string stateName)
        {
            return this.ArrayOfFSMState.Where(state => string.Compare(state.Name.Trim(), stateName.Trim()) == 0).FirstOrDefault();
        }

        /// <summary>
        /// Adds a new trigger to trigger list.
        /// </summary>
        /// <param name="triggerToAdd">The trigger to add.</param>
        /// <returns></returns>
        public bool AddNewTrigger(FSMTrigger triggerToAdd)
        {
            bool exists = false;
            foreach (FSMTrigger item in this.ArrayOfFSMTrigger)
            {
                if (item.CompareTo(triggerToAdd) == true)
                {
                    exists = true;
                    break;
                }
            }

            if (exists == false)
            {
                this.ArrayOfFSMTrigger.Add(triggerToAdd);
                return true;
            }

            return false;
        }

        public FSMTrigger FoundSteppInTriggerList(FSMStep step)
        {
            foreach (FSMTrigger item in this.ArrayOfFSMTrigger)
            {
                if (string.IsNullOrEmpty(item.CommonID))
                {
                    if (string.Compare(item.SequenceID, step.Name) == 0)
                    {
                        return item;
                    }
                }
                else
                {
                    if (string.Compare(item.CommonID, step.Name) == 0)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public FSMTrigger FoundStringTriggerList(string triggerName)
        {
            foreach (FSMTrigger item in this.ArrayOfFSMTrigger)
            {
                if (string.IsNullOrEmpty(item.CommonID))
                {
                    if (string.Compare(item.Name, triggerName) == 0)
                    {
                        return item;
                    }
                }
                else
                {
                    if (string.Compare(item.CommonID, triggerName) == 0)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
}