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
        private bool highlight;
        private bool represented;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomVertex"/> is represented.
        /// </summary>
        /// <value>
        ///   <c>true</c> if represented; otherwise, <c>false</c>.
        /// </value>
        public bool Represented
        {
            get
            {
                return this.represented;
            }
            set
            {
                this.represented = value;
                NotifyChanged("Represented");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomVertex"/> is highlight.
        /// </summary>
        /// <value>
        ///   <c>true</c> if highlight; otherwise, <c>false</c>.
        /// </value>
        public bool Highlight
        {
            get
            {
                return this.highlight;
            }
            set
            {
                this.highlight = value;
                NotifyChanged("Highlight");
            }
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
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

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ID
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyChanged("Text");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        public CustomVertex()
        {
            this._text = "";
            this.highlight = false;
            this.represented = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="c">The c.</param>
        public CustomVertex(string text, Color c)
        {
            this._text = text;
            this.BackgroundColor= c;
            this.highlight = false;
            this.represented = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="c">The c.</param>
        public CustomVertex(string id,string text,Color c)
        {
            this.ID = id;
            this._text = text;
            this._color = c;
            this.highlight = false;
            this.represented = false;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns></returns>
        public bool CompareTo(CustomVertex vertex)
        {
            if (this.ID==vertex.ID && this.Text==vertex.Text)
                return true;
            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
