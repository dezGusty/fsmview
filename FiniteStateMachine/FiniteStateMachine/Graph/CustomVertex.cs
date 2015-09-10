using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.Graph
{
    public class CustomVertex : INotifyPropertyChanged  
    {
         private bool _active;
        private string _text;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
            }
        }

        public CustomVertex(string text)
        {
            Text = text;
        }

        public string ID
        { 
            get; 
            private set;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", ID,Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
