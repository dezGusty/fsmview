using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineViewer
{
    public class CustomVertex : INotifyPropertyChanged
    {
        private string _text;

        public string ID
        { 
            get; 
            private set; 
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyChanged("Text");
            }
        }

        public CustomVertex(string text)
        {
            Text = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomVertex(string id,string text)
        {
            ID = id;
            Text = text;
        }

        public override string ToString()
        {
            return string.Format("{0}",Text);
        }
    }
}
