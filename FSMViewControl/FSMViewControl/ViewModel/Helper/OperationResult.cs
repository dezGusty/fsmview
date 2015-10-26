using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMViewControl
{
    public class OperationResult
    {
        private bool succes;
        private string message;
        private Result resultID;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OperationResult"/> is succes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succes; otherwise, <c>false</c>.
        /// </value>
        public bool Succes
        {
            get
            {
                return this.succes;
            }
            set
            {
                this.succes = value;
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        /// <summary>
        /// Gets or sets the result identifier.
        /// Used to handle exceptions
        /// </summary>
        /// <value>
        /// The result identifier.
        /// </value>
        public Result ResultID
        {
            get
            {
                return this.resultID;
            }
            set
            {
                this.resultID = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        public OperationResult()
        {
            this.message = "";
            this.succes = false;
            this.resultID = Result.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <param name="message">The message.</param>
        public OperationResult(bool result, string message)
        {
            this.succes = result;
            this.message = message;
        }

    }
}
