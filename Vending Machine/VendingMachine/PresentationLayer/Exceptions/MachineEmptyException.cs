using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.PresentationLayer.Exceptions
{
    internal class MachineEmptyException : Exception
    {
        private const string DefaultMessage = "There are no products in the machine";

        public MachineEmptyException()
            : base(DefaultMessage)
        {
        }
    }
}
