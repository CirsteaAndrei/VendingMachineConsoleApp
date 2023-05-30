using System.Collections.Generic;
using iQuest.VendingMachine.BusinessLayer.Payment;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal interface IBuyView
    {
        public abstract int RequestProduct();

        public abstract void DispenseProduct(string productName);

        public abstract int AskForPaymentMethod(IEnumerable<PaymentMethod> paymentMethods);
    }
}
