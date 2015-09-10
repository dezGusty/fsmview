using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineViewer
{

    public class CustomEdge :Edge<CustomVertex>, INotifyPropertyChanged
    {
        private string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public CustomEdge( CustomVertex source, CustomVertex target)
            : base(source, target)
        {
        }

        public CustomEdge(string id,CustomVertex source, CustomVertex target)
            : base(source, target)
        {
            this.id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
