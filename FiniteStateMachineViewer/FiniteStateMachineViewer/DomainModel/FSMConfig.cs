using System;
using System.Collections.ObjectModel;
using System.IO;

namespace FiniteStateMachineViewer.DomainModel
{
    [Serializable]
    public class FSMConfig
    {
        /// <summary>
        /// Gets or sets the array of FSMV trigger.
        /// </summary>
        /// <value>
        /// The array of FSMV trigger.
        /// </value>
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
        public Collection<FSMState> ArrayOfFSMState
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
        public string DefaultTriggerOnReset
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
        public string DefaultTriggerOnError
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
            FSMConfig fsmvConfigs = FSMVUtilities.FromXmlString<FSMConfig>(xmlContent);
            return fsmvConfigs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMConfig"/> class.
        /// </summary>
        public FSMConfig()
        {

        }
    }
}
