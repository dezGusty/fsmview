using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.DomainModel
{
    public class FSMVConfig
    {
        public Collection<FSMVTrigger> ArrayOfFSMVTrigger
        {
            get;
            set;
        }

        public Collection<FSMVState> ArrayOfFSMVState
        {
            get;
            set;
        }

        public string DefaultTriggerOnReset
        {
            get;
            set;
        }

        public string DefaultTriggerOnError
        {
            get;
            set;
        }

        public void LoadFromXMLFileAbsolute(string absoluteFilePath)
        {
            string xmlContent = File.ReadAllText(absoluteFilePath);
            this.LoadFromXML(xmlContent);
        }

        public FSMVConfig LoadFromXML(string xmlContent)
        {
            FSMVConfig fsmvConfigs = FSMVUtilities.FromXmlString<FSMVConfig>(xmlContent);
            return fsmvConfigs;
        }

        public FSMVConfig()
        {

        }
    }
}
