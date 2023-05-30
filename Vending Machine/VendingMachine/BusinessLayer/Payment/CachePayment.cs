using iQuest.VendingMachine.PresentationLayer.PaymentTerminals;

namespace iQuest.VendingMachine.BusinessLayer.Payment
{
    internal class CachePayment : IPayAlgorithm
    {
        public string Name => "CachePayment";
        CachePaymentTerminal cachePaymentTerminal;

        public CachePayment(CachePaymentTerminal cachePaymentTerminal)
        {
            this.cachePaymentTerminal = cachePaymentTerminal;
        }

        public void Run(float price)
        {
            float totalInsertedAmount = 0;
            while (totalInsertedAmount < price)
            {
                totalInsertedAmount += cachePaymentTerminal.AskForMoney(totalInsertedAmount, price);
            }
            cachePaymentTerminal.GiveBackChange(totalInsertedAmount, price);
        }
    }
}
