using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiniteStateMachineViewer
{
    public class OperationResult
    {
        private bool succes;
        private string message;

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

        public OperationResult()
        {
            this.message = "";
            this.succes = false;
        }

        public OperationResult(bool result,string message)
        {
            this.succes = result;
            this.message = message;
        }

    }
}
