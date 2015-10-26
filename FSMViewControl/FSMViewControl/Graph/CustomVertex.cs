using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FSMViewControl
{
    /// <summary>
    /// Class that implements interface INotifyPropertyChanged
    /// </summary>
    [Serializable]
    public class CustomVertex : INotifyPropertyChanged
    {
        private string _text;
        private Color _color;
        private bool highlight;
        private bool represented;
        private double xPos;
        private double yPos;

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>

        public double X
        {
            get
            {
                return this.xPos;
            }
            set
            {
                this.xPos = value;
            }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>

        public double Y
        {
            get
            {
                return this.yPos;
            }
            set
            {
                this.yPos = value;
            }
        }

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
            this.xPos = 0;
            this.yPos = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        public CustomVertex(string text, Color color, double x, double y)
        {
            this._text = text;
            this.xPos = x;
            this.yPos = y;
            this._color = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="c">The color.</param>
        public CustomVertex(string text, Color color)
        {
            this._text = text;
            this._color = color;
            this.highlight = false;
            this.represented = false;
            this.xPos = 0;
            this.yPos = 0;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns></returns>
        public bool CompareTo(CustomVertex vertex)
        {
            if (string.Compare(this.Text.Trim(), vertex.Text.Trim()) == 0)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
