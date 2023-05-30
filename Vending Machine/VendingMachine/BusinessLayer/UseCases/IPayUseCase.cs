namespace iQuest.VendingMachine.BusinessLayer.UseCases
{
    internal interface IPayUseCase
    {
        string Name { get; }

        string Description { get; }

        bool CanExecute { get; }

        void Execute(float price);
    }
}
