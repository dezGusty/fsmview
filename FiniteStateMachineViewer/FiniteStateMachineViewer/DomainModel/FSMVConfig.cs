using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    public class FSMVConfig
    {
        /// <summary>
        /// Gets or sets the array of FSMV trigger.
        /// </summary>
        /// <value>
        /// The array of FSMV trigger.
        /// </value>
        public Collection<FSMVTrigger> ArrayOfFSMVTrigger
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
        public Collection<FSMVState> ArrayOfFSMVState
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
        public FSMVConfig LoadFromXML(string xmlContent)
        {
            FSMVConfig fsmvConfigs = FSMVUtilities.FromXmlString<FSMVConfig>(xmlContent);
            return fsmvConfigs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMVConfig"/> class.
        /// </summary>
        public FSMVConfig()
        {

        }
    }
}
