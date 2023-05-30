using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class InvalidColumnException : Exception
    {
        private const string DefaultMessage = "Invalid column id";

        public InvalidColumnException()
            : base(DefaultMessage)
        {
        }
    }
}
