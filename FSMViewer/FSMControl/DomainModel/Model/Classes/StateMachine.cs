using FSMControl.DomainModel.FirstVersion;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMControl.DomainModel.Model.Interfaces;
using System.Windows.Media;
using System.Xml.Schema;
using System.Xml.Linq;

namespace FSMControl
{
    public class StateMachine
    {
        public CustomGraph MyGraph;

        public Version CurrentVersion
        {
            get;
            set;
        }

        public string Xml
        {
            get;
            set;
        }

        public string XmlSeq
        {
            get;
            set;
        }

        public virtual void GetDates()
        {

        }

         public virtual void SetGraphNodes()
        {

        }

         public virtual void RepresentThisMachine(Color color)
         {

         }
    }
}
