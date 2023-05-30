using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class InsufficientStockException : Exception
    {
        private const string DefaultMessage = "The selected product is sold out";

        public InsufficientStockException()
            : base(DefaultMessage)
        {
        }
    }
}
