﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer.DomainModel
{
    public class FSMVState
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

        public string ReentryUsingTrigger
        {
            get;
            set;
        }

        public Collection<AllowedTrigger> ArrayOfAllowedTrigger
        {
            get;
            set;
        }

        public string DefaultHandler
        {
            get;
            set;
        }

        //returneaza numele starii in care duce trigerul cu numele trigName si stare curenta\
        public AllowedTrigger FoundTriggerInCureentState(FSMVTrigger trig)
        {
            AllowedTrigger tr = new AllowedTrigger();
            tr = this.ArrayOfAllowedTrigger.Where(t => (t.TriggerName == trig.Name || t.StateAndTriggerName == trig.CommonID)).FirstOrDefault();
            return tr;

        }
    }
}
