using GraphSharp;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FiniteStateMachineViewer
{

    public class CustomEdge :TypedEdge<CustomVertex>, INotifyPropertyChanged
    {
        private string id;
        private Color color;
        public Color EdgeColor 
        { 
            get
            {
                return color;
            }
            set
            {
                color = value;
                NotifyPropertyChanged("EdgeColor");
            }
        }

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
            : base(source, target, EdgeTypes.General)
        {
            this.color = Colors.Black;
        }

        public CustomEdge(string id,CustomVertex source, CustomVertex target,Color color)
            : base(source, target,EdgeTypes.General)
        {
            this.id = id;
            this.color = color;
        }

        public bool CompareTo(CustomEdge edge)
        {
            if (this.ID == edge.ID)
                return true;
            return false;
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
