using GraphSharp.Controls;
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

        public bool Highlight
        {
            get;
            set;
        }

        public Color BackgroundColor
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
                NotifyChanged("BackgroundColor");
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
            this._text = "";
            this.Highlight = false;
        }

        public CustomVertex(string text, Color c)
        {
            this._text = text;
            BackgroundColor = c;
            this.Highlight = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomVertex(string id,string text,Color c)
        {
            this.ID = id;
            this._text = text;
            this._color = c;
            this.Highlight = false;
        }

        public bool CompareTo(CustomVertex vertex)
        {
            if (this.ID==vertex.ID && this.Text==vertex.Text)
                return true;
            return false;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
