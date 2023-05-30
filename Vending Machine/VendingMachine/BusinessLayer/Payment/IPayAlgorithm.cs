using System;
using System.Collections.Generic;
using System.Text;

namespace iQuest.VendingMachine.BusinessLayer.Payment
{
    internal interface IPayAlgorithm
    {
        string Name { get; }
        public void Run(float price);
    }
}
