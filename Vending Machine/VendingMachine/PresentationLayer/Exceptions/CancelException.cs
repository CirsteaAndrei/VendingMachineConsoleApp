using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class CancelException : Exception
    {
        private const string DefaultMessage = "No information has been given. The operation has been canceled";

        public CancelException()
            : base(DefaultMessage)
        {
        }
    }
}
