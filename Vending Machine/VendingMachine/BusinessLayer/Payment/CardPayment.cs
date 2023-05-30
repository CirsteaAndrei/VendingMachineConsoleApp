using iQuest.VendingMachine.PresentationLayer.PaymentTerminals;

namespace iQuest.VendingMachine.BusinessLayer.Payment
{
    internal class CardPayment : IPayAlgorithm
    {
        CardPaymentTerminal cardPaymentTerminal;
        public string Name => "CardPayment";
        public CardPayment(CardPaymentTerminal cardPaymentTerminal)
        {
            this.cardPaymentTerminal = cardPaymentTerminal;
        }
        public void Run(float price)
        {
            cardPaymentTerminal.AskForCardNumber(price);
        }
    }
}
