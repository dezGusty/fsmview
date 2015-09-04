using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraph.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleVertex : INotifyPropertyChanged
    {
        private bool _active;
        private string _text;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                NotifyChanged("Active");
            }
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
        public SampleVertex(string text)
        {
            Text = text;
        }
        public void Change()
        {
            Active = !Active;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
