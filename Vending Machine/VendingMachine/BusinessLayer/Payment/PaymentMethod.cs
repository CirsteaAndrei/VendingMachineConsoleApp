
namespace iQuest.VendingMachine.BusinessLayer.Payment
{
    internal class PaymentMethod
    {
        public int Id;
        public string Name;

        public PaymentMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
