namespace iQuest.VendingMachine.BusinessLayer.UseCases
{
    internal interface IUseCase
    {
        string Name { get; }

        string Description { get; }

        bool CanExecute { get; }

        void Execute();
    }
}