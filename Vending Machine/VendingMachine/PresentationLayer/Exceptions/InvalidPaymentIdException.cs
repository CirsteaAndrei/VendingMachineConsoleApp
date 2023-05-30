using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class InvalidPaymentIdException : Exception
    {
        private const string DefaultMessage = "Invalid payment Id";

        public InvalidPaymentIdException()
            : base(DefaultMessage)
        {
        }
    }
}
