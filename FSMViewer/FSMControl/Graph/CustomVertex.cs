using System;
using System.ComponentModel;
using System.Windows.Media;

namespace FSMControl
{
    /// <summary>
    /// Class that implements interface INotifyPropertyChanged
    /// </summary>
    [Serializable]
    public class CustomVertex : INotifyPropertyChanged
    {
        private string text;
        private Color color;
        private bool highlight;
        private bool represented;
        private double positionX;
        private double positionY;

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
                return this.positionX;
            }

            set
            {
                this.positionX = value;
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
                return this.positionY;
            }

            set
            {
                this.positionY = value;
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
                this.NotifyChanged("Represented");
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
                this.NotifyChanged("Highlight");
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
                return this.color;
            }

            set
            {
                this.color = value;
                this.NotifyChanged("BackgroundColor");
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
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.NotifyChanged("Text");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        public CustomVertex()
        {
            this.text = string.Empty;
            this.highlight = false;
            this.represented = false;
            this.positionX = 0;
            this.positionY = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        public CustomVertex(string text, Color color, double x, double y)
        {
            this.text = text;
            this.positionX = x;
            this.positionY = y;
            this.color = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVertex"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="c">The color.</param>
        public CustomVertex(string text, Color color)
        {
            this.text = text;
            this.color = color;
            this.highlight = false;
            this.represented = false;
            this.positionX = 0;
            this.positionY = 0;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns></returns>
        public bool CompareTo(CustomVertex vertex)
        {
            if (vertex != null)
            {
                if (string.Compare(this.Text.Trim(), vertex.Text.Trim()) == 0)
                {
                    return true;
                }
            }

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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}