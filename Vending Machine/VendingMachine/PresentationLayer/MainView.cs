using System;
using System.Collections.Generic;
using iQuest.VendingMachine.BusinessLayer.UseCases;

namespace iQuest.VendingMachine.PresentationLayer
{
    internal class MainView : DisplayBase
    {
        public void DisplayApplicationHeader()
        {
            ApplicationHeaderControl applicationHeaderControl = new ApplicationHeaderControl();
            applicationHeaderControl.Display();
        }

        public IUseCase ChooseCommand(IEnumerable<IUseCase> useCases)
        {
            CommandSelectorControl commandSelectorControl = new CommandSelectorControl
            {
                UseCases = useCases
            };
            return commandSelectorControl.Display();
        }
        public void DisplayError(string message, ConsoleColor color)
        {
            DisplayLine(message, color);
        }
    }
}