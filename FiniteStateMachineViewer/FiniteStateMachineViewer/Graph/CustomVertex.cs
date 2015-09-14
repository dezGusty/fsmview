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
    public class CustomVertex : INotifyPropertyChanged
    {
        private string _text;
        private Color _color;

        public Color Color
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
                NotifyChanged("Color");
            }
        }

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

        public CustomVertex()
        {
            Text = "";
        }

        public CustomVertex(string text, Color c)
        {
            Text = text;
            Color = c;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomVertex(string id,string text,Color c)
        {
            ID = id;
            Text = text;
            Color = c;
        }

        public override string ToString()
        {
            return string.Format("{0}",Text);
        }
    }
}
