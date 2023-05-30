using iQuest.VendingMachine.BusinessLayer.Payment;
using iQuest.VendingMachine.PresentationLayer;
using System.Collections.Generic;

namespace iQuest.VendingMachine.BusinessLayer.UseCases
{
    internal class PayUseCase : IPayUseCase
    {
        private readonly IBuyView buyView;
        private readonly List<IPayAlgorithm> paymentAlgorithms;
        private readonly IEnumerable<PaymentMethod> paymentMethods;

        public string Name => "pay";
        public string Description => "Pay the product you wish to buy";
        public bool CanExecute => false;
        public PayUseCase(IBuyView buyView, List<IPayAlgorithm> paymentAlgorithms, List<PaymentMethod> paymentMethods)
        {
            this.buyView = buyView;
            this.paymentAlgorithms = paymentAlgorithms;
            this.paymentMethods = paymentMethods;
        }
        public void Execute(float price)
        {
            int paymentId = buyView.AskForPaymentMethod(paymentMethods);
            paymentAlgorithms[paymentId].Run(price);
        }
    }
}
