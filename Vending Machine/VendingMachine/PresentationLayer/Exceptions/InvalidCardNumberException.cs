using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class InvalidCardNumberException : Exception
    {
        private const string DefaultMessage = "Invalid card number";

        public InvalidCardNumberException()
            : base(DefaultMessage)
        {
        }
    }
}
